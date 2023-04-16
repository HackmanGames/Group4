using System;

[Serializable]
public struct ConfigPanel {
    public string name;
    public string panelType;
    public string scene_json;
}

[Serializable]
public struct ConfigGameItem {
    public string target_panel;
    public string icon;
    public string name;
    public string help;
}

[Serializable]
public struct ConfigTeamName {
    public string str;
}

[Serializable]
public struct ConfigWheelItem
{
    public string id;
    public string name;
    public string description;
}

[Serializable]
public struct ConfigWheelQues
{
    public string question;
}

[Serializable]
public struct ConfigSpyQuestion
{
    public string content;
}