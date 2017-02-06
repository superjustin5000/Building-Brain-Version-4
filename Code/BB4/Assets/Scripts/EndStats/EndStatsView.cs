using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Linq;

using UnityEngine.UI;

[RequireComponent(typeof(EndStatsController))]
public class EndStatsView : MonoBehaviour {

	Text totalUsage;
	Text totalPoints;

	Transform taskList;
	Transform taskListItemTemplate;


	void Awake() {
		totalUsage = GameObject.Find("TotalUsage").GetComponent<Text>();
		totalPoints = GameObject.Find("TotalPoints").GetComponent<Text>();

		taskList = GameObject.Find("TaskList").GetComponent<Transform>();
		taskListItemTemplate = GameObject.Find("TaskListItemTemplate").GetComponent<Transform>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void setTotalUsage(float usage) {
		totalUsage.text = usage.ToString("#.00");
	}

	public void setTotalPoints(int points) {
		totalPoints.text = points.ToString();
	}



	public void setTaskList(List<Task> taskList) {

		if (taskList.Count <= 0) {
			//Destroy(taskListItemTemplate.gameObject);
			return;
		}

		for (int i=0; i<taskList.Count; i++) {
			Task task = taskList[i];

			Transform taskListItem = Instantiate<GameObject>(taskListItemTemplate.gameObject).GetComponent<Transform>();
			taskListItem.SetParent(this.taskList);

			Text name = taskListItem.FindChild("TaskListItemName").GetComponent<Text>();
			Text status = taskListItem.FindChild("TaskListItemStatus").GetComponent<Text>();
			Text points = taskListItem.FindChild("TaskListItemPoints").GetComponent<Text>();

			name.text = task.name;
			status.text = task.State.ToString();
			int p = 0;
			if (task.State == Task.CompleteState.completed)
				p = task.PointValue;
			points.text = p.ToString();



		}

		Destroy(taskListItemTemplate.gameObject);

	}


}
