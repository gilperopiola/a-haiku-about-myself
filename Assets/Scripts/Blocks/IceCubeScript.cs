using UnityEngine;
using System.Collections;

public class IceCubeScript : MonoBehaviour {

    int direction = (int)Directions.NONE;
    float speed = 0.1f;

    public Vector2 posXY = new Vector2(0, 0);

    void Start() {
        Functions.Materials.Alpha.Models.setAlpha(gameObject, 0.8f);
    }

	void Update () {
        move();
	}

    void move() {
        if (direction == (int)Directions.NONE) return;

        float speedX = 0;
        float speedZ = 0;

        switch (direction) {
            case (int)Directions.UP: speedX = speed; speedZ = 0; break;
            case (int)Directions.DOWN: speedX = -speed; speedZ = 0; break;
            case (int)Directions.LEFT: speedX = 0; speedZ = speed; break;
            case (int)Directions.RIGHT: speedX = 0; speedZ = -speed; break;
        }

        transform.position += new Vector3(speedX, 0, speedZ);

        if (reachedBlockCenter()) {
            calibratePosition();
            updatePos();
            reactToBlock();

            if (needsToStop(direction)) {
                stopMoving();
            }
        }
    }

    bool reachedBlockCenter() {
        return (Functions.Common.floatIsInteger(transform.position.x) && Functions.Common.floatIsInteger(transform.position.z));
    }

    void calibratePosition() {
        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
    }

    void updatePos() {

        Data.Map.upperData[(int)posXY.x, (int)posXY.y] = (int)Blocks.VOID;
        Data.Map.upperObjects[(int)posXY.x, (int)posXY.y] = null;

        switch (direction) {
            case (int)Directions.UP: posXY.y--; break;
            case (int)Directions.DOWN: posXY.y++; break;
            case (int)Directions.LEFT: posXY.x--; break;
            case (int)Directions.RIGHT: posXY.x++; break;
        }

        Data.Map.upperData[(int)posXY.x, (int)posXY.y] = (int)Blocks.CUBE;
        Data.Map.upperObjects[(int)posXY.x, (int)posXY.y] = gameObject;
    }

    void reactToBlock() {
        switch (Data.Map.data[(int)posXY.x, (int)posXY.y]) {
            case (int)Blocks.NORMAL:
                stopMoving();
                break;

            case (int)Blocks.ICE:
                break;

            case (int)Blocks.FALLING:
                Data.Map.objects[(int)posXY.x, (int)posXY.y].GetComponent<BlockScript>().setFalling();
                break;

            case (int)Blocks.EXIT:
                stopMoving();
                break;

            case (int)Blocks.BEGINNING:
                break;
        }
    }

    bool needsToStop(int dir) {
        int addX = 0;
        int addY = 0;

        switch (dir) {
            case (int)Directions.UP: addY = -1; break;
            case (int)Directions.DOWN: addY = 1; break;
            case (int)Directions.LEFT: addX = -1; break;
            case (int)Directions.RIGHT: addX = 1; break;
        }

        switch (Data.Map.data[(int)posXY.x + addX, (int)posXY.y + addY]) {
            case (int)Blocks.VOID:
                return true;
            case (int)Blocks.DECORATION:
                return true;
        }

        if (Data.Map.upperObjects[(int)posXY.x + addX, (int)posXY.y + addY] == gameObject) { //Without this it throws an Stack Overflow
            return false;
        }

        switch (Data.Map.upperData[(int)posXY.x + addX, (int)posXY.y + addY]) {
            case (int)Blocks.CUBE:
                Data.Map.upperObjects[(int)posXY.x + addX, (int)posXY.y + addY].GetComponent<IceCubeScript>().startMoving(dir);
                return true;
        }

        return false;
    }

    void stopMoving() {
        direction = (int)Directions.NONE;
    }

    public void startMoving(int dir) {
        if (!needsToStop(dir)) {
            direction = dir;
        }
    }
}
