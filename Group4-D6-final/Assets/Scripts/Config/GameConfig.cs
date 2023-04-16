using UnityEngine;

public class GameConfig : Singleton<GameConfig>
{
    public readonly string PanelPath = "Prefabs/UIs/Panel";
    public readonly string ItemPath= "Prefabs/UIs/Item";
    public readonly string JsonPath =
#if UNITY_ANDROID && !UNITY_EDITOR
                    "jar:file://" + Application.dataPath + "!/assets/Jsons/";
#elif UNITY_IPHONE && !UNITY_EDITOR
                    Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_MAC || UNITY_STANDALONE_WIN || UNITY_EDITOR
                    Application.streamingAssetsPath + "/Jsons/";
#else
                    string.Empty;
#endif

    public readonly string DataTablePath =
#if UNITY_ANDROID && !UNITY_EDITOR
                    "jar:file://" + Application.dataPath + "!/assets/DataTable/";
#elif UNITY_IPHONE && !UNITY_EDITOR
                    Application.dataPath + "/Raw/DataTable/";
#elif UNITY_STANDALONE_MAC || UNITY_STANDALONE_WIN || UNITY_EDITOR
                    Application.streamingAssetsPath + "/DataTable/";
#else
                    string.Empty;
#endif

    public readonly string version = "v1.0.0";


    public readonly float NUM_FLOAT_DUR = 0.5f;
    public readonly float NUM_EXIT_DUR = 0.4f;
    public readonly float NUM_POS_REFRESH_DUR = 0.3f;
}