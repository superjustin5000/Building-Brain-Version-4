using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Linq;

using UnityEditor;

public class TaskTimelineWindow : EditorWindow {

	[MenuItem("Simulation Maker/Simulation Task Timeline")]
	public static void OpenWindow() {
		EditorWindow.GetWindow(typeof(TaskTimelineWindow), false, "Task TimeLine");
	}

	public static string dbFilePath = "Assets/Editor/SimulationMaker/SimulationDatabase.asset";
	public static string dbFilePathNoExtension = "Assets/Editor/SimulationMaker/SimulationDatabase";

	SimulationTaskDatabase db;

	void drawCreateButton() {
		if (GUILayout.Button("Create Simulation Task Database Asset")) {
			createDatabaseScriptableObject();
		}
	}
	static void createDatabaseScriptableObject() {
		SimulationTaskDatabase o = ScriptableObject.CreateInstance<SimulationTaskDatabase>();
		AssetDatabase.CreateAsset(o, dbFilePath);
		AssetDatabase.Refresh();
	}



	DatabaseSimulation selectedSimulation;
	DatabaseTask selectedTask;




	//local variables.

	[SerializeField] public int timeLineStartY = 250;


	void OnGUI() {

		db = AssetDatabase.LoadAssetAtPath<SimulationTaskDatabase>(dbFilePath);

		if (db != null) {


			//look for mouse clicks.

			Event e = Event.current;
			if (e.type == EventType.mouseDown) {

			}
			else if (e.type == EventType.MouseUp) {
				if (createTaskAtPosition(e.mousePosition))
					e.Use();
				
				Repaint();
			}



			drawTopMenu();

			if (selectedSimulation != null) {

				drawTimeLine();

				drawTasksOnTimeLine();

			}

		}

		else {

			drawCreateButton();
		}

	}


	void drawTopMenu() {
		BeginWindows();

		Vector2 size = new Vector2(Screen.width, 20);
		Vector2 pos = Vector2.zero;
		Rect rect = new Rect(pos.x, pos.y, size.x, size.y);
		GUILayout.Window(1, rect, drawTopMenuControls, "");

		EndWindows();
	}


	void drawTopMenuControls(int id) {

		EditorGUILayout.BeginHorizontal();
		{
			int simIndex = MyEditor.ObjectList<DatabaseSimulation>(db.simulations, selectedSimulation, "Select Simulation");
			if (simIndex >=0)
				selectedSimulation = db.simulations[simIndex];
			else
				selectedSimulation = null;

			//deselect all simulations.
			foreach (DatabaseSimulation s in db.simulations)
				s.selected = false;
			

			if (GUILayout.Button("Create Simulation")) {
				if (EditorUtility.DisplayDialog("Confirm", "Continuing will lose unsaved changes to current simulation", "Continue", "Cancel")) {
					DatabaseSimulation sim = new DatabaseSimulation();
					db.simulations.Add(sim);
					selectedSimulation = sim;
				}
			}
		}
		EditorGUILayout.EndHorizontal();

		if (selectedSimulation != null) {

			//select this simulation.
			selectedSimulation.selected = true;

			//Simulation variables.

			EditorGUILayout.BeginHorizontal();
			{
				selectedSimulation.name = EditorGUILayout.TextField("Sim Name", selectedSimulation.name);
				DatabaseSimulation.SimulationLength lengthEnum = (DatabaseSimulation.SimulationLength)EditorGUILayout.EnumPopup("Length", selectedSimulation.lengthEnum);
				//selected new length.
				//do conversion from enum to number of seconds.
				if (lengthEnum != selectedSimulation.lengthEnum) {
					selectedSimulation.lengthEnum = lengthEnum;
					selectedSimulation.length = (int)selectedSimulation.lengthEnum;
					selectedSimulation.length *= 3600;
					//Debug.Log(selectedSimulation.length);
				}


			}
			EditorGUILayout.EndHorizontal();

			selectedSimulation.description = EditorGUILayout.TextField("Sim Description", selectedSimulation.description);


			if (GUILayout.Button("Delete Simulation")) {
				if (EditorUtility.DisplayDialog("Confirm", "This Action Cannot Be Undone.", "Continue", "Cancel")) {
					db.simulations.Remove(selectedSimulation);
					if (db.simulations.Count > 0) {
						selectedSimulation = db.simulations[0];
					}
					else {
						selectedSimulation = null;
						return;
					}
				}
			}



			//check the task list to see if the selected task is in it. if not.. set it to null.
			bool found = false;
			for(int i=0; i<selectedSimulation.tasks.Count; i++) {
				DatabaseTask t = selectedSimulation.tasks[i];
				if (t == selectedTask) {
					found = true;
					break;
				}
			}
			if (!found) selectedTask = null;


			//the simulation's selected task.
			// show the selected task options.
			if (selectedTask != null) {

				EditorGUILayout.Space();

				EditorGUILayout.BeginHorizontal();
				{
					selectedTask.name = EditorGUILayout.TextField("Task Name", selectedTask.name);
					selectedTask.type = (Task.TaskType)EditorGUILayout.EnumPopup("Type", selectedTask.type);

					if (selectedTask.type == Task.TaskType.MoveObject || selectedTask.type == Task.TaskType.TurnOff || selectedTask.type == Task.TaskType.TurnOn) {
						selectedTask.taskObject = (SimObject)EditorGUILayout.ObjectField(selectedTask.taskObject, typeof(SimObject), true);
					}

				}
				EditorGUILayout.EndHorizontal();

				selectedTask.description = EditorGUILayout.TextField("Task Description", selectedTask.description);



				//START TIME EDITING......

				EditorGUILayout.BeginHorizontal();
				{


					selectedTask.startTime = EditorGUILayout.IntSlider("Start Time", selectedTask.startTime, 0, selectedSimulation.length);

					//move the seconds to the closest 5 min mark.
					int fiveMin = 300;
					int distance = selectedTask.startTime % fiveMin;
					if (distance <= 150)
						selectedTask.startTime -= distance;
					else selectedTask.startTime += (fiveMin - distance);


					//draw the time in 3 text fields without labels.
					int time = selectedTask.startTime;
					int hours = time / 3600;
					int minSec = time % 3600;
					int mins = minSec / 60;
					int secs = minSec % 60;

					EditorGUILayout.LabelField(hours.ToString("00 hours") + " " + mins.ToString("00 mins") + " " + secs.ToString("00 secs"));


				}
				EditorGUILayout.EndHorizontal();



				//TIME LMIT EDITING

				EditorGUILayout.BeginHorizontal();
				{


					selectedTask.timeLimit = EditorGUILayout.IntSlider("Time Limit", selectedTask.timeLimit, 30, DatabaseTask.maxTimeLimit);

					//move the seconds to the closest 5 min mark.
					int fiveSec = 10;
					int distance = selectedTask.timeLimit % fiveSec;
					if (distance <= 5)
						selectedTask.timeLimit -= distance;
					else selectedTask.timeLimit += (fiveSec - distance);


					//draw the time in 3 text fields without labels.
					int time = selectedTask.timeLimit;
					int mins = time / 60;
					int secs = time % 60;

					EditorGUILayout.LabelField(mins.ToString("00 mins") + " " + secs.ToString("00 secs"));


				}
				EditorGUILayout.EndHorizontal();


				// LENGTH EDITING...

				EditorGUILayout.BeginHorizontal();
				{


					selectedTask.length = EditorGUILayout.IntSlider("Length", selectedTask.length, 0, (selectedSimulation.length-selectedTask.startTime));

					//move the seconds to the closest 5 min mark.
					int thirty = 30;
					int distance = selectedTask.length % thirty;
					if (distance <= 15)
						selectedTask.length -= distance;
					else selectedTask.length += (thirty - distance);


					//draw the time in 3 text fields without labels.
					int time = selectedTask.length;
					int hours = time / 3600;
					int minSec = time % 3600;
					int mins = time / 60;
					int secs = time % 60;

					EditorGUILayout.LabelField(hours.ToString("00 hours ") + mins.ToString("00 mins") + " " + secs.ToString("00 secs"));


				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				selectedTask.pointValue = EditorGUILayout.IntField("Point Value", selectedTask.pointValue);

				if (GUILayout.Button("Delete Task")) {
					if (EditorUtility.DisplayDialog("Confirm", "This Action Cannot Be Undone.", "Continue", "Cancel")) {
						selectedSimulation.tasks.Remove(selectedTask);
						if (selectedSimulation.tasks.Count > 0) {
							selectedTask = selectedSimulation.tasks[0];
						}
						else selectedTask = null;
					}
				}
				EditorGUILayout.EndHorizontal();

			}

		}

	}




	void drawTimeLine() {
		int width = Screen.width;
		int fifteenMinutesInSeconds = 900;
		if (selectedSimulation.lengthEnum == DatabaseSimulation.SimulationLength.Three)
			fifteenMinutesInSeconds = 300;
		if (selectedSimulation.lengthEnum == DatabaseSimulation.SimulationLength.Six)
			fifteenMinutesInSeconds = 600;
		
		int numIntervals = selectedSimulation.length / fifteenMinutesInSeconds;
		float distanceBetweenIntervals = (float)width / (float)numIntervals;
		//Debug.Log(numIntervals);
		//divide into 15 minute intervals.

		int numberOfLabels = 12;
		int labelEvery = numIntervals / numberOfLabels;
		//Debug.Log("labelevery = " + labelEvery + " because numlabels = " + numberOfLabels + " and numintervals = " + numIntervals);

		int y1 = timeLineStartY;
		Handles.color = Color.grey;
		Handles.DrawLine(new Vector3(0,y1,0), new Vector3(width,y1,0));

		for (int i=0; i<numIntervals; i++) {

			int xPos = (int)(i * distanceBetweenIntervals);
			int y2 = Screen.height;
			int tempY1 = y1;


			//draw label.
			if (i % labelEvery == 0) {
				int yText = y1 - 20;
				int xText = xPos - 11;
				int seconds = fifteenMinutesInSeconds * i;
				int hour = seconds / 3600;
				int min = seconds % 3600;
				min /=60;
				string label = hour.ToString() + ":" + min.ToString();

				//move the text more if it's longer.
				if (hour >= 10)
					xText -= 7;

				EditorGUI.LabelField(new Rect(xText, yText, 100,100), label);

				Handles.color = new Color(0.1f, 0.1f, 0.1f, 0.7f);
				//increase y1 so you know it corresponds to the label.
				tempY1 -= 5;
			}

			else {
				Handles.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
			}

			Handles.DrawLine(new Vector3(xPos, tempY1, 0), new Vector3(xPos, y2));

		}
	}



	void drawTasksOnTimeLine() {
		if (selectedSimulation.tasks.Count < 0)
			return;


		for(int i=0; i<selectedSimulation.tasks.Count; i++) {

			DatabaseTask t = selectedSimulation.tasks[i];

			int timeLineX = t.startTime;

			int x = convertTimeLineXToWindowX(timeLineX);

			int y = t.startY;


			Handles.color = Color.red;
			Handles.CircleCap(0, new Vector3(x, y, 0), Quaternion.identity, 5);

			if (t == selectedTask) {
				Handles.color = Color.green;
				Handles.CircleCap(0, new Vector3(x, y, 0), Quaternion.identity, 9);
			}

			//show a line of the time limit and how long it will last.
			int timeLimitX1 = x;
			int timeLimitX2 = convertTimeLineXToWindowX(timeLineX + t.timeLimit);
			Handles.color = Color.blue;
			//draw three lines to make it a thick line.
			Handles.DrawLine(new Vector3(timeLimitX1, y-1, 0), new Vector3(timeLimitX2, y-1, 0));
			Handles.DrawLine(new Vector3(timeLimitX1, y, 0), new Vector3(timeLimitX2, y, 0));
			Handles.DrawLine(new Vector3(timeLimitX1, y+1, 0), new Vector3(timeLimitX2, y+1, 0));


			//now for the length.
			//show a line of the time limit and how long it will last.
			int lengthX1 = timeLimitX2;
			//from latest start to earliest end.
			int lengthX2 = lengthX1 + convertTimeLineXToWindowX(t.length) - (timeLimitX2-timeLimitX1);
			//from earliest end to lastest end.
			int lengthX3 = lengthX2;
			int lengthX4 = lengthX3 + (timeLimitX2-timeLimitX1);
			Handles.color = Color.yellow;
			//draw three lines to make it a thick line.
			Handles.DrawLine(new Vector3(lengthX1, y-1, 0), new Vector3(lengthX2, y-1, 0));
			Handles.DrawLine(new Vector3(lengthX1, y, 0), new Vector3(lengthX2, y, 0));
			Handles.DrawLine(new Vector3(lengthX1, y+1, 0), new Vector3(lengthX2, y+1, 0));

			Handles.color = Color.red;
			Handles.DrawLine(new Vector3(lengthX3, y-1, 0), new Vector3(lengthX4, y-1, 0));
			Handles.DrawLine(new Vector3(lengthX3, y, 0), new Vector3(lengthX4, y, 0));
			Handles.DrawLine(new Vector3(lengthX3, y+1, 0), new Vector3(lengthX4, y+1, 0));
		}
	}





	int convertTimeLineXToWindowX(int timeLineX) {

		int totalSeconds = selectedSimulation.length;
		int totalWidth = Screen.width;

		float percent = (float)timeLineX / (float)totalSeconds;

		int x = (int)(totalWidth * percent);

		return x;
	}






	int convertMouseXToTimeLineX(float mouseX) {
		int totalSeconds = selectedSimulation.length;
		int totalWidth = Screen.width;

		float percent = mouseX / (float)totalWidth;
		int second = (int)(percent * totalSeconds);

		//round to the nearest value of 5 minutes which is 300 seconds.
		int distance = (second % 300);

		if (distance <= 150) second -= distance;
		else second += (300 - distance);

		return second;
	}



	//creating and editing tasks.
	bool createTaskAtPosition(Vector2 mPos) {

		if (selectedSimulation == null) return false;

		if (mPos.y < timeLineStartY) return false;


		//check if it's the same position as a current task.
		int x = convertMouseXToTimeLineX(mPos.x);

		for (int i=0; i<selectedSimulation.tasks.Count; i++) {
			DatabaseTask t = selectedSimulation.tasks[i];

			//convert start time to x value.
			int taskX = convertTimeLineXToWindowX(t.startTime);
			//Debug.Log("Task X = " + taskX);
			float range = 5;
			if (mPos.x > taskX-range && mPos.x < taskX+range) {
				if ((mPos.y > t.startY-range) && (mPos.y < t.startY+range)) {
					//falls in the range of y..just select that task.
					selectedTask = t;
					return false;
				}
				else continue;
			}
			else continue;
		}

		//create new task.
		DatabaseTask newTask = new DatabaseTask(x, (int)mPos.y);

		//add task to task list of selected simulation.
		selectedSimulation.tasks.Add(newTask);
		//selectedSimulation.tasks.Sort();

		//set task as selected task.
		selectedTask = newTask;

		return true;
	}



}
