using UnityEngine;
using System.Collections;

public class Task : MonoBehaviour {


	public enum TaskType {
		TurnOn,
		TurnOff,
		MoveObject,
		GoTo,
		TalkTo,
	}
	[SerializeField] TaskType type;
	public TaskType Type {
		get {
			return type;
		}
	}

	public enum CompleteState {
		inProgress,
		completed,
		neverStarted,
		failed
	}
	[SerializeField] CompleteState state = CompleteState.inProgress;
	public CompleteState State {
		set {
			state = value;
		}
		get {
			return state;
		}
	}



	string taskName;
	public string TaskName {
		set { taskName = value; }
		get {
			return taskName;
		}
	}

	string description;
	public string Description {
		set {
			description = value;
		}
		get {
			return description;
		}
	}

	int startTime;
	public int StartTime {
		set { startTime = value; }
		get { return startTime; }
	}

	int timeLimit;
	public int TimeLimit {
		set { timeLimit = value; }
		get {
			return timeLimit;
		}
	}

	int timeLength;
	public int TimeLength {
		set { timeLength = value; }
		get { return timeLength; }
	}

	int pointValue;
	public int PointValue { 
		set { pointValue = value; }
		get { 
			return pointValue; 
		} 
	}


	[SerializeField] SimObject taskObject;
	public SimObject TaskObject {
		get { return taskObject; }
	}


	void Awake() {

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}



}