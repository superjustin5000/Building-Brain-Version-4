using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EmissionView : MonoBehaviour
{
	[SerializeField]Light spotLight;
	[SerializeField]Button button;

	bool _isOn = true;
	Text _buttonText;

	void Start ()
	{
		_buttonText = button.GetComponentInChildren<Text> ();
	}
	// Update is called once per frame
	void Update () 
	{
		if (_isOn) {
			_buttonText.text = "OFF";
		}
		else {
			_buttonText.text = "ON";
		}
	}

	public void ToggleLight()
	{
		_isOn = !_isOn;
		spotLight.enabled = _isOn;
	}

	public bool LightIsOn
	{
		get{return _isOn;}
	}
}
