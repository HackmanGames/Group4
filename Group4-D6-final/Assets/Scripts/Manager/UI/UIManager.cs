using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PanelType
{
    Main = 0,
    Pop = 1,
    Alert = 2,
    Top = 3,
    Loading = 4,
}

public class UIManager : Singleton<UIManager>
{
    int AUTO_RELEASE_PANEL_NUM = 12;
    Dictionary<PanelType, Stack> panel_list;
    Dictionary<string, ConfigPanel> config_panel;
    BasePanel alertpanel;
    BasePanel mainPanel;

    GameObject eventSystem;

    List<BasePanel> panel_stack_list;
    Dictionary<PanelType, Transform> canvas_dict;
    Dictionary<String, BasePanel> showing_dict;
    Dictionary<String, BasePanel> hide_dict;
    Dictionary<String, int> panel_count_dict;

    public UIManager()
    {
        panel_list = new Dictionary<PanelType, Stack>();
        canvas_dict = new Dictionary<PanelType, Transform>();
        hide_dict = new Dictionary<string, BasePanel>();
        showing_dict = new Dictionary<string, BasePanel>();
        panel_count_dict = new Dictionary<string, int>();
        panel_stack_list = new List<BasePanel>();
        InitCanvas();
    }
        
    void InitCanvas()
    {
        GameObject.DontDestroyOnLoad(GameObject.Find("Canvas"));
        GameObject.DontDestroyOnLoad(GameObject.Find("EventSystem"));
        canvas_dict.Add(PanelType.Main, GameObject.Find("Main") .transform);
        canvas_dict.Add(PanelType.Pop, GameObject.Find("Pop") .transform);
        canvas_dict.Add(PanelType.Alert, GameObject.Find("Alert") .transform);
        canvas_dict.Add(PanelType.Loading, GameObject.Find("Loading") .transform);
        panel_list.Add(PanelType.Pop, new Stack());
        eventSystem = GameObject.Find("EventSystem");
    }

    private BasePanel OpenPanel(Type t, PanelType type, IUIParam param_data = null)
    {
        string name = t.Name;
        string panel_temp_name = name;
        PanelData panel_data = PanelConfig.Instance.GetPanelConfig(t);
        switch (type)
        {
            case PanelType.Main:
                panel_temp_name = name;
                break;
            default:
                int count = 0;
                if(panel_count_dict.TryGetValue(name, out count))
                {
                    count = count + 1;
                    panel_count_dict[name] = count;
                }else{
                    panel_count_dict.Add(name, count);
                }
                panel_temp_name = name + "<" + count.ToString() + ">";
                break;
        }

        BasePanel panel;
        if(showing_dict.ContainsKey(panel_temp_name)){
            int count = 0;
            while(true){
                panel = panel_stack_list[panel_stack_list.Count - 1];;
                if(panel.gameObject.name == panel_temp_name){
                    break;
                }else{
                    count ++;
                    panel.Hide();
                    panel_stack_list.RemoveAt(panel_stack_list.Count - 1);
                }
            }
            if(count > 0 && panel.isReveal) panel.ReTop();
            panel.SetData(param_data);
            return panel;
        }else if(hide_dict.ContainsKey(panel_temp_name)){
            panel = hide_dict[panel_temp_name];
            hide_dict.Remove(panel_temp_name);
        }else
        {
            panel = panel_data.MakeInstance() as BasePanel;
            panel.Create(panel_data, canvas_dict[type]);
        }

        panel.Open();
        panel.SetData(param_data);
        panel.gameObject.name = panel_temp_name;
        panel.transform.SetAsLastSibling();
        showing_dict.Add(panel_temp_name, panel);
        if(panel_data.CheckIsInStack()){
            if(panel_stack_list.Count > 0){
                BasePanel p = panel_stack_list[panel_stack_list.Count - 1];
                if(!p.isReveal) p.Reveal();
            }

            panel_stack_list.Add(panel);
        }

        return panel;
    }

    public void CloseTopPanel(){
        BasePanel panel = panel_stack_list[panel_stack_list.Count - 1];
        if(panel.OnProcessEsc()) panel.Close();
    }

    public BasePanel ShowMainPanel(Type t){
        return OpenPanel(t, PanelType.Main);
    }

    public BasePanel ShowPop(Type t) {
        return OpenPanel(t, PanelType.Pop);
    }

    public BasePanel ShowLoading(Type t) {
        return OpenPanel(t, PanelType.Loading);
    }

    public void ClosePanel(String panel_name){
        BasePanel p;
        if(showing_dict.TryGetValue(panel_name, out p))
        {
            p.Hide();
            showing_dict.Remove(panel_name);
            if(Regex.IsMatch(panel_name, @"\w+<\d+>")){
                string panel_real_name = Regex.Match(panel_name, @"\w+(?=<.*)").Value;
                panel_count_dict[panel_real_name] = panel_count_dict[panel_real_name] - 1;
            }
            
            for (int i = 0; i < panel_stack_list.Count; i++)
            {
                if(panel_stack_list[i] == p){
                    panel_stack_list.RemoveAt(i);
                    break;
                }
            }
            hide_dict.Add(panel_name, p);
        }
    }

    public void CloseAllPanel(){
        while (panel_stack_list.Count > 0)
        {
            BasePanel p = panel_stack_list[panel_stack_list.Count - 1];
            p.Close();
        }
    }

    //检查自动释放界面
    void CheckAutoRelease(){

    }

    public void DisableInputEvent()
    {
        eventSystem.SetActive(false);
    }
    public void EnableInputEvent()
    {
        eventSystem.SetActive(true);
    }
}