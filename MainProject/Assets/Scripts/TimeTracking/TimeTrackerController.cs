using UnityEngine;
using System.Collections;


/// <summary>
/// Time tracker controller.
/// </summary>
[RequireComponent(typeof(TimeScaleController))] //---for knowing the current scale.
public class TimeTrackerController : MonoBehaviour {

	[SerializeField]
	/// <summary>
	/// The time tracker modal object.
	/// </summary>
	TimeTracker timeTracker;


	float timer = 0;

	///Enumeration for the TimeFormat.
	/// can be in Minutes and Seconds,
	/// Hours, Minutes, and Seconds,
	/// Days, Hours, Minutes, and Seconds,
	/// or the Current time of day e.g. 10:00 AM
	public enum TimeFormat {
		minSec,
		hoMinSec,
		dayHoMinSec,
		timeOfDay
	}



	/// <summary>
	/// Occurs when on one second passed.
	/// </summary>
	public delegate void TimeTrackerAction(float time);
	public event TimeTrackerAction onOneSecondPassed;



	// Use this for initialization
	void Start () {
		timeTracker = new TimeTracker();

		onOneSecondPassed += oneSecondPassed;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!timeTracker.getIsPaused()) { ///not currently paused.

			///get time scale...
			float timeScale = GetComponent<TimeScaleController>().getTimeScale();
			float adjustedDT = Time.deltaTime * timeScale;

			//timer += Time.deltaTime; //get the time from last frame.
			timer += adjustedDT;


			//one second has passed...
			if (timer >= 1.0f) { 

				//keep any leftover deltatime stored in timer... so don't set it = 0. just sub 1.
				timer -= 1.0f;

				//increment in the time tracker.

				int curTime = timeTracker.getSimTimeInSeconds();
				//set new time to be current time

				timeTracker.setSimTimeInSeconds(curTime + 1);


				//fire event to let everyone know that a second has passed.
				//parameter is for the current time of day in seconds.
				if (onOneSecondPassed != null)
					onOneSecondPassed(timeTracker.getSimTimeInSeconds() + timeTracker.getSimStartTime());

			}

		}

		//let's try to print the time now.
		//printSimTime(TimeFormat.timeOfDay);

	}



	public void oneSecondPassed(float time) { ///speed of this method calling should increase with time scale.
		printSimTime(TimeFormat.timeOfDay);
	}



	/// <summary>
	/// Prints the sim time.
	/// </summary>
	/// <returns>The sim time.</returns>
	/// <param name="desiredFormat">Desired format. --- consult TimeFormat Enum</param>
	public void printSimTime(TimeFormat desiredFormat) {
		Debug.Log("Current simulation time = " + getSimTime(desiredFormat) );
		
	}








	/// <summary>
	/// Gets the sim time.
	/// </summary>
	/// <returns>The sim time.</returns>
	/// <param name="desiredFormat">Desired format. --- consult TimeFormat Enum</param>
	public string getSimTime(TimeFormat desiredFormat) {

		int seconds = timeTracker.getSimTimeInSeconds();

		string time="";


		switch(desiredFormat) {

		case TimeFormat.minSec:
			time = convertSecToMinSec(seconds);
			break;
		case TimeFormat.hoMinSec:
			time = convertSecToHoMinSec(seconds);
			break;
		case TimeFormat.dayHoMinSec:
			time = convertSecToDayHoMinSec(seconds);
			break;
		case TimeFormat.timeOfDay:
			time = convertSecToTimeOfDay(seconds);
			break;
		default:
			time = seconds.ToString();
			break;
		}

		return time;
	}






	/// <summary>
	/// Converts the sec to minutes seconds.
	/// </summary>
	/// <returns>The sec to minutes seconds as a string.</returns>
	/// <param name="sec">Sec.</param>
	string convertSecToMinSec(int sec) {

		int min = sec / 60;
		int rem = sec % 60;

		string s = min.ToString("00") + " Minutes and " + rem.ToString("00") + " Seconds";

		return s;

	}

	/// <summary>
	/// Converts the seconds to hours minutes seconds.
	/// </summary>
	/// <returns>The sec to hours minutes seconds as a string.</returns>
	/// <param name="sec">Sec.</param>
	string convertSecToHoMinSec(int sec) {

		int ho = sec / 3600;
		int rem1 = sec % 3600;

		int min = rem1 / 60;
		int rem = rem1 % 60;

		string s = ho.ToString("00") + " Hours, " + min.ToString("00") + " Minutes, and " + rem.ToString("00") + " Seconds";

		return s;

	}


	/// <summary>
	/// Converts the seconds to days hours minites seconds.
	/// </summary>
	/// <returns>The days hours minutes seconds as a string.</returns>
	/// <param name="sec">Sec.</param>
	string convertSecToDayHoMinSec(int sec) {

		int day = sec / 86400;
		int rem1 = sec % 86400;

		int ho = rem1 / 3600;
		int rem2 = rem1 % 3600;

		int min = rem2 / 60;
		int rem = rem2 % 60;

		string s = day.ToString("00") + " Days, " + ho.ToString("00") + " Hours, " + min.ToString("00") + " Minutes, and " + rem.ToString("00") + " Seconds";

		return s;

	}



	/// <summary>
	/// Converts the seconds to time of day.
	/// </summary>
	/// <returns>The time of day as a string.</returns>
	/// <param name="sec">Sec.</param>
	string convertSecToTimeOfDay(int sec) {

		int start = timeTracker.getSimStartTime();

		int timeOfDay = sec + start; //start is how many seconds from midnight the sim started.

		//now do the same as the other methods...exept.. don't want days.
		//filter out days by modding with seconds in days.
		int secsInCurrentDay = timeOfDay % 86400;

		//get in hours.
		int hoursInCurrentDay = secsInCurrentDay / 3600;

		//get seconds left this hour.
		int secsInCurrentHour = secsInCurrentDay % 3600;

		//get in minutes.
		int minutesInCurrentHour = secsInCurrentHour / 60;

		//gets seconds left this minute.
		int secondsInCurrentMinute = secsInCurrentHour % 60;

		//get am/pm.
		string ampm = "AM";
		if (hoursInCurrentDay > 11) ampm = "PM";

		//convert hours to 12 hour time.
		hoursInCurrentDay = hoursInCurrentDay % 12;

		//build string.
		string s = hoursInCurrentDay.ToString("00") + ":" + minutesInCurrentHour.ToString("00") + ":" + secondsInCurrentMinute.ToString("00") + " " + ampm;

		return s;

	}




	/// <summary>
	/// Sets the sim start time. FORMAT = 00:00:00 AM
	/// </summary>
	/// <param name="timeOfDay">Time of day as string. //format should be .. 00:00:00 am</param>
	public void setSimulationStartTime(string timeOfDay) {


		//separate the string into parts.
		string hours = new string(timeOfDay.ToCharArray(0,2));
		string minutes = new string(timeOfDay.ToCharArray(3,2));
		string seconds = new string(timeOfDay.ToCharArray(6,2));
		string ampm = new string (timeOfDay.ToCharArray(9,2));

		//convert to ints.
		int hourSeconds = int.Parse(hours) * 3600;
		int minuteSeconds = int.Parse(minutes) * 60;
		int secs = int.Parse(seconds);

		if (ampm.ToLower() == "pm") hourSeconds += 43200; //add 12 hours worth of seconds if pm.

		//add up the seconds.
		int totalSecs = hourSeconds + minuteSeconds + secs;

		//set new.
		timeTracker.setSimStartTime(totalSecs);

	}





	/// <summary>
	/// Pause this instance.
	/// </summary>
	public void pause() {
		timeTracker.pause();
	}

	/// <summary>
	/// Resume this instance.
	/// </summary>
	public void resume() {
		timeTracker.resume();
	}





}
