using UnityEngine;

public class BlocksManager : MonoBehaviour {

    static Transform parent = null;

	public static void createBlock(int x, int y, int type){
        parent = GameObject.Find("Blocks").transform;

        Data.Map.data [x, y] = type;

		Vector3 vectorPos = new Vector3 (14f - y, -6.5f, 14f - x);
        Vector3 upperVectorPos = new Vector3 (14f - y, -5.5f, 14f - x);
        Quaternion quatRot = new Quaternion (0, 0, 0, 0);

		switch(type) {
            case (int)Blocks.NORMAL:
                Data.Map.objects[x, y] = (GameObject)Instantiate(Resources.Load("Blocks/Normal/Normal"), vectorPos, quatRot);
                break;

            case (int)Blocks.ICE: 
                Data.Map.objects[x, y] = (GameObject) Instantiate (Resources.Load ("Blocks/Ice/Ice"), vectorPos, quatRot);
			    break;

            case (int)Blocks.CUBE:
                Data.Map.objects[x, y] = (GameObject)Instantiate(Resources.Load("Blocks/Ice/Ice"), vectorPos, quatRot);
                Data.Map.upperObjects[x, y] = (GameObject)Instantiate(Resources.Load("Blocks/IceCube/IceCube"), upperVectorPos, quatRot);

                Data.Map.data[x, y] = (int)Blocks.ICE;
                Data.Map.upperData[x, y] = (int)Blocks.CUBE;

                Data.Map.upperObjects[x, y].GetComponent<IceCubeScript>().posXY = new Vector2(x, y);

                break;

            case (int)Blocks.FALLING:
				Data.Map.objects [x, y] = (GameObject)Instantiate (Resources.Load ("Blocks/Falling/Falling"), vectorPos, quatRot);
				break;

            case (int)Blocks.DECORATION: 
                switch(Random.Range(1, 4)) {
                    case 1:
                        Data.Map.objects[x, y] = (GameObject)Instantiate(Resources.Load("Decorations/Decoration1"), vectorPos, quatRot);
                        break;
                    case 2:
                        Data.Map.objects[x, y] = (GameObject)Instantiate(Resources.Load("Decorations/Decoration4"), vectorPos, quatRot);
                        break;
                    case 3:
                        Data.Map.objects[x, y] = (GameObject)Instantiate(Resources.Load("Decorations/Decoration4"), vectorPos, quatRot);
                        break;
                }
                Data.Map.objects[x, y].transform.SetParent(GameObject.Find("Decorations").transform);
                return;

            case (int)Blocks.EXIT: 
                Data.Map.objects[x, y] = (GameObject)Instantiate(Resources.Load("Blocks/Exit/Exit"), vectorPos, quatRot);
			    GameObject exitBack = (GameObject) Instantiate(Resources.Load("Blocks/Exit2/Exit2Back"), vectorPos + new Vector3(0, 1, 0), Quaternion.Euler(0, -90, 0));
                GameObject exitFront = (GameObject)Instantiate(Resources.Load("Blocks/Exit2/Exit2Front"), vectorPos + new Vector3(0, 1, 0), Quaternion.Euler(0, -90, 0));

                exitBack.transform.SetParent(GameObject.Find("Decorations").transform);
                exitFront.transform.SetParent(GameObject.Find("Decorations").transform);

                Data.Level.exitPos = new Vector2(x, y);
                break;

            case (int)Blocks.BEGINNING: 
                Data.Map.objects[x, y] = (GameObject)Instantiate(Resources.Load("Blocks/Ice/Ice"), vectorPos, quatRot);
                Data.Level.beginPos = new Vector2(x, y);
                PlayerManager.setTilesetPos(x, y);
                break;
            default:
                return;
        }

        Data.Map.objects[x, y].transform.SetParent(parent);

        Data.Map.objects[x, y].GetComponent<BlockScript>().posX = x;
        Data.Map.objects[x, y].GetComponent<BlockScript>().posY = y;
    }
}
