using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : BasePool<T> where T : class, IPoolAble
{
    Stack<T> stack;
    Func<T> GetObjHandler;

    public ObjectPool(Func<T> get_handler)
    {
        stack = new Stack<T>();
        GetObjHandler = get_handler;
    }

    public void Clear()
    {
        stack.Clear();
    }

    public void Dispose()
    {
        foreach (T item in stack)
        {
            item.Recycle();
        }
        stack.Clear();
        stack = null;
    }

    public T Pop()
    {
        if (stack.Count > 0)
        {
            return stack.Pop();
        }
        else
        {
            if(null == GetObjHandler) throw new NotImplementedException();
            return GetObjHandler();
        }
    }

    public void Push(T obj)
    {
        stack.Push(obj);
    }
}