using UnityEngine;
using System.Collections;

using UnityEngine.UI;

//can't have the ui without the actual object.
[RequireComponent(typeof(EnergyUsingObject))]
//needs a physics collider..
[RequireComponent(typeof(BoxCollider))]


public class UIEnergyUsingObject : MonoBehaviour {


	/// <summary>
	/// if is showing UI.
	/// </summary>
	bool isShowingUI = false;

	/// <summary>
	/// The current UI.
	/// when it is shown.
	/// otherwise it'll be null.
	/// </summary>
	GameObject currentUI;


	//public GameObject uiPrefab;


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
			Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
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
				}
			}


		}


	}





	/// <summary>
	/// Shows the UI.
	/// </summary>
	void showUI() {
		//destroy it incase it already exists for some reason.
		//hideUI();

		/*
		//create it from prefab.
		GameObject temp = Resources.Load<GameObject>("UIEnergyUsage/UIEnergyUsingObject") as GameObject;
		currentUI = Instantiate(temp);

		Transform canvas = GameObject.Find("HUDCanvas").transform;
		currentUI.transform.SetParent(canvas);
		currentUI.transform.localScale = new Vector3(1,1,1);
		currentUI.transform.localPosition = new Vector3(0, -2.6f, 0);

		//initialize the menu title.
		currentUI.transform.FindChild("ObjectName").GetComponent<Text>().text = GetComponent<EnergyUsingObject>().objectType;

		//initialize the button text and onclick listener.
		Button onOffButton = currentUI.transform.FindChild("TurnOnOff").GetComponent<Button>();
		Text buttonText = onOffButton.transform.FindChild("Text").GetComponent<Text>();
		//check if the object is on.
		EnergyUsingObject euo = GetComponent<EnergyUsingObject>();
		if (euo.getIsPoweredOn()) { //is turned on.. display turn off.
			buttonText.text = "Turn Off";
		}

		//set the listener for the button.
		onOffButton.onClick.AddListener(pressOnOff);

		*/

		//NEW WAY OF DOING IT THROUGH THE HUD.

		GameObject.Find("HUD").GetComponent<HUDController>().selectObject(GetComponent<EnergyUsingObject>());

		//isShowingUI = true;
	}

	/// <summary>
	/// Hides the UI.
	/// </summary>
	void hideUI() {
		//if the ui is there.. destroy and nullify it.
		if (currentUI != null) {
			GameObject.Destroy(currentUI);
			currentUI = null;
		}

		isShowingUI = false;
	}





	/// <summary>
	/// Presses the on / off button.
	/// </summary>
	public void pressOnOff() {

		//if on.. press off.. otherwise press on.
		EnergyUsingObject euo = GetComponent<EnergyUsingObject>();

		if (euo.getIsPoweredOn()) { //is turned on.. display turn off
			bool success = pressOff();
			if (!success) {
				Debug.Log("ATTENTION! Could not turn off " + euo.objectType + ". THERE MUST BE A BUG SOMEHWERE.");
			}
		}
		else {
			bool success = pressOn();
			if (!success) {
				Debug.Log("ATTENTION! Could not turn on " + euo.objectType + ". Perhaps it doesn't have power supplied, or the batteries are dead(if it has batteries).");
			}
		}

	}


	/// <summary>
	/// Presses the on button.
	/// </summary>
	bool pressOn() {
		bool success = false;
		//power on the object.
		success = GetComponent<EnergyUsingObject>().powerOn();

		if (success) {
			//set the button text to say power off.
			Button onOffButton = currentUI.transform.FindChild("TurnOnOff").GetComponent<Button>();
			Text buttonText = onOffButton.transform.FindChild("Text").GetComponent<Text>();
			buttonText.text = "Turn Off";
		}

		return success;

	}

	/// <summary>
	/// Presses the off button.
	/// </summary>
	bool pressOff() {
		bool success = false;
		//power off the object.
		success = GetComponent<EnergyUsingObject>().powerOff();

		if (success) {
			//set the button text to say power on.
			Button onOffButton = currentUI.transform.FindChild("TurnOnOff").GetComponent<Button>();
			Text buttonText = onOffButton.transform.FindChild("Text").GetComponent<Text>();
			buttonText.text = "Turn On";
		}

		return success;
	}


}
