using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetArea : MonoBehaviour {
	public GameObject[] chips;
	public TextMesh betText;
	private int betValue;
	private Transform[] chipsPos;

	// Use this for initialization
	void Start () {
		betValue = 0;
		chipsPos = new Transform[4];
		for (int i = 0; i < chipsPos.Length; i++) {
			chipsPos [i] = chips [i].transform;
		}
	}
	

	private void OnTriggerEnter(Collider col){
		if (col.transform.name == "Chip") {
			betValue += 10;
			betText.text = "Your Bet:" + betValue;
			Instantiate (chips[3], chipsPos[3].position, chipsPos[3].rotation);
		}
	}

	private void OnTriggerStay(Collider col){

	}

	private void OnTriggerExit(Collider col){

	}
}
