using UnityEngine;
using System.Collections;

public class PlugLoadView : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//on a mouse click.
		if (Input.GetMouseButtonDown(0)) {
			//Debug.Log("clicked");
			//check if the mouse is clicking on the box collider.
			//raycast from mouse position relative to camera.
			Ray ray = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//if the ray hits something.
			if (Physics.Raycast(ray, out hit)) {
				//Debug.Log("Mouse Over");
				//check what the game object of the hit collider is.
				if (hit.collider.gameObject == gameObject) { //hit this object.
					//showing ui.. hide it.
					/*if (isShowingUI) {
						hideUI();
					}
					//otherwise.. show it.
					else {
						showUI();
					}
					*/
					showUI();

					Debug.Log("hit the damn plug load");
				}
			}
			
			
		}
	}




	void showUI() {

		
		//NEW WAY OF DOING IT THROUGH THE HUD.
		
		GameObject.Find("HUD").GetComponent<HUDController>().selectObject(GetComponent<PlugLoadController>());
		
		//isShowingUI = true;
	}


}
