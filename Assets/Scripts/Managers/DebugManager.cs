using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour {

    static bool enabled = true;
    static Text debugText;

	void Awake() {
        if (GameObject.Find("DebugText") == null) {
            return;
        }

        debugText = GameObject.Find("DebugText").GetComponent<Text>();

        if (!enabled) {
            GameObject.Find("DebugText").GetComponent<CanvasRenderer>().SetAlpha(0);
        }
	}

    public static void showMessage(string s) {
        debugText.text = s;
    }
}
