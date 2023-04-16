using System;
using System.Collections.Generic;
using UnityEngine;
public class Timer
{
    float times = 0;
    float max_times = 0;
    float next_time = 0;
    float end_time = 0;
    float delay_time = 0;

    Action first_enter;
    Action<float, float> update;
    Action<float> call_back;

    public Timer(float delay_time, float all_time, Action<float, float> update_func, Action first_enter = null, Action<float> callback = null)
    {
        this.delay_time = delay_time;
        next_time = Time.time;
        end_time = Time.time + all_time + delay_time;

        this.first_enter = first_enter;
        this.update = update_func;
        this.call_back = callback;
    }

    public void Enter() { if (null != first_enter) first_enter(); }
    public void Update(float now_time, float escap_time)
    {
        if (null != update) update(now_time, escap_time);
        next_time = next_time + delay_time;
    }
    public void CallBack(float now_time) { if (null != call_back) call_back(now_time); }

    public bool CanRemove() { return next_time >= end_time; }
    public bool CanUpdate(float now_time) { return next_time <= now_time; }
}

public class TimerManager : Singleton<TimerManager>
{
    List<Timer> timer_list;

    public TimerManager()
    {
        timer_list = new List<Timer>();
    }

    public void Update(float now_time, float escap_time)
    {
        for(int i = 0; i < timer_list.Count; i++){
            Timer timer = timer_list[i];
            
            if (timer.CanUpdate(now_time)) { timer.Update(now_time, escap_time); }

            if (timer.CanRemove())
            {
                timer.CallBack(now_time);
                timer_list.Remove(timer);
                i --;
            }
        }
    }

    /// <summary>
    /// 添加时间管理器
    /// </summary>
    /// <param name="delay_time">每次执行的间隔时间</param>
    /// <param name="all_time">总时间（时间到则停止执行）</param>
    /// <param name="update_func">时间内每次执行的函数</param>
    /// <param name="first_enter">首次开启就执行的函数</param>
    /// <param name="callback">记时结束后执行</param>
    /// <returns>Timer类</returns>
    public Timer AddTimer(float delay_time, float all_time, Action<float, float> update_func, Action first_enter = null, Action<float> callback = null)
    {
        Timer timer = new Timer(delay_time, all_time, update_func, first_enter, callback);
        timer.Enter();
        timer_list.Add(timer);

        return timer;
    }

    public bool StopTimer(Timer timer, bool force_stop = false){
        if(!force_stop) timer.CallBack(Time.time);
        return timer_list.Remove(timer);
    }
}