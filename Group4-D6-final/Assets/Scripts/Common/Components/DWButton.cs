using UnityEngine;
using UnityEngine.UI;

public class DWButton : Button
{
    new void Start() {
        base.Start();
        transition = Transition.None;
    }

    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        TweenUtils.ScaleTween(transform, new Vector3(0.9f, 0.9f, 0.9f), 0.1f);
    }
    public override void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        TweenUtils.ScaleTween(transform, Vector3.one, 0.1f);
    }
}