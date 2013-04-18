using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float horizontalMovementSpeed = 5f;
	public float gravity = -10f;
	bool jumping = false;
	bool falling = false;
	bool climbing = false;
	CharacterController characterController;

	// Use this for initialization
	void Start () 
	{
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{		
		float x = 0f;
		float y = -1f;
		float z = 0f;
		
		if (Input.GetKeyDown(KeyCode.Space) == false && jumping != true)
		{
			StartCoroutine ("Fall");
		}
		
		if (jumping == true)
		{
			y = 5f;
		}
		if (falling == true)
		{
			y = -7f;
		}
		
		if (climbing == true)
		{
			y = Input.GetAxis("Vertical");
			y *= horizontalMovementSpeed;
		}
		
		if (characterController.isGrounded == true)
		{
			falling = false;
			
			if (Input.GetKeyDown(KeyCode.Space) == true && jumping != true && falling != true)
			{
				StartCoroutine ("Jump");
			}
		}
		
		x = Input.GetAxis ("Horizontal");
		x *= horizontalMovementSpeed;
		Vector3 movementVector = new Vector3 (x, y, z);
		movementVector *= Time.deltaTime;
		characterController.Move(movementVector);		
	}
	
	void OnTriggerEnter (Collider characterController)
	{
		if (characterController.gameObject.tag == "Ladder")
		{
			climbing = true;
		}
	}
	
	void OnTriggerExit (Collider characterController)
	{
		if (characterController.gameObject.tag == "Ladder")
		{
			climbing = false;
			StartCoroutine ("EndClimb");
		}
	}
	
	IEnumerator Jump ()
	{
		jumping = true;
		yield return new WaitForSeconds (0.4f);
		jumping = false;
	}
	
	IEnumerator Fall ()
	{
		falling = true;
		yield return new WaitForSeconds (0.4f);
		falling = false;
	}
	
	IEnumerator EndClimb ()
	{
		jumping = true;
		yield return new WaitForSeconds (0.1f);
		jumping = false;
	}
}
