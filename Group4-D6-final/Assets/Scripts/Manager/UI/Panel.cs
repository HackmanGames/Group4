public class Panel : UIBase {
    public bool isReveal = false;
    public bool isHide = false;
    protected virtual void OnCreate(){}
    protected virtual void OnShow(){}
    protected virtual void OnReTop(){}
    protected virtual void OnUpdate(IUIParam param_data = null){}
    protected virtual void OnReveal(){}
    protected virtual void OnHided(){}
    protected virtual void OnDestory(){}
    public virtual bool OnProcessEsc(){
        return true;
    }
}