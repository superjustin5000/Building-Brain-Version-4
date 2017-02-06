using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class HUDView : MonoBehaviour {

	//top menu..
	Button increaseTimeScale, decreaseTimeScale, resetTimeScale;
	Text simTimeScale;

	Dropdown timeFormat;
	Text simTime;


	//side menu..
	Text objectInfoLabel, objectName, objectDescription, objectEnergyUsageLabel, objectEnergyUsed, objectEnergyPerSec, objectBatteriesLabel;
	RectTransform objectBatteryLifeInner;
	Button objectTurnOnOff, objectConDiscon;



	void Awake() {
		increaseTimeScale = GameObject.Find("IncreaseTimeScale").GetComponent<Button>();
		decreaseTimeScale = GameObject.Find("DecreaseTimeScale").GetComponent<Button>();
		resetTimeScale = GameObject.Find("ResetTimeScale").GetComponent<Button>();
		simTimeScale = GameObject.Find("SimTimeScale").GetComponent<Text>();
		
		timeFormat = GameObject.Find("TimeFormat").GetComponent<Dropdown>();
		simTime = GameObject.Find("SimTime").GetComponent<Text>();

		objectInfoLabel = GameObject.Find("ObjectInfoLabel").GetComponent<Text>();
		objectName = GameObject.Find("ObjectName").GetComponent<Text>();
		objectDescription = GameObject.Find("ObjectDescription").GetComponent<Text>();

		objectEnergyUsageLabel = GameObject.Find("ObjectEnergyUsageLabel").GetComponent<Text>();
		objectEnergyUsed = GameObject.Find("ObjectEnergyUsed").GetComponent<Text>();
		objectEnergyPerSec = GameObject.Find("ObjectEnergyPerSec").GetComponent<Text>();
		objectBatteriesLabel = GameObject.Find("ObjectBatteriesLabel").GetComponent<Text>();
		objectBatteryLifeInner = GameObject.Find("ObjectBatteryLifeInner").GetComponent<RectTransform>();
		objectTurnOnOff = GameObject.Find("ObjectTurnOnOff").GetComponent<Button>();
		objectConDiscon = GameObject.Find("ObjectConDiscon").GetComponent<Button>();

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public void setSimTimeScale(string s) {
		simTimeScale.text = s;
	}

	public void setSimTime(string s) {
		simTime.text = s;
	}

	public int getTimeFormatValue() {
		return timeFormat.value;
	}
	/*'
	 *
	 *
	 *
	 *
	 *
	 *
	 *
	 *
	 */


	// -------- SELECTED OBJECT INFORMATION

	public void setObjectName(string s) {
		objectName.text = s;
	}
	public void setObjectDescription(string s) {
		objectDescription.text = s;
	}
	public void hideObjectInfo() {
		objectInfoLabel.gameObject.SetActive(false);
		objectName.gameObject.SetActive(false);
		objectDescription.gameObject.SetActive(false);
	}
	public void showObjectInfo() {
		objectInfoLabel.gameObject.SetActive(true);
		objectName.gameObject.SetActive(true);
		objectDescription.gameObject.SetActive(true);
	}
	/*
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 */
	
	// -------- SELECTED OBJECT ENERGY USAGE.

	public void setObjectEnergyUsed(string s) {
		objectEnergyUsed.text = s;
	}
	public void setObjectEnergyPerSec(string s) {
		objectEnergyPerSec.text = s;
	}


	public void setObjectTurnOnOffButtonText(string s) {
		objectTurnOnOff.transform.FindChild("Text").GetComponent<Text>().text = s;
	}
	public void enableObjectTurnOnOffButton() {
		objectTurnOnOff.interactable = true;
	}
	public void disableObjectTurnOnOffButton() {
		objectTurnOnOff.interactable = false;
	}

	public void setObjectConDisconButtonText(string s) {
		objectConDiscon.transform.FindChild("Text").GetComponent<Text>().text = s;
	}


	public void hideEnergyUsage() {
		objectEnergyUsageLabel.gameObject.SetActive(false);
		objectEnergyUsed.gameObject.SetActive(false);
		objectEnergyPerSec.gameObject.SetActive(false);
		objectTurnOnOff.gameObject.SetActive(false);
		objectConDiscon.gameObject.SetActive(false);
	}
	public void showEnergyUsage() {
		objectEnergyUsageLabel.gameObject.SetActive(true);
		objectEnergyUsed.gameObject.SetActive(true);
		objectEnergyPerSec.gameObject.SetActive(true);
		objectTurnOnOff.gameObject.SetActive(true);
		objectConDiscon.gameObject.SetActive(true);
	}

	public void hideBattery() {
		objectBatteriesLabel.gameObject.SetActive(false);
		objectBatteryLifeInner.gameObject.SetActive(false);
		objectBatteryLifeInner.parent.gameObject.SetActive(false);
	}
	public void showBattery() {
		objectBatteriesLabel.gameObject.SetActive(true);
		objectBatteryLifeInner.gameObject.SetActive(true);
		objectBatteryLifeInner.parent.gameObject.SetActive(true);
	}

	public void setObjectBatteryLifeInnerScale(float s) {
		Vector3 scale = objectBatteryLifeInner.localScale;
		scale.x = s;
		objectBatteryLifeInner.localScale = scale;
	}
	/*
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 */
	


}
