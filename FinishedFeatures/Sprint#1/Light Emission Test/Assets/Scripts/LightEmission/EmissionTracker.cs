using UnityEngine;
using System.Collections;

public class EmissionTracker : MonoBehaviour 
{
	[SerializeField]float bogusValue; 	//temporary value to test emission
	bool _emitting = true;
	float _totalEmission = 0;		//to be replaced by Justin's Energy Unit Class


	float timer;	//debug purposes only. 

	// Update is called once per frame
	void Update () 
	{
		bogusValue = Random.Range(0.001f,1f);
		if (timer >= 1 && _emitting) {
			_totalEmission += bogusValue;
			Debug.Log(bogusValue); 
			timer = 0;
		}

		timer += Time.deltaTime;
	}

	public float TotalEmission
	{
		get{return _totalEmission;}
	}

	public bool Emitting
	{
		get{return _emitting;}
		set{_emitting = value;}
	}
}
