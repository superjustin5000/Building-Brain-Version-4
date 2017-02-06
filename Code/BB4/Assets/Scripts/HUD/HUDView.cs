using UnityEngine;
using System.Collections;

using UnityEngine.UI;

using System.Collections.Generic;
using System.Linq;

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
	Text objectSelectablePlugLoadsLabel;
	RectTransform selectablePlugLoadsList;
	RectTransform selectablePlugLoadTemplate;

	//plug load.
	Button plugLoadTurnOnOff;
	Text plugLoadConnectedObjectsListLabel;
	RectTransform plugLoadConnectedObjectsList;
	RectTransform plugLoadConnectedObjectTemplate;



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

		/*
		 * energy using object gui stuff.
		 */
		objectEnergyUsageLabel = GameObject.Find("ObjectEnergyUsageLabel").GetComponent<Text>();
		objectEnergyUsed = GameObject.Find("ObjectEnergyUsed").GetComponent<Text>();
		objectEnergyPerSec = GameObject.Find("ObjectEnergyPerSec").GetComponent<Text>();
		objectBatteriesLabel = GameObject.Find("ObjectBatteriesLabel").GetComponent<Text>();
		objectBatteryLifeInner = GameObject.Find("ObjectBatteryLifeInner").GetComponent<RectTransform>();
		objectTurnOnOff = GameObject.Find("ObjectTurnOnOff").GetComponent<Button>();
		objectConDiscon = GameObject.Find("ObjectConDiscon").GetComponent<Button>();

		objectSelectablePlugLoadsLabel = GameObject.Find("ObjectSelectablePlugLoadsLabel").GetComponent<Text>();
		selectablePlugLoadsList = GameObject.Find("SelectablePlugLoadsList").GetComponent<RectTransform>();
		selectablePlugLoadTemplate = GameObject.Find ("SelectablePlugLoadTemplate").GetComponent<RectTransform>();
		/*
		 * plug load gui stuff.
		 */
		plugLoadTurnOnOff = GameObject.Find("ObjectTurnOnOffPlugLoad").GetComponent<Button>();
		plugLoadConnectedObjectsListLabel = GameObject.Find("ObjectConnectedObjectsLabel").GetComponent<Text>();
		plugLoadConnectedObjectsList = GameObject.Find("ConnectedObjectsList").GetComponent<RectTransform>();
		plugLoadConnectedObjectTemplate = GameObject.Find("ConnectedObjectTemplate").GetComponent<RectTransform>();

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
	}
	public void hideEUOPowerButtons() {
		objectTurnOnOff.gameObject.SetActive(false);
	}
	public void showEnergyUsage() {
		objectEnergyUsageLabel.gameObject.SetActive(true);
		objectEnergyUsed.gameObject.SetActive(true);
		objectEnergyPerSec.gameObject.SetActive(true);
	}
	public void showEUOPowerButtons() {
		objectTurnOnOff.gameObject.SetActive(true);
	}




	public void  hideDisconnectShowPlugLoadsList() {
		objectConDiscon.gameObject.SetActive(false);
		selectablePlugLoadsList.transform.parent.parent.gameObject.SetActive(true);
		objectSelectablePlugLoadsLabel.gameObject.SetActive(true);

	}
	public void showDisconnectHidePlugLoadsList() {
		objectConDiscon.gameObject.SetActive(true);
		selectablePlugLoadsList.transform.parent.parent.gameObject.SetActive(false);
		objectSelectablePlugLoadsLabel.gameObject.SetActive(false);
	}
	public void hideDisconnectAndPlugLoadList() {
		objectConDiscon.gameObject.SetActive(false);
		selectablePlugLoadsList.transform.parent.parent.gameObject.SetActive(false);
		objectSelectablePlugLoadsLabel.gameObject.SetActive(false);
	}



	public void setListOfSelectablePlugLoads(EnergyUsingObject euo) {

		//clear the list.
		int childs = selectablePlugLoadsList.childCount;
		for (int i = childs - 1; i >= 0; i--)
		{
			GameObject.Destroy(selectablePlugLoadsList.GetChild(i).gameObject);
		}

		PlugLoadController[] plugLoads = PlugLoadController.getAllPlugLoads();

		for (int i=0; i<plugLoads.Length; i++) {

			GameObject go = Instantiate(selectablePlugLoadTemplate.gameObject) as GameObject;
			go.transform.SetParent(selectablePlugLoadsList);

			//position properly.
			RectTransform t = go.GetComponent<RectTransform>();
			Vector3 pos = t.localPosition;
			pos.z = 0;
			t.localPosition = pos;
			
			go.transform.localScale = new Vector3(1,1,1);

			Text name = go.transform.FindChild("PlugLoadName").GetComponent<Text>();
			Button connectButton = go.transform.Find("ConnectToPlugLoadButton").GetComponent<Button>();

			PlugLoadController plc = plugLoads[i];

			name.text = plc.getName();
			connectButton.onClick.AddListener( () => plc.addEuo(euo) );

		}

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

	/*
	 * 
	 * 
	 * 
	 * --------- ========= PLUG LOAD STUFF
	 * 
	 * 
	 * 
	 * 
	 */


	public void plugLoad_setListOfEnergyUsingObjects(List<EnergyUsingObject> euoList, PlugLoadController plc) {

		//clear the list.
		int childs = plugLoadConnectedObjectsList.childCount;
		for (int i = childs - 1; i >= 0; i--)
		{
			GameObject.Destroy(plugLoadConnectedObjectsList.GetChild(i).gameObject);
		}

		//create the new ones.

		for (int i=0; i<euoList.Count; i++) {
			EnergyUsingObject euo = euoList[i];

			GameObject go = Instantiate(plugLoadConnectedObjectTemplate.gameObject) as GameObject;
			go.transform.SetParent(plugLoadConnectedObjectsList);

			RectTransform t = go.GetComponent<RectTransform>();
			Vector3 pos = t.localPosition;
			pos.z = 0;
			t.localPosition = pos;

			go.transform.localScale = new Vector3(1,1,1);

			Text name = go.transform.FindChild("ConObjName").GetComponent<Text>();
			Text usagePerSec = go.transform.FindChild("ConObjUsagePerSec").GetComponent<Text>();
			Button supply = go.transform.FindChild("ConObjSupplyButton").GetComponent<Button>();
			Button unplug = go.transform.FindChild("ConObjUnplugButton").GetComponent<Button>();


			name.text = euo.getName();
			usagePerSec.text = euo.getEnergyUsagePerSec().ToString() + " / sec.";


			supply.onClick.AddListener( () => euo.toggleSupplyingEnergy(supply) );

			Text supplyText = supply.transform.FindChild("Text").GetComponent<Text>();
			string supplyButtonLabel = "Supply";
			if (euo.getIsEnergySupplied()) {
				supplyButtonLabel = "Deny";
			}
			supplyText.text = supplyButtonLabel;


			unplug.onClick.AddListener( () => plc.remEuo(euo) );
			unplug.onClick.AddListener( () => plugLoad_setListOfEnergyUsingObjects(euoList, plc) );

		}


	}



	public void plugLoad_hideConnectedObjectList() {
		plugLoadConnectedObjectsList.transform.parent.parent.gameObject.SetActive(false);
		plugLoadConnectedObjectsListLabel.gameObject.SetActive(false);
	}
	public void plugLoad_showConnectedObjectList() {
		plugLoadConnectedObjectsList.transform.parent.parent.gameObject.SetActive(true);
		plugLoadConnectedObjectsListLabel.gameObject.SetActive(true);
	}


	public void plugLoad_setTurnOnOffButtonText(string s) {
		plugLoadTurnOnOff.transform.FindChild("Text").GetComponent<Text>().text = s;
	}
	public void plugLoad_hideOnOffButton() {
		plugLoadTurnOnOff.gameObject.SetActive(false);
	}
	public void plugLoad_showOnOffButton() {
		plugLoadTurnOnOff.gameObject.SetActive(true);
	}


}
