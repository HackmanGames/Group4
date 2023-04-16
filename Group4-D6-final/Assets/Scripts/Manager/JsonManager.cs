using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Serialization<T>
{
    [SerializeField]
    private List<T> target;
    public List<T> ToList() { return target; }

    public Serialization(List<T> target)
    {
        this.target = target;
    }
}

[Serializable]
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    List<TKey> keys;
    [SerializeField]
    List<TValue> values;

    Dictionary<TKey, TValue> target;
    public Dictionary<TKey, TValue> ToDictionary() { return target; }

    public Serialization(Dictionary<TKey, TValue> target)
    {
        this.target = target;
    }

    public void OnAfterDeserialize()
    {
        target = new Dictionary<TKey, TValue>();
        int count = Mathf.Min(keys.Count, values.Count);
        for (int i = 0; i < count; i++)
        {
            target.Add(keys[i], values[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        this.keys = new List<TKey>(target.Keys);
        this.values = new List<TValue>(target.Values);
    }
}

public class JsonHelper
{
    public static T ReadDataFromJson<T>(string json_file_name)
    {
        string path = GameConfig.Instance.JsonPath + json_file_name + ".json";

        WWW t_WWW = new WWW(path);     //格式必须是"ANSI"，不能是"UTF-8"
        while (!t_WWW.isDone)
        {
            if (t_WWW.error != null)
            {
                Debug.LogError("error : " + path);
            }
        }

        string json =
#if UNITY_EDITOR
            File.ReadAllText(path);
#else
            t_WWW.text;
#endif

        T data = JsonUtility.FromJson<T>(json);
        return data;
    }

    public static void ToJson<T>(T data, string json_file_name)
    {
        string path = GameConfig.Instance.JsonPath + json_file_name + ".json";
        string json = JsonUtility.ToJson(data);
        if (File.Exists(path)) File.Delete(path);
        File.WriteAllText(path, json);
    }
}

public class JsonManager : Singleton<JsonManager>
{
    //保存已经加载过的Json数据结果,便于下次获取时直接返回
    Dictionary<string, object> config_Dict;

    public JsonManager()
    {
        config_Dict = new Dictionary<string, object>();
    }

    public List<T> GetJsonConfig<T>(string json_file_name)
    {
        List<T> target_list;
        if (!config_Dict.ContainsKey(json_file_name))
        {
            Serialization<T> list = JsonHelper.ReadDataFromJson<Serialization<T>>(json_file_name);
            target_list = list.ToList();
            config_Dict.Add(json_file_name, target_list);
        }
        else
        {
            target_list = config_Dict[json_file_name] as List<T>;
        }

        return target_list;
    }

    public Dictionary<TKey, TValue> GetJsonConfig<TKey, TValue>(string json_file_name)
    {
        Dictionary<TKey, TValue> target_dict;
        if (!config_Dict.ContainsKey(json_file_name))
        {
            Serialization<TKey, TValue> dict = JsonHelper.ReadDataFromJson<Serialization<TKey, TValue>>(json_file_name);
            target_dict = dict.ToDictionary();
            config_Dict.Add(json_file_name, target_dict);
        }
        else
        {
            target_dict = config_Dict[json_file_name] as Dictionary<TKey, TValue>;
        }

        return target_dict;
    }

    public void ToJson<T>(List<T> target, string json_file_name)
    {
        config_Dict[json_file_name] = target;

        Serialization<T> target_list = new Serialization<T>(target);
        JsonHelper.ToJson<Serialization<T>>(target_list, json_file_name);
    }

    public void ToJson<TKey, TValue>(Dictionary<TKey, TValue> target, string json_file_name)
    {
        config_Dict[json_file_name] = target;

        Serialization<TKey, TValue> target_dictionay = new Serialization<TKey, TValue>(target);
        JsonHelper.ToJson<Serialization<TKey, TValue>>(target_dictionay, json_file_name);
    }
}