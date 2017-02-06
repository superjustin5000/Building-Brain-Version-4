using UnityEngine;
using System.Collections;

public class TaskStart : MonoBehaviour {

	void Awake() {



	}

	// Use this for initialization
	void Start () {

		SimulationManager.sharedSimManager.TimeTracker.onOneSecondPassed += OneSecond;

	}


	void OneSecond(float time) {


		float realTime = time - SimulationManager.sharedSimManager.TimeTracker.getSimStartTime();




		foreach(Task t in SimulationManager.sharedSimManager.getListOfTasks()) {

			if (t.State == Task.CompleteState.neverStarted) {

				if (t.StartTime == realTime) {
					t.State = Task.CompleteState.inProgress;
					Debug.Log("Task Has Started");

					//tell simulation manager there is task happening.
					SimulationManager.sharedSimManager.CurrentTask = t;

				}

			}

			else if (t.State == Task.CompleteState.inProgress) {

				//calculate end time.
				int totalLength = (t.StartTime + t.TimeLimit + t.TimeLength);
				if (realTime == totalLength) { //this is the absolute latest the task can be completed by.

					//fail
					t.State = Task.CompleteState.failed;

					Debug.Log("TASK FAILED!!!");
				}

			}

		}

	}

}