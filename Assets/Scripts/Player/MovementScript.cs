using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

    int direction = (int)Directions.NONE;
    float speed = 0.1f;

    float angle = 0;

    Vector3 initialScale;

    void Start() {
        initialScale = transform.localScale;
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

        animate();

        if (PlayerManager.reachedBlockCenter()) {
            PlayerManager.calibratePosition();
            PlayerManager.updatePos();
            PlayerManager.reactToBlock();

            if (PlayerManager.needsToStop(direction)) {
                PlayerManager.stopMoving();
            }
        }
    }

    void animate() {
        angle += 0.1f;

        float sin = Mathf.Sin(angle) / 950;

        transform.localScale = new Vector3(initialScale.x + sin, initialScale.y + sin, initialScale.z + sin);
    }

    public void setDirection(int dir) {
        direction = dir;
    }

    public int getDirection() {
        return direction;
    }
}
