using UnityEngine;
using UnityEngine.Events;
using System.Collections;


/// <summary>
/// Tester. Subclass this class, and write your test methods in the specific subclass.
/// </summary>
public class Tester : MonoBehaviour {
	
	//public delegate void TestAction ();
	//public static event TestAction FireTestMethod;
	#region
	[Space(10)]
	[Header("Firing Test Methods.")]
	public float fireTime;
	float timer;
	#endregion


	[System.Serializable]
	public class TestAction : UnityEvent{}
	[Space(10)]
	public TestAction FireTestMethod;


	// Use this for initialization
	void Start () {
	
	}



	// Update is called once per frame
	void Update () {

		//waits the length of time.. then fires the test.
		timer += Time.deltaTime;
		if (timer >= fireTime) {
			timer = 0f;
			if (FireTestMethod != null)
				FireTestMethod.Invoke();
				//Invoke("FireTestMethod", 0);

		}



	}
}
