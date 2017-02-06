using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EmissionController : MonoBehaviour {

	EmissionView _view;
	EmissionTracker _modal;

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

		totalEmissTemp.text = _modal.TotalEmission.ToString();
	}
}
