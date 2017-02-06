using UnityEngine;
using System.Collections;

public class InteractionController : MonoBehaviour {

	bool nearObject = false;
	bool carrying = false;
	Camera mainCam;
	[SerializeField]InteractionView view;
	InteractionObject selectedObject;
	Rigidbody carryingObject;
	[SerializeField]float distance;
	void Start()
	{
		mainCam = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () 
	{
		if(NearObject || carrying)
		{
			InteractionControl();
		}
	}

	public bool NearObject
	{
		get{ return nearObject;}
		set{nearObject = value;}
	}

	public InteractionObject Selected
	{
		get{return selectedObject;}
		set{selectedObject = value;}
	}

	public void InteractionControl()
	{
		if(carrying)
		{
			Carry(carryingObject);
		}

		else
		{
			PickUp();
		}

	}

	void PickUp()
	{
		if(Input.GetKeyDown(KeyCode.P))//Input.GetKey("PickUp"))
		{
			float x = Screen.width /2; 
			float y = Screen.height/2;

			Ray ray = mainCam.ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit))
			{
				selectedObject = hit.collider.GetComponent<InteractionObject>();
				if(selectedObject != null)
				{
					carrying = true;
					carryingObject = selectedObject.RBody;
				}
			}

		}
	}

	void Carry(Rigidbody x)
	{
		x.isKinematic = true;
		x.transform.position = Vector3.Lerp(x.transform.position, transform.position + transform.forward * distance, Time.time);
		if(Input.GetKeyDown(KeyCode.P))
		{
			drop(x);
		}
	}





	//JUSTIN's METHODS


	bool isAtDropZone() {


		return true;
	}


	void drop(Rigidbody x) {

		x.isKinematic = false;
		carrying = false; 
		carryingObject = null;
		selectedObject = null;



	}


}
