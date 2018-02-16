using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject player1Prefab;
	public GameObject player2Prefab;

	public Text player1ScoreText;
	public Text player2ScoreText;

	public float respawnTime = 1.2f;

	private GameObject player1;
	private GameObject player2;

	private int player1Score;
	private int player2Score;

	private bool respawnPlayer1;
	private bool respawnPlayer2;
	private float player1RespawnTimer;
	private float player2RespawnTimer;

	void Start () {
		player1Score = 0;
		player2Score = 0;
		player1ScoreText.text = "Player1: " + player1Score;
		player2ScoreText.text = "Player2: " + player2Score;
		respawnPlayer1 = false;
		respawnPlayer2 = false;

		player1 = (GameObject)Instantiate (player1Prefab, new Vector3 (-2, 0, 0), Quaternion.identity);
		player2 = (GameObject)Instantiate (player2Prefab, new Vector3 (2, 0, 0), Quaternion.identity);	
	}

	public void PlayerGotKilled(string whichPlayer) {
		if(whichPlayer == "Player1") {
			UpdatePlayer2Score ();

			respawnPlayer1 = true;
			player1RespawnTimer = Time.time + respawnTime;
		} else {
			UpdatePlayer1Score ();

			respawnPlayer2 = true;
			player2RespawnTimer = Time.time + respawnTime;
		}
	}

	void UpdatePlayer1Score() {
		player1Score += 1;
		player1ScoreText.text = "Player1: " + player1Score;
	}

	void UpdatePlayer2Score() {
		player2Score += 1;
		player2ScoreText.text = "Player2: " + player2Score;
	}

	void Update() {
		if(respawnPlayer1 && Time.time >= player1RespawnTimer) {
			respawnPlayer1 = false;
			player1 = (GameObject)Instantiate (player1Prefab, new Vector3 (-2, 0, 0), Quaternion.identity);
		}

		if(respawnPlayer2 && Time.time >= player2RespawnTimer) {
			respawnPlayer2 = false;
			player2 = (GameObject)Instantiate (player2Prefab, new Vector3 (2, 0, 0), Quaternion.identity);
		}
	}
}
