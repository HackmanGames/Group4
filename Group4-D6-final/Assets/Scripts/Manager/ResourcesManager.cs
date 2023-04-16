using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager: Singleton<ResourcesManager>{
    private Dictionary<string, Object> objectDict;

    public ResourcesManager(){
        objectDict = new Dictionary<string, Object>();
    }

    Object Load(string path){
        Object obj = Resources.Load(path);
        if(null == obj){
            Debug.LogError($"不存在{path}这个资源！！！");
        }else{
            objectDict.Add(path, obj);
        }
        return obj;
    }

    T Load<T>(string path) where T : class{
        T obj = Resources.Load(path, typeof(T)) as T;
        if(null == obj){
            Debug.LogError($"不存在{path}这个资源！！！");
        }else{
            objectDict.Add(path, obj as Object);
        }
        return obj;
    }

    public T LoadResouces<T>(string path) where T : class{
        if(objectDict.ContainsKey(path)) return objectDict[path] as T;
        return Load<T>(path);
    }

    public Object LoadResouces(string path){
        if(objectDict.ContainsKey(path)) return objectDict[path];
        return Load(path);
    }

    public void Clear(){
        objectDict = new Dictionary<string, Object>();
    }
}