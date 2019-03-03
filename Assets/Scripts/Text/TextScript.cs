using UnityEngine;
using System.Collections;

public class TextScript : MonoBehaviour {
    /* Components */
    CanvasRenderer renderer;

    /* Variables */
    float alphaSpeed = 0.01f;
    float moveSpeed = 0.1f;
	float growSpeed = 0.0002f;

    bool showing = false;
    bool waiting = false;
    bool hiding = false;

    float timerWaiting = 0;
    float timeWaiting = 3.3f;

    bool ending = false;
    float timerEnd = 0;

    float alpha = 0;

	void Start () {
        renderer = gameObject.GetComponent<CanvasRenderer>();
        renderer.SetAlpha(0);
        alpha = 0;
	}

    public void show() {
        showing = true;
    }

    void Update () {
        if (ending) {
            timerEnd += Time.deltaTime;

            if (timerEnd >= 2.0f) {
                FlowManager.resetSession();
                Application.LoadLevel(0);
            }
        }

        if (showing) {
            renderer.SetAlpha(alpha + alphaSpeed);
            alpha = alpha + alphaSpeed;
            if (name != "TextHaiku") transform.position -= new Vector3(0, moveSpeed, 0);
			transform.localScale += new Vector3(growSpeed, 0, 0);

            if (alpha >= 1.0f) {
                showing = false;

                if (name == "TextHaiku") {
                    //ending = true;
                    transform.localScale -= new Vector3(growSpeed, 0, 0);
                }

                waiting = true;
            }

        } else if (waiting) {
            timerWaiting += Time.deltaTime;

            if (timerWaiting >= timeWaiting) {
                waiting = false;
                hiding = true;

                FlowManager.hideForeground();
            }
        } else if (hiding) {
            renderer.SetAlpha(alpha - alphaSpeed);
            alpha = alpha - alphaSpeed;
            transform.position += new Vector3(0, moveSpeed, 0);
			transform.localScale -= new Vector3(growSpeed, 0, 0);

            if (alpha <= 0) {
                showing = false;
                waiting = false;
                hiding = false;

                timerWaiting = 0;
            }
        }
	}
}
