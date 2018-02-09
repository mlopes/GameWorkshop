using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header("Ship Settings")]
	public float acceleration = 50f;
	public float maximumSpeed = 10f;
	public float rotationFactor = 50f;
	public float maximumAngularSpeed = 10f;

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

	private void Rotate ()
	{
		float rotationDirection = Input.GetAxis (horizontalAxis);

		if (rb.angularVelocity.magnitude > maximumAngularSpeed || rb.angularVelocity.magnitude < -maximumSpeed) {
			rb.AddTorque ((transform.up * -1) * (rb.angularVelocity.magnitude - maximumSpeed));
		} else {
			rb.AddTorque (transform.up * rotationDirection * rotationFactor * Time.deltaTime);
		}
	}

	private void Move ()
	{
		bool isAccelerating = Input.GetButton (verticalAxis);

		if (rb.velocity.magnitude > maximumSpeed) {
			rb.AddForce ((transform.forward * -1) * (rb.velocity.magnitude - maximumSpeed));
		} else if (isAccelerating) {
			rb.AddForce (transform.forward * acceleration);
		}
	}

	private void Clamp ()
	{
		BumpAgainstTheEdges ();

		ClampPosition ();

		ClampRotation ();
	}

	private void BumpAgainstTheEdges ()
	{
		if (transform.position.z > 9f || transform.position.z < -9f || transform.position.x > 12f || transform.position.x < -12f) {
			rb.velocity *= -1;
		}
	}

	private void ClampPosition ()
	{
		rb.position = new Vector3 (Mathf.Clamp (rb.position.x, -12f, 12f), 0.0f, Mathf.Clamp (rb.position.z, -9f, 9f));
	}

	private void ClampRotation ()
	{
		if (rb.rotation.y != 0) {
			Quaternion r = rb.rotation;
			r.x = 0;
			r.z = 0;
			float m = Mathf.Sqrt (r.x * r.x + r.y * r.y + r.z * r.z + r.w * r.w);
			rb.rotation = new Quaternion (r.x / m, r.y / m, r.z / m, r.w / m);
		}
	}
}
