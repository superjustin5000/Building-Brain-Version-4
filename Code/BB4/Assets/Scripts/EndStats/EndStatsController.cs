using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Linq;


[RequireComponent(typeof(EndStatsView))]
public class EndStatsController : MonoBehaviour {

	EndStatsView view;


	void Awake() {

		view = GetComponent<EndStatsView>();
	}

	// Use this for initialization
	void Start () {
	
		setUpStats();
		setUpTaskList();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void setUpStats() {
		view.setTotalUsage(SimulationManager.sharedSimManager.getSimulationTotalEnergyUsage());
		view.setTotalPoints(SimulationManager.sharedSimManager.getTotalPoints());
	}

	void setUpTaskList() {

		List<Task> taskList = SimulationManager.sharedSimManager.getListOfTasks();

		view.setTaskList(taskList);

	}

	
	
	
	public void GoToMainMenu() {
		Application.LoadLevel(SimulationManager.SceneMainMenu);
	}

}
