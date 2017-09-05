using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardSpawn : MonoBehaviour {
    public GameObject[] spawnees;
	public Text winPhrase;
	public GameObject[] spawned = new GameObject[2];
    public Transform spawnPos1;
    public Transform spawnPos2;
    private SteamVR_TrackedObject trackedObj;  // referência para o controle

    int randomInt;
	int cardPlayerNumber;
	int cardDealerNumber;


    private SteamVR_Controller.Device Controller
    {  // Properties para o controle
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Start()
    {                         // recupera referência para o controle
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
		if (Controller.GetHairTriggerDown ()) {
			for (int i = 0; i < spawned.Length; i++) {
				if (spawned [i]) {
					print(spawned[i].transform.name);
					Destroy (spawned [i]);
				}
			}
			SpawnCards();
		}
    }


    void SpawnCards()
    {
        randomInt = Random.Range(0, spawnees.Length);
		GameObject cardDealer = Instantiate(spawnees[randomInt], spawnPos1.position, spawnees[randomInt].transform.rotation) as GameObject;
		spawned [0] = cardDealer;

        randomInt = Random.Range(0, spawnees.Length);
		GameObject cardPlayer = Instantiate(spawnees[randomInt], spawnPos2.position, spawnees[randomInt].transform.rotation) as GameObject;
		spawned [1] = cardPlayer;

		if (cardPlayer.GetComponent<CardValue>().cardValue > cardDealer.GetComponent<CardValue>().cardValue) {
			winPhrase.text = "You Win";
			print ("You Lose");
		} else {
			winPhrase.text = "You Lose";
			print ("You Win");
		}
    }
}
