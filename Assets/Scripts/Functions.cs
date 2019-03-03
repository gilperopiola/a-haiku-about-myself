using UnityEngine;
using System;
using System.Collections;

namespace Functions{

	/******************************
	 ************Common************
	 ******************************/
	public static class Common {
		static System.Random rnd = new System.Random();
		
		static public void invertBool(ref bool var){
			if (var == true){
				var = false;
			} else {
				var = true;
			}
		}
		
		static public int getRandomInt(int minInclusive, int maxInclusive){
			return rnd.Next(minInclusive, maxInclusive + 1);
		}

		static public void setUnityInPixels(){
			Camera.main.orthographicSize = Screen.height / 2f;
		}

        static public bool floatIsInteger(float n) {
            return (Mathf.Abs(n) - Mathf.Abs(Mathf.Floor(n)) < 0.03f || Mathf.Abs(n) - Mathf.Abs(Mathf.Floor(n)) > 0.97f);
        }
    }

	/*******************************
	 *****Space Transformations*****
	 *******************************/
	public static class SpaceTransformations {

		static public void moveLeft(GameObject gameObject, float distance){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x - distance,
			                                            gameObject.transform.position.y,
			                                            gameObject.transform.position.z);
		}

		static public void moveRight(GameObject gameObject, float distance){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x + distance,
			                                            gameObject.transform.position.y,
			                                            gameObject.transform.position.z);
		}

		static public void moveUp(GameObject gameObject, float distance){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x,
			                                            gameObject.transform.position.y - distance,
			                                            gameObject.transform.position.z);
		}

		static public void moveDown(GameObject gameObject, float distance){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x,
			                                            gameObject.transform.position.y + distance,
			                                            gameObject.transform.position.z);
		}

		static public void moveNear(GameObject gameObject, float distance){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x,
			                                            gameObject.transform.position.y,
			                                            gameObject.transform.position.z - distance);
		}

		static public void moveFar(GameObject gameObject, float distance){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x,
			                                            gameObject.transform.position.y,
			                                            gameObject.transform.position.z + distance);
		}

		public static class Arrays {

			static public void moveLeft(GameObject[] array, float distance){
				foreach(GameObject gameObject in array){
					SpaceTransformations.moveLeft(gameObject, distance);
				}
			}
			
			static public void moveRight(GameObject[] array, float distance){
				foreach(GameObject gameObject in array){
					SpaceTransformations.moveRight(gameObject, distance);
				}
			}

			static public void moveDown(GameObject[] array, float distance){
				foreach(GameObject gameObject in array){
					SpaceTransformations.moveDown(gameObject, distance);
				}
			}

			static public void moveUp(GameObject[] array, float distance){
				foreach(GameObject gameObject in array){
					SpaceTransformations.moveUp(gameObject, distance);
				}
			}

			static public void moveNear(GameObject[] array, float distance){
				foreach(GameObject gameObject in array){
					SpaceTransformations.moveNear(gameObject, distance);
				}
			}

			static public void moveFar(GameObject[] array, float distance){
				foreach(GameObject gameObject in array){
					SpaceTransformations.moveFar(gameObject, distance);
				}
			}
		}
	}

	/*****************************
	 **********Materials**********
	 *****************************/
	public static class Materials {

		public static class Alpha {

			public static class Sprites {

				static public void setAlpha(GameObject gameObject, float alpha) {
					SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
					renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
				}

				static public void addAlpha(GameObject gameObject, float alpha){
					setAlpha(gameObject, getAlpha(gameObject) + alpha);
				}

				static public void substractAlpha(GameObject gameObject, float alpha){
					setAlpha(gameObject, getAlpha(gameObject) - alpha);
				}

				static public float getAlpha(GameObject gameObject){
					if (gameObject.GetComponent<SpriteRenderer>() == null){
						Debug.LogError("ERROR: This GameObject doesn't have a SpriteRenderer" + gameObject);
						return -1;
					}

					SpriteRenderer renderer = gameObject.GetComponentsInChildren<SpriteRenderer>()[0];
					return renderer.color.a;
				}

				static public class Arrays {

					static public void setAlpha(GameObject[] array, float alpha) {
						foreach(GameObject gameObject in array){
							Sprites.setAlpha(gameObject, alpha);
						}
					}
					
					static public void addAlpha(GameObject[] array, float alpha){
						foreach(GameObject gameObject in array){
							Sprites.addAlpha(gameObject, alpha);
						}
					}
					
					static public void substractAlpha(GameObject[] array, float alpha){
						foreach(GameObject gameObject in array){
							Sprites.substractAlpha(gameObject, alpha);
						}
					}
				}

			}

			public static class Models {

				static public void setAlpha(GameObject gameObject, float alpha){
					MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
					
					for (int i = 0; i < renderers.Length; i++) {
						Color newColor = new Color( renderers[i].material.color.r, 
						                           renderers[i].material.color.g, 
						                           renderers[i].material.color.b,
						                           alpha);

						renderers[i].material.SetColor("_Color", newColor);
					}
				}

				static public void addAlpha(GameObject gameObject, float alpha){
					setAlpha(gameObject, getAlpha(gameObject) + alpha);
				}
				
				static public void substractAlpha(GameObject gameObject, float alpha){
					setAlpha(gameObject, getAlpha(gameObject) - alpha);
				}

				static public float getAlpha(GameObject gameObject){
					if (gameObject == null){
						Debug.LogError("Error: Trying to get alpha of a null GameObject");
						return -1;
					}
					if (gameObject.GetComponentsInChildren<MeshRenderer>().Length == 0){
						return -1;
					}

					MeshRenderer renderer = gameObject.GetComponentsInChildren<MeshRenderer>()[0];
					
					return renderer.material.color.a;
				}



				static public class Arrays {
					static public void setAlpha(GameObject[] array, float alpha){
						foreach(GameObject gameObject in array){
							Models.setAlpha(gameObject, alpha);
						}
					}
					
					static public void addAlpha(GameObject[] array, float alpha){
						foreach(GameObject gameObject in array){
							Models.addAlpha(gameObject, alpha);
						}
					}
					
					static public void substractAlpha(GameObject[] array, float alpha){
						foreach(GameObject gameObject in array){
							Models.substractAlpha(gameObject, alpha);
						}
					}
				}
			}

		}

		public static class Colors {

			public static class Sprites {

				public static void highlight(GameObject gameObject, string color) {
					SpriteRenderer renderer = gameObject.GetComponentsInChildren<SpriteRenderer>()[0];
					Color outColor;
					
					switch (color){
						case "Normal": outColor = Color.white; break;
						case "Gray": outColor = Color.gray; break;
						case "Grey": outColor = Color.grey; break;
						case "Red": outColor = Color.red; break;
						case "Green": outColor = Color.green; break;
						case "Yellow": outColor = Color.yellow; break;
						case "Blue": outColor = Color.blue; break;
						case "Black": outColor = Color.black; break;
                        default: /*Color.TryParseHexString(color, out outColor);*/ outColor = Color.white; break;
					}

					renderer.material.color = outColor;
				}

				public static void setRGB(GameObject gameObject, float r, float g, float b){
					SpriteRenderer renderer = gameObject.GetComponentsInChildren<SpriteRenderer>()[0];
					renderer.color = new Color(r, g, b, renderer.color.a);
				}

				public static class Arrays {

					public static void highlight(GameObject[] array, string color){
						foreach(GameObject gameObject in array){
							Sprites.highlight(gameObject, color);
						}
					}

					public static void setRGB(GameObject[] array, float r, float g, float b){
						foreach(GameObject gameObject in array){
							Sprites.setRGB(gameObject, r, g, b);
						}
					}

				}

			}

			public static class Models {

				public static void highlight(GameObject gameObject, string color){
					MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

					foreach (MeshRenderer renderer in renderers){
						Color outColor;
						
						switch (color){
							case "Normal":outColor = Color.white; break;
							case "Gray":outColor = Color.gray; break;
							case "Grey":outColor = Color.grey; break;
							case "Red":outColor = Color.red; break;
							case "Green":outColor = Color.green; break;
							case "Yellow":outColor = Color.yellow; break;
							case "Blue":outColor = Color.blue; break;
							case "Black":outColor = Color.black; break;
                            default:/*Color.TryParseHexString(color, out outColor);*/ outColor = Color.white; break;
						}

						renderer.material.color = outColor;
					}
				}

				public static void setRGB(GameObject gameObject, float r, float g, float b){
					MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
					
					for (int i = 0; i < renderers.Length; i++) {
						Color newColor = new Color(r, g, b, renderers[i].material.color.a);
						renderers[i].material.SetColor("_Color", newColor);
					}
				}

				public static class Arrays {

					public static void highlight(GameObject[] array, string color){
						foreach(GameObject gameObject in array){
							Models.highlight(gameObject, color);
						}
					}

					public static void setRGB(GameObject[] array, float r, float g, float b){
						foreach(GameObject gameObject in array){
							Models.setRGB(gameObject, r, g, b);
						}
					}

				}

			}
		}
	}
}
	