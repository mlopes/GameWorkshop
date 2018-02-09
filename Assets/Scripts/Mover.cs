using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed = 10f;
	public float lifetime = .3f;

	private float expiryTime;

	void Start () {
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
		expiryTime = Time.time + lifetime;
	}

	void FixedUpdate () {
		if(Time.time >= expiryTime) {
			Destroy (gameObject);
		}
	}
}
