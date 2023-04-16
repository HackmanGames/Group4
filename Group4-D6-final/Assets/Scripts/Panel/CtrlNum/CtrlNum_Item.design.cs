using UnityEngine.UI;

public partial class CtrlNum_Item: UIItem {
	Image img_back;
	Text txt_num;

	protected override void InitUIs() {
		img_back = Utils.GetComponent<Image>(gameObject, "img_back");
		txt_num = Utils.GetComponent<Text>(gameObject, "txt_num");
	}
}
