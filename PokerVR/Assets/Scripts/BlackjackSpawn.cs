using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;



public class BlackjackSpawn : MonoBehaviour {
	public GameObject[] spawnees;//pode
	public Text winPhrase;
	public Transform[] tableSpawnPos;
	public Text money;
	public GameObject[] spawned;//ja nasceu
	public Transform[] spawnPos;
	private int playerMoney = 100;
	public GameObject betArea;
	private int cp;
	private int sp;


	int randomInt;
	int[] cardPlayerNumber;
	int[] cardDealerNumber;
	GameObject[] cardPlayer;
	GameObject[] cardDealer;
	GameObject[] cardTable;
	int state = 0;
	bool controllerPressed = false;





	void Awake(){
		spawned = new GameObject[spawnees.Length];
		cardPlayer = new GameObject[10];
		cardDealer = new GameObject[2];
		cardTable = new GameObject[5];


	}

	void Start()
	{  
		// recupera referência para o controle
		//trackedObj = controller.GetComponent<SteamVR_TrackedObject>();
		SpawnCards();
		//money = GameObject.Find ("MoneyText").;
	}

	void Update()
	{


		var rotationVector = spawnees [0].transform.rotation.eulerAngles;
		rotationVector.x = -90;

		//Script X = this.GetComponent<Valve.VR.InteractionSystem.Hand>();

		//Debug.Log (X.GetStandardInteractionButtonDown());

		if (ViveInput.GetPressDown (HandRole.RightHand, ControllerButton.Pad)) {
			Vector3 pos = tableSpawnPos [sp].position;
			//pos.x += 1;
			randomInt = Random.Range (0, spawnees.Length);
			cardPlayer [cp] = Instantiate (spawnees [randomInt], pos, Quaternion.Euler (rotationVector)) as GameObject;
			spawned [sp] = cardPlayer [cp];
			cp += 1;
			sp += 1;
		}

	}



	void SpawnCards()
	{
		var rotationVector = spawnees [0].transform.rotation.eulerAngles;
		rotationVector.x = -90;

		//Cartas Dealer
		randomInt = Random.Range(0, spawnees.Length);
		cardDealer[0] = Instantiate(spawnees[randomInt], spawnPos[0].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [0] = cardDealer[0];

		randomInt = Random.Range(0, spawnees.Length);
		cardDealer[1] = Instantiate(spawnees[randomInt], spawnPos[1].position, spawnees[randomInt].transform.rotation) as GameObject;
		spawned [1] = cardDealer[1];

		//Cartas jogador
		randomInt = Random.Range(0, spawnees.Length);
		cardPlayer[0] = Instantiate(spawnees[randomInt], spawnPos[2].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [2] = cardPlayer[0];

		randomInt = Random.Range(0, spawnees.Length);
		cardPlayer[1] = Instantiate(spawnees[randomInt], spawnPos[3].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [3] = cardPlayer [1];

		cp = 2;
		sp = 4;
	}
}
