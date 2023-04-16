using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public partial class MainGame_MainPanel : BasePanel {
    CtrlNum_Item pre_item;
    Vector3 init_pos;
    
    
    protected override void OnCreate()
    {
        base.OnCreate();
        imgbtn_menu.onClick.AddListener(()=>{
            UIManager.Instance.CloseAllPanel();
            UIManager.Instance.ShowMainPanel(typeof(GameMenu_MainPanel));
        });
        imgbtn_restart.onClick.AddListener(()=>{
            InitGame();
            AudioController.Instance.SoundPlay("refresh");
        });
        init_pos = obj_nums.transform.localPosition;
    }

    protected override void OnShow()
    {
        base.OnShow();
        InitGame();
        AudioController.Instance.BGMSetVolume(0.3f);
    }

    void InitGame()
    {
        GameModel.Instance.Init();
        Utils.RemoveAllChilds(obj_nums.gameObject);
        Utils.RemoveAllChilds(trans_target.gameObject);
        for (int i = 0; i < 7; i++)
        {
            var item = new CtrlNumContainer_Item();
            item.Open(obj_nums.transform);
            item.handler_click_cb = HandlerNumItemClick;
        }
        txt_desc.text = $"Total Combo:{GameModel.Instance.total_combo}\nCombo:{GameModel.Instance.combo}\nRemaind Times:{GameModel.Instance.remaind_kill_count}";
        
        obj_nums.transform.localPosition = init_pos + new Vector3(0, 50, 0);
        obj_nums.transform.DOLocalMove(init_pos, GameConfig.Instance.NUM_FLOAT_DUR);
    }

    protected override void OnBindEvents()
    {
        base.OnBindEvents();
        BindEvent(GameModel.Instance, GameModel.Event.GAME_INFO_UPDATE, ()=>{
            txt_desc.text = $"Total Combo:{GameModel.Instance.total_combo}\nCombo:{GameModel.Instance.combo}\nRemaind Times:{GameModel.Instance.remaind_kill_count}";
        });
    }

    void HandlerNumItemClick(CtrlNum_Item item, NumInfoData infoData)
    {

        GameModel.Instance.cur_numinfo = infoData;
        var trans = item.gameObject.transform;
        trans.SetParent(trans_target, true);
        
        if(pre_item != null)
        {
            var it = pre_item;
            it.transform.DOLocalMoveY(-500, GameConfig.Instance.NUM_EXIT_DUR).OnComplete(()=>{
                GameObject.DestroyImmediate(it.gameObject);
            });
        }
        GameModel.Instance.AddCombo();
        item.transform.DOLocalMove(Vector3.zero, GameConfig.Instance.NUM_FLOAT_DUR).OnStart(()=>{UIManager.Instance.DisableInputEvent();}).OnComplete(()=>{UIManager.Instance.EnableInputEvent();});
        item.click_cb = (it, info)=>{
            if (GameModel.Instance.KillCur())
            {
                GameObject.DestroyImmediate(it.gameObject);
            }
        };
        pre_item = item;
    }
}
