using System;
using System.Collections;

public interface BasePool<T> : IDisposable
{
    T Pop();
    void Push(T obj);
    void Clear();
}   

public interface IPoolAble {
    void Recycle();
}