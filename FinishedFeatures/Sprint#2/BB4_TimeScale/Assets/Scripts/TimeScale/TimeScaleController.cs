using UnityEngine;
using System.Collections;

/// <summary>
/// Time scale controller.
/// Has instance of time scale and uses it as Modal for controlling time scale.
/// Just add it to a game object.
/// </summary>
[RequireComponent(typeof(TimeTrackerController))]
public class TimeScaleController : MonoBehaviour {


	/// <summary>
	/// The max time scale. negative or positive.
	/// </summary>
	public float maxTimeScale = 200;


	/// <summary>
	/// The time scale increment. how much to increment/decrement when adjusting.
	/// </summary>
	float timeScaleIncrement = 0.1f;


	[SerializeField]
	/// <summary>
	/// The time scale modal.
	/// </summary>
	TimeScale timeScale;


	// Use this for initialization
	void Start () {
		timeScale = new TimeScale();
	}


	/// <summary>
	/// Gets the time scale.
	/// </summary>
	/// <returns>The time scale.</returns>
	public float getTimeScale() {
		return timeScale.getTimeScale();
	}




	/// <summary>
	/// Increases the time scale.
	/// </summary>
	public void increaseTimeScale() {
		float ts = timeScale.getTimeScale();

		ts += timeScaleIncrement;
		if (ts > maxTimeScale) ts = maxTimeScale;

		timeScale.setTimeScale(ts);

		//update UI HERE..
	}

	/// <summary>
	/// Decreases the time scale.
	/// </summary>
	public void decreaseTimeScale() {
		float ts = timeScale.getTimeScale();
		
		ts -= timeScaleIncrement;
		//if (ts < -maxTimeScale) ts = -maxTimeScale;
		if (ts< 0) ts = 0;

		timeScale.setTimeScale(ts);
		
		//update UI HERE..
	}

	/// <summary>
	/// Sets the specific time scale.
	/// </summary>
	/// <param name="newTimeScale">New time scale.</param>
	public void setSpecificTimeScale(float newTimeScale) {

		if (newTimeScale > maxTimeScale) newTimeScale = maxTimeScale;
		//if (newTimeScale < -maxTimeScale) newTimeScale = -maxTimeScale;
		if (newTimeScale < 0) newTimeScale = 0;

		timeScale.setTimeScale(newTimeScale);

		//udpate UI HERE...
	}

	/// <summary>
	/// Resets the time scale.
	/// </summary>
	public void resetTimeScale() {
		timeScale.setTimeScale(1.0f);

		//udate UI HERE..
	}




	/// <summary>
	/// Prints the time scale to the console for testing.
	/// </summary>
	public void printTimeScale() {
		float ts = timeScale.getTimeScale();
		Debug.Log("(Class: TimeScaleController) - Time Scale is " + ts);
	}


}
