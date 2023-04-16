using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {
    private static GameSceneManager _instance;
    public static GameSceneManager Instance {
        get { return _instance;}
    }

    void Start(){
        _instance = this;
    }
    
    string pre_scene = string.Empty;
    public void LoadScene(string scene_name)
    {
        StartCoroutine(StartLoadScene(scene_name));
    }

    IEnumerator StartLoadScene(string scene_name){
        AsyncOperation pre_operation;
        if(pre_scene != string.Empty){
            pre_operation = SceneManager.UnloadSceneAsync(pre_scene);
            while (pre_operation!= null && !pre_operation.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);
        while(!operation.isDone){
            yield return new WaitForEndOfFrame();
        }

        pre_scene = scene_name;
        yield return new WaitForSeconds(1f);
    }
}