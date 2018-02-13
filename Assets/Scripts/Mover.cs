using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed = 10f;
	public float lifetime = .3f;

	void Start () {
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * Random.Range(speed-1, speed+1);
		Destroy (gameObject, lifetime);
	}
}
