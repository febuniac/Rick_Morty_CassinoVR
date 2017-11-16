﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;



public class BlackjackSpawn : MonoBehaviour {
	public GameObject[] spawnees;//pode
	public TextMesh winPhrase;
	public Text money;
	public GameObject[] spawned;//ja nasceu
	private int playerMoney = 100;
	public GameObject betArea;
	private int cp;
	private int sp;
	private int pp;
	private int dp;
	private int cd;
	public Transform[] dealerPos;
	public Transform[] playerPos;

	public Text winText;
	int randomInt;
	int[] cardPlayerNumber;
	int[] cardDealerNumber;
	GameObject[] cardPlayer;
	GameObject[] cardDealer;
	GameObject[] cardTable;
	int state = 0;
	bool controllerPressed = false;
	int playerCount;
	int dealerCount;

	public Button hitButton;
	public Button standButton;
	public Button surrenderButton;
	public Button splitButton;
	public Button betButton;
	public Button againButton;

	public GameObject hitCanvas;
	public GameObject betCanvas;

	public GameObject BetArea;


	void Awake(){
		spawned = new GameObject[spawnees.Length];
		cardPlayer = new GameObject[10];
		cardDealer = new GameObject[10];
		cardTable = new GameObject[5];
		playerCount = 0;
		dealerCount = 0;


	}

	void Start()
	{  
		winPhrase.text = "Place your bet!";
	}

	void OnStand() {

		var rotationVector = spawnees [0].transform.rotation.eulerAngles;
		rotationVector.x = -90;

		Vector3 dpos = dealerPos [dp].position;

		cardDealer [1].transform.rotation = Quaternion.Euler (rotationVector);
		print (" cicici");
		while (dealerCount < 17) {
			Debug.Log(" dentro do while");
			

			randomInt = Random.Range (0, spawnees.Length);
			cardDealer [dp] = Instantiate (spawnees [randomInt], dpos, Quaternion.Euler (rotationVector)) as GameObject;
			spawned [sp] = cardDealer [dp];
			dealerCount += convertBJ (cardDealer [cd].GetComponent<CardValue> ().cardValue);
			print ("Carta Dealer: " + convertBJ (cardDealer [cd].GetComponent<CardValue> ().cardValue));
			print ("Dealer Count: " + dealerCount);

			dp += 1;
			sp += 1;
			cd += 1;
		}
		SendMessage ("OnSettle");
		print ("saiu do while");
		print (dealerCount);
		OnSettle ();

		againButton.gameObject.SetActive (true);
		hitButton.gameObject.SetActive (false);
		standButton.gameObject.SetActive (false);
		surrenderButton.gameObject.SetActive (false);
		splitButton.gameObject.SetActive (false);

	}

	void OnAgain() {
		againButton.gameObject.SetActive (false);
		betButton.gameObject.SetActive (true);
		Restart ();
	}


	void OnSettle() {
		print ("saiu do while");

		if (dealerCount <= 21 & dealerCount > playerCount) {
			//SendMessage ("OnWin");
			BetArea.GetComponent<BetArea>().SendMessage("OnLose");
			winPhrase.text = "You Lose!";
		} else {
			BetArea.GetComponent<BetArea>().SendMessage("OnWin");
			winPhrase.text = "You Win!";
		}
	}

	void OnHit () {

		var rotationVector = spawnees [0].transform.rotation.eulerAngles;
		rotationVector.x = -90;

		Vector3 pos = playerPos [pp].position;
		randomInt = Random.Range (0, spawnees.Length);
		cardPlayer [cp] = Instantiate (spawnees [randomInt], pos, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [sp] = cardPlayer [cp];
		playerCount += convertBJ(cardPlayer [cp].GetComponent<CardValue> ().cardValue);
		if (playerCount > 21) {
			winPhrase.text = "You Lose!";

			BetArea.GetComponent<BetArea>().SendMessage("OnLose");

			againButton.gameObject.SetActive (true);
			hitButton.gameObject.SetActive (false);
			standButton.gameObject.SetActive (false);
			surrenderButton.gameObject.SetActive (false);
			splitButton.gameObject.SetActive (false);
		}
		cp += 1;
		pp += 1;
		sp += 1;
	}

	void OnBet() {
		winPhrase.text = "";
		betButton.gameObject.SetActive (false);
		hitButton.gameObject.SetActive (true);
		standButton.gameObject.SetActive (true);
		surrenderButton.gameObject.SetActive (true);
		splitButton.gameObject.SetActive (true);
		SpawnCards ();
	}

	void Restart() {
		print ("restart");
		foreach (GameObject card in cardDealer) {
			Destroy (card);
		}
		foreach (GameObject card in cardPlayer) {
			Destroy (card);
		}
		foreach (GameObject card in cardTable) {
			Destroy (card);
		}
		dealerCount = 0;
		playerCount = 0;
		sp = 0;
		winPhrase.text = "";
	}


	void SpawnCards()
	{
		var rotationVector = spawnees [0].transform.rotation.eulerAngles;
		rotationVector.x = -90;

		//Cartas Dealer
		randomInt = Random.Range(0, spawnees.Length);
		cardDealer[0] = Instantiate(spawnees[randomInt], dealerPos[0].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [0] = cardDealer[0];

		randomInt = Random.Range(0, spawnees.Length);
		cardDealer[1] = Instantiate(spawnees[randomInt], dealerPos[1].position, spawnees[randomInt].transform.rotation) as GameObject;
		spawned [1] = cardDealer[1];

		//Cartas jogador
		randomInt = Random.Range(0, spawnees.Length);
		cardPlayer[0] = Instantiate(spawnees[randomInt], playerPos[0].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [2] = cardPlayer[0];

		randomInt = Random.Range(0, spawnees.Length);
		cardPlayer[1] = Instantiate(spawnees[randomInt], playerPos[1].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [3] = cardPlayer [1];

		playerCount += convertBJ(cardPlayer [0].GetComponent<CardValue> ().cardValue);
		playerCount += convertBJ(cardPlayer [1].GetComponent<CardValue> ().cardValue);
		dealerCount += convertBJ (cardDealer [0].GetComponent<CardValue> ().cardValue);
		dealerCount += convertBJ (cardDealer [1].GetComponent<CardValue> ().cardValue);

		print ("Player Count:" + playerCount);

		cp = 2;
		pp = 2;
		dp = 2;
		cd = 2;
	}


	int convertBJ (int cardValue) {
		int bjValue;
		if (cardValue >= 10) {
			bjValue = 10;
		} else {
			bjValue = cardValue + 1;
		}

		return bjValue;
	}
}
