using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Key Controller Class
 **/
public class KeyScript : MonoBehaviour {

	GameObject player;	// Player Reference

	// Use this for initialization
	void Start () { player = GameObject.FindGameObjectWithTag ("Player"); }
	// Update is called once per frame
	void Update () {}

	void OnCollisionEnter(Collision other) {
		// If Player Touches Key, Give Key to Player and Destroy Key
		if (other.gameObject == player) {
			player.GetComponent<PlayerController> ().GiveKey ();
			Destroy (gameObject);
		}
	}
}
