using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//to render it.
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

//to interact with it.
[RequireComponent(typeof(UIEnergyHub))]
//needs a physics collider..
[RequireComponent(typeof(BoxCollider))]

[System.Serializable]
public class EnergyHub : MonoBehaviour {

	#region
	[Space(5)]
	[Header("Energy Using Objects")]
	[Space(5)]
	/// <summary>
	/// The energy using object list.
	/// </summary>
	[SerializeField]
	public List<EnergyUsingObject> energyUsingObjectList = new List<EnergyUsingObject>();

	#endregion



	#region
	[Space(5)]
	[Header("Energy Supply")]
	[Space(5)]
	/// <summary>
	/// if is supplying energy.
	/// </summary>
	[SerializeField]
	bool isSupplyingEnergy = true;

	#endregion



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//constantly supply energy!
		if (isSupplyingEnergy) {
			//supplyEnergy();
		}

		//Debug.Log(getTotalEnergyUsage());
	}


	/// <summary>
	/// Supplies the energy to all objects in the list.
	/// </summary>
	public void supplyEnergy() {
		foreach (EnergyUsingObject o in energyUsingObjectList) {
			//o.updateEnergyUse();
		}
	}


	/// <summary>
	/// Gets the total energy usage of all objects in the list.
	/// </summary>
	/// <returns>The total energy usage.</returns>
	public float getTotalEnergyUsage() {
		float total = 0;
		foreach(EnergyUsingObject o in energyUsingObjectList) {
			total += o.getTotalEnergyUsage();
		}
		return total;
	}





	/// <summary>
	/// Gets if is supplying energy.
	/// </summary>
	/// <returns><c>true</c>, if supplying energy, <c>false</c> otherwise.</returns>
	public bool getIsSupplyingEnergy() {
		return isSupplyingEnergy;
	}

	/// <summary>
	/// Sets if is supplying energy.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void setIsSupplyingEnergy(bool state) {
		isSupplyingEnergy = state;
	}
}
