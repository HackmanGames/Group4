using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelData {
    public Type type;
    public string name;
    public string prefab_name;
    public string prefab_path;
    public bool in_stack;
    
    public PanelData(string name, Type type, string prefab_name, bool in_stack = true)
    {
        this.name = name;
        this.type = type;
        this.prefab_name = prefab_name;
        this.in_stack = in_stack;
        prefab_path = GameConfig.Instance.PanelPath + "/" + prefab_name;
    }

    public object MakeInstance(){
        return Activator.CreateInstance(type);
    }

    public string GetPanelPrefabPath(){
        return prefab_path;
    }

    public bool CheckIsInStack(){
        return in_stack;
    }
}

public class PanelConfig : Singleton<PanelConfig> {
    static Dictionary<Type, PanelData> panel_config = new Dictionary<Type, PanelData>();

    public PanelConfig(){
		panel_config.Add(typeof(GameMenu_MainPanel), new PanelData("GameMenu_MainPanel", typeof(GameMenu_MainPanel), "GameMenu/GameMenu_MainPanel"));
		panel_config.Add(typeof(MainGame_MainPanel), new PanelData("MainGame_MainPanel", typeof(MainGame_MainPanel), "MainGame/MainGame_MainPanel"));
    }//end

    public void AddPanelData(Type t, PanelData data){
        if(panel_config.ContainsKey(t)){
            Debug.Log($"已存在名为{t.Name}的面板");
        }
        panel_config.Add(t, data);
    }

    public PanelData GetPanelConfig(Type t){
        PanelData data;
        panel_config.TryGetValue(t, out data);
        if(data == null) Debug.LogError($"不存在名字为{t.Name}的面板,请检查PanelConfig中是否添加了相应的配置。");
        return data;
    }
}
