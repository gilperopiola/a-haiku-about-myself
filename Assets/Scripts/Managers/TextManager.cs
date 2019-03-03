using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextManager : MonoBehaviour {

    /* Texts */
    static public GameObject text1;
    static public GameObject text2;
    static public GameObject text3;
    static public GameObject textHaiku;

    /* Variables */
    static public float timer = 0;

    static float time1 = 0.5f;
    static float time2 = 2.2f;
    static float time3 = 3.9f;

    static public bool shown1 = false;
    static public bool shown2 = false;
    static public bool shown3 = false;

    static public int haikusPlayed = 0;

    void Awake() {
        text1 = GameObject.Find("Text1");
        text2 = GameObject.Find("Text2");
        text3 = GameObject.Find("Text3");
        textHaiku = GameObject.Find("TextHaiku");
    }

    void Update() {
        if (text1 == null) {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= time1 && !shown1) {
            haikusPlayed++;
            SoundManager.playHaiku(haikusPlayed);
            text1.GetComponent<TextScript>().show();
            shown1 = true;
        } else if (timer >= time2 && !shown2){
            haikusPlayed++;
            SoundManager.playHaiku(haikusPlayed);
            text2.GetComponent<TextScript>().show();
            shown2 = true;
        } else if (timer >= time3 && !shown3) {
            haikusPlayed++;
            SoundManager.playHaiku(haikusPlayed);
            text3.GetComponent<TextScript>().show();
            shown3 = true;
        }
    }

    public static void showTitle() {
        textHaiku.GetComponent<TextScript>().show();
    }

    public static void setNewTexts() {
        timer = 0;

        shown1 = false;
        shown2 = false;
        shown3 = false;

        switch (Data.Session.level) {
            case 1:
                text1.GetComponent<Text>().text = "Everything pitch black";
                text2.GetComponent<Text>().text = "But some of my memories";
                text3.GetComponent<Text>().text = "Float still in my mind";
                break;

            case 2:
                text1.GetComponent<Text>().text = "I'm now taken back";
                text2.GetComponent<Text>().text = "To when I had a family";
                text3.GetComponent<Text>().text = "To when I was loved";
                break;

            case 3:
                text1.GetComponent<Text>().text = "The laughter, the food";
                text2.GetComponent<Text>().text = "Children, warmth, nothing much else";
                text3.GetComponent<Text>().text = "But I was happy";
                break;

            case 4:
                text1.GetComponent<Text>().text = "Then it came. Cancer";
                text2.GetComponent<Text>().text = "Possessing my whole body";
                text3.GetComponent<Text>().text = "Possessing my soul";
                break;

            case 5:
                text1.GetComponent<Text>().text = "Tired, sad, restless";
                text2.GetComponent<Text>().text = "Happy family no more";
                text3.GetComponent<Text>().text = "Tears replaced laughter";
                break;

            case 6:
                text1.GetComponent<Text>().text = "That cold, rainy day";
                text2.GetComponent<Text>().text = "My family loved me no more";
                text3.GetComponent<Text>().text = "And it broke my heart";
                break;

            case 7:
                text1.GetComponent<Text>().text = "That cold, rainy day";
                text2.GetComponent<Text>().text = "Without a single goodbye";
                text3.GetComponent<Text>().text = "They sent me away";
                break;

            case 8:
                text1.GetComponent<Text>().text = "Raindrops everywhere";
                text2.GetComponent<Text>().text = "Like tears from the sky above";
                text3.GetComponent<Text>().text = "Caressing my paws";
                break;

            case 9:
                text1.GetComponent<Text>().text = "Alone in this world";
                text2.GetComponent<Text>().text = "Raindrops, merging with my tears";
                text3.GetComponent<Text>().text = "My heart stops working";
                break;

            case 10:
                text1.GetComponent<Text>().text = "And as I lay there";
                text2.GetComponent<Text>().text = "Shivering, cold, all alone";
                text3.GetComponent<Text>().text = "I draw my last breath";
                break;
        }
    }
}
