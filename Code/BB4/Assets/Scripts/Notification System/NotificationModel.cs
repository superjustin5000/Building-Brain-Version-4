using UnityEngine;
using System.Collections;

public class NotificationModel : MonoBehaviour 
{
	//public Room[] _rooms;		//room class that will hold name of room, plugload, and room lightemission. 

	//temporary test values 
	NotificationController ctrl;
	public string[] rmNames;
	float[] testEmissions;

	// Use this for initialization
	void Start () {
		ctrl = GetComponent<NotificationController>();
		testEmissions = new float[rmNames.Length];
	}
	
	// Update is called once per frame
	void Update () {
		Emit();
	}

	public string GetRoomName(int index)
	{
		return rmNames[index];
	}

	public float GetEmission(int index)
	{
		return testEmissions[index];
	}

	public int Size
	{
		get{return rmNames.Length;}
	}

	void Emit()
	{
		for(int i = 0; i < testEmissions.Length; i++)
		{
			testEmissions[i] += Time.deltaTime * Random.Range(0,10);

			if(testEmissions[i] >= 50)
			{
				ctrl.SetActiveQuest(i);
			}
		}
	}
}
