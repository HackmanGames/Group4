using System;
using System.Collections.Generic;

namespace DWEventSystem{
    public enum EventType{
        HAVE_DATA,
        EMPTY
    }

    public class DWEventHandler {
        public int id;
        public EventType type;
        public Action empty_func;
        public Action<IEventData> data_func;

        public DWEventHandler(int id, Action func){
            this.id = id;
            this.type = EventType.EMPTY;
            empty_func = func;
        }

        public DWEventHandler(int id, Action<IEventData> func){
            this.id = id;
            this.type = EventType.HAVE_DATA;
            data_func = func;
        }
    }

    public class EventSystem : IDisposable {
        public Dictionary<int, Action> empty_dict;
        public Dictionary<int, Action<IEventData>> data_dict;

        public EventSystem(){
            empty_dict = new Dictionary<int, Action>();
            data_dict = new Dictionary<int, Action<IEventData>>();
        }

        public DWEventHandler Bind(int id, Action func){
            if(!empty_dict.ContainsKey(id)){
                empty_dict.Add(id, func);
            }else{
                empty_dict[id] += func;
            }

            return new DWEventHandler(id, func);
        }

        public DWEventHandler Bind(int id, Action<IEventData> func){
            if(!data_dict.ContainsKey(id)){
                data_dict.Add(id, func);
            }else{
                data_dict[id] += func;
            }

            return new DWEventHandler(id, func);
        }

        public void UnBind(DWEventHandler handler){
            if(handler.type.Equals(EventType.EMPTY)){
                empty_dict[handler.id] -= handler.empty_func;
                if(empty_dict[handler.id] == null) empty_dict.Remove(handler.id);
            }else{
                data_dict[handler.id] -= handler.data_func;
                if(data_dict[handler.id] == null) data_dict.Remove(handler.id);
            }
        }

        public void Fire(int id, IEventData data = null){
            Action empty_action;
            if(empty_dict.TryGetValue(id, out empty_action)) {
                empty_action.Invoke();
            };

            if(data == null) return;
            Action<IEventData> data_action;
            if(data_dict.TryGetValue(id, out data_action)) {
                data_action.Invoke(data);
            }
        }

        public void Dispose()
        {
            empty_dict.Clear();
            data_dict.Clear();
        }
    }
}