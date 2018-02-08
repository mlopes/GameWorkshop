using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header("Ship Settings")]
	public float acceleration = 50f;
	public float maximumSpeed = 10f;
	public float rotationFactor = 50f;

	[Header("Fire Settings")]
	public float fireRate;

	[Header("Associated Objects")]
	public GameObject shot;
	public Transform shotSpawn;

		
	private Rigidbody rb;
	private string horizontalAxis;
	private string verticalAxis;
	private string fireButton;

	private float timeWhenItCanFireAfain;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();

		if(name == "Player1") {
			horizontalAxis = "Horizontal";
			verticalAxis = "Vertical";
			fireButton = "Fire1";
		} else {
			horizontalAxis = "HorizontalP2";
			verticalAxis = "VerticalP2";
			fireButton = "Fire2";
		}
	}

	void Update()
	{
		if (Input.GetButton(fireButton) && Time.time > timeWhenItCanFireAfain) {
			timeWhenItCanFireAfain = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void FixedUpdate ()
	{
		Rotate ();
		Move ();
		Clamp ();
	}

	void Rotate ()
	{
		float rotationDirection = Input.GetAxis (horizontalAxis);

		if (!rb.velocity.Equals (Vector3.zero)) {
			transform.Rotate (new Vector3 (0, rotationDirection, 0) * rotationFactor * Time.deltaTime);
		}
	}

	void Move ()
	{
		bool isAccelerating = Input.GetButton (verticalAxis);

		if (rb.velocity.magnitude > maximumSpeed) {
			rb.AddForce ((transform.forward * -1) * (rb.velocity.magnitude - maximumSpeed));
		} else if (isAccelerating) {
			rb.AddForce (transform.forward * acceleration);
		}
	}

	void Clamp ()
	{
		if (transform.position.z > 9f || transform.position.z < -9f || transform.position.x > 12f || transform.position.x < -12f) {
			rb.velocity *= -1;
		}

		rb.position = new Vector3 (Mathf.Clamp (rb.position.x, -12f, 12f), 0.0f, Mathf.Clamp (rb.position.z, -9f, 9f));
	}
}
