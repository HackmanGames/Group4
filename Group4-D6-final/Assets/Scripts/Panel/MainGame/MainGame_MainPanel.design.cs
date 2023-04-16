using UnityEngine;
using UnityEngine.UI;

public partial class MainGame_MainPanel: BasePanel {
	GameObject obj_nums;
	Text txt_desc;
	Transform trans_target;
	ImgButton imgbtn_menu;
	ImgButton imgbtn_restart;

	protected override void InitUIs() {
		obj_nums = Utils.GetGameObject(gameObject, "obj_nums");
		txt_desc = Utils.GetComponent<Text>(gameObject, "txt_desc");
		trans_target = Utils.GetComponent<Transform>(gameObject, "trans_target");
		imgbtn_menu = Utils.GetComponent<ImgButton>(gameObject, "obj/imgbtn_menu");
		imgbtn_restart = Utils.GetComponent<ImgButton>(gameObject, "obj/imgbtn_restart");
	}
}
