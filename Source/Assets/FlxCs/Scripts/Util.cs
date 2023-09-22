using System.Collections.Generic;

namespace FlxCs
{
    public static class Util
    {
        public static void DictSet(Dictionary<int, List<Score>> result, int? key, List<Score> val)
        {
            if (key == null)
                return;

            int _key = key.Value;

            if (!result.ContainsKey(_key))
                result.Add(_key, val);

            result[_key] = val;
        }

        public static List<T> DictGet<T>(Dictionary<int, List<T>> dict, int? key)
        {
            if (key == null)
                return null;

            if (!dict.ContainsKey(key.Value))
                return null;

            return dict[key.Value];
        }

        public static void DictInsert(Dictionary<int, List<int>> result, int key, int val)
        {
            if (!result.ContainsKey(key))
                result.Add(key, new List<int>());

            List<int> lst = result[key];
            lst.Insert(0, val);
        }
    }
}
