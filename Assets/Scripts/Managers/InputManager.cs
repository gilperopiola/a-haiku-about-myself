using UnityEngine;

public class InputManager : MonoBehaviour {

    static CanvasRenderer foregroundRenderer;

    public static bool hasPressedFirstDirection = false; //It's to detect which control scheme the player uses 

    void Awake() {
        if (GameObject.Find("Foreground") == null) {
            return;
        }

        foregroundRenderer = GameObject.Find("Foreground").GetComponent<CanvasRenderer>();
    }

    void Update () {
        if (foregroundRenderer != null) {
            if (foregroundRenderer.GetAlpha() >= 0.3f) {
                return;
            }
        }

	    if (Input.GetKeyDown(Data.Keys.up) || Input.GetKeyDown(Data.Keys.upSecondary)) {

            if (Data.Session.level == 0 && !hasPressedFirstDirection) {
                hasPressedFirstDirection = true;
                Data.Session.isometricControls = true;
            }

            if (Data.Session.isometricControls) {
                PlayerManager.moveUp();
            } else {
                PlayerManager.moveLeft();
            }
        }
        if (Input.GetKeyDown(Data.Keys.down) || Input.GetKeyDown(Data.Keys.downSecondary)) {
            if (Data.Session.isometricControls) {
                PlayerManager.moveDown();
            } else {
                PlayerManager.moveRight();
            }
        }
        if (Input.GetKeyDown(Data.Keys.left) || Input.GetKeyDown(Data.Keys.leftSecondary)) {
            if (Data.Session.isometricControls) {
                PlayerManager.moveLeft();
            } else {
                PlayerManager.moveDown();
            }
        }
        if (Input.GetKeyDown(Data.Keys.right) || Input.GetKeyDown(Data.Keys.rightSecondary)) {

            if (Data.Session.level == 0 && !hasPressedFirstDirection) {
                hasPressedFirstDirection = true;
                Data.Session.isometricControls = false;
            }

            if (Data.Session.isometricControls) {
                PlayerManager.moveRight();
            } else {
                PlayerManager.moveUp();
            }
        }
		if (Input.GetKeyDown (Data.Keys.reset) || Input.GetKeyDown (Data.Keys.resetSecondary)) {
			FlowManager.resetLevel(Data.Session.level);
		}

        if (Input.GetKey(Data.Keys.zoomIn)) {
            //Camera.main.orthographicSize = Camera.main.orthographicSize - 0.05f;
            //DebugManager.showMessage(Camera.main.orthographicSize.ToString());
        }

        if (Input.GetKey(Data.Keys.zoomOut)) {
            //Camera.main.orthographicSize = Camera.main.orthographicSize + 0.05f;
            //DebugManager.showMessage(Camera.main.orthographicSize.ToString());
        }

        if (Input.GetKeyDown(Data.Keys.changeControls)) {
            Functions.Common.invertBool(ref Data.Session.isometricControls);
        }

        if (Input.GetKeyDown(Data.Keys.restartGame)) {
            FlowManager.resetLevel(-1);
        }

        if (Input.GetKeyDown(Data.Keys.goToLevel1)) {
            FlowManager.resetLevel(0);
        }

        if (Input.GetKeyDown(Data.Keys.goToLevel2)) {
            FlowManager.resetLevel(1);
        }

        if (Input.GetKeyDown(Data.Keys.goToLevel3)) {
            FlowManager.resetLevel(2);
        }

        if (Input.GetKeyDown(Data.Keys.goToLevel4)) {
            FlowManager.resetLevel(3);
        }

        if (Input.GetKeyDown(Data.Keys.goToLevel5)) {
            FlowManager.resetLevel(4);
        }

        if (Input.GetKeyDown(Data.Keys.goToLevel6)) {
            FlowManager.resetLevel(5);
        }

        if (Input.GetKeyDown(Data.Keys.goToLevel7)) {
            FlowManager.resetLevel(6);
        }

        if (Input.GetKeyDown(Data.Keys.goToLevel8)) {
            FlowManager.resetLevel(7);
        }
    }
}
