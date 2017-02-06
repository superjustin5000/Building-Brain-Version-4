using UnityEngine;
using System.Collections;

//to render it.
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

//so it can be interacted with.
[RequireComponent(typeof(UIEnergyUsingObject))]


[System.Serializable]
public class EnergyUsingObject : MonoBehaviour {

	#region
	[Space(5)]
	[Header("Power Supply")]
	[Space(5)]
	[SerializeField]
	EnergyUsage powerSupply;
	#endregion

	#region
	[Space(5)]
	[Header("Other Info")]
	[Space(5)]
	public string objectType;
	#endregion



	// Use this for initialization
	void Start () {
		//GetComponent<Renderer>().material.color = new Color(10,100,100);
	}


	// Update is called once per frame
	/// <summary>
	/// Update this instance.
	/// /// Updates the energy use.
	/// Does necessary calculations for averages and totals.
	/// should be called each frame from the energy hub.
	/// </summary>
	/// 
	void Update () {

		bool shouldDrainBattery = false;

		//get how much energy to add based on average per sec.
		float energyToAdd = powerSupply.getAvgUsePerSec() * Time.deltaTime;

		if (powerSupply.getIsEnergySupplied()) { //hub supplying energy.

			bool shouldChargeBattery = false;

			if (powerSupply.getIsOn()) { //if on.

				if (powerSupply.getIsUsingBattery()) { //using battery.. don't use enrgy.
					shouldDrainBattery = true;
				}

				else { //not using battery, power on, energy supplied... USE ENERGY...!
					//add to total usage.
					powerSupply.addTotalUse(energyToAdd);

					shouldChargeBattery = true;
				}

			}

			else { //turned off.
				shouldChargeBattery = true;
			}


			if (shouldChargeBattery) {
				
				//and charge battery as well.
				int numB = getNumBatteries();
				for (int i=0; i<numB; i++) {
					Battery b = powerSupply.getBatteryList()[i];
					if (!b.getIsFull()) {
						//found a not full battery.. charge this one and break.. so you don't charge the others too.
						b.setEnergy(b.getCurEnergy() + energyToAdd);
						break;
					}
				}
				
			}

		}

		else { //no energy supplied from hub.

			if (powerSupply.getIsOn()) {
				if (powerSupply.getIsUsingBattery()) {
					shouldDrainBattery = true;
				}
			}

		}


		if (shouldDrainBattery) {
			//Debug.Log("Using battery");
			//start depleting the first available battery that's not empty.
			int numB = getNumBatteries();
			for (int i=0; i<numB; i++) {
				Battery b = powerSupply.getBatteryList()[i];
				if (!b.getIsEmpty()) {
					//found a not empty battery.. deplete this one and 'break'.. so you don't empty the others too.
					b.setEnergy(b.getCurEnergy() - energyToAdd);
					break;
				}
			}
		}


	}











	/// <summary>
	/// Gets the total energy usage.
	/// </summary>
	/// <returns>The total energy usage.</returns>
	public float getTotalEnergyUsage() {

		return powerSupply.getTotalUse();

	}

	/// <summary>
	/// Gets the energy usage per sec.
	/// </summary>
	/// <returns>The energy usage per sec.</returns>
	public float getEnergyUsagePerSec() {
		return powerSupply.getAvgUsePerSec();
	}










	/// <summary>
	/// Powers on.
	/// </summary>
	public bool powerOn() {
		if (hasPower()) {
			powerSupply.setIsOn(true);
			return true;
		}
		else {
			return false;
		}
	}
	
	/// <summary>
	/// Powers off.
	/// </summary>
	public bool powerOff() {
		powerSupply.setIsOn(false);
		return true;
	}
	
	
	/// <summary>
	/// Gets if is powered on.
	/// </summary>
	/// <returns><c>true</c>, if is powered on, <c>false</c> otherwise.</returns>
	public bool getIsPoweredOn() {
		return powerSupply.getIsOn();
	}



	/// <summary>
	/// returns if has power.
	/// </summary>
	/// <returns><c>true</c>, if power was hased, <c>false</c> otherwise.</returns>
	bool hasPower() {
		bool batteriesNotEmpty = false; //assume batteries are empty.. 
		foreach(Battery b in powerSupply.getBatteryList()) {
			if (!b.getIsEmpty()) { 
				//if any has any charge, break loop.
				batteriesNotEmpty = true;
				break;
			}
		}

		return (powerSupply.getIsEnergySupplied() || batteriesNotEmpty);
	}









	/// <summary>
	/// Starts the using battery.
	/// </summary>
	public void startUsingBattery() {
		powerSupply.setIsUsingBattery(true);
	}
	
	/// <summary>
	/// Stops the using battery.
	/// </summary>
	public void stopUsingBattery() {
		powerSupply.setIsUsingBattery(false);
	}



	
	
	
	/// <summary>
	/// Gets the is using battery.
	/// </summary>
	/// <returns><c>true</c>, if is using battery, <c>false</c> otherwise.</returns>
	public bool getIsUsingBattery() {
		return powerSupply.getIsUsingBattery();
	}







	/// <summary>
	/// Gets the number batteries.
	/// </summary>
	/// <returns>The number batteries in batterylist.</returns>
	public int getNumBatteries() {
		return powerSupply.getBatteryList().Count;
	}
	
	/// <summary>
	/// Gets the battery level at batIndex.
	/// </summary>
	/// <returns>The <see cref="System.Single"/>.</returns>
	/// <param name="batIndex">Bat index.</param>
	public float getBatteryEnergyAt(int batIndex) {
		if (batIndex >= getNumBatteries() || batIndex < 0) {
			Debug.Log("getBatteryLevelAt - " + batIndex + " - NUM BATTERIES OUT OF BOUNDS");
			return -1;
		}
		Battery bat = powerSupply.getBatteryList()[batIndex];
		return bat.getCurEnergy();
	}

	/// <summary>
	/// Sets the battery energy at batIndex and set to amt.
	/// </summary>
	/// <param name="batIndex">Bat index.</param>
	/// <param name="amt">Amt.</param>
	public void setBatteryEnergyAt(int batIndex, float amt) {
		if (batIndex >= getNumBatteries() || batIndex < 0) {
			Debug.Log("setBatteryLevelAt - " + batIndex + " - NUM BATTERIES OUT OF BOUNDS");
			return;
		}
		Battery bat = powerSupply.getBatteryList()[batIndex];
		bat.setEnergy(amt);
	}
	
	
	/// <summary>
	/// Adds the battery to batteryList
	/// </summary>
	/// <param name="bat">Bat. the Battery to add</param>
	public void addBattery(Battery bat) {
		powerSupply.getBatteryList().Add(bat);
	}
	
	/// <summary>
	/// Removes the battery at batIndex.
	/// </summary>
	/// <param name="batIndex">Bat index.</param>
	public void removeBatteryAt(int batIndex) {
		if (batIndex >= getNumBatteries() || batIndex < 0) {
			Debug.Log("removeBatteryAt - " + batIndex + " - NUM BATTERIES OUT OF BOUNDS");
			return;
		}
		powerSupply.getBatteryList().RemoveAt(batIndex);
	}




	/// <summary>
	/// Starts the supplying energy.
	/// </summary>
	public void startSupplyingEnergy() {
		powerSupply.setIsEnergySupplied(true);
		stopUsingBattery();
	}

	/// <summary>
	/// Stops the supplying energy.
	/// </summary>
	public void stopSupplyingEnergy() {
		powerSupply.setIsEnergySupplied(false);
		startUsingBattery();
	}

	/// <summary>
	/// Gets the is energy supplied.
	/// </summary>
	/// <returns><c>true</c>, if is energy supplied was gotten, <c>false</c> otherwise.</returns>
	public bool getIsEnergySupplied() {
		return powerSupply.getIsEnergySupplied();
	}



}
