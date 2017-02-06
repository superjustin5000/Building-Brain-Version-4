using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class InteractionView : MonoBehaviour {

	SphereCollider trigger;
	[SerializeField]InteractionController ctrl;

	// Use this for initialization
	void Start () {
		trigger = GetComponent<SphereCollider>();
	}

	void OnTriggerEnter(Collider player)
	{
		if(player.tag == "Player")
			ctrl.NearObject = true;

	}

	void OnTriggerExit(Collider player)
	{
		ctrl.NearObject = false;
	}
}
