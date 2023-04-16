using System;

public interface IEventSender {
    int Bind(int id, Action func);
    int Bind(int id, Action<IEventData> func);
    int Bind(Enum enum_type, Action func);
    int Bind(Enum enum_type, Action<IEventData> func);
    void UnBind(int handler_id);
    void Fire(int id, IEventData data = null);
    void Fire(Enum enum_type, IEventData data = null);
    void Clear();
}