using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class InteractionObject : MonoBehaviour {

	[SerializeField]bool canPickUp = false;
	[SerializeField]bool canToggle = false;

	Rigidbody rBody;

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Rigidbody RBody
	{
		get{return rBody;}
	}
}
