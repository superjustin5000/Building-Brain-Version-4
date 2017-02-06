using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Keeps track of the total energy usage of the object, for the current session of the Simulation.
/// </summary>
[System.Serializable]
public class EnergyUsage {

	#region
	[Space(5)]
	[Header("Usage")]
	[Space(5)]
	/// <summary>
	/// The current expected average use of energy per second of time in the simulation.
	/// </summary>
	[SerializeField]
	float avgUsePerSec;

	/// <summary>
	/// The number of measurements.
	/// </summary>
	//[SerializeField]
	int numberOfUsages;

	/// <summary>
	/// The total use of energy in the current sim.
	/// </summary>
	[SerializeField]
	protected float totalUse;



	[SerializeField][HideInInspector]
	PlugLoad plugLoad;



	#endregion


	#region
	[Space(5)]
	[Header("Controls")]
	[Space(5)]

	/// <summary>
	/// whether the energy is currently being used for the object.
	/// </summary>
	[SerializeField]
	protected bool isOn = true;
	
	[SerializeField]
	protected bool isEnergySupplied = false;

	#endregion




	#region
	[Space(5)]
	[Header("Battery Info")]
	[Space(5)]
	
	/// <summary>
	/// The battery list.
	/// </summary>
	[SerializeField]
	private List<Battery> batteryList = new List<Battery>();


	#endregion



	void Start() {
		
	}
	
	
	void Update() {
		
	}




	
	/// <summary>
	/// Gets The current use of energy per second of time in the simulation.
	/// </summary>
	/// <returns>The current use per minute.</returns>
	public float getAvgUsePerSec() {
		//deltatime is fraction of second that it calculates cur use.
		return avgUsePerSec;
	}
	
	
	/// <summary>
	/// Gets the number of usages.
	/// </summary>
	/// <returns>The number of usages.</returns>
	public int getNumberOfUsages() {
		return numberOfUsages;
	}



	/// <summary>
	/// Gets the total use.
	/// </summary>
	/// <returns>The total use.</returns>
	public float getTotalUse() {
		return totalUse;
	}






	/// <summary>
	/// Sets the avg use per sec.
	/// </summary>
	/// <param name="amt">Amt.</param>
	public void setAvgUsePerSec(float amt) {
		avgUsePerSec = amt;
	}

	/// <summary>
	/// Sets the number of usages.
	/// </summary>
	/// <param name="amt">Amt.</param>
	public void addUsages(int amt) {
		numberOfUsages += amt;
		if (numberOfUsages < 0) numberOfUsages = 0;
	}

	/// <summary>
	/// Adds the total use.
	/// </summary>
	/// <param name="use">Use... subtracts if negative.</param>
	public void addTotalUse(float use) {
		totalUse += use;
		if (totalUse < 0) totalUse = 0;
	}









	/// <summary>
	/// Gets whether the energy use is currently on.
	/// </summary>
	/// <returns><c>true</c>, if is on, <c>false</c> otherwise.</returns>
	public bool getIsOn() {
		return isOn;
	}

	/// <summary>
	/// Sets is on or off.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void setIsOn(bool state) {
		isOn = state;
	}









	/// <summary>
	/// Gets the battery list.
	/// </summary>
	/// <returns>The battery list.</returns>
	public List<Battery> getBatteryList() {
		return batteryList;
	}





	/// <summary>
	/// Gets if is energy supplied.
	/// </summary>
	/// <returns><c>true</c>, if is energy supplied, <c>false</c> otherwise.</returns>
	public bool getIsEnergySupplied() {
		return isEnergySupplied;
	}

	/// <summary>
	/// Sets if is energy supplied.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void setIsEnergySupplied(bool state) {
		isEnergySupplied = state;
	}








	public PlugLoad getPlugLoad() {
		return plugLoad;
	}

	public void setPlugLoad(PlugLoad pl) {
		plugLoad = pl;
	}



}
