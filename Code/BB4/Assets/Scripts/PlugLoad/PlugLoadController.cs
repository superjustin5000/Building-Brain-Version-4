using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(PlugLoad))]
[RequireComponent(typeof(PlugLoadView))]

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class PlugLoadController : SimObject {

	PlugLoad plugLoad;
	PlugLoadView plugLoadView;

	//public delegate void energySupplyAction();
	//public event energySupplyAction OnSupplyEnergy;


	void Awake() {
		plugLoad = GetComponent<PlugLoad>();
		plugLoadView = GetComponent<PlugLoadView>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//give energy every frame to those that are in the list.
		if (getIsOn()) {
			foreach (EnergyUsingObject e in getEuoList()) {
				if (e != null) {
					float amt = e.ReceiveEnergy();

					plugLoad.addToAggregateUsage(amt);
				}
			}
		}

	}


	public PlugLoad getPlugLoad() {
		if (plugLoad == null) plugLoad = GetComponent<PlugLoad>();
		return plugLoad;
	}


	public void turnOn() {
		getPlugLoad().setIsOn(true);

		// supply energy to all connected objects.
		foreach(EnergyUsingObject euo in getEuoList()) {
			euo.startSupplyingEnergy();
		}
	}

	public void turnOff() {
		getPlugLoad().setIsOn(false);


		//deny supply to all connected objects.
		foreach(EnergyUsingObject euo in getEuoList()) {
			euo.stopSupplyingEnergy();
		}

	}

	public void toggleOnOff() {
		getPlugLoad().setIsOn(!getPlugLoad().getIsOn());
	}

	public bool getIsOn() {
		return getPlugLoad().getIsOn();
	}



	/// <summary>
	/// Gets the euo list.
	/// </summary>
	/// <returns>The euo list.</returns>
	public List<EnergyUsingObject> getEuoList() {
		return getPlugLoad().getEuoList();
	}

	/// <summary>
	/// Sets the euo.
	/// </summary>
	/// <param name="euo">Euo.</param>
	/// <param name="index">Index.</param>
	public void replaceEuo(EnergyUsingObject euo, int index) {
		remEuo(index);
		euo.connectToPlugLoad(getPlugLoad());
		getEuoList().Insert(index, euo);
	}

	/// <summary>
	/// Adds the euo.
	/// </summary>
	/// <param name="euo">Euo.</param>
	public void addEuo(EnergyUsingObject euo) {
		if (euo != null)
			euo.connectToPlugLoad(getPlugLoad());
		getEuoList().Add(euo);
	}


	/// <summary>
	/// Rems the euo.
	/// </summary>
	/// <param name="index">Index.</param>
	public void remEuo(int index) {
		EnergyUsingObject euo = getEuoList()[index];
		if (euo != null)
			euo.disconnectFromPlugLoad();
		getEuoList().RemoveAt(index);
	}


	public void remEuo(EnergyUsingObject euo) {
		if (euo != null)
			euo.disconnectFromPlugLoad();
		getEuoList().Remove(euo);
	}


	/// <summary>
	/// Ises the euo connected.
	/// </summary>
	/// <returns><c>true</c>, if euo connected was ised, <c>false</c> otherwise.</returns>
	/// <param name="euo">Euo.</param>
	public bool isEuoConnected(EnergyUsingObject euo) {
		bool found = false;
		if (euo.getConnectedPlugLoad() != null)
			if (euo.getConnectedPlugLoad() == getPlugLoad())
				found = true;

		return found;
	}








	public float getTotalEnergyUsed() {
		float total = 0;

		foreach(EnergyUsingObject o in getEuoList()) {
			if (o!=null)
				total += o.getTotalEnergyUsage();
		}

		return total;
	}

	public float getTotalUsagePerSec() {
		float total = 0;

		foreach(EnergyUsingObject o in getEuoList()) {
			if (o!=null)
				total += o.getEnergyUsagePerSec();
		}
		return total;
	}






	public float getAggregateUsage() {
		return plugLoad.getAggregateUsage();
	}




	public static PlugLoadController[] getAllPlugLoads() {

		PlugLoadController[] loads = GameObject.FindObjectsOfType<PlugLoadController>();


		return loads;

	}




}
