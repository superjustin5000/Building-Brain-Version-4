using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[System.Serializable]
public class SimObject : MonoBehaviour {


	SimulationManager sm;


	#region
	[Header("SIM OBJECT INFO")]
	[SerializeField]
	string name;
	[SerializeField]
	string description;
	#endregion

	void Awake() {
		//check the current task.
		sm = SimulationManager.sharedSimManager;
	}

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


	/// <summary>
	/// Checks the task completion.
	/// Should be called whenever an object is interacted
	/// in a way that would complete a task.
	/// Uses simulation manager to get the current task
	/// </summary>
	public virtual void checkTaskCompletion() {

		Task t = sm.CurrentTask;

		//make sure a task exists.
		if (t != null) {

			//if the tasks interaction object is this object.
			if (t.TaskObject == this) { //you've completed this task.

				//you've just completed the task.
				Debug.Log("Completed Task");

				t.State = Task.CompleteState.completed;

			}

		}

	}

}

 
