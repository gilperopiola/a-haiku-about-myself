using UnityEngine;
using System.Collections;

public class DecorationFloatEffect : MonoBehaviour {

	float angle;

	float initScale;

	float initPosX;
	float initPosY;
	float initPosZ;

	float stretchX;
	float stretchY;
	float stretchZ;

    void Start() {
		initScale = transform.localScale.x;

		initPosX = transform.position.x;
		initPosY = transform.position.y;
		initPosZ = transform.position.z;

		stretchX = (float)Functions.Common.getRandomInt (0, 100) / (float)25000;
		stretchY = (float)Functions.Common.getRandomInt (0, 100) / (float)25000;
		stretchZ = (float)Functions.Common.getRandomInt (0, 100) / (float)25000;

		angle = Functions.Common.getRandomInt (0, 359);
	}

    void Update() {
		angle += 0.012f;
	
		float cos = (Mathf.Cos (angle) + 1) / 2;
		float sin = (Mathf.Sin (angle) + 1) / 2;

		float scale = (initScale + cos / 100);
		float rotation = cos;
		float translation = cos;

		transform.Rotate (new Vector3 (rotation, rotation, rotation));
		transform.localScale = new Vector3 (scale + stretchX * sin, scale + stretchY * sin, scale + stretchZ * sin);
		transform.position = new Vector3 (initPosX, initPosY + translation, initPosZ);
	}

}
