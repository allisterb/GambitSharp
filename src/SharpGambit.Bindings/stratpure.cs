// ----------------------------------------------------------------------------
// <auto-generated>
// This is autogenerated code by CppSharp.
// Do not edit this file or all your changes will be lost after re-generation.
// </auto-generated>
// ----------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.Security;
using __CallingConvention = global::System.Runtime.InteropServices.CallingConvention;
using __IntPtr = global::System.IntPtr;

#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required

namespace gambit
{
    namespace Gambit
    {
        /// <summary>
        /// <para>This class represents a strategy profile on a strategic game.</para>
        /// <para>It specifies exactly one strategy for each player defined on the</para>
        /// <para>game.</para>
        /// </summary>
        public unsafe abstract partial class PureStrategyProfileRep : IDisposable
        {
            [StructLayout(LayoutKind.Sequential, Size = 40)]
            public partial struct __Internal
            {
                internal __IntPtr vfptr_PureStrategyProfileRep;
                internal global::gambit.Gambit.GameObjectPtr.__Internal m_nfg;
                internal global::gambit.Gambit.Array.__Internal m_profile;
            }

            public __IntPtr __Instance { get; protected set; }

            internal static readonly new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::gambit.Gambit.PureStrategyProfileRep> NativeToManagedMap =
                new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::gambit.Gambit.PureStrategyProfileRep>();

            internal static void __RecordNativeToManagedMapping(IntPtr native, global::gambit.Gambit.PureStrategyProfileRep managed)
            {
                NativeToManagedMap[native] = managed;
            }

            internal static bool __TryGetNativeToManagedMapping(IntPtr native, out global::gambit.Gambit.PureStrategyProfileRep managed)
            {
    
                return NativeToManagedMap.TryGetValue(native, out managed);
            }

            protected bool __ownsNativeInstance;

            internal static PureStrategyProfileRep __CreateInstance(__IntPtr native, bool skipVTables = false)
            {
                if (native == __IntPtr.Zero)
                    return null;
                return new PureStrategyProfileRepInternal(native.ToPointer(), skipVTables);
            }

            internal static PureStrategyProfileRep __GetOrCreateInstance(__IntPtr native, bool saveInstance = false, bool skipVTables = false)
            {
                if (native == __IntPtr.Zero)
                    return null;
                if (__TryGetNativeToManagedMapping(native, out var managed))
                    return (PureStrategyProfileRep)managed;
                var result = __CreateInstance(native, skipVTables);
                if (saveInstance)
                    __RecordNativeToManagedMapping(native, result);
                return result;
            }

            internal static PureStrategyProfileRep __GetInstance(__IntPtr native)
            {
                if (!__TryGetNativeToManagedMapping(native, out var managed))
                    throw new global::System.Exception("No managed instance was found");
                var result = (PureStrategyProfileRep)managed;
                if (result.__ownsNativeInstance)
                    result.SetupVTables();
                return result;
            }

            internal static PureStrategyProfileRep __CreateInstance(__Internal native, bool skipVTables = false)
            {
                return new PureStrategyProfileRepInternal(native, skipVTables);
            }

            protected PureStrategyProfileRep(void* native, bool skipVTables = false)
            {
                if (native == null)
                    return;
                __Instance = new __IntPtr(native);
                if (!skipVTables)
                    SetupVTables(true);
            }

            public void Dispose()
            {
                Dispose(disposing: true, callNativeDtor : __ownsNativeInstance );
            }

            partial void DisposePartial(bool disposing);

            internal protected virtual void Dispose(bool disposing, bool callNativeDtor )
            {
                if (__Instance == IntPtr.Zero)
                    return;
                NativeToManagedMap.TryRemove(__Instance, out _);
                *(IntPtr*)(__Instance + 0) = __VTables.Tables[0];
                DisposePartial(disposing);
                if (__ownsNativeInstance)
                    Marshal.FreeHGlobal(__Instance);
                __Instance = IntPtr.Zero;
            }

            /// <summary>Set the strategy for a player</summary>
            public abstract void SetStrategy(global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameStrategyRep> _0);

            /// <summary>Get the payoff to player pl that results from the profile</summary>
            public abstract global::gambit.Gambit.Rational GetPayoff(int pl);

            /// <summary>Get the value of playing strategy against the profile</summary>
            public abstract global::gambit.Gambit.Rational GetStrategyValue(global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameStrategyRep> _0);

            protected global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameRep> MNfg
            {
                get
                {
                    return global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameRep>.__CreateInstance(new __IntPtr(&((__Internal*)__Instance)->m_nfg));
                }

                set
                {
                    if (ReferenceEquals(value, null))
                        throw new global::System.ArgumentNullException("value", "Cannot be null because it is passed by value.");
                    ((__Internal*)__Instance)->m_nfg = *(global::gambit.Gambit.GameObjectPtr.__Internal*) value.__Instance;
                }
            }

            protected global::gambit.Gambit.Array<global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameStrategyRep>> MProfile
            {
                get
                {
                    return global::gambit.Gambit.Array<global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameStrategyRep>>.__CreateInstance(new __IntPtr(&((__Internal*)__Instance)->m_profile));
                }

                set
                {
                    if (ReferenceEquals(value, null))
                        throw new global::System.ArgumentNullException("value", "Cannot be null because it is passed by value.");
                    ((__Internal*)__Instance)->m_profile = *(global::gambit.Gambit.Array.__Internal*) value.__Instance;
                }
            }

            /// <summary>
            /// <para>Create a copy of the strategy profile.</para>
            /// <para>Caller is responsible for memory management of the created object.</para>
            /// </summary>
            protected abstract global::gambit.Gambit.PureStrategyProfileRep Copy
            {
                get;
            }

            /// <summary>Get the index uniquely identifying the strategy profile</summary>
            public virtual int Index
            {
                get
                {
                    var ___GetIndexDelegate = __VTables.GetMethodDelegate<global::gambit.Delegates.Func_int___IntPtr>(0, 2);
                    var ___ret = ___GetIndexDelegate(__Instance);
                    return ___ret;
                }
            }

            /// <summary>Get the outcome that results from the profile</summary>
            /// <remarks>Set the outcome that results from the profile</remarks>
            public abstract global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameOutcomeRep> Outcome
            {
                get;

                set;
            }

            #region Virtual table interop

            // PureStrategyProfileRep *Copy() const = 0
            private static global::gambit.Delegates.Func___IntPtr___IntPtr _CopyDelegateInstance;

            private static __IntPtr _CopyDelegateHook(__IntPtr __instance)
            {
                var __target = global::gambit.Gambit.PureStrategyProfileRep.__GetInstance(__instance);
                var ___ret = __target.Copy;
                return ___ret is null ? __IntPtr.Zero : ___ret.__Instance;
            }

            // virtual ~PureStrategyProfileRep() = default
            private static global::gambit.Delegates.Action___IntPtr_int _dtorDelegateInstance;

            private static void _dtorDelegateHook(__IntPtr __instance, int delete)
            {
                var __target = global::gambit.Gambit.PureStrategyProfileRep.__GetInstance(__instance);
                __target.Dispose(disposing: true, callNativeDtor: true);
            }

            // long GetIndex() const
            private static global::gambit.Delegates.Func_int___IntPtr _GetIndexDelegateInstance;

            private static int _GetIndexDelegateHook(__IntPtr __instance)
            {
                var __target = global::gambit.Gambit.PureStrategyProfileRep.__GetInstance(__instance);
                var ___ret = __target.Index;
                return ___ret;
            }

            // void SetStrategy(const GameStrategy &) = 0
            private static global::gambit.Delegates.Action___IntPtr___IntPtr _SetStrategyDelegateInstance;

            private static void _SetStrategyDelegateHook(__IntPtr __instance, __IntPtr _0)
            {
                var __target = global::gambit.Gambit.PureStrategyProfileRep.__GetInstance(__instance);
                var __result0 = global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameStrategyRep>.__GetOrCreateInstance(_0, false);
                __target.SetStrategy(__result0);
            }

            // GameOutcome GetOutcome() const = 0
            private static global::gambit.Delegates.Action___IntPtr___IntPtr _GetOutcomeDelegateInstance;

            private static void _GetOutcomeDelegateHook(__IntPtr __instance, __IntPtr @return)
            {
                var __target = global::gambit.Gambit.PureStrategyProfileRep.__GetInstance(__instance);
                var ___ret = __target.Outcome;
                if (ReferenceEquals(___ret, null))
                    throw new global::System.ArgumentNullException("___ret", "Cannot be null because it is passed by value.");
                *(global::gambit.Gambit.GameObjectPtr.__Internal*) @return = *(global::gambit.Gambit.GameObjectPtr.__Internal*) ___ret.__Instance;
            }

            // void SetOutcome(GameOutcome p_outcome) = 0
            private static global::gambit.Delegates.Action___IntPtr___IntPtr _SetOutcomeDelegateInstance;

            private static void _SetOutcomeDelegateHook(__IntPtr __instance, __IntPtr p_outcome)
            {
                var __target = global::gambit.Gambit.PureStrategyProfileRep.__GetInstance(__instance);
                var __result0 = global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameOutcomeRep>.__GetOrCreateInstance(p_outcome, false);
                __target.Outcome = __result0;
            }

            // Rational GetPayoff(int pl) const = 0
            private static global::gambit.Delegates.Action___IntPtr___IntPtr_int _GetPayoffDelegateInstance;

            private static void _GetPayoffDelegateHook(__IntPtr __instance, __IntPtr @return, int pl)
            {
                var __target = global::gambit.Gambit.PureStrategyProfileRep.__GetInstance(__instance);
                var ___ret = __target.GetPayoff(pl);
                if (ReferenceEquals(___ret, null))
                    throw new global::System.ArgumentNullException("___ret", "Cannot be null because it is passed by value.");
                *(global::gambit.Gambit.Rational.__Internal*) @return = *(global::gambit.Gambit.Rational.__Internal*) ___ret.__Instance;
            }

            // Rational GetStrategyValue(const GameStrategy &) const = 0
            private static global::gambit.Delegates.Action___IntPtr___IntPtr___IntPtr _GetStrategyValueDelegateInstance;

            private static void _GetStrategyValueDelegateHook(__IntPtr __instance, __IntPtr @return, __IntPtr _0)
            {
                var __target = global::gambit.Gambit.PureStrategyProfileRep.__GetInstance(__instance);
                var __result1 = global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameStrategyRep>.__GetOrCreateInstance(_0, false);
                var ___ret = __target.GetStrategyValue(__result1);
                if (ReferenceEquals(___ret, null))
                    throw new global::System.ArgumentNullException("___ret", "Cannot be null because it is passed by value.");
                *(global::gambit.Gambit.Rational.__Internal*) @return = *(global::gambit.Gambit.Rational.__Internal*) ___ret.__Instance;
            }

            internal static class VTableLoader
            {
                private static volatile bool initialized;
                private static readonly IntPtr*[] ManagedVTables = new IntPtr*[1];
                private static readonly IntPtr*[] ManagedVTablesDtorOnly = new IntPtr*[1];
                private static readonly IntPtr[] Thunks = new IntPtr[8];
                private static CppSharp.Runtime.VTables VTables;
                private static readonly global::System.Collections.Generic.List<CppSharp.Runtime.SafeUnmanagedMemoryHandle>
                    SafeHandles = new global::System.Collections.Generic.List<CppSharp.Runtime.SafeUnmanagedMemoryHandle>();
                
                static VTableLoader()
                {
                    _CopyDelegateInstance += _CopyDelegateHook;
                    _dtorDelegateInstance += _dtorDelegateHook;
                    _GetIndexDelegateInstance += _GetIndexDelegateHook;
                    _SetStrategyDelegateInstance += _SetStrategyDelegateHook;
                    _GetOutcomeDelegateInstance += _GetOutcomeDelegateHook;
                    _SetOutcomeDelegateInstance += _SetOutcomeDelegateHook;
                    _GetPayoffDelegateInstance += _GetPayoffDelegateHook;
                    _GetStrategyValueDelegateInstance += _GetStrategyValueDelegateHook;
                    Thunks[0] = Marshal.GetFunctionPointerForDelegate(_CopyDelegateInstance);
                    Thunks[1] = Marshal.GetFunctionPointerForDelegate(_dtorDelegateInstance);
                    Thunks[2] = Marshal.GetFunctionPointerForDelegate(_GetIndexDelegateInstance);
                    Thunks[3] = Marshal.GetFunctionPointerForDelegate(_SetStrategyDelegateInstance);
                    Thunks[4] = Marshal.GetFunctionPointerForDelegate(_GetOutcomeDelegateInstance);
                    Thunks[5] = Marshal.GetFunctionPointerForDelegate(_SetOutcomeDelegateInstance);
                    Thunks[6] = Marshal.GetFunctionPointerForDelegate(_GetPayoffDelegateInstance);
                    Thunks[7] = Marshal.GetFunctionPointerForDelegate(_GetStrategyValueDelegateInstance);
                }

                public static CppSharp.Runtime.VTables SetupVTables(IntPtr instance, bool destructorOnly = false)
                {
                    if (!initialized)
                    {
                        lock (ManagedVTables)
                        {
                            if (!initialized)
                            {
                                initialized = true;
                                VTables.Tables = new IntPtr[] { *(IntPtr*)(instance + 0) };
                                VTables.Methods = new Delegate[1][];
                                ManagedVTablesDtorOnly[0] = CppSharp.Runtime.VTables.CloneTable(SafeHandles, instance, 0, 8, 0);
                                ManagedVTablesDtorOnly[0][1] = Thunks[1];
                                ManagedVTables[0] = CppSharp.Runtime.VTables.CloneTable(SafeHandles, instance, 0, 8, 0);
                                ManagedVTables[0][0] = Thunks[0];
                                ManagedVTables[0][1] = Thunks[1];
                                ManagedVTables[0][2] = Thunks[2];
                                ManagedVTables[0][3] = Thunks[3];
                                ManagedVTables[0][4] = Thunks[4];
                                ManagedVTables[0][5] = Thunks[5];
                                ManagedVTables[0][6] = Thunks[6];
                                ManagedVTables[0][7] = Thunks[7];
                                VTables.Methods[0] = new Delegate[8];
                            }
                        }
                    }

                    if (destructorOnly)
                    {
                        *(IntPtr**)(instance + 0) = ManagedVTablesDtorOnly[0];
                    }
                    else
                    {
                        *(IntPtr**)(instance + 0) = ManagedVTables[0];
                    }
                    return VTables;
                }
            }

            protected CppSharp.Runtime.VTables __vtables;
            internal virtual CppSharp.Runtime.VTables __VTables
            {
                get {
                    if (__vtables.IsEmpty)
                        __vtables.Tables = new IntPtr[] { *(IntPtr*)(__Instance + 0) };
                    return __vtables;
                }

                set {
                    __vtables = value;
                }
            }
            internal virtual void SetupVTables(bool destructorOnly = false)
            {
                if (__VTables.IsTransient)
                    __VTables = VTableLoader.SetupVTables(__Instance, destructorOnly);
            }
            #endregion
        }

        public unsafe partial class PureStrategyProfile : IDisposable
        {
            [StructLayout(LayoutKind.Sequential, Size = 8)]
            public partial struct __Internal
            {
                internal __IntPtr rep;

                [SuppressUnmanagedCodeSecurity, DllImport("sharpgambit", EntryPoint = "c__N_Gambit_S_PureStrategyProfile__PureStrategyProfile", CallingConvention = __CallingConvention.Cdecl)]
                internal static extern void dtor(__IntPtr __instance);
            }

            public __IntPtr __Instance { get; protected set; }

            internal static readonly new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::gambit.Gambit.PureStrategyProfile> NativeToManagedMap =
                new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::gambit.Gambit.PureStrategyProfile>();

            internal static void __RecordNativeToManagedMapping(IntPtr native, global::gambit.Gambit.PureStrategyProfile managed)
            {
                NativeToManagedMap[native] = managed;
            }

            internal static bool __TryGetNativeToManagedMapping(IntPtr native, out global::gambit.Gambit.PureStrategyProfile managed)
            {
    
                return NativeToManagedMap.TryGetValue(native, out managed);
            }

            protected bool __ownsNativeInstance;

            internal static PureStrategyProfile __CreateInstance(__IntPtr native, bool skipVTables = false)
            {
                if (native == __IntPtr.Zero)
                    return null;
                return new PureStrategyProfile(native.ToPointer(), skipVTables);
            }

            internal static PureStrategyProfile __GetOrCreateInstance(__IntPtr native, bool saveInstance = false, bool skipVTables = false)
            {
                if (native == __IntPtr.Zero)
                    return null;
                if (__TryGetNativeToManagedMapping(native, out var managed))
                    return (PureStrategyProfile)managed;
                var result = __CreateInstance(native, skipVTables);
                if (saveInstance)
                    __RecordNativeToManagedMapping(native, result);
                return result;
            }

            internal static PureStrategyProfile __CreateInstance(__Internal native, bool skipVTables = false)
            {
                return new PureStrategyProfile(native, skipVTables);
            }

            private static void* __CopyValue(__Internal native)
            {
                var ret = Marshal.AllocHGlobal(sizeof(__Internal));
                *(__Internal*) ret = native;
                return ret.ToPointer();
            }

            private PureStrategyProfile(__Internal native, bool skipVTables = false)
                : this(__CopyValue(native), skipVTables)
            {
                __ownsNativeInstance = true;
                __RecordNativeToManagedMapping(__Instance, this);
            }

            protected PureStrategyProfile(void* native, bool skipVTables = false)
            {
                if (native == null)
                    return;
                __Instance = new __IntPtr(native);
            }

            public void Dispose()
            {
                Dispose(disposing: true, callNativeDtor : __ownsNativeInstance );
            }

            partial void DisposePartial(bool disposing);

            internal protected virtual void Dispose(bool disposing, bool callNativeDtor )
            {
                if (__Instance == IntPtr.Zero)
                    return;
                NativeToManagedMap.TryRemove(__Instance, out _);
                DisposePartial(disposing);
                if (__ownsNativeInstance)
                    Marshal.FreeHGlobal(__Instance);
                __Instance = IntPtr.Zero;
            }
        }

        /// <summary>
        /// <para>This class iterates through the contingencies in a strategic game.</para>
        /// <para>It visits each strategy profile in turn, advancing one contingency</para>
        /// <para>on each call of NextContingency().  Optionally, the strategy of</para>
        /// <para>one player may be held fixed during the iteration (by the use of the</para>
        /// <para>second constructor).</para>
        /// </summary>
        public unsafe partial class StrategyProfileIterator : IDisposable
        {
            [StructLayout(LayoutKind.Sequential, Size = 104)]
            public partial struct __Internal
            {
                internal byte m_atEnd;
                internal global::gambit.Gambit.StrategySupportProfile.__Internal m_support;
                internal global::gambit.Gambit.Array.__Internal m_currentStrat;
                internal global::gambit.Gambit.PureStrategyProfile.__Internal m_profile;
                internal int m_frozen1;
                internal int m_frozen2;

                [SuppressUnmanagedCodeSecurity, DllImport("sharpgambit", EntryPoint = "c__N_Gambit_S_StrategyProfileIterator__StrategyProfileIterator", CallingConvention = __CallingConvention.Cdecl)]
                internal static extern void dtor(__IntPtr __instance);
            }

            public __IntPtr __Instance { get; protected set; }

            internal static readonly new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::gambit.Gambit.StrategyProfileIterator> NativeToManagedMap =
                new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::gambit.Gambit.StrategyProfileIterator>();

            internal static void __RecordNativeToManagedMapping(IntPtr native, global::gambit.Gambit.StrategyProfileIterator managed)
            {
                NativeToManagedMap[native] = managed;
            }

            internal static bool __TryGetNativeToManagedMapping(IntPtr native, out global::gambit.Gambit.StrategyProfileIterator managed)
            {
    
                return NativeToManagedMap.TryGetValue(native, out managed);
            }

            protected bool __ownsNativeInstance;

            internal static StrategyProfileIterator __CreateInstance(__IntPtr native, bool skipVTables = false)
            {
                if (native == __IntPtr.Zero)
                    return null;
                return new StrategyProfileIterator(native.ToPointer(), skipVTables);
            }

            internal static StrategyProfileIterator __GetOrCreateInstance(__IntPtr native, bool saveInstance = false, bool skipVTables = false)
            {
                if (native == __IntPtr.Zero)
                    return null;
                if (__TryGetNativeToManagedMapping(native, out var managed))
                    return (StrategyProfileIterator)managed;
                var result = __CreateInstance(native, skipVTables);
                if (saveInstance)
                    __RecordNativeToManagedMapping(native, result);
                return result;
            }

            internal static StrategyProfileIterator __CreateInstance(__Internal native, bool skipVTables = false)
            {
                return new StrategyProfileIterator(native, skipVTables);
            }

            private static void* __CopyValue(__Internal native)
            {
                var ret = Marshal.AllocHGlobal(sizeof(__Internal));
                *(__Internal*) ret = native;
                return ret.ToPointer();
            }

            private StrategyProfileIterator(__Internal native, bool skipVTables = false)
                : this(__CopyValue(native), skipVTables)
            {
                __ownsNativeInstance = true;
                __RecordNativeToManagedMapping(__Instance, this);
            }

            protected StrategyProfileIterator(void* native, bool skipVTables = false)
            {
                if (native == null)
                    return;
                __Instance = new __IntPtr(native);
            }

            public void Dispose()
            {
                Dispose(disposing: true, callNativeDtor : __ownsNativeInstance );
            }

            partial void DisposePartial(bool disposing);

            internal protected virtual void Dispose(bool disposing, bool callNativeDtor )
            {
                if (__Instance == IntPtr.Zero)
                    return;
                NativeToManagedMap.TryRemove(__Instance, out _);
                DisposePartial(disposing);
                if (__ownsNativeInstance)
                    Marshal.FreeHGlobal(__Instance);
                __Instance = IntPtr.Zero;
            }
        }

        public unsafe partial class PureStrategyProfileRepInternal : global::gambit.Gambit.PureStrategyProfileRep, IDisposable
        {
            private static void* __CopyValue(__Internal native)
            {
                var ret = Marshal.AllocHGlobal(sizeof(__Internal));
                *(__Internal*) ret = native;
                return ret.ToPointer();
            }

            internal PureStrategyProfileRepInternal(__Internal native, bool skipVTables = false)
                : this(__CopyValue(native), skipVTables)
            {
                __ownsNativeInstance = true;
                __RecordNativeToManagedMapping(__Instance, this);
            }

            internal PureStrategyProfileRepInternal(void* native, bool skipVTables = false)
                : base((void*) native)
            {
            }

            /// <summary>Set the strategy for a player</summary>
            public override void SetStrategy(global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameStrategyRep> _0)
            {
                var ___SetStrategyDelegate = __VTables.GetMethodDelegate<global::gambit.Delegates.Action___IntPtr___IntPtr>(0, 3);
                if (ReferenceEquals(_0, null))
                    throw new global::System.ArgumentNullException("_0", "Cannot be null because it is a C++ reference (&).");
                var __arg0 = _0.__Instance;
                ___SetStrategyDelegate(__Instance, __arg0);
            }

            /// <summary>Get the payoff to player pl that results from the profile</summary>
            public override global::gambit.Gambit.Rational GetPayoff(int pl)
            {
                var ___GetPayoffDelegate = __VTables.GetMethodDelegate<global::gambit.Delegates.Action___IntPtr___IntPtr_int>(0, 6);
                var ___ret = new global::gambit.Gambit.Rational.__Internal();
                ___GetPayoffDelegate(__Instance, new IntPtr(&___ret), pl);
                var _____ret = global::gambit.Gambit.Rational.__CreateInstance(___ret);
                global::gambit.Gambit.Rational.__Internal.dtor(new __IntPtr(&___ret));
                return _____ret;
            }

            /// <summary>Get the value of playing strategy against the profile</summary>
            public override global::gambit.Gambit.Rational GetStrategyValue(global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameStrategyRep> _0)
            {
                var ___GetStrategyValueDelegate = __VTables.GetMethodDelegate<global::gambit.Delegates.Action___IntPtr___IntPtr___IntPtr>(0, 7);
                if (ReferenceEquals(_0, null))
                    throw new global::System.ArgumentNullException("_0", "Cannot be null because it is a C++ reference (&).");
                var __arg0 = _0.__Instance;
                var ___ret = new global::gambit.Gambit.Rational.__Internal();
                ___GetStrategyValueDelegate(__Instance, new IntPtr(&___ret), __arg0);
                var _____ret = global::gambit.Gambit.Rational.__CreateInstance(___ret);
                global::gambit.Gambit.Rational.__Internal.dtor(new __IntPtr(&___ret));
                return _____ret;
            }

            /// <summary>
            /// <para>Create a copy of the strategy profile.</para>
            /// <para>Caller is responsible for memory management of the created object.</para>
            /// </summary>
            protected override global::gambit.Gambit.PureStrategyProfileRep Copy
            {
                get
                {
                    var ___CopyDelegate = __VTables.GetMethodDelegate<global::gambit.Delegates.Func___IntPtr___IntPtr>(0, 0);
                    var ___ret = ___CopyDelegate(__Instance);
                    var __result0 = global::gambit.Gambit.PureStrategyProfileRep.__GetOrCreateInstance(___ret, true);
                    return __result0;
                }
            }

            /// <summary>Get the outcome that results from the profile</summary>
            /// <remarks>Set the outcome that results from the profile</remarks>
            public override global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameOutcomeRep> Outcome
            {
                get
                {
                    var ___GetOutcomeDelegate = __VTables.GetMethodDelegate<global::gambit.Delegates.Action___IntPtr___IntPtr>(0, 4);
                    var ___ret = new global::gambit.Gambit.GameObjectPtr.__Internal();
                    ___GetOutcomeDelegate(__Instance, new IntPtr(&___ret));
                    var _____ret = global::gambit.Gambit.GameObjectPtr<global::gambit.Gambit.GameOutcomeRep>.__CreateInstance(___ret);
                    global::gambit.Gambit.GameObjectPtr.__Internal.dtorc__N_Gambit_S_GameObjectPtr____N_Gambit_S_GameOutcomeRep(new __IntPtr(&___ret));
                    return _____ret;
                }

                set
                {
                    var ___SetOutcomeDelegate = __VTables.GetMethodDelegate<global::gambit.Delegates.Action___IntPtr___IntPtr>(0, 5);
                    if (ReferenceEquals(value, null))
                        throw new global::System.ArgumentNullException("value", "Cannot be null because it is passed by value.");
                    var __arg0 = value.__Instance;
                    ___SetOutcomeDelegate(__Instance, __arg0);
                }
            }
        }
    }
}
