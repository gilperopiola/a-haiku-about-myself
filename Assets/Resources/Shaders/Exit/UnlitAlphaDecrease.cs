using UnityEngine;
using System.Collections;

public class UnlitAlphaDecrease : MonoBehaviour {

	float angle = 0;
    float speed = 0.027f;

    public static bool winEffect = false;

    static float colorTimes = 50;

    public static float r = 255;
    public static float g = 255;
    public static float b = 255;

    static float winR = 170;
    static float winG = 50;
    static float winB = 160;

    static float diffR = (255 - winR) / colorTimes;
    static float diffG = (255 - winG) / colorTimes;
    static float diffB = (255 - winB) / colorTimes;

	void Update () {
        if (winEffect) {
            angle += speed * 2;

            float value = 5.6f + Mathf.Sin(angle) / 2;

            if (value <= 5.15f) {
                value = 5.15f;
                angle -= speed * 6;
            }

            Color color = new Color((r - diffR) / 255, (g - diffG) / 255, (b - diffB) / 255);
            r -= diffR;
            g -= diffG;
            b -= diffB;

            gameObject.GetComponentInChildren<Renderer>().material.SetFloat("_AlphaY", value);
            gameObject.GetComponentInChildren<Renderer>().material.SetColor("_Color", color);
        } else {
            angle += speed; 
            gameObject.GetComponentInChildren<Renderer>().material.SetFloat("_AlphaY", 5.6f + Mathf.Sin(angle) / 2);
            gameObject.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.white);
        }
	}

    public static void setWin(bool boolean) {
        r = 255;
        g = 255;
        b = 255;

        winEffect = boolean;
    }
}
