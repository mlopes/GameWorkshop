using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour
{
	public float tumble = .8f;

	void Start ()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble; 
	}
}