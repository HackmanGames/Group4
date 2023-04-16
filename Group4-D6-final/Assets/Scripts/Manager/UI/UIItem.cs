using UnityEngine;

public abstract class UIItem : UIBase
{
    public GameObject gameObject;
    public Transform transform;
    public string panel_path;

    public UIItem()
    {
        Init();
    }

    public void Open(Transform parent = null)
    {
        gameObject = GameObject.Instantiate(ResourcesManager.Instance.LoadResouces<GameObject>(GameConfig.Instance.PanelPath + "/" + panel_path));
        transform = gameObject.transform;

        transform.SetParent(parent, false);
        transform.SetAsLastSibling();

        InitUIs();
        OnCreate();
        BindEvent();
    }

    public virtual void Init(){}
    protected virtual void InitUIs(){}
    protected virtual void OnCreate(){}

    public virtual void BindEvent() {}
    public virtual void SetShow(bool flag) { gameObject.SetActive(flag); }
    public virtual void UnInit(){}
    
    public virtual void Remove(){
        UnInit();
        ClearBinds();
        GameObject.Destroy(gameObject);
    }
}