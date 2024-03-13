#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using FlxCs;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        TestOne("TestSomeFunctionExterme", "met");
        TestOne("MetaX_Version", "met");
    }

    private void TestOne(string str, string query)
    {
        Result result = Flx.Score(str, query);
        print(result.ToString());
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
        string str = "";

        foreach (KeyValuePair<int, List<T>> entry in dict)
        {
            str += entry.Key + " ";
            //Print(entry.Value);
        }

        Debug.Log(str);
    }
}
#endif
