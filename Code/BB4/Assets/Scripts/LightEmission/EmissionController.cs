using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EmissionController : MonoBehaviour {

	EmissionView _view;
	EmissionTracker _modal;
	bool nearSwitch = false;
	[SerializeField]Text totalEmissTemp;

	void Start()
	{
		_view = GetComponent<EmissionView> ();
		_modal = GetComponent<EmissionTracker> ();
	}

	// Update is called once per frame
	void Update () 
	{
		_modal.Emitting = _view.LightIsOn;
		//totalEmissTemp.text = _modal.TotalEmission.ToString();
		if (nearSwitch && Input.GetKeyUp (KeyCode.Space)) 
		{
			_view.ToggleLight ();
		}
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.CompareTag ("Player")) 
		{
			nearSwitch = true;
			_view.TextState (true);
		}
	}
		
	void OnTriggerExit(Collider player)
	{
		nearSwitch = false;
		_view.TextState (false);
		Debug.Log ("Out");
	}
}
