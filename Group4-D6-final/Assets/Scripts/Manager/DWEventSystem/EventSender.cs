using System;
using System.Collections.Generic;
using DWEventSystem;

public class EventSender<T> : IEventSender where T : new() {
    public enum Event {};
    private static T _instance;
    public static T Instance
    {
        get {
            if(null == _instance){
                _instance = new T();
            }
            return _instance;
        }
    }
    
    EventSystem system;
    Dictionary<int, DWEventHandler> handler_dict;
    int count_handler_id = 0;

    public EventSender(){
        system = new EventSystem();
        handler_dict = new Dictionary<int, DWEventHandler>();
    }

    public int Bind(int id, Action func) {
        count_handler_id += 1;
        handler_dict.Add(count_handler_id, system.Bind(id, func));

        return count_handler_id;
    }

    public int Bind(int id, Action<IEventData> func) {
        count_handler_id += 1;
        handler_dict.Add(count_handler_id, system.Bind(id, func));

        return count_handler_id;
    }

    public int Bind(Enum enum_type, Action func) {
        count_handler_id += 1;
        handler_dict.Add(count_handler_id, system.Bind(Convert.ToInt32(enum_type), func));

        return count_handler_id;
    }

    public int Bind(Enum enum_type, Action<IEventData> func) {
        count_handler_id += 1;
        handler_dict.Add(count_handler_id, system.Bind(Convert.ToInt32(enum_type), func));

        return count_handler_id;
    }

    public void UnBind(int handler_id){
        DWEventHandler handler;
        if(handler_dict.TryGetValue(handler_id, out handler)){
            system.UnBind(handler);
            handler_dict.Remove(handler_id);
        }
    }

    public void Fire(int id, IEventData data = null) { system.Fire(id, data); }
    public void Fire(Enum enum_type, IEventData data = null) { system.Fire(Convert.ToInt32(enum_type), data); }

    public virtual void Clear(){
        system.Dispose();
        handler_dict.Clear();
        count_handler_id = 0;
    }
}