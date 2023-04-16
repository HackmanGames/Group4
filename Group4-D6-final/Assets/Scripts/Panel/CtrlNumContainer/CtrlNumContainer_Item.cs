using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public partial class CtrlNumContainer_Item : UIItem
{

    Queue<CtrlNum_Item> quene_items = new Queue<CtrlNum_Item>();

    public override void Init()
    {
        panel_path = "MainGame/CtrlNumContainer_Item";
    }

    public Action<CtrlNum_Item, NumInfoData> handler_click_cb;

    protected override void OnCreate()
    {
        AddQuene();
        AddQuene();
        AddQuene();
        Refresh();
    }

    void AddQuene()
    {
        var item = new CtrlNum_Item();
        item.Open(gameObject.transform);
        item.transform.localPosition = new Vector2(0, 100 + 250 * quene_items.Count + UnityEngine.Random.Range(5, 200));
        quene_items.Enqueue(item);
        item.click_cb = HandlerNumItemClick;
    }

    void HandlerNumItemClick(CtrlNum_Item item, NumInfoData infoData)
    {
        if (quene_items.Peek() != item)
        {
            AudioController.Instance.SoundPlay("item_click_error");
            return;
        }
        NumInfoData cur = GameModel.Instance.cur_numinfo;

        if (cur != null)
        {
            if (cur.color_id != infoData.color_id)
            {
                if (infoData.num == 1)
                {
                    if (cur.num != 6)
                    {
                        AudioController.Instance.SoundPlay("item_click_error");
                        return;
                    }
                }
                else if (infoData.num - cur.num != 1)
                {
                    AudioController.Instance.SoundPlay("item_click_error");
                    return;
                }
            }
        }

        AddQuene();
        quene_items.Dequeue();
        Refresh();
        AudioController.Instance.SoundPlay("item_click");
        handler_click_cb?.Invoke(item, infoData);
    }

    void Refresh()
    {
        var array = quene_items.ToArray();
        for (int i = 0; i < quene_items.Count; i++)
        {
            array[i].transform.DOLocalMove(new Vector3(0, 100 + 250 * i, 0), GameConfig.Instance.NUM_POS_REFRESH_DUR);
        }
        quene_items.Peek().StartAnim();
    }
}
