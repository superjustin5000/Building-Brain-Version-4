using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EmissionView : MonoBehaviour
{
	[SerializeField]Light[] spotLight;
	[SerializeField]Text button;

	bool _isOn = true;
	Text _buttonText;

	void Start ()
	{
		_buttonText = button;
	}
		// Update is called once per frame
	void Update () 
	{
	}

	public void TextState(bool state){
		
		_buttonText.gameObject.SetActive (state);
	}

	public void ToggleLight()
	{
		_isOn = !_isOn;
		_buttonText.text = (_isOn) ? "TURN OFF" :  "TURN ON";
		foreach(Light l in spotLight)
//<<<<<<< HEAD
			if (l != null)
				l.enabled = _isOn;
//=======
//			l.enabled = _isOn;
		
//>>>>>>> Sprint6_2_E
	}
		
	public bool LightIsOn
	{
		get{return _isOn;}
	}
}
