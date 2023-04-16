using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Action onclick;
    public Action onPointDown;
    public Action onPointUp;
    public Action onPointEnter;
    public Action onPointExit;
    public Action<PointerEventData> onclick_with_data;
    public Action<PointerEventData> onPointDown_with_data;
    public Action<PointerEventData> onPointUp_with_data;
    public Action<PointerEventData> onPointEnter_with_data;
    public Action<PointerEventData> onPointExit_with_data;

    public Action onLongPress;
    public float durationThreshold = 0.4f;
    private bool isPointerDown = false;
    private bool longPressTriggered = false;
    private float timePressStarted;

    void Update(){
        if(onLongPress != null && isPointerDown && !longPressTriggered){
            if(Time.time - timePressStarted > durationThreshold)
            {
                longPressTriggered = true;
                onLongPress.Invoke();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!longPressTriggered){
            if(onclick != null){
                onclick();
            }
            if(onclick_with_data != null){
                onclick_with_data(eventData);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        timePressStarted = Time.time;
        isPointerDown = true;
        longPressTriggered = false;

        if(null != onPointDown) onPointDown();
        if(null != onPointDown_with_data) onPointDown_with_data(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(null != onPointEnter) onPointEnter();
        if(null != onPointEnter_with_data) onPointEnter_with_data(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(null != onPointExit) onPointExit();
        if(null != onPointExit_with_data) onPointExit_with_data(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(null != onPointUp) onPointUp();
        if(null != onPointUp_with_data) onPointUp_with_data(eventData);
        // 鼠标点击后移动会触发onpointerup,待详查
        // AudioController.Instance.SoundPlay("click_clip");
        isPointerDown = false;
    }

    public static EventListener AddEventListenr(GameObject obj){
        EventListener listener = obj.GetComponent<EventListener>();
        if(listener == null){
            listener = obj.AddComponent<EventListener>();
        }

        return listener;
    }

    public static void RemoveEventListener(GameObject obj){
        EventListener listener = obj.GetComponent<EventListener>();
        if(listener == null) return;
        GameObject.Destroy(listener);
    }
}