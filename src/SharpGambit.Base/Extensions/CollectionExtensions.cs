using System.Data;

namespace SharpGambit
{
    public static class CollectionExtensions
    {
        public static T? PeekIfNotEmpty<T>(this Stack<T> q) => q.Count > 0 ? q.Peek() : default(T);

        public static T? PopIfNotEmpty<T>(this Stack<T> q) => q.Count > 0 ? q.Pop() : default(T);

        public static void Pop<T>(this Stack<T> stack, int n)
        {
            for (int i = 1; i <= n; i++)
            {
                stack.Pop();
            }
        }
        public static int[] GetDims(this Array array)
        {
            if (array.Rank == 0)
            {
                return [];
            }
            int[] dims = new int[array.Rank];   
            for (int i = 0; i < array.Rank; i++)
            {
                dims[i] = array.GetLength(i);   
            }
            return dims;
        }
        
        public static int[] GetStrides(this Array array)
        {
            if (array.Rank == 0)
            {
                return [];
            }
            var dimensions = array.GetDims();   
            if (dimensions.Length == 0)
            {
                return Array.Empty<int>();
            }
            int[] strides = new int[dimensions.Length];
            int stride = 1;            
            for (int i = strides.Length - 1; i >= 0; i--)
            {
                strides[i] = stride;
                stride *= dimensions[i];
            }
            return strides;
        }

        public static int[] GetIndices(this Array array, int index)
        {
            if (array.Rank == 0)
            {
                return [];
            }
            int[] indices = new int[array.Rank];
            var strides = array.GetStrides();
            int remainder = index;
            for (int i = 0; i < strides.Length; i++)
            {
                var stride = strides[i];
                indices[i] = remainder / stride;
                remainder %= stride;
            }
            return indices;
        }
        public static int GetIndex(this int[] array, int[] indices)
        {
            var strides = array.GetStrides();
            if (strides.Length == 0)
            {
                return 0;
            }
            int index = 0;
            for (int i = 0; i < indices.Length; i++)
            {
                index += strides[i] * indices[i];
            }
            return index;
        }


        public static string JoinWith(this IEnumerable<string> s, string j)
        {
            if (s.Count() == 0)
            {
                return "";
            }
            else if (s.Count() == 1)
            {
                return s.First();
            }
            else
            {
                return s.Aggregate((a, b) => a + j + b);
            }
        }

        public static string JoinWithSpaces(this IEnumerable<string> s)
        {
            if (s.Count() == 0)
            {
                return "";
            }
            else if (s.Count() == 1)
            {
                return s.First();
            }
            else
            {
                return s.Aggregate((a, b) => a + " " + b);
            }
        }

        public static T FailIfKeyNotPresent<T>(this Dictionary<string, object> d, string k)
            => d.ContainsKey(k) ? (T)d[k] : throw new KeyNotFoundException($"The required key {k} is not present.");

        public static T? TryGet<T>(this Dictionary<string, object> d, string k) => d.ContainsKey(k) ? (T)d[k] : default(T);

        public static T Get<T>(this Dictionary<string, object> d, string k) => d.ContainsKey(k) ? (T)d[k] : throw new KeyNotFoundException();

        public static void AddIfNotExists<K, V>(this Dictionary<K, V> d, K k, V v) where K : notnull
        {
            if (!d.ContainsKey(k))
            {
                d.Add(k, v);
            }
        }

        public static T SingleOrFailure<T>(this IEnumerable<T> collection, Func<T, bool> p, string failureMessage = "No element in the collection satisfies the condition.") => 
            collection.SingleOrDefault(p) ?? throw new ArgumentException(failureMessage);
            
             
    }
}