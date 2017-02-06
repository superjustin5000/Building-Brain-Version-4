using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine.Serialization;

[System.Serializable]
public class SimulationTaskDatabase : ScriptableObject {

	[SerializeField] public List<DatabaseSimulation> simulations = new List<DatabaseSimulation>();





	///- --------- CRAZY METHODS..
	/// 
	/// 


	//md5 encryption.


	public static string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}



}
[System.Serializable]
public class DatabaseSimulation {
	[SerializeField] public string name;
	[SerializeField] public string description;
	[SerializeField] public int length; //in seconds.

	[SerializeField] public bool selected = false;

	public enum SimulationLength {
		Three = 3,
		Six = 6,
		Twelve = 12,
		TwentyFour = 24,
		ThritySix = 36,
		FortyEight = 48
	}
	[SerializeField] public SimulationLength lengthEnum;

	[SerializeField] public List<DatabaseTask> tasks = new List<DatabaseTask>();

	public DatabaseSimulation() {
		name = "S-" + SimulationTaskDatabase.Md5Sum(System.DateTime.Now.ToString());
		description = "Temp Description";
		length = 3600 * 24;
		lengthEnum = SimulationLength.TwentyFour;
	}


	public override string ToString ()
	{
		return name;
	}
}





[System.Serializable]
public class DatabaseTask {

	public static int maxTimeLimit = 300;

	[SerializeField] public Task.TaskType type;
	[SerializeField] public string name, description;
	[SerializeField] public int startTime, startY, timeLimit, length, pointValue;
	[SerializeField] public SimObject taskObject;

	public DatabaseTask(int start, int y) {
		
		name = "T-" + SimulationTaskDatabase.Md5Sum(System.DateTime.Now.ToString());
		description = "Temp Description";
		startTime = start;
		startY = y;
		timeLimit = 60; //1 minute;
		length = 3600; //1 hour;
		pointValue = 100; //arbitrary.
	}

	public override string ToString ()
	{
		return name;
	}

}