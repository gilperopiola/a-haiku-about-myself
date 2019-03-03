using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowManager : MonoBehaviour {

    static GameObject foreground;
    static CanvasRenderer foregroundRenderer;
    static float foregroundHideSpeed = 0.004f;
    static float foregroundShowSpeed = 0.008f;

    static bool hiding = false;
    static bool showing = false;

    static bool textsSet = false;
    static bool resetting = false;

    static float foregroundAlpha;

	void Awake() {
        hiding = false;

        foreground = GameObject.Find("Foreground");
        foregroundRenderer = (foreground != null) ? foreground.GetComponent<CanvasRenderer>() : null;
        foregroundAlpha = foregroundRenderer.GetAlpha();

        LevelManager.init();
    }

	void Update () {
        UpdateTime();

	    if (hiding) {
            if (!resetting){
                foregroundRenderer.SetAlpha(foregroundAlpha - foregroundHideSpeed);
                foregroundAlpha = foregroundAlpha - foregroundHideSpeed;
            } else {
                foregroundRenderer.SetAlpha(foregroundAlpha - foregroundHideSpeed * 3);
                foregroundAlpha = foregroundAlpha - foregroundHideSpeed * 3;
            }

            if (foregroundAlpha <= 0) {
                hiding = false;
                resetting = false;
            }
        }

        if (showing) {
            if (!resetting){
                foregroundRenderer.SetAlpha(foregroundAlpha + foregroundShowSpeed);
                foregroundAlpha = foregroundAlpha + foregroundShowSpeed;
            } else {
                foregroundRenderer.SetAlpha(foregroundAlpha + foregroundShowSpeed * 3);
                foregroundAlpha = foregroundAlpha + foregroundShowSpeed * 3;
            }

            if (!resetting) {
                if (foregroundAlpha >= 0.3f) {
                    if (Data.Session.level > 0 && !textsSet && Data.Session.level < 11) {
                        TextManager.setNewTexts();
                        textsSet = true;
                    } else if (Data.Session.level > 0 && !textsSet) {
                        TextManager.showTitle();
                    }
                }

                if (foregroundAlpha >= 1 && Data.Session.level < 11) {
                    showing = false;

                    if (Data.Session.level > 0) {
                        LevelManager.unloadLevel();
                        LevelManager.loadLevel();
                    }
                }
            } else {
                if (foregroundAlpha >= 1) {
                    bool finished = LevelManager.unloadLevel();

                    if (Data.Session.level == -1) {
                        bool wait = FlowManager.resetSession();
                        Application.LoadLevel(0);
                    }

                    LevelManager.loadLevel();

                    Data.Level.timesReset++;
                    Data.Level.timeFromLastReset = 0;

                    PlayerManager.setTilesetPos((int)Data.Level.beginPos.x, (int)Data.Level.beginPos.y);
                    PlayerManager.setLevelInitialPos(Data.Session.level + 1);
                    PlayerManager.stopMoving();

                    showing = false;
                    hideForeground();
                }
            }
        }
    }

    public static void hideForeground() {
        UnlitAlphaDecrease.setWin(false);
        hiding = true;
    }

    public static void showForeground() {
        showing = true;
    }

    public static void advanceLevel() {
        AnalyticsManager.SendEvent(AnalyticsEvents.FINISH_LEVEL, new Dictionary<string, object> {
            {"level", Data.Session.level},
            {"times reset", Data.Level.timesReset},
            {"time elapsed", Data.Level.totalTime},
            {"time elapsed without reset", Data.Level.timeFromLastReset}
        });

        Data.Session.level++;
        Data.Level.timesReset = 0;
        Data.Level.timeFromLastReset = 0;
        Data.Level.totalTime = 0;

        showForeground();
        SoundManager.playReachedExit();

        textsSet = false;
    }

	public static void resetLevel(int level = -1){
        Data.Session.level = level;
        TextManager.haikusPlayed = (level + 1) * 3;

        resetting = true;
        showForeground();
	}

    public static bool resetSession() {
        Data.Session.level = 0;
        Data.Session.isometricControls = true;

        TextManager.shown1 = false;
        TextManager.shown2 = false;
        TextManager.shown3 = false;
        TextManager.haikusPlayed = 0;
        TextManager.timer = 0;

        resetting = false;
        showing = false;
        hiding = false;
        foregroundHideSpeed = 0.004f;
        foregroundShowSpeed = 0.008f;
        textsSet = false;

        InputManager.hasPressedFirstDirection = false;

        return true;

    }

    void UpdateTime() {
        Data.Level.timeFromLastReset += Time.deltaTime;
        Data.Level.totalTime += Time.deltaTime;
    }
}
