using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	Rigidbody rigidbody3D;
	public float speed = 4;
	public float jump = 0f;
	private float vertMove = 0f;

	void Start () 
	{
		
		rigidbody3D = GetComponent<Rigidbody>();

	}

	void FixedUpdate () 
	{
		vertMove = 0;	

		// Checks for user inputs.
		float horizontal = Input.GetAxis("Horizontal");
		if (Input.GetKeyDown ("space")) {
			
			vertMove = jump;

		}



		Vector3 movement = new Vector3(horizontal, vertMove, 0);

		// Makes the player move.
		rigidbody3D.AddForce(movement * speed / Time.deltaTime);


		// Limits the speed of the rigidbody.
		if (rigidbody3D.velocity.magnitude > speed)
		{
			rigidbody3D.velocity = rigidbody3D.velocity.normalized * speed;
		}




	}
}