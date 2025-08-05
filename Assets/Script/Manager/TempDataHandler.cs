using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TempDataHandler
{
    private static readonly Dictionary<string, object> _objDict = new Dictionary<string, object>();

    public static void Set<T>(string key, T data) where T : class
    {
        if (!_objDict.ContainsKey(key))
        {
            _objDict.Add(key, data);
            return;
        }

        _objDict[key] = data;
    }

    public static T Get<T>(string key) where T : class
    {
        if (_objDict.TryGetValue(key, out var value)) return value as T;

        Debug.LogError($"Data voi key: {key} chua duoc tao");
        return null;
    }
}
