using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Linq;

[System.Serializable]
[RequireComponent(typeof(PlugLoadController))]
public class PlugLoad : MonoBehaviour {

	[SerializeField][HideInInspector]
	List<EnergyUsingObject> euoList = new List<EnergyUsingObject>();

	[SerializeField][HideInInspector]
	bool isOn;

	float aggregateUsage;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public List<EnergyUsingObject> getEuoList() {
		if (euoList == null) Debug.Log("euo list = null");
		return euoList;
	}


	public bool getIsOn() {
		return isOn;
	}

	public void setIsOn(bool val) {
		isOn = val;
	}


	public float getAggregateUsage() {
		return aggregateUsage;
	}
	public void addToAggregateUsage(float amt) {
		aggregateUsage += amt;
	}

}
