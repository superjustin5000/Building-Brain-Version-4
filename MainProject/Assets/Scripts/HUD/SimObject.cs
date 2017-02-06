using UnityEngine;
using System.Collections;

[System.Serializable]
public class SimObject : MonoBehaviour {

	[SerializeField]
	string name;
	[SerializeField]
	string description;


	public string getName() {
		return name;
	}
	public string getDesc() {
		return description;
	}

	public void setName(string n) {
		name = n;
	}
	public void setDescription(string d) {
		description = d;
	}

}

 
