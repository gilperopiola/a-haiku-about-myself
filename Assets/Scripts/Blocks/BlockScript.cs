using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	bool falling = false;

    public int posX = -1;
    public int posY = -1;

    void Update(){
		fall ();
	}

	void fall(){
		if (falling) {
			transform.position -= new Vector3 (0, 0.01f, 0);

            if (transform.position.y <= -6.6f) {
                Data.Map.data[posX, posY] = (int)Blocks.VOID;
                //Data.Map.objects[posX, posY] = null;
            }

            if (transform.position.y <= -7.4f) {
                falling = false;
                Destroy(this.gameObject);
            }
		}
	}

	public void setFalling(){
		falling = true;
	}
}
