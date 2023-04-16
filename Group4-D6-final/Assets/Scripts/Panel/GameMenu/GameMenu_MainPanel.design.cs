using UnityEngine.UI;

public partial class GameMenu_MainPanel: BasePanel {
	Button btn_startGame;
	Text txt_btnname;

	protected override void InitUIs() {
		btn_startGame = Utils.GetComponent<Button>(gameObject, "btn_startGame");
		txt_btnname = Utils.GetComponent<Text>(gameObject, "btn_startGame/txt_btnname");
	}
}
