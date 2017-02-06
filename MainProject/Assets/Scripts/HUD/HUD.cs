using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {


	SimObject selectedObject;

	TimeTrackerController.TimeFormat timeFormat = TimeTrackerController.TimeFormat.timeOfDay;

	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void setSelectedObject(SimObject o) {
		if (o != null)
			selectedObject = o;
	}

	public SimObject getSelectedObject() {
		return selectedObject;

	}

	public void setTimeFormat(TimeTrackerController.TimeFormat t) {
		timeFormat = t;
	}
	public TimeTrackerController.TimeFormat getTimeFormat() {
		return timeFormat;
	}

}
