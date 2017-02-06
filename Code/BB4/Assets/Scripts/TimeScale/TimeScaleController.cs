using UnityEngine;
using System.Collections;

/// <summary>
/// Time scale controller.
/// Has instance of time scale and uses it as Modal for controlling time scale.
/// Just add it to a game object.
/// </summary>
[RequireComponent(typeof(TimeTrackerController))]
public class TimeScaleController : MonoBehaviour {

	SimulationManager sm;

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





	void Awake() {
		sm = SimulationManager.sharedSimManager;
		sm.TimeScale = this;
	}

	// Use this for initialization
	void Start () {
		//timeScale = new TimeScale();
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

		float increment = timeScaleIncrement;

		//As the number get's bigger the incrememnt gets bigger.
		//so that the number can get bigger faster.
		if ((Mathf.Approximately(ts,0) || ts > 0) && ts < 0.1f)             increment = 0.01f;
		else if ((Mathf.Approximately(ts,0.1f) || ts > 0.1f) && ts < 0.5f)  increment = 0.05f;
		else if ((Mathf.Approximately(ts,0.5f) || ts > 0.5f) && ts < 2)     increment = 0.1f;
		else if ((Mathf.Approximately(ts,2)    || ts > 2) && ts < 10)       increment = 1.00f;
		else if ((Mathf.Approximately(ts,10)   || ts > 10) && ts < 100)     increment = 5.00f;
		else if ((Mathf.Approximately(ts,100)  || ts > 100) && ts < 300)    increment = 10.00f;
		else if ((Mathf.Approximately(ts,300)  || ts > 300))                increment = 100.00f;

		Debug.Log(ts);

		ts += increment;

		if (ts > maxTimeScale) ts = maxTimeScale;

		timeScale.setTimeScale(ts);

		//update UI HERE..
	}

	/// <summary>
	/// Decreases the time scale.
	/// </summary>
	public void decreaseTimeScale() {
		float ts = timeScale.getTimeScale();


		float decrement = timeScaleIncrement;


		if (ts <= maxTimeScale && ts > 300)   decrement = 100;
		else if (ts <= 300   && ts > 100)     decrement = 10;
		else if (ts <= 100   && ts > 10)      decrement = 5;
		else if (ts <= 10    && ts > 2)       decrement = 1;
		else if (ts <= 2     && ts > 0.5f)    decrement = 0.1f;
		else if (ts <= 0.5f  && ts > 0.1f)    decrement = 0.05f;
		else if (ts <= 0.1f)                  decrement = 0.01f;

		ts -= decrement;
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
