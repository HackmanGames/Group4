using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Table;

public partial class GameMenu_MainPanel : BasePanel {
    protected override void OnCreate()
    {
        base.OnCreate();
        btn_startGame.onClick.AddListener(()=>{
			AudioController.Instance.SoundPlay("menu_click");
            UIManager.Instance.ShowMainPanel(typeof(MainGame_MainPanel));
            Close();
        });
    }

    protected override void OnShow()
    {
        base.OnShow();
        AudioController.Instance.BGMSetVolume(1f);
    }
}