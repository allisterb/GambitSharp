//   Copyright (c) Microsoft Corporation.  All rights reserved.
/*============================================================
** Class: BigRational
**
** Purpose: 
** --------
** This class is used to represent an arbitrary precision
** BigRational number
**
** A rational number (commonly called a fraction) is a ratio
** between two integers.  For example (3/6) = (2/4) = (1/2)
**
** Arithmetic
** ----------
** a/b = c/d, iff ad = bc
** a/b + c/d  == (ad + bc)/bd
** a/b - c/d  == (ad - bc)/bd
** a/b % c/d  == (ad % bc)/bd
** a/b * c/d  == (ac)/(bd)
** a/b / c/d  == (ad)/(bc)
** -(a/b)     == (-a)/b
** (a/b)^(-1) == b/a, if a != 0
**
** Reduction Algorithm
** ------------------------
** Euclid's algorithm is used to simplify the fraction.
** Calculating the greatest common divisor of two n-digit
** numbers can be found in
**
** O(n(log n)^5 (log log n)) steps as n -> +infinity
============================================================*/

namespace GambitSharp {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;

    [Serializable]
    [ComVisible(false)]
    public struct Rational : IComparable, IComparable<Rational>, IDeserializationCallback, IEquatable<Rational>, ISerializable {

        // ---- SECTION:  members supporting exposed properties -------------*
        private BigInteger m_numerator;
        private BigInteger m_denominator;

        private static readonly Rational s_brZero = new Rational(BigInteger.Zero);
        private static readonly Rational s_brOne = new Rational(BigInteger.One);
        private static readonly Rational s_brMinusOne = new Rational(BigInteger.MinusOne);

        // ---- SECTION:  members for internal support ---------*
        #region Members for Internal Support
        [StructLayout(LayoutKind.Explicit)]
        internal struct DoubleUlong {
            [FieldOffset(0)]
            public double dbl;
            [FieldOffset(0)]
            public ulong uu;
        }
        private const int DoubleMaxScale = 308;
        private static readonly BigInteger s_bnDoublePrecision = BigInteger.Pow(10, DoubleMaxScale);
        private static readonly BigInteger s_bnDoubleMaxValue = (BigInteger)Double.MaxValue;
        private static readonly BigInteger s_bnDoubleMinValue = (BigInteger)Double.MinValue;

        [StructLayout(LayoutKind.Explicit)]
        internal struct DecimalUInt32 {
            [FieldOffset(0)]
            public Decimal dec;
            [FieldOffset(0)]
            public int flags;
        }
        private const int DecimalScaleMask = 0x00FF0000;
        private const int DecimalSignMask = unchecked((int)0x80000000);
        private const int DecimalMaxScale = 28;
        private static readonly BigInteger s_bnDecimalPrecision = BigInteger.Pow(10, DecimalMaxScale);
        private static readonly BigInteger s_bnDecimalMaxValue = (BigInteger)Decimal.MaxValue;
        private static readonly BigInteger s_bnDecimalMinValue = (BigInteger)Decimal.MinValue;

        private const String c_solidus = @"/";
        #endregion Members for Internal Support

        // ---- SECTION: public properties --------------*
        #region Public Properties
        public static Rational Zero {
            get {
                return s_brZero;
            }
        }

        public static Rational One {
            get {
                return s_brOne;
            }
        }

        public static Rational MinusOne {
            get {
                return s_brMinusOne;
            }
        }

        public Int32 Sign {
            get {
                return m_numerator.Sign;
            }
        }

        public BigInteger Numerator {
            get {
                return m_numerator;
            }
        }

        public BigInteger Denominator {
            get {
                return m_denominator;
            }
        }

        #endregion Public Properties

        // ---- SECTION: public instance methods --------------*
        #region Public Instance Methods

        // GetWholePart() and GetFractionPart()
        // 
        // BigRational == Whole, Fraction
        //  0/2        ==     0,  0/2
        //  1/2        ==     0,  1/2
        // -1/2        ==     0, -1/2
        //  1/1        ==     1,  0/1
        // -1/1        ==    -1,  0/1
        // -3/2        ==    -1, -1/2
        //  3/2        ==     1,  1/2
        public BigInteger GetWholePart() {
            return BigInteger.Divide(m_numerator, m_denominator);
        }

        public Rational GetFractionPart() {
            return new Rational(BigInteger.Remainder(m_numerator, m_denominator), m_denominator);
        }

        public override bool Equals(Object? obj) {
            if (obj == null)
                return false;

            if (!(obj is Rational))
                return false;
            return this.Equals((Rational)obj);
        }

        public override int GetHashCode() {
            return (m_numerator / Denominator).GetHashCode();
        }

        // IComparable
        int IComparable.CompareTo(Object? obj) {
            if (obj == null)
                return 1;
            if (!(obj is Rational))
                throw new ArgumentException("Argument must be of type BigRational", "obj");
            return Compare(this, (Rational)obj);
        }

        // IComparable<BigRational>
        public int CompareTo(Rational other) {
            return Compare(this, other);
        }

        // Object.ToString
        public override String ToString() {
            if (m_denominator == 1)
            {
                return m_numerator.ToString("R", CultureInfo.InvariantCulture);
            }
            StringBuilder ret = new StringBuilder();
            ret.Append(m_numerator.ToString("R", CultureInfo.InvariantCulture));
            ret.Append(c_solidus);
            ret.Append(Denominator.ToString("R", CultureInfo.InvariantCulture));
            return ret.ToString();
        }

        // IEquatable<BigRational>
        // a/b = c/d, iff ad = bc
        public Boolean Equals(Rational other) {
            if (this.Denominator == other.Denominator) {
                return m_numerator == other.m_numerator;
            }
            else {
                return (m_numerator * other.Denominator) == (Denominator * other.m_numerator);
            }
        }

        #endregion Public Instance Methods

        // -------- SECTION: constructors -----------------*
        #region Constructors

        public Rational(BigInteger numerator) {
            m_numerator = numerator;
            m_denominator = BigInteger.One;
        }

        // BigRational(Double)
        public Rational(Double value) {
            if (Double.IsNaN(value)) {
                throw new ArgumentException("Argument is not a number", "value");
            }
            else if (Double.IsInfinity(value)) {
                throw new ArgumentException("Argument is infinity", "value");
            }

            bool isFinite;
            int sign;
            int exponent;
            ulong significand;
            SplitDoubleIntoParts(value, out sign, out exponent, out significand, out isFinite);

            if (significand == 0) {
                this = Rational.Zero;
                return;
            }

            m_numerator = significand;
            m_denominator = 1 << 52;

            if (exponent > 0) {
                m_numerator = BigInteger.Pow(m_numerator, exponent);
            }
            else if (exponent < 0) {
                m_denominator = BigInteger.Pow(m_denominator, -exponent);
            }
            if (sign < 0) {
                m_numerator = BigInteger.Negate(m_numerator);
            }
            Simplify();
        }

        // BigRational(Decimal) -
        //
        // The Decimal type represents floating point numbers exactly, with no rounding error.
        // Values such as "0.1" in Decimal are actually representable, and convert cleanly
        // to BigRational as "11/10"
        public Rational(Decimal value) {
            int[] bits = Decimal.GetBits(value);
            if (bits == null || bits.Length != 4 || (bits[3] & ~(DecimalSignMask | DecimalScaleMask)) != 0 || (bits[3] & DecimalScaleMask) > (28 << 16)) {
                throw new ArgumentException("invalid Decimal", "value");
            }

            if (value == Decimal.Zero) {
                this = Rational.Zero;
                return;
            }

            // build up the numerator
            ulong ul = (((ulong)(uint)bits[2]) << 32) | ((ulong)(uint)bits[1]);   // (hi    << 32) | (mid)
            m_numerator = (new BigInteger(ul) << 32) | (uint)bits[0];             // (hiMid << 32) | (low)

            bool isNegative = (bits[3] & DecimalSignMask) != 0;
            if (isNegative) {
                m_numerator = BigInteger.Negate(m_numerator);
            }

            // build up the denominator
            int scale = (bits[3] & DecimalScaleMask) >> 16;     // 0-28, power of 10 to divide numerator by
            m_denominator = BigInteger.Pow(10, scale);

            Simplify();
        }

        public Rational(BigInteger numerator, BigInteger denominator) {
            if (denominator.Sign == 0) {
                throw new DivideByZeroException();
            }
            else if (numerator.Sign == 0) {
                // 0/m -> 0/1
                m_numerator = BigInteger.Zero;
                m_denominator = BigInteger.One;
            }
            else if (denominator.Sign < 0) {
                m_numerator = BigInteger.Negate(numerator);
                m_denominator = BigInteger.Negate(denominator);
            }
            else {
                m_numerator = numerator;
                m_denominator = denominator;
            }
            Simplify();
        }

        public Rational(BigInteger whole, BigInteger numerator, BigInteger denominator) {
            if (denominator.Sign == 0) {
                throw new DivideByZeroException();
            }
            else if (numerator.Sign == 0 && whole.Sign == 0) {
                m_numerator = BigInteger.Zero;
                m_denominator = BigInteger.One;
            }
            else if (denominator.Sign < 0) {
                m_denominator = BigInteger.Negate(denominator);
                m_numerator = (BigInteger.Negate(whole) * m_denominator) + BigInteger.Negate(numerator);
            }
            else {
                m_denominator = denominator;
                m_numerator = (whole * denominator) + numerator;
            }
            Simplify();
        }
        #endregion Constructors

        // -------- SECTION: public static methods -----------------*
        #region Public Static Methods

        public static Rational Abs(Rational r) {
            return (r.m_numerator.Sign < 0 ? new Rational(BigInteger.Abs(r.m_numerator), r.Denominator) : r);
        }

        public static Rational Negate(Rational r) {
            return new Rational(BigInteger.Negate(r.m_numerator), r.Denominator);
        }

        public static Rational Invert(Rational r) {
            return new Rational(r.Denominator, r.m_numerator);
        }

        public static Rational Add(Rational x, Rational y) {
            return x + y;
        }

        public static Rational Subtract(Rational x, Rational y) {
            return x - y;
        }


        public static Rational Multiply(Rational x, Rational y) {
            return x * y;
        }

        public static Rational Divide(Rational dividend, Rational divisor) {
            return dividend / divisor;
        }

        public static Rational Remainder(Rational dividend, Rational divisor) {
            return dividend % divisor;
        }

        public static Rational DivRem(Rational dividend, Rational divisor, out Rational remainder) {
            // a/b / c/d  == (ad)/(bc)
            // a/b % c/d  == (ad % bc)/bd

            // (ad) and (bc) need to be calculated for both the division and the remainder operations.
            BigInteger ad = dividend.m_numerator * divisor.Denominator;
            BigInteger bc = dividend.Denominator * divisor.m_numerator;
            BigInteger bd = dividend.Denominator * divisor.Denominator;

            remainder = new Rational(ad % bc, bd);
            return new Rational(ad, bc);
        }


        public static Rational Pow(Rational baseValue, BigInteger exponent) {
            if (exponent.Sign == 0) {
                // 0^0 -> 1
                // n^0 -> 1
                return Rational.One;
            }
            else if (exponent.Sign < 0) {
                if (baseValue == Rational.Zero) {
                    throw new ArgumentException("cannot raise zero to a negative power", "baseValue");
                }
                // n^(-e) -> (1/n)^e
                baseValue = Rational.Invert(baseValue);
                exponent = BigInteger.Negate(exponent);
            }

            Rational result = baseValue;
            while (exponent > BigInteger.One) {
                result = result * baseValue;
                exponent--;
            }

            return result;
        }

        public static Rational Convert(object obj) => obj switch
        {
            Rational r => r,
            int i => new Rational((BigInteger)i),
            BigInteger bi => new Rational(bi),
            float f when f == Math.Floor(f + 0.00001) => new Rational((BigInteger) System.Convert.ToInt32(f)),
            float f => new Rational(f),
            double d when d == Math.Floor(d + 0.00001) => new Rational((BigInteger)System.Convert.ToInt32(d)),
            double d => new Rational(d),
            decimal d => new Rational(d),
            
            _ => throw new ArgumentException($"Cannot convert {obj} to a Rational value.")
        };

        public static ITuple ToTuple(ITuple r) => r.Length switch
        {
            2 => ValueTuple.Create((Rational)r[0]!, (Rational)r[1]!),
            3 => ValueTuple.Create((Rational)r[0]!, (Rational)r[1]!, (Rational)r[2]!),
            4 => ValueTuple.Create((Rational)r[0]!, (Rational)r[1]!, (Rational)r[2]!, (Rational)r[3]!),
            5 => ValueTuple.Create((Rational)r[0]!, (Rational)r[1]!, (Rational)r[2]!, (Rational)r[3]!, (Rational)r[4]!),
            6 => Tuple.Create((Rational)r[0]!, (Rational)r[1]!, (Rational)r[2]!, (Rational)r[3]!, (Rational)r[4]!, (Rational)r[5]!),
            7 => Tuple.Create((Rational)r[0]!, (Rational)r[1]!, (Rational)r[2]!, (Rational)r[3]!, (Rational)r[4]!, (Rational)r[5]!, (Rational)r[6]!),
            8 => Tuple.Create((Rational)r[0]!, (Rational)r[1]!, (Rational)r[2]!, (Rational)r[3]!, (Rational)r[4]!, (Rational)r[5]!, (Rational)r[6]!, (Rational)r[7]!),
            var l => throw new ArgumentOutOfRangeException($"Unsupported tuple length: {l}.")
        };
        // Least Common Denominator (LCD)
        //
        // The LCD is the least common multiple of the two denominators.  For instance, the LCD of
        // {1/2, 1/4} is 4 because the least common multiple of 2 and 4 is 4.  Likewise, the LCD
        // of {1/2, 1/3} is 6.
        //       
        // To find the LCD:
        //
        // 1) Find the Greatest Common Divisor (GCD) of the denominators
        // 2) Multiply the denominators together
        // 3) Divide the product of the denominators by the GCD
        public static BigInteger LeastCommonDenominator(Rational x, Rational y) {
            // LCD( a/b, c/d ) == (bd) / gcd(b,d)
            return (x.Denominator * y.Denominator) / BigInteger.GreatestCommonDivisor(x.Denominator, y.Denominator);
        }

        public static int Compare(Rational r1, Rational r2) {
            //     a/b = c/d, iff ad = bc
            return BigInteger.Compare(r1.m_numerator * r2.Denominator, r2.m_numerator * r1.Denominator);
        }
        #endregion Public Static Methods

        #region Operator Overloads
        public static bool operator ==(Rational x, Rational y) {
            return Compare(x, y) == 0;
        }

        public static bool operator !=(Rational x, Rational y) {
            return Compare(x, y) != 0;
        }

        public static bool operator <(Rational x, Rational y) {
            return Compare(x, y) < 0;
        }

        public static bool operator <=(Rational x, Rational y) {
            return Compare(x, y) <= 0;
        }

        public static bool operator >(Rational x, Rational y) {
            return Compare(x, y) > 0;
        }

        public static bool operator >=(Rational x, Rational y) {
            return Compare(x, y) >= 0;
        }

        public static Rational operator +(Rational r) {
            return r;
        }

        public static Rational operator -(Rational r) {
            return new Rational(-r.m_numerator, r.Denominator);
        }

        public static Rational operator ++(Rational r) {
            return r + Rational.One;
        }

        public static Rational operator --(Rational r) {
            return r - Rational.One;
        }

        public static Rational operator +(Rational r1, Rational r2) {
            // a/b + c/d  == (ad + bc)/bd
            return new Rational((r1.m_numerator * r2.Denominator) + (r1.Denominator * r2.m_numerator), (r1.Denominator * r2.Denominator));
        }

        public static Rational operator -(Rational r1, Rational r2) {
            // a/b - c/d  == (ad - bc)/bd
            return new Rational((r1.m_numerator * r2.Denominator) - (r1.Denominator * r2.m_numerator), (r1.Denominator * r2.Denominator));
        }

        public static Rational operator *(Rational r1, Rational r2) {
            // a/b * c/d  == (ac)/(bd)
            return new Rational((r1.m_numerator * r2.m_numerator), (r1.Denominator * r2.Denominator));
        }

        public static Rational operator /(Rational r1, Rational r2) {
            // a/b / c/d  == (ad)/(bc)
            return new Rational((r1.m_numerator * r2.Denominator), (r1.Denominator * r2.m_numerator));
        }

        public static Rational operator %(Rational r1, Rational r2) {
            // a/b % c/d  == (ad % bc)/bd
            return new Rational((r1.m_numerator * r2.Denominator) % (r1.Denominator * r2.m_numerator), (r1.Denominator * r2.Denominator));
        }
        #endregion Operator Overloads

        // ----- SECTION: explicit conversions from BigRational to numeric base types  ----------------*
        #region explicit conversions from BigRational
        //[CLSCompliant(false)]
        public static explicit operator SByte(Rational value) {
            return (SByte)(BigInteger.Divide(value.m_numerator, value.m_denominator));
        }

        //[CLSCompliant(false)]
        public static explicit operator UInt16(Rational value) {
            return (UInt16)(BigInteger.Divide(value.m_numerator, value.m_denominator));
        }

        //[CLSCompliant(false)]
        public static explicit operator UInt32(Rational value) {
            return (UInt32)(BigInteger.Divide(value.m_numerator, value.m_denominator));
        }

        //[CLSCompliant(false)]
        public static explicit operator UInt64(Rational value) {
            return (UInt64)(BigInteger.Divide(value.m_numerator, value.m_denominator));
        }

        public static explicit operator Byte(Rational value) {
            return (Byte)(BigInteger.Divide(value.m_numerator, value.m_denominator));
        }

        public static explicit operator Int16(Rational value) {
            return (Int16)(BigInteger.Divide(value.m_numerator, value.m_denominator));
        }

        public static implicit operator Int32(Rational value) {
            return (Int32)(BigInteger.Divide(value.m_numerator, value.m_denominator));
        }

        public static explicit operator Int64(Rational value) {
            return (Int64)(BigInteger.Divide(value.m_numerator, value.m_denominator));
        }

        public static explicit operator BigInteger(Rational value) {
            return BigInteger.Divide(value.m_numerator, value.m_denominator);
        }

        public static explicit operator Single(Rational value) {
            // The Single value type represents a single-precision 32-bit number with
            // values ranging from negative 3.402823e38 to positive 3.402823e38      
            // values that do not fit into this range are returned as Infinity
            return (Single)((Double)value);
        }

        public static explicit operator Double(Rational value) {
            // The Double value type represents a double-precision 64-bit number with
            // values ranging from -1.79769313486232e308 to +1.79769313486232e308
            // values that do not fit into this range are returned as +/-Infinity
            if (SafeCastToDouble(value.m_numerator) && SafeCastToDouble(value.m_denominator)) {
                return (Double)value.m_numerator / (Double)value.m_denominator;
            }

            // scale the numerator to preseve the fraction part through the integer division
            BigInteger denormalized = (value.m_numerator * s_bnDoublePrecision) / value.m_denominator;
            if (denormalized.IsZero)
                return (value.Sign < 0) ? BitConverter.Int64BitsToDouble(unchecked((long)0x8000000000000000)) : 0d; // underflow to -+0

            Double result = 0;
            bool isDouble = false;
            int scale = DoubleMaxScale;

            while (scale > 0) {
                if (!isDouble) {
                    if (SafeCastToDouble(denormalized)) {
                        result = (Double)denormalized;
                        isDouble = true;
                    }
                    else {
                        denormalized = denormalized / 10;
                    }
                }
                result = result / 10;
                scale--;
            }

            if (!isDouble)
                return (value.Sign < 0) ? Double.NegativeInfinity : Double.PositiveInfinity;
            else
                return result;
        }

        public static explicit operator Decimal(Rational value) {
            // The Decimal value type represents decimal numbers ranging
            // from +79,228,162,514,264,337,593,543,950,335 to -79,228,162,514,264,337,593,543,950,335
            // the binary representation of a Decimal value is of the form, ((-2^96 to 2^96) / 10^(0 to 28))
            if (SafeCastToDecimal(value.m_numerator) && SafeCastToDecimal(value.m_denominator)) {
                return (Decimal)value.m_numerator / (Decimal)value.m_denominator;
            }

            // scale the numerator to preseve the fraction part through the integer division
            BigInteger denormalized = (value.m_numerator * s_bnDecimalPrecision) / value.m_denominator;
            if (denormalized.IsZero) {
                return Decimal.Zero; // underflow - fraction is too small to fit in a decimal
            }
            for (int scale = DecimalMaxScale; scale >= 0; scale--) {
                if (!SafeCastToDecimal(denormalized)) {
                    denormalized = denormalized / 10;
                }
                else {
                    DecimalUInt32 dec = new DecimalUInt32();
                    dec.dec = (Decimal)denormalized;
                    dec.flags = (dec.flags & ~DecimalScaleMask) | (scale << 16);
                    return dec.dec;
                }
            }
            throw new OverflowException("Value was either too large or too small for a Decimal.");
        }
        #endregion explicit conversions from BigRational

        // ----- SECTION: implicit conversions from numeric base types to BigRational  ----------------*
        #region implicit conversions to BigRational

        //[CLSCompliant(false)]
        public static implicit operator Rational(SByte value) {
            return new Rational((BigInteger)value);
        }

        //[CLSCompliant(false)]
        public static implicit operator Rational(UInt16 value) {
            return new Rational((BigInteger)value);
        }

        //[CLSCompliant(false)]
        public static implicit operator Rational(UInt32 value) {
            return new Rational((BigInteger)value);
        }

        //[CLSCompliant(false)]
        public static implicit operator Rational(UInt64 value) {
            return new Rational((BigInteger)value);
        }

        public static implicit operator Rational(Byte value) {
            return new Rational((BigInteger)value);
        }

        public static implicit operator Rational(Int16 value) {
            return new Rational((BigInteger)value);
        }

        public static implicit operator Rational(Int32 value) {
            return new Rational((BigInteger)value);
        }

        public static implicit operator Rational(Int64 value) {
            return new Rational((BigInteger)value);
        }

        public static implicit operator Rational(BigInteger value) {
            return new Rational(value);
        }

        public static implicit operator Rational(Single value) {
            return new Rational((Double)value);
        }

        public static implicit operator Rational(Double value) {
            return new Rational(value);
        }

        public static implicit operator Rational(Decimal value) {
            return new Rational(value);
        }

      
        #endregion implicit conversions to BigRational

        

        // ----- SECTION: private serialization instance methods  ----------------*
        #region serialization
        void IDeserializationCallback.OnDeserialization(Object? sender) {
            try {
                // verify that the deserialized number is well formed
                if (m_denominator.Sign == 0 || m_numerator.Sign == 0) {
                    // n/0 -> 0/1
                    // 0/m -> 0/1
                    m_numerator   = BigInteger.Zero;
                    m_denominator = BigInteger.One;
                }
                else if (m_denominator.Sign < 0) {
                    m_numerator = BigInteger.Negate(m_numerator);
                    m_denominator = BigInteger.Negate(m_denominator);
                }
                Simplify();
            }
            catch (ArgumentException e) {
                throw new SerializationException("invalid serialization data", e);
            }
        }

        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
            if (info == null) {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Numerator", m_numerator);
            info.AddValue("Denominator", m_denominator);
        }
        
        Rational(SerializationInfo info, StreamingContext context) {
            if (info == null) {
                throw new ArgumentNullException("info");
            }

            m_numerator   = (BigInteger)(info.GetValue("Numerator", typeof(BigInteger)) ?? throw new NullReferenceException());
            m_denominator = (BigInteger)(info.GetValue("Denominator", typeof(BigInteger)) ?? throw new NullReferenceException());
        }
        #endregion serialization

        // ----- SECTION: private instance utility methods ----------------*
        #region instance helper methods
        private void Simplify() { 
            // * if the numerator is {0, +1, -1} then the fraction is already reduced
            // * if the denominator is {+1} then the fraction is already reduced
            if (m_numerator == BigInteger.Zero) {
                m_denominator = BigInteger.One;
            }

            BigInteger gcd = BigInteger.GreatestCommonDivisor(m_numerator, m_denominator); 
            if(gcd > BigInteger.One) { 
                m_numerator = m_numerator / gcd; 
                m_denominator = Denominator / gcd; 
            } 
        }
        #endregion instance helper methods

        // ----- SECTION: private static utility methods -----------------*
        #region static helper methods
        private static bool SafeCastToDouble(BigInteger value) {
            return s_bnDoubleMinValue <= value && value <= s_bnDoubleMaxValue;
        }

        private static bool SafeCastToDecimal(BigInteger value) {
            return s_bnDecimalMinValue <= value && value <= s_bnDecimalMaxValue;
        }

        private static void SplitDoubleIntoParts(double dbl, out int sign, out int exp, out ulong man, out bool isFinite) {
            DoubleUlong du;
            du.uu = 0;
            du.dbl = dbl;

            sign = 1 - ((int)(du.uu >> 62) & 2);
            man = du.uu & 0x000FFFFFFFFFFFFF;
            exp = (int)(du.uu >> 52) & 0x7FF;
            if (exp == 0) {
                // Denormalized number.
                isFinite = true;
                if (man != 0)
                    exp = -1074;
            }
            else if (exp == 0x7FF) {
                // NaN or Infinite.
                isFinite = false;
                exp = Int32.MaxValue;
            }
            else {
                isFinite = true;
                man |= 0x0010000000000000; // mask in the implied leading 53rd significand bit
                exp -= 1075;
           }
      }

      private static double GetDoubleFromParts(int sign, int exp, ulong man) {
          DoubleUlong du;
          du.dbl = 0;

          if (man == 0) {
              du.uu = 0;
          }
          else {
              // Normalize so that 0x0010 0000 0000 0000 is the highest bit set
              int cbitShift = CbitHighZero(man) - 11;
              if (cbitShift < 0)
                  man >>= -cbitShift;
              else
                  man <<= cbitShift;

              // Move the point to just behind the leading 1: 0x001.0 0000 0000 0000
              // (52 bits) and skew the exponent (by 0x3FF == 1023)
              exp += 1075;

              if (exp >= 0x7FF) {
                  // Infinity
                  du.uu = 0x7FF0000000000000;
              }
              else if (exp <= 0) {
                  // Denormalized
                  exp--;
                  if (exp < -52) {
                      // Underflow to zero
                      du.uu = 0;
                  }
                  else {
                      du.uu = man >> -exp;
                  }
              }
              else {
                  // Mask off the implicit high bit
                  du.uu = (man & 0x000FFFFFFFFFFFFF) | ((ulong) exp << 52);
              }        
          }

          if (sign < 0) {
              du.uu |= 0x8000000000000000;
          }

          return du.dbl;
      }

      private static int CbitHighZero(ulong uu) {
          if ((uu & 0xFFFFFFFF00000000) == 0)
              return 32 + CbitHighZero((uint) uu);
          return CbitHighZero((uint) (uu >> 32));
      }

      private static int CbitHighZero(uint u) {
          if (u == 0)
              return 32;

          int cbit = 0;
          if ((u & 0xFFFF0000) == 0) {
              cbit += 16;
              u <<= 16;
          }
          if ((u & 0xFF000000) == 0) {
              cbit += 8;
              u <<= 8;
          }
          if ((u & 0xF0000000) == 0) {
              cbit += 4;
              u <<= 4;
          }
          if ((u & 0xC0000000) == 0) {
              cbit += 2;
              u <<= 2;
          }
          if ((u & 0x80000000) == 0)
              cbit += 1;
          return cbit;
      }

      #endregion static helper methods
    } // BigRational
} // namespace Numerics
