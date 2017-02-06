using UnityEngine;
using System.Collections;


/// <summary>
/// Time tracker.
/// This is the Modal object for the TimeTracker game object.
/// To Use:
/// Attach TimeTrackerController Script to empty game object.
/// </summary>
[System.Serializable]
public class TimeTracker {

	#region

	[Header("SIM TIMER")]

	[SerializeField]
	int simTimeInSeconds;
	[SerializeField]
	int simStartTime;

	[SerializeField]
	bool isPaused = false;
	
	#endregion

	/// <summary>
	/// Gets the sim time in seconds.
	/// </summary>
	/// <returns>The sim time in seconds.</returns>
	public int getSimTimeInSeconds() {
		return simTimeInSeconds;
	}
	/// <summary>
	/// Gets the start sim time.
	/// </summary>
	/// <returns>The start sim time.</returns>
	public int getSimStartTime() {
		return simStartTime;
	}

	/// <summary>
	/// Gets the is paused.
	/// </summary>
	/// <returns><c>true</c>, if is paused was gotten, <c>false</c> otherwise.</returns>
	public bool getIsPaused() {
		return isPaused;
	}




	/// <summary>
	/// Sets the sim time in seconds.
	/// </summary>
	/// <param name="simTime">Sim time.</param>
	public void setSimTimeInSeconds(int simTime) {
		simTimeInSeconds = simTime;
	}

	/// <summary>
	/// Sets the sim start time.
	/// </summary>
	/// <param name="startTime">Start time.</param>
	public void setSimStartTime(int startTime) {
		simStartTime = startTime;
	}

	/// <summary>
	/// Pause this instance.
	/// </summary>
	public void pause() {
		isPaused = true;
	}

	/// <summary>
	/// Resume this instance.
	/// </summary>
	public void resume() {
		isPaused = false;
	}

}
