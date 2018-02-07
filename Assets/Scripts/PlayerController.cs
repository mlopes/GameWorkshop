using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float acceleration = 50f;
	public float maximumSpeed = 10f;
	public float rotationFactor = 50f;
		
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		bool isAccelerating;
		bool isRotatingLeft;
		bool isRotatingRight;

		if (name == "Player1") {
			isAccelerating = Input.GetKey (KeyCode.W);
			isRotatingLeft = Input.GetKey (KeyCode.A);
			isRotatingRight = Input.GetKey (KeyCode.D);
		} else {
			isAccelerating = Input.GetKey (KeyCode.I);
			isRotatingLeft = Input.GetKey (KeyCode.J);
			isRotatingRight = Input.GetKey (KeyCode.L);
		}

		if (!rb.velocity.Equals (Vector3.zero)) {
			if (isRotatingLeft) {
				transform.Rotate (new Vector3(0, -1, 0) * rotationFactor * Time.deltaTime);
			} else if (isRotatingRight) {
				transform.Rotate (new Vector3(0, 1, 0) * rotationFactor * Time.deltaTime);
			}
		}

		if(rb.velocity.magnitude > maximumSpeed) {
			rb.AddForce ((transform.forward * -1) * (rb.velocity.magnitude - maximumSpeed));
		} else if (isAccelerating) {
			rb.AddForce (transform.forward * acceleration);
		}


		if(
			transform.position.z > 9f ||
			transform.position.z < -9f ||
			transform.position.x > 12f ||
			transform.position.x < -12f ) {

			rb.velocity *= -1;

			rb.position = new Vector3
				(
					Mathf.Clamp (rb.position.x, -12f, 12f),
					0.0f,
					Mathf.Clamp (rb.position.z, -9f, 9f)
				);
			
		}
	}
}
