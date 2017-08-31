using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawn : MonoBehaviour {
    public GameObject[] spawnees;
    public Transform spawnPos1;
    public Transform spawnPos2;
    public Transform spawnPos3;
    private SteamVR_TrackedObject trackedObj;  // referência para o controle

    int randomInt;


    private SteamVR_Controller.Device Controller
    {  // Properties para o controle
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {                         // recupera referência para o controle
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {
        SpawnCards();
    }


    void SpawnCards()
    {
        randomInt = Random.Range(0, spawnees.Length);
        Instantiate(spawnees[randomInt], spawnPos1.position, spawnPos1.rotation);
        randomInt = Random.Range(0, spawnees.Length);
        Instantiate(spawnees[randomInt], spawnPos2.position, spawnPos2.rotation);
        randomInt = Random.Range(0, spawnees.Length);
        Instantiate(spawnees[randomInt], spawnPos3.position, spawnPos3.rotation);
    }
}
