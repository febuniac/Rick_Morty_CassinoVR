using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetArea : MonoBehaviour {
	public GameObject[] chips;
	public TextMesh betText;
	private float betValue;
	private float playerMoney;
	public Transform[] chipsPos;
	public TextMesh moneyText;
	private List<GameObject> chipsI = new List<GameObject>();

	// Use this for initialization
	void Start () {
		betValue = 0;
		playerMoney = 500;
		moneyText.text = "Your Money: $" + playerMoney;
	}
	
	private void OnGameEnd(){

		GameObject[] chipsRed = GameObject.FindGameObjectsWithTag ("ChipRed");
		GameObject[] chipsBlue = GameObject.FindGameObjectsWithTag ("ChipBlue");
		GameObject[] chipsGreen = GameObject.FindGameObjectsWithTag ("ChipGreen");
		GameObject[] chipsBlack = GameObject.FindGameObjectsWithTag ("ChipBlack");

		foreach (GameObject chip in chipsRed)
			GameObject.Destroy (chip);

		foreach (GameObject chip in chipsGreen)
			GameObject.Destroy (chip);

		foreach (GameObject chip in chipsBlack)
			GameObject.Destroy (chip);

		foreach (GameObject chip in chipsBlue)
			GameObject.Destroy (chip);
		
		Instantiate (chips[0], chipsPos[0].position, chipsPos[0].rotation);
		Instantiate (chips[1], chipsPos[1].position, chipsPos[1].rotation);
		Instantiate (chips[2], chipsPos[2].position, chipsPos[2].rotation);
		Instantiate (chips[3], chipsPos[3].position, chipsPos[3].rotation);
		betValue = 0;
		betText.text = "Your Bet: \n  $" + betValue;
	}

	private void ResetChips(){
		for (int i=0; i<chipsI.Count ;i++) {
			Destroy (chipsI[i]);
		}
	}

	private void OnTriggerEnter(Collider col){
		if (col.tag == "ChipRed") {
			betValue += 10;
			playerMoney -= 10;
			if (playerMoney >= 0) {
				betText.text = "Your Bet: \n  $" + betValue;
				moneyText.text = "Your Money: $" + playerMoney;
				Instantiate (chips[3], chipsPos[3].position, chipsPos[3].rotation);
			}
		}

		else if (col.tag == "ChipGreen") {
			betValue += 25;
			playerMoney -= 25;
			if (playerMoney >= 0) {
				betText.text = "Your Bet: \n  $" + betValue;
				moneyText.text = "Your Money: $" + playerMoney;
				Instantiate (chips[1], chipsPos[1].position, chipsPos[1].rotation);
			}
		}

		if (col.tag == "ChipBlue") {
			betValue += 500;
			playerMoney -= 500;
			if (playerMoney >= 0) {
				betText.text = "Your Bet: \n  $" + betValue;
				moneyText.text = "Your Money: $" + playerMoney;
				Instantiate (chips[2], chipsPos[2].position, chipsPos[3].rotation);
			} else {
				
			}
		}

		if (col.tag == "ChipBlack") {
			print ("BLACK");
			betValue += 100;
			playerMoney -= 100;
			if (playerMoney >= 0) {				
				betText.text = "Your Bet: \n  $" + betValue;
				moneyText.text = "Your Money: $" + playerMoney;
				Instantiate (chips[0], chipsPos[0].position, chipsPos[0].rotation);
			}
		}
		chipsI.Add (col.gameObject);
	}

	void OnWin() {
		playerMoney += betValue*2;
		betValue = 0;
		moneyText.text = "Your Money: $" + playerMoney;
		betText.text = "Your Bet: \n  $" + 0;
		ResetChips ();
	}

	void OnLose() {
		betValue = 0;
		betText.text = "Your Bet: \n  $" + 0;
		ResetChips ();
	}

	void OnTie() {
		playerMoney += betValue;
		betValue = 0;
		moneyText.text = "Your Money: $" + playerMoney;
		betText.text = "Your Bet: \n  $" + 0;
		ResetChips ();
	}

	void OnSurrender() {
		betValue = 0;
		playerMoney += betValue / 2;
		moneyText.text = "Your Money: $" + playerMoney;
		betText.text = "Your Bet: \n  $" + 0;
		ResetChips ();
	}

		
//	private void spawnChip(int chip){
//		if (chip == 3){
//			betValue = 10;
//		}
//		else if (chip == 1){
//			betValue = 25;
//		}
//		else if (chip == 2){
//			betValue = 0;
//		}
//		else if (chip == 0){
//			betValue = 10;
//		}
//		betValue += 10;
//		betText.text = "Your Bet:" + betValue;
//		Instantiate (chips[chip], chipsPos[chip].position, chipsPos[chip].rotation);
//		
//	}
}
