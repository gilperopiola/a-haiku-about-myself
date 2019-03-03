using UnityEngine;

public class PlayerManager : MonoBehaviour {

    static Vector2 posXY = new Vector2(0, 0);
    static int lookingAt = (int)Directions.UP;

    public static void updatePos() {
        switch (GameObject.Find("Ember").GetComponent<MovementScript>().getDirection()) {
            case (int)Directions.UP: posXY.y--; break;
            case (int)Directions.DOWN: posXY.y++; break;
            case (int)Directions.LEFT: posXY.x--; break;
            case (int)Directions.RIGHT: posXY.x++; break;
        }
    }

    public static void resetRotation() {
        GameObject.Find("Ember").transform.rotation = Quaternion.Euler(0, 0, 0);
        lookingAt = (int)Directions.UP;
    }

    public static void setTilesetPos(int x, int y) {
        GameObject block = Data.Map.objects[x, y];
        Vector3 pos = new Vector3(block.transform.position.x, block.transform.position.y + 1, block.transform.position.z);

        GameObject.Find("Ember").transform.position = pos;

        posXY = new Vector2(x, y);
    }

    public static void moveUp() {

        if (GameObject.Find("Ember").GetComponent<MovementScript>().getDirection() != (int)Directions.NONE) return;

        if (needsToStop((int)Directions.UP)) {
            rotatePlayer((int)Directions.UP);
            return;
        }

        GameObject.Find("Ember").GetComponent<MovementScript>().setDirection((int)Directions.UP);
        rotatePlayer((int)Directions.UP);
    }
    public static void moveDown() {

        if (GameObject.Find("Ember").GetComponent<MovementScript>().getDirection() != (int)Directions.NONE) return;

        if (needsToStop((int)Directions.DOWN)) {
            rotatePlayer((int)Directions.DOWN);
            return;
        }

        GameObject.Find("Ember").GetComponent<MovementScript>().setDirection((int)Directions.DOWN);
        rotatePlayer((int)Directions.DOWN);
    }
    public static void moveLeft() {

        if (GameObject.Find("Ember").GetComponent<MovementScript>().getDirection() != (int)Directions.NONE) return;

        if (needsToStop((int)Directions.LEFT)) {
            rotatePlayer((int)Directions.LEFT);
            return;
        }

        GameObject.Find("Ember").GetComponent<MovementScript>().setDirection((int)Directions.LEFT);
        rotatePlayer((int)Directions.LEFT);
    }
    public static void moveRight() {

        if (GameObject.Find("Ember").GetComponent<MovementScript>().getDirection() != (int)Directions.NONE) return;

        if (needsToStop((int)Directions.RIGHT)) {
            rotatePlayer((int)Directions.RIGHT);
            return;
        }

        GameObject.Find("Ember").GetComponent<MovementScript>().setDirection((int)Directions.RIGHT);
        rotatePlayer((int)Directions.RIGHT);
    }

    public static void rotatePlayer(int dir) {
        if (dir == lookingAt) return;

        switch (lookingAt) {
            case (int)Directions.UP:
                if (dir == (int)Directions.RIGHT) {
                    GameObject.Find("Ember").transform.Rotate(0, 90, 0);
                }
                if (dir == (int)Directions.DOWN) {
                    GameObject.Find("Ember").transform.Rotate(0, 180, 0);
                }
                if (dir == (int)Directions.LEFT) {
                    GameObject.Find("Ember").transform.Rotate(0, 270, 0);
                }
                break;
            case (int)Directions.DOWN:
                if (dir == (int)Directions.LEFT) {
                    GameObject.Find("Ember").transform.Rotate(0, 90, 0);
                }
                if (dir == (int)Directions.UP) {
                    GameObject.Find("Ember").transform.Rotate(0, 180, 0);
                }
                if (dir == (int)Directions.RIGHT) {
                    GameObject.Find("Ember").transform.Rotate(0, 270, 0);
                }
                break;
            case (int)Directions.LEFT:
                if (dir == (int)Directions.UP) {
                    GameObject.Find("Ember").transform.Rotate(0, 90, 0);
                }
                if (dir == (int)Directions.RIGHT) {
                    GameObject.Find("Ember").transform.Rotate(0, 180, 0);
                }
                if (dir == (int)Directions.DOWN) {
                    GameObject.Find("Ember").transform.Rotate(0, 270, 0);
                }
                break;
            case (int)Directions.RIGHT:
                if (dir == (int)Directions.DOWN) {
                    GameObject.Find("Ember").transform.Rotate(0, 90, 0);
                }
                if (dir == (int)Directions.LEFT) {
                    GameObject.Find("Ember").transform.Rotate(0, 180, 0);
                }
                if (dir == (int)Directions.UP) {
                    GameObject.Find("Ember").transform.Rotate(0, 270, 0);
                }
                break;
        }

        lookingAt = dir;
    }

    public static void calibratePosition() {
        Vector3 pos = GameObject.Find("Ember").transform.position;
        GameObject.Find("Ember").transform.position = new Vector3(Mathf.Round(pos.x), pos.y, Mathf.Round(pos.z));
    }

    public static bool reachedBlockCenter() {
        Vector3 pos = GameObject.Find("Ember").transform.position;

        return (Functions.Common.floatIsInteger(pos.x) && Functions.Common.floatIsInteger(pos.z));
    }

    public static void reactToBlock() {
        switch(Data.Map.data[(int)posXY.x, (int)posXY.y]) {
            case (int)Blocks.NORMAL:
                stopMoving();
                break;

            case (int)Blocks.ICE: 
                break;

			case (int)Blocks.FALLING:
				Data.Map.objects [(int)posXY.x, (int)posXY.y].GetComponent<BlockScript> ().setFalling();
				break;

            case (int)Blocks.EXIT: 
                stopMoving();
                UnlitAlphaDecrease.setWin(true);
                FlowManager.advanceLevel();
                break;

            case (int)Blocks.BEGINNING: 
                break;
        }
    }

    public static bool needsToStop(int dir) {
        int addX = 0;
        int addY = 0;

        switch (dir) {
            case (int)Directions.UP: addY = -1; break;
            case (int)Directions.DOWN: addY = 1; break;
            case (int)Directions.LEFT: addX = -1; break;
            case (int)Directions.RIGHT: addX = 1; break;
        }

        switch(Data.Map.data[(int)posXY.x + addX, (int)posXY.y + addY]) {
            case (int)Blocks.VOID:
                return true;
            case (int)Blocks.DECORATION:
                return true;
        }

        switch (Data.Map.upperData[(int)posXY.x + addX, (int)posXY.y + addY]) {
            case (int)Blocks.CUBE:
                Data.Map.upperObjects[(int)posXY.x + addX, (int)posXY.y + addY].GetComponent<IceCubeScript>().startMoving(dir);
                return true;
        }

        return false;
    }

    public static void stopMoving() {
        GameObject.Find("Ember").GetComponent<MovementScript>().setDirection((int)Directions.NONE);
        SoundManager.playReachedBlock();
    }

	public static void setLevelInitialPos(int level){
		switch (level) {
			case 1:
				PlayerManager.rotatePlayer (Data.InitialDirection.level1);
				break;
			case 2:
				PlayerManager.rotatePlayer (Data.InitialDirection.level2);
				break;
			case 3:
				PlayerManager.rotatePlayer (Data.InitialDirection.level3);
				break;
			case 4:
				PlayerManager.rotatePlayer (Data.InitialDirection.level4);
				break;
			case 5:
				PlayerManager.rotatePlayer (Data.InitialDirection.level5);
				break;
			case 6:
				PlayerManager.rotatePlayer (Data.InitialDirection.level6);
				break;
			case 7:
				PlayerManager.rotatePlayer (Data.InitialDirection.level7);
				break;
			case 8:
				PlayerManager.rotatePlayer (Data.InitialDirection.level8);
				break;
            case 9:
                PlayerManager.rotatePlayer(Data.InitialDirection.level9);
                break;
            case 10:
                PlayerManager.rotatePlayer(Data.InitialDirection.level10);
                break;
            case 11:
                PlayerManager.rotatePlayer(Data.InitialDirection.level11);
                break;
        }
	}
}
