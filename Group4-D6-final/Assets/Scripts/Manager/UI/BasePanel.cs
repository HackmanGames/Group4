using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : Panel
{
    public PanelData panel_data;
    public GameObject gameObject;
    public Transform transform;
    public bool need_animation = true;

    public BasePanel()
    {
    }

    public void Create(PanelData data, Transform parent = null){
        panel_data = data;
        gameObject = GameObject.Instantiate(ResourcesManager.Instance.LoadResouces<GameObject>(data.GetPanelPrefabPath()));
        transform = gameObject.transform;
        transform.SetParent(parent, false);
        Init();
        InitUIs();
        OnCreate();
    }

    #region  忽略
    protected virtual void Init(){}
    protected abstract void InitUIs();
    
    public void Open()
    {
        Show();
    }
    
    public virtual void SetShow(bool flag) { gameObject.SetActive(flag); }
    
    public void Release(){
        OnDestory();
        GameObject.Destroy(gameObject);
    }

    public virtual void Hide()
    {
        isHide = true;
        UnInit();
        SetShow(false);
        Reveal();
        OnHided();
    }
    #endregion

    public virtual void Close()
    {
        UIManager.Instance.ClosePanel(gameObject.name);
    }

    public virtual void Show()
    {
        isHide = false;
        OnBindEvents();
        OnShow();
        OnReTop();
        SetShow(true);
    }

    public virtual void SetData(IUIParam param_data = null)
    {
        OnUpdate(param_data);
    }

    public virtual void ReTop()
    {
        isReveal = false;
        OnReTop();
    }

    public virtual void Reveal(){
        isReveal = true;
        OnReveal();
    }


    public virtual void UnInit() {
        ClearBinds();
    }

    protected virtual void OnBindEvents(){
    }
}