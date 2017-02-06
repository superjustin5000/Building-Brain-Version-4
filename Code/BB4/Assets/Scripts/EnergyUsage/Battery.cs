using UnityEngine;
using System.Collections;

[System.Serializable]
public class Battery {

	/// <summary>
	/// The max energy.
	/// </summary>
	[SerializeField]
	float maxEnergy;

	/// <summary>
	/// The current energy.
	/// </summary>
	[SerializeField]
	float curEnergy;

	/// <summary>
	/// If the battery is charging.
	/// </summary>
	[SerializeField]
	bool isCharging;


	/// <summary>
	/// If the battery is full.
	/// </summary>
	bool isFull;

	/// <summary>
	/// If the battery is empty.
	/// </summary>
	bool isEmpty;



	/// <summary>
	/// Gets the current energy.
	/// </summary>
	/// <returns>The current energy.</returns>
	public float getCurEnergy() {
		return curEnergy;
	}

	/// <summary>
	/// Gets the max energy.
	/// </summary>
	/// <returns>The max energy.</returns>
	public float getMaxEnergy() {
		return maxEnergy;
	}


	/// <summary>
	/// Sets the max energy.
	/// </summary>
	/// <param name="amt">Amt.</param>
	public void setMaxEnergy(float amt) {
		maxEnergy = amt;
		if (curEnergy > maxEnergy) {
			curEnergy = maxEnergy;
			isFull = true;
		}
		else {
			isFull = false;
			if (curEnergy <= 0) {
				curEnergy = 0;
				isEmpty = true;
			}
		}
	}





	/// <summary>
	/// Sets if is charging.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void setIsCharging(bool state) {
		isCharging = state;
	}

	/// <summary>
	/// Gets  if is charging.
	/// </summary>
	/// <returns><c>true</c>, if is charging, <c>false</c> otherwise.</returns>
	public bool getIsCharging() {
		return isCharging;
	}

	

	/// <summary>
	/// Gets if is full.
	/// </summary>
	/// <returns><c>true</c>, if is full, <c>false</c> otherwise.</returns>
	public bool getIsFull() {
		return isFull;
	}


	/// <summary>
	/// Gets if is empty.
	/// </summary>
	/// <returns><c>true</c>, if is empty, <c>false</c> otherwise.</returns>
	public bool getIsEmpty() {
		return isEmpty;
	}




	/// <summary>
	/// Sets the charge.
	/// </summary>
	/// <param name="amt">Amt.</param>
	public void setEnergy(float amt) {
		curEnergy = amt;

		//assume not full and not empty.
		isFull = false;
		isEmpty = false;
		if (curEnergy > maxEnergy) { 
			curEnergy = maxEnergy;
			isFull = true; ////---- known to be full.
		}
		else if (curEnergy < 0) {
			curEnergy = 0;
			isEmpty = true; /// --- only case where it can be empty.
		}
	}




}
