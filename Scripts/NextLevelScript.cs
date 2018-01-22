using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * Next Level Script
 **/
public class NextLevelScript : MonoBehaviour {

	GameObject player;								// Player Reference
	PlayerController playerController;				// Player Controller Reference
	public delegate void OnNextLevel ();			// Delegate Called when Next Level is needed
	public static event OnNextLevel onNextLevel;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController> ();

		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = Resources.Load("teleport") as AudioClip;
		audioSource.volume = 0.2f;
	}

	// Update is called once per frame
	void Update () {}

	void OnTriggerEnter(Collider other) {
		// If player has the current level's key and steps into teleport gate advance level
		if (other.gameObject == player && playerController.hasKey) {
			GameController.LEVEL++;
			onNextLevel.Invoke ();
			audioSource.Play();
		}
	}
}
