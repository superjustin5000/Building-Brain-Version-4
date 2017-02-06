using UnityEngine;
using System.Collections;


[System.Serializable]
/// <summary>
/// Time scale. - Controls the multiplier for the speed of time passing.
/// </summary>
public class TimeScale {

	[SerializeField]
	[Range(0,200)] //the theoretical range.(may change based of UX).
	/// <summary>
	/// The time scale.
	/// </summary>
	float timeScale = 1.0f;


	/// <summary>
	/// Gets the time scale.
	/// </summary>
	/// <returns>The time scale.</returns>
	public float getTimeScale() {
		return timeScale;
	}

	/// <summary>
	/// Sets the time scale.
	/// </summary>
	/// <param name="newTimeScale">New time scale.</param>
	public void setTimeScale(float newTimeScale) {
		timeScale = newTimeScale;
	}

}
