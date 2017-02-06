using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class SmartBuildingCharacterController : MonoBehaviour 
{
	[SerializeField]float _moveSpeed = 10.0f;
	[SerializeField]float rotateSpeed = 10.0f;
	[SerializeField]float gravity = 10.0f; 
	[SerializeField]float maxVelocityChange = 10.0f;
	[SerializeField]bool canJump = false;
	[SerializeField]float jumpHeight = 2.0f;
	bool grounded = false; 
	Rigidbody rigidBody; 

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.freezeRotation = true; 
		rigidBody.useGravity = false; 
	}

	void FixedUpdate()
	{
		GroundedMovement ();
	}

	void OnCollisionStay()
	{
		grounded = true;
	}

	void GroundedMovement()
	{
		if (grounded) {
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Strife"), 0, Input.GetAxis("Vertical"));
			transform.eulerAngles += new Vector3(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= _moveSpeed;
			
			Vector3 velocity = rigidBody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
			
			if(canJump && Input.GetButton("Jump"))
			{
				rigidBody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z );
			}
		}
		
		rigidBody.AddForce (new Vector3 (0, -gravity * rigidBody.mass, 0));
		
		grounded = false; 
	}
	float CalculateJumpVerticalSpeed()
	{
		return Mathf.Sqrt (2 * jumpHeight * gravity);
	}
}
