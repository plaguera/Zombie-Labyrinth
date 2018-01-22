using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Potion Controller Class
 **/
public class PotionScript : MonoBehaviour {

	GameObject player;								// Player Reference
	public int healthRestore;						// Amount of Health Player Gets from Potion

	// Use this for initialization
	void Start () { player = GameObject.FindGameObjectWithTag ("Player"); }
	// Update is called once per frame
	void Update () {}

	void OnCollisionEnter(Collision other) {
		// If Player Touches Potion, Give Health to Player and Destroy Potion
		if (other.gameObject == player) {
			player.GetComponent<PlayerHealth> ().TakeDamage (-healthRestore);
			Destroy (gameObject);
		}
	}
}
