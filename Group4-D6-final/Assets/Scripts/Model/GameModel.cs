using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : EventSender<GameModel>
{
    new public enum Event
    {
        GAME_START,
        GAME_INFO_UPDATE,
    }

    public List<Color> list_color = new List<Color>();
    public NumInfoData cur_numinfo;

    public int total_combo = 0;
    public int combo = 0;
    public int remaind_kill_count = 6;

    public GameModel(){
        AddColor("#ffd966");
        AddColor("#a4907c");
        AddColor("#ffbf9b");
        AddColor("#94af9f");
        cur_numinfo = null;
        Init();
    }

    void AddColor(string hex)
    {
        var color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        list_color.Add(color);
    }

    public void Init()
    {
        cur_numinfo = null;
        remaind_kill_count = 6;
        total_combo = 0;
        combo = 0;
        Fire(Event.GAME_INFO_UPDATE);
    }

    public void AddCombo()
    {
        total_combo ++;
        combo ++;
        Fire(Event.GAME_INFO_UPDATE);
    }

    public bool KillCur()
    {
        if(remaind_kill_count <= 0) return false;
        
        combo = 0;
        cur_numinfo = null;
        remaind_kill_count --;
        Fire(Event.GAME_INFO_UPDATE);
        return true;
    }
}
