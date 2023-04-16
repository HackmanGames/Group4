using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NumInfoData {
	public int num;
	public int color_id;
}

public partial class CtrlNum_Item : UIItem {
	public override void Init(){
		panel_path = "MainGame/CtrlNum_Item";
	}

	public NumInfoData numInfoData;
	public Action<CtrlNum_Item, NumInfoData> click_cb;

    protected override void OnCreate()
    {
        base.OnCreate();
		numInfoData = GetNumInfoData();
		txt_num.text = numInfoData.num.ToString();
		img_back.color = GameModel.Instance.list_color[numInfoData.color_id];;
		gameObject.GetComponent<EventListener>().onclick = ()=>{
			click_cb?.Invoke(this, numInfoData);
		};

    }

	public void StartAnim()
	{
		txt_num.transform.DOShakeRotation(2f, 20, 2, 40, false).SetLoops(-1);;
	}

	NumInfoData GetNumInfoData()
	{
		return new NumInfoData(){
			num = UnityEngine.Random.Range(1, 7), 
			color_id = UnityEngine.Random.Range(0, GameModel.Instance.list_color.Count)
		};
	}
}
