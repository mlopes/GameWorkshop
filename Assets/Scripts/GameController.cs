using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject player1Prefab;
	public GameObject player2Prefab;

	private GameObject player1;
	private GameObject player2;

	void Start () {
		player1 = (GameObject)Instantiate (player1Prefab, new Vector3 (-2, 0, 0), Quaternion.identity);
		player2 = (GameObject)Instantiate (player2Prefab, new Vector3 (2, 0, 0), Quaternion.identity);	
	}
}
