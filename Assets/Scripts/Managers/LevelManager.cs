using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static void init() {
        Application.targetFrameRate = 60;

        loadLevel();
    }

    public static bool unloadLevel() {
        for (int i = 0; i < Data.Map.data.GetLength(1); i++) {
            for (int j = 0; j < Data.Map.data.GetLength(0); j++) {
                Data.Map.data[j, i] = 0;
                Destroy(Data.Map.objects[j, i]);

                Data.Map.upperData[j, i] = 0;
                Destroy(Data.Map.upperObjects[j, i]);
            }
        }
		Destroy(GameObject.Find("Exit2Back(Clone)"));
        Destroy(GameObject.Find("Exit2Front(Clone)"));

        return true;
    }

	public static void loadLevel(){
        string[] lines;

        lines = System.IO.File.ReadAllLines(@"Assets/Resources/Levels/level_" + (Data.Session.level + 1).ToString() + ".txt");

        int levelWidth = System.Convert.ToInt16(lines[0]);
		int nLines = 0;

		int[,] arrayTiles = new int[levelWidth, levelWidth];

		for (int i = 1; i < lines.Length; i++) {
			if ((i - 1) % levelWidth == 0 && i != 1) {
				nLines++;
			}

			arrayTiles[(i - 1) - levelWidth * nLines, nLines] = System.Convert.ToInt16(lines[i]);
		}

		for (int i = 0; i < arrayTiles.GetLength (1); i++) {
			for (int j = 0; j < arrayTiles.GetLength (0); j++) {
				BlocksManager.createBlock (j, i, arrayTiles [j, i]);
			}
		}

		PlayerManager.setLevelInitialPos (Data.Session.level + 1);
	}
}
