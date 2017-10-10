using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;



public class CardSpawn : MonoBehaviour {
    public GameObject[] spawnees;
	public Text winPhrase;
	public Text money;
	public GameObject[] spawned;
	public Transform[] spawnPos;
	private int playerMoney = 100;


    int randomInt;
	int[] cardPlayerNumber;
	int[] cardDealerNumber;
	GameObject[] cardPlayer;
	GameObject[] cardDealer;

	bool controllerPressed = false;



	void Awake(){
		spawned = new GameObject[spawnees.Length];
		cardPlayer = new GameObject[2];
		cardDealer = new GameObject[2];

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


		//Script X = this.GetComponent<Valve.VR.InteractionSystem.Hand>();

		//Debug.Log (X.GetStandardInteractionButtonDown());
	

		if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Pad)) {
			controllerPressed = true;
			for (int i = 0; i < spawned.Length; i++) {
				if (spawned [i]) {
					Destroy (spawned [i]);
				}
			}
			SpawnCards();


		}

	}


    void SpawnCards()
    {
        randomInt = Random.Range(0, spawnees.Length);
		cardDealer[0] = Instantiate(spawnees[randomInt], spawnPos[0].position, spawnees[randomInt].transform.rotation) as GameObject;
		spawned [0] = cardDealer[0];

		randomInt = Random.Range(0, spawnees.Length);
		cardDealer[1] = Instantiate(spawnees[randomInt], spawnPos[1].position, spawnees[randomInt].transform.rotation) as GameObject;
		spawned [1] = cardDealer[1];

        randomInt = Random.Range(0, spawnees.Length);
		cardPlayer[0] = Instantiate(spawnees[randomInt], spawnPos[2].position, spawnees[randomInt].transform.rotation) as GameObject;
		spawned [2] = cardPlayer[0];

		randomInt = Random.Range(0, spawnees.Length);
		cardPlayer[1] = Instantiate(spawnees[randomInt], spawnPos[3].position, spawnees[randomInt].transform.rotation) as GameObject;
		spawned [3] = cardPlayer [1];

    }
}
