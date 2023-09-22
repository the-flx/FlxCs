#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using FlxCs;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Score score = Flx.Score("switch-to-buffer", "stb");

        print(score.score);

        Print(score.indices);
    }

    private void Other()
    {
        print(Flx.Word('C'));

        var lst = new List<int>() { 1, 2, 3, 4 };

        Flx.IncVec(lst, null, null, null);

        foreach (int i in lst)
        {
            print(i);
        }
    }

    public static void Print<T>(List<T> lst)
    {
        string str = "";

        foreach (var elm in lst)
        {
            str += elm.ToString() + " ";
        }

        Debug.Log(str);
    }

    public static void Print<T>(Dictionary<int, List<T>> dict)
    {
        foreach (KeyValuePair<int, List<T>> entry in dict)
        {
            Debug.LogWarning(entry.Key);
            Print(entry.Value);
        }
    }
}
#endif
