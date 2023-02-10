namespace BTFS
{
    using SFDGameScriptInterface;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public static class Global
    {
        /* BTFS Settings */
        public static bool Debug = true;
        public static bool UnstableBuild = true;
        public static readonly string Version = "v0.50 Beta";

        // Original author: NearHuscarl
        // https://github.com/NearHuscarl/BotExtended/blob/master/src/BotExtended/Library/SharpHelper.cs
        #region NearHuscarlCode
        public static T StringToEnum<T>(string str) { return (T)Enum.Parse(typeof(T), str); }
        public static T[] EnumToArray<T>() { return (T[])Enum.GetValues(typeof(T)); }
        public static string EnumToString<T>(T enumVal) { return Enum.GetName(typeof(T), enumVal); }
        
        public static void Timeout(Action callback, uint interval) {Events.UpdateCallback.Start(e => callback.Invoke(), interval, 1);}
        
        public static bool TryParseEnum<T>(string str, out T result) where T : struct, IConvertible
        {
            result = default(T);

            if (!typeof(T).IsEnum)
            {
                return false;
            }

            int index = -1;
            if (int.TryParse(str, out index))
            {
                if (Enum.IsDefined(typeof(T), index))
                {
                    // https://stackoverflow.com/questions/10387095/cast-int-to-generic-enum-in-c-sharp
                    result = (T)(object)index;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (!Enum.TryParse(str, ignoreCase: true, result: out result))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    
        public static void Shuffle<T> (this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1) 
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static Int32 GetPercent(Int32 b, Int32 a)
        {
            if (b == 0) return 0;

            return (Int32)( a / (b / 100M));
        }

        public static int[] GenerateRandomArrayNonDuplicate (int length)
        {
            Random random = new Random();
            return Enumerable.Range (0, length).OrderBy(c => random.Next()).ToArray();
        }

        public static float GetPercentByValue (float current, float maximum)
        {
            float result = (float) Math.Round ((double) (100 * current) / maximum);

            return result;
        }

        public static double GetPercentByValue (double current, double maximum)
        {
            double result = Math.Round ((double) (100 * current) / maximum);

            return result;
        }

        public static int GetPercentByValue (int current, int maximum)
        {
            int result = (int) Math.Round ((double) (100 * current) / maximum);

            return result;
        }

        public static T[] Append<T>(this T[] array, T item)
        {
            if (array == null)
            {
                return new T[] { item };
            }
            T[] result = new T[array.Length + 1];
            array.CopyTo(result, 0);
            result[array.Length] = item;
            return result;
        }
    }
}
