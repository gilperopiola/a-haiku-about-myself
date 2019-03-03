using UnityEngine;
using System.Collections;

public class SplashScreenScript : MonoBehaviour {

    CanvasRenderer haikuDefinitionRenderer;
    float haikuDefinitionAngle = 0;
    bool haikuDefinitionDisplayed = false;

    bool started = true;

	void Start () {
        Cursor.visible = false;
 
        haikuDefinitionRenderer = GameObject.Find("HaikuDefinition").GetComponent<CanvasRenderer>();
        haikuDefinitionRenderer.SetAlpha(0);
	}

    void Update() {

        if (!started) {
            if (Input.anyKeyDown) {
                started = true;
            }

            return;
        }

        haikuDefinitionAngle += 0.90f * Time.deltaTime;

        haikuDefinitionRenderer.SetAlpha((-Mathf.Cos(haikuDefinitionAngle) + 1) * 2 - 0.5f);

        if (haikuDefinitionRenderer.GetAlpha() >= 0.8f) {
            haikuDefinitionDisplayed = true;
        }

        if (haikuDefinitionDisplayed && haikuDefinitionRenderer.GetAlpha() <= 0) {
            Application.LoadLevel(1);
        }
	}
}
