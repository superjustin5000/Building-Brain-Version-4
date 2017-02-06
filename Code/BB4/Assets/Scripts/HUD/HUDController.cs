using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HUD))]
[RequireComponent(typeof(HUDView))]
public class HUDController : MonoBehaviour {

	SimulationManager sm;

	HUD hud;
	HUDView hudView;

	TimeTrackerController time;
	TimeScaleController scale;


	void Awake() {
		sm = SimulationManager.sharedSimManager;
		sm.Hud = this;

		hud = GetComponent<HUD>();
		hudView = GetComponent<HUDView>();
	}


	// Use this for initialization
	void Start () {
		
		hudView.setSimTimeScale(scale.getTimeScale().ToString("#.00") + "x");

		//subscribe to the event of one second passing.
		//time = GameObject.Find("TimeTracker+Scale").GetComponent<TimeTrackerController>();
		//time.onOneSecondPassed += onOneSecPassed;
	}

	void OnEnable() {
		time = GameObject.FindObjectOfType<TimeTrackerController>();
		time.onOneSecondPassed += onOneSecPassed;

		scale = GameObject.FindObjectOfType<TimeScaleController>();
	}

	void OnDisable() {
		time.onOneSecondPassed -= onOneSecPassed;
	}



	// Update is called once per frame
	void Update () {
	


		//updating the energy usage of a selected object.
		SimObject obj = hud.getSelectedObject();
		if (obj != null) {

			hudView.showObjectInfo();

			if (obj.GetType() == typeof(EnergyUsingObject)) {

				hudView.showEnergyUsage();
				hudView.showEUOPowerButtons();

				hudView.plugLoad_hideConnectedObjectList();
				hudView.plugLoad_hideOnOffButton();

				//do the energy using stuff.
				
				EnergyUsingObject euo = (EnergyUsingObject)obj;
				
				hudView.setObjectEnergyUsed(euo.getTotalEnergyUsage().ToString());
				hudView.setObjectEnergyPerSec(euo.getEnergyUsagePerSec().ToString() + " / sec.");


				//if this thing has batteries.
				if (euo.getNumBatteries() > 0) {
					hudView.showBattery();
					//get battery energy..returned as percent.
					float batteryEnergy = euo.getBatteryEnergyAt(0);
					Debug.Log(batteryEnergy);
					hudView.setObjectBatteryLifeInnerScale(batteryEnergy);
				}
				else {
					hudView.hideBattery();
				}

				bool isPluggedIn = euo.getIsPluggedIn();
				string onOffButtonText = euo.getIsPoweredOn() ? "Turn Off" : "Turn On";

				string conDisconButtonText = "Unplug";
				hudView.setObjectTurnOnOffButtonText(onOffButtonText);
				hudView.setObjectConDisconButtonText(conDisconButtonText);


				if (!isPluggedIn) {
					hudView.hideDisconnectShowPlugLoadsList();
				}
				else {
					hudView.showDisconnectHidePlugLoadsList();
				}


				if (euo.hasPower()) hudView.enableObjectTurnOnOffButton();
				else                hudView.disableObjectTurnOnOffButton();
			}

			/*
			 * Not energy using objet.. look for other types.
			 */
			else {
				hudView.hideEnergyUsage();
				hudView.hideEUOPowerButtons();
				hudView.hideDisconnectAndPlugLoadList();
				hudView.hideBattery();
				hudView.plugLoad_hideConnectedObjectList();
				hudView.plugLoad_hideOnOffButton();


				/*
				 * ITS A PLUG LOAD.. DO PLUG LOAD STUFF.
				 */
				if (obj.GetType() == typeof(PlugLoadController)) {

					PlugLoadController pl = (PlugLoadController)obj;

					hudView.showEnergyUsage();
					hudView.plugLoad_showOnOffButton();

					string onOffString = pl.getIsOn() ? "Turn Off" : "Turn On";
					hudView.plugLoad_setTurnOnOffButtonText(onOffString);

					if (pl.getIsOn())
						hudView.plugLoad_showConnectedObjectList();

					hudView.setObjectEnergyUsed(pl.getTotalEnergyUsed().ToString());
					hudView.setObjectEnergyPerSec(pl.getTotalUsagePerSec().ToString() + " / sec.");

				}


			}
		}
		else {
			hudView.hideObjectInfo();
			hudView.hideEnergyUsage();
			hudView.hideEUOPowerButtons();
			hudView.hideDisconnectAndPlugLoadList();
			hudView.hideBattery();

			hudView.plugLoad_hideConnectedObjectList();
			hudView.plugLoad_hideOnOffButton();
		}

	}





	void onOneSecPassed(float t) {
		//get the time as a string.
		TimeTrackerController.TimeFormat format = hud.getTimeFormat();
		string timeString = time.getSimTime(format);

		hudView.setSimTime(timeString);

	}




	//-----------  MESSING AROUND WITH TIME STUFF.


	public void selectedNewTimeFormat() {
		//get the value of the drop down.
		int value = hudView.getTimeFormatValue();
		//convert to time format.
		TimeTrackerController.TimeFormat f = (TimeTrackerController.TimeFormat)value;
		//set the time format to the model.
		hud.setTimeFormat(f);

		//call one sec passed to update the time with the new format.
		onOneSecPassed(0);
	}



	public void increaseTimeScale() {
		//set new time scale.
		scale.increaseTimeScale();
		//get that scale.
		float ts = scale.getTimeScale();
		//update view.
		hudView.setSimTimeScale(ts.ToString("#.00") + "x");
	}

	public void decreaseTimeScale() {
		//set new time scael.
		scale.decreaseTimeScale();
		//get the new scale.
		float ts = scale.getTimeScale();
		//update view.
		hudView.setSimTimeScale(ts.ToString("#.00") + "x");
	}

	public void resetTimeScale() {
		//set
		scale.resetTimeScale();
		//get
		float ts = scale.getTimeScale();
		//update
		hudView.setSimTimeScale(ts.ToString("#.00") + "x");
	}




	
	//-----------  SELECTED OBJECT STUFF.

	public void selectObject(SimObject obj) {

		if (obj != null) {
			//do the generic stuff.

			hudView.setObjectName(obj.getName());
			hudView.setObjectDescription(obj.getDesc());


			hud.setSelectedObject(obj);



			//get other object set up stuff.
			if (obj.GetType() == typeof(PlugLoadController)) {
				PlugLoadController pl = (PlugLoadController)obj;

				hudView.plugLoad_setListOfEnergyUsingObjects(pl.getEuoList(), pl);

			}
			else if (obj.GetType() == typeof(EnergyUsingObject)) {
				EnergyUsingObject euo = (EnergyUsingObject)obj;
				hudView.setListOfSelectablePlugLoads(euo);
			}

		}

	}




	/// - -------------- METHODS FOR IF THE OBJECT IS ENERGY USING
	/// 
	/// 

	public void selectedObjectToggleOnOff() {
		//don't have to check if it's null since this method can only be called from a button..
		//the button only exists if the object is not null...sooo.
		//get selected.
		SimObject obj = hud.getSelectedObject();

		if (obj.GetType() == typeof(EnergyUsingObject)) {
			EnergyUsingObject euo = (EnergyUsingObject)obj;
			//power on or off.
			bool objOn = euo.getIsPoweredOn();
			if (objOn) {
				euo.powerOff();
			}
			else {
				euo.powerOn();
			}
		}
		if (obj.GetType() == typeof(PlugLoadController)) {

			PlugLoadController plc = (PlugLoadController)obj;

			if (plc.getIsOn()) {
				plc.turnOff();
			}
			else {
				plc.turnOn();
			}

		}
		//view is updated in the update method.
	}


	public void selectedObjectToggleConDiscon() {
		EnergyUsingObject obj = (EnergyUsingObject)hud.getSelectedObject();
		//is power supplied or not?
		bool objCon = obj.getConnectedPlugLoad() != null;
		if (objCon) {
			obj.getConnectedPlugLoad().GetComponent<PlugLoadController>().remEuo(obj);
		}
		else {
			Debug.Log("TRYING TO DISCONNECT BUT NOT CONNECTED TO A PLUG LOAD.. CHECK PLUG LOAD.. or something else is seriously wrong");
		}
	}


	public void selectedObjectShowPlugLoads() {

	}
	public void selectedObjectSelectPlugLoad(PlugLoad pl) {


	}

}
