using System;
using System.Collections.Generic;

public class UIBase {
    Dictionary<IEventSender, List<int>> event_dict;

    public UIBase() {
        event_dict = new Dictionary<IEventSender, List<int>>();
    }

    protected void BindEvent(IEventSender event_sender, Enum event_id, Action func){
        List<int> handler_ids;
        if(event_dict.TryGetValue(event_sender, out handler_ids)){
            handler_ids.Add(event_sender.Bind(event_id, func));
        }else{
            handler_ids = new List<int>();
            event_dict.Add(event_sender, handler_ids);
            handler_ids.Add(event_sender.Bind(event_id, func));
        }
    }

    protected void UnBind(IEventSender event_sender, Enum event_id, Action func){
        List<int> handler_ids;
        if(event_dict.TryGetValue(event_sender, out handler_ids)){
            handler_ids.Add(event_sender.Bind(event_id, func));
        }else{
            handler_ids = new List<int>();
            event_dict.Add(event_sender, handler_ids);
            handler_ids.Add(event_sender.Bind(event_id, func));
        }
    }

    public void ClearBinds(){
        foreach (var item in event_dict)
        {
            foreach (var handler_id in item.Value)
            {
                item.Key.UnBind(handler_id);
            }
        }
        event_dict.Clear();
    }
}