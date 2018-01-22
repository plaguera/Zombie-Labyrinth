using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Sword Controller Script
 **/
public class SwordController : MonoBehaviour {

	public bool alreadyAttacked = false;			// Has Player already attacked in current melee animation
	public int baseDamage = 25;						// Damage player does to zombies
	AudioClip microphoneInput;
	float level = 0.0f;

	// Use this for initialization
	void Start () {
		microphoneInput = Microphone.Start (Microphone.devices [0], true, 999, 44100);
	}
	
	// Update is called once per frame
	void Update () {
		GetMicrophoneLevel ();
	}

	void GetMicrophoneLevel() {
		int dec = 128;
		float[] waveData = new float[dec];
		int micPosition = Microphone.GetPosition (null) - (dec + 1);
		//Debug.Log ("Pos : " + micPosition);
		microphoneInput.GetData (waveData, Mathf.Abs(micPosition));
		float levelMax = 0;
		for (int i = 0; i < dec; i++) {
			float wavePeak = waveData [i] * waveData [i];
			if (levelMax < wavePeak)
				levelMax = wavePeak;
		}
		level = Mathf.Sqrt (Mathf.Sqrt (levelMax));
	}

	void OnCollisionEnter(Collision other) {
		// Hurt Enemy if Melee Animation is Playing, only once per animation
		if (other.gameObject.tag == "Enemy" && !alreadyAttacked && GameController.player.GetComponent<PlayerController>().isAttacking) {
			alreadyAttacked = true;
			int damage = baseDamage + (int)(baseDamage * (level / 2));
			Debug.Log (damage);
			other.gameObject.GetComponent<EnemyHealth> ().TakeDamage (damage + Random.Range(-5,5));
		}
	}
}
