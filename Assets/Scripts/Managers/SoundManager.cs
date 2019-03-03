using UnityEngine;
using System.Collections;

using System;

public class SoundManager : MonoBehaviour {

    public static void playHaiku(int n) {
        if (Data.Session.voice) {
            string filename = (n <= 9) ? "0" + n : n.ToString();
            GameObject.Find(filename).GetComponent<AudioSource>().PlayDelayed(Data.VoiceDelay.delay[Convert.ToInt16(filename)]);
        }
    }

    public static void playReachedExit() {
        if (Data.Session.sound) {
            GameObject.Find("Teleport").GetComponent<AudioSource>().Play();
        }
    }

    public static void playReachedBlock() {
        if (Data.Session.sound) {
            GameObject.Find("Thump").GetComponent<AudioSource>().Play();
        }
    }

    public static void playShowText() {
        if (Data.Session.sound) {
            GameObject.Find("Scribble").GetComponent<AudioSource>().PlayDelayed(2);
        }
    }

    void Awake() {
        setDelays();
    }

    void setDelays() {
        Data.VoiceDelay.delay[1] = 0.0f;
        Data.VoiceDelay.delay[2] = 1.5f;
        Data.VoiceDelay.delay[3] = 2.5f;

        Data.VoiceDelay.delay[4] = 0.2f;
        Data.VoiceDelay.delay[5] = 1.0f;
        Data.VoiceDelay.delay[6] = 2.6f;

        Data.VoiceDelay.delay[7] = 0.0f;
        Data.VoiceDelay.delay[8] = 1.2f;
        Data.VoiceDelay.delay[9] = 2.7f;

        Data.VoiceDelay.delay[10] = 0.0f;
        Data.VoiceDelay.delay[11] = 1.2f;
        Data.VoiceDelay.delay[12] = 2.2f;

        Data.VoiceDelay.delay[13] = 0.0f;
        Data.VoiceDelay.delay[14] = 1.0f;
        Data.VoiceDelay.delay[15] = 2.5f;

        Data.VoiceDelay.delay[16] = 0.0f;
        Data.VoiceDelay.delay[17] = 0.5f;
        Data.VoiceDelay.delay[18] = 2.3f;

        Data.VoiceDelay.delay[19] = 0.0f;
        Data.VoiceDelay.delay[20] = 1.1f;
        Data.VoiceDelay.delay[21] = 2.1f;

        Data.VoiceDelay.delay[22] = 0.0f;
        Data.VoiceDelay.delay[23] = 1.5f;
        Data.VoiceDelay.delay[24] = 2.5f;
    }
}
