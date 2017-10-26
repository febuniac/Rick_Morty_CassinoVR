using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardValue : MonoBehaviour {
	public int cardValue;
	public string naipe;

	void Awake () {
		naipe = transform.name;
		naipe = naipe.Substring (3, naipe.Length);
	}
}
