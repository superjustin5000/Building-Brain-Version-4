using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Linq;

using UnityEditor;

[RequireComponent(typeof(MainMenu))]
[RequireComponent(typeof(MainMenuView))]
public class MainMenuController : MonoBehaviour {

	MainMenu modal;
	MainMenuView view;

	SimulationTaskDatabase db;

	void Awake() {
		modal = GetComponent<MainMenu>();
		view = GetComponent<MainMenuView>();
	}

	// Use this for initialization
	void Start () {
	
		//get the filename.
		string dbFilePath = "Assets/Editor/SimulationMaker/SimulationDatabase.asset";
		db = AssetDatabase.LoadAssetAtPath<SimulationTaskDatabase>(dbFilePath);


		//taken out for now.
		//view.initializeDbSimList(db.simulations);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void beginSimulation() {

		//turn time of day strings into ints.
		int hours = int.Parse(modal.StartHours);
		//flip if negative.
		hours = hours < 0 ? -hours : hours;
		//mod
		hours = hours % 12;
		if (hours == 0) hours = 12;
		if (modal.StartAMPM == "PM") hours += 12;

		int min = int.Parse(modal.StartMinutes);
		min = min < 0 ? -min : min;
		min = min % 60;

		int sec = int.Parse(modal.StartSeconds);
		sec = sec < 0 ? -sec : sec;
		sec = sec % 60;

		int totalSecs = sec + (min*60) + (hours*60*60);

		//convert all start time into 1 string.
		string timeofday = TimeTrackerController.convertSecondsToTimeOfDayString(totalSecs);

		//save to sim manager.
		SimulationManager.sharedSimManager.startTimeOfDay = timeofday;
		SimulationManager.sharedSimManager.startRunTimeInsSeconds = modal.RunTime;

		//begin the new scene.
		//Debug.Log("Starting time of day is " + timeofday);
		//Debug.Log("Run time in seconds is " + modal.RunTime);





		//create real tasks from database tasks.
		List<Task> tasks = new List<Task>();

		//get the index of the selected database simulation.
		//whichever was last selected in the task timeline window.
		//is the selected one.

		int selected = 0;
		//int selected = view.getSelectedSimulation();
		for (int i=0; i<db.simulations.Count; i++) {
			DatabaseSimulation s = db.simulations[i];
			if (s.selected) {
				selected = i;
				break;
			}
		}


		DatabaseSimulation sim = db.simulations[selected];

		for (int i=0; i<sim.tasks.Count; i++) {

			DatabaseTask t = sim.tasks[i];
			Debug.Log(t);

			Task task = new Task();

			task.TaskName = t.name;
			task.Description = t.description;
			task.StartTime = t.startTime;
			task.TimeLimit = t.timeLimit;
			task.TimeLength = t.length;

			task.PointValue = t.pointValue;

			task.State = Task.CompleteState.neverStarted;

			SimulationManager.sharedSimManager.getListOfTasks().Add(task);


		}




		Application.LoadLevel(SimulationManager.SceneSimulation);

	}



	public void setRunTime(int val) {
		int runSeconds = 0;
		switch(val) {
		case 0:
			runSeconds = 3 * 3600;
			break;
		case 1:
			runSeconds = 6 * 3600;
			break;
		case 2:
			runSeconds = 12 * 3600;
			break;
		case 3:
			runSeconds = 24 * 3600;
			break;
		case 4:
			runSeconds = 36 * 3600;
			break;
		case 5:
			runSeconds = 48 * 3600;
			break;

		}
		modal.RunTime = runSeconds;
	}

	public void setStartHours(string hour) {
		modal.StartHours = hour;
	}
	public void setStartMinutes(string min) {
		modal.StartMinutes = min;
	}
	public void setStartSeconds(string sec) {
		modal.StartSeconds = sec;
	}
	public void setStartAMPM(int val) {
		if (val == 0) {
			modal.StartAMPM = "AM";
		}
		else if (val == 1) {
			modal.StartAMPM = "PM";
		}
	}



}
