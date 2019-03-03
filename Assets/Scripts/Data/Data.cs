using UnityEngine;

public class Data {
    public class Session {
        public static int level = 0;
        public static bool music = true;
        public static bool sound = true;
        public static bool voice = false;

        public static bool isometricControls = true;
    }

    public class Level {
        public static Vector2 beginPos = new Vector2(-1, -1);
        public static Vector2 exitPos = new Vector2(-1, -1);

        public static int timesReset = 0;
        public static float timeFromLastReset = 0.0f;
        public static float totalTime = 0.0f;
    }

    public class Map {
        public static int width = 15;
        public static int height = 15;

        public static int[,] data = new int[width, height];
        public static GameObject[,] objects = new GameObject[width, height];

        public static int[,] upperData = new int[width, height];
        public static GameObject[,] upperObjects = new GameObject[width, height];
    }

    public class Keys {
        public static KeyCode up = KeyCode.W;
        public static KeyCode upSecondary = KeyCode.UpArrow;

        public static KeyCode down = KeyCode.S;
        public static KeyCode downSecondary = KeyCode.DownArrow;

        public static KeyCode left = KeyCode.A;
        public static KeyCode leftSecondary = KeyCode.LeftArrow;

        public static KeyCode right = KeyCode.D;
        public static KeyCode rightSecondary = KeyCode.RightArrow;

        public static KeyCode reset = KeyCode.R;
        public static KeyCode resetSecondary = KeyCode.Escape;

        public static KeyCode zoomIn = KeyCode.KeypadPlus;
        public static KeyCode zoomOut = KeyCode.KeypadMinus;

        public static KeyCode changeControls = KeyCode.Tab;

        public static KeyCode restartGame = KeyCode.Escape;

        public static KeyCode goToLevel1 = KeyCode.Alpha1;
        public static KeyCode goToLevel2 = KeyCode.Alpha2;
        public static KeyCode goToLevel3 = KeyCode.Alpha3;
        public static KeyCode goToLevel4 = KeyCode.Alpha4;
        public static KeyCode goToLevel5 = KeyCode.Alpha5;
        public static KeyCode goToLevel6 = KeyCode.Alpha6;
        public static KeyCode goToLevel7 = KeyCode.Alpha7;
        public static KeyCode goToLevel8 = KeyCode.Alpha8;
    }

    public class InitialDirection {
        public static int level1 = (int)Directions.UP;
        public static int level2 = (int)Directions.LEFT;
        public static int level3 = (int)Directions.DOWN;
        public static int level4 = (int)Directions.RIGHT;
        public static int level5 = (int)Directions.DOWN;
        public static int level6 = (int)Directions.UP;
        public static int level7 = (int)Directions.RIGHT;
        public static int level8 = (int)Directions.RIGHT;
        public static int level9 = (int)Directions.DOWN;
        public static int level10 = (int)Directions.UP;
        public static int level11 = (int)Directions.UP;
    }

    public class VoiceDelay {
        public static float[] delay = new float[99];
    }
}
