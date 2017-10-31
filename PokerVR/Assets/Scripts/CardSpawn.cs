using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;



public class Cardspawns : MonoBehaviour {
    public GameObject[] spawnees;//pode
	public Text winPhrase;
	public Transform[] tableSpawnPos;
	public Text money;
	public GameObject[] spawned;//ja nasceu
	public Transform[] spawnPos;
	private int playerMoney = 100;
	public GameObject betArea;


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
		cardPlayer = new GameObject[2];
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

		var rotationVector = spawnees[0].transform.rotation.eulerAngles;;

		//Script X = this.GetComponent<Valve.VR.InteractionSystem.Hand>();

		//Debug.Log (X.GetStandardInteractionButtonDown());
	
		if (ViveInput.GetPressDown (HandRole.RightHand, ControllerButton.Pad)) {	
			if (state < 4) {
				state += 1;
			} else {
				state = 0;
				betArea.SendMessage ("OnGameEnd");

			}

			if (state == 0) {
				controllerPressed = true;
				for (int i = 0; i < spawned.Length; i++) {
					if (spawned [i]) {
						Destroy (spawned [i]);
					}
				}
				SpawnCards ();

			} else if (state == 1) {
				rotationVector.x = -90;

				for (int i = 0; i < 3; i++) {
					cardTable [i].transform.rotation = Quaternion.Euler (rotationVector);			
				}

			} else if (state == 2) {
				rotationVector.x = -90;
				cardTable [3].transform.rotation = Quaternion.Euler (rotationVector);			


			} else if (state == 3) {
				rotationVector.x = -90;
				cardTable [4].transform.rotation = Quaternion.Euler (rotationVector);		
			} else if (state == 4) {
				rotationVector.x = -90;
				cardDealer [0].transform.rotation = Quaternion.Euler (rotationVector);	
				cardDealer [1].transform.rotation = Quaternion.Euler (rotationVector);



			}

		}

	}


	void ConvertCardValue(int cardValue){
		char newValue;
		if (cardValue < 10) {
			//newValue = (char)cardValue + 1;
			newValue = 'a';
		} else if (cardValue == 10) {
			newValue = 'j';
		} else if (cardValue == 11) {
			newValue = 'q';
		} else if (cardValue == 12) {
			newValue = 'k';
		} else if (cardValue == 13) {
			newValue = 'a';
		}
	}


    void SpawnCards()
	{
		var rotationVector = spawnees [0].transform.rotation.eulerAngles;;
		rotationVector.x = 90;

        //Cartas Dealer
		randomInt = Random.Range(0, spawnees.Length);
		cardDealer[0] = Instantiate(spawnees[randomInt], spawnPos[0].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [0] = cardDealer[0];

		randomInt = Random.Range(0, spawnees.Length);
		cardDealer[1] = Instantiate(spawnees[randomInt], spawnPos[1].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [1] = cardDealer[1];

		//Cartas jogador
        randomInt = Random.Range(0, spawnees.Length);
		cardPlayer[0] = Instantiate(spawnees[randomInt], spawnPos[2].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [2] = cardPlayer[0];

		randomInt = Random.Range(0, spawnees.Length);
		cardPlayer[1] = Instantiate(spawnees[randomInt], spawnPos[3].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [3] = cardPlayer [1];

		//Cartas da mesa
		randomInt = Random.Range(0, spawnees.Length);
		cardTable[0] = Instantiate(spawnees[randomInt], tableSpawnPos[0].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [4] = cardTable [0];

		randomInt = Random.Range(0, spawnees.Length);
		cardTable[1] = Instantiate(spawnees[randomInt], tableSpawnPos[1].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [5] = cardTable [1];

		randomInt = Random.Range(0, spawnees.Length);
		cardTable[2] = Instantiate(spawnees[randomInt], tableSpawnPos[2].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [6] = cardTable [2];

		randomInt = Random.Range(0, spawnees.Length);
		cardTable[3] = Instantiate(spawnees[randomInt], tableSpawnPos[3].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [7] = cardTable [3];

		randomInt = Random.Range(0, spawnees.Length);
		cardTable[4] = Instantiate(spawnees[randomInt], tableSpawnPos[4].position, Quaternion.Euler (rotationVector)) as GameObject;
		spawned [8] = cardTable [4];
    }
}
