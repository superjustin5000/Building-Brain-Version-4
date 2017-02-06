using UnityEngine;
using System.Collections;


public class TimeTrackerTester : Tester {



	// Use this for initialization
	void Start () {
	
	}


	public void test_SetStartTimeOfSimulation() {

		TimeTrackerController time = GameObject.Find("TimeTracker").GetComponent<TimeTrackerController>();

		//the time that the log should now print.. should be..
		//the simulation running time + this start time below
		time.setSimulationStartTime("10:20:10 PM");

		//print to prove it.
		time.printSimTime(TimeTrackerController.TimeFormat.timeOfDay);

	}

	public void test_SetStartTimeOfSimulationAM() {

		TimeTrackerController time = GameObject.Find("TimeTracker").GetComponent<TimeTrackerController>();
		
		//the time that the log should now print.. should be..
		//the simulation running time + this start time below
		time.setSimulationStartTime("08:07:00 AM");

		//print to prove it.
		time.printSimTime(TimeTrackerController.TimeFormat.timeOfDay);

	}

}
