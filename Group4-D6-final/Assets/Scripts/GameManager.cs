using System.Collections.Generic;
using UnityEngine;
using Table;

public class GameManager : MonoBehaviour{
    float game_time = 0;
    void Awake() {
        DontDestroyOnLoad(gameObject);
        game_time = Time.time;
    }
    
    void Start(){
        Input.multiTouchEnabled = false;
        UIManager m = UIManager.Instance;
        UIManager.Instance.ShowMainPanel(typeof(GameMenu_MainPanel));
        AudioController.Instance.BGMPlay("bgm");
    }

    void Update(){
        float excap_time = Time.time - game_time;
        game_time = Time.time;

        TimerManager.Instance.Update(game_time, excap_time);
    }
}