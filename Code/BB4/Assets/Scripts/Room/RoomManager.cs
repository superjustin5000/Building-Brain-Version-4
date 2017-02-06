using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour 
{

	RoomModal modal;
	RoomView view;
	
	// Use this for initialization
	void Start () 
	{
		view = GetComponent<RoomView>();
		modal = GetComponent<RoomModal>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//notification system update.
	}

	void OnTriggerEnter(Collider player)
	{
		if(player.tag == "Player")
		{
			view.EnterRoom();
			view.ChangeText(modal.rmName);
		}
	}
}
