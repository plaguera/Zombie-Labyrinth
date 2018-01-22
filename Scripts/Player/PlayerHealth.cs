using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Player Health Script
 **/
public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.

	public delegate void OnDeath ();							// Delegate triggered when player dies.
	public static event OnDeath onDeath;

	Animator anim;                                              // Reference to the Animator component.
	private AudioSource audioSource;									// Reference to Audio Source Component
	bool isDead = false;										// Has the player been killed
	private AudioSource punchAudio;


	void Start(){
		audioSource = gameObject.AddComponent<AudioSource>();
		punchAudio = gameObject.AddComponent<AudioSource>();
		audioSource.clip = Resources.Load("power-up") as AudioClip;
		punchAudio.clip = Resources.Load("punch") as AudioClip;
		audioSource.reverbZoneMix = 1.0f;
		audioSource.volume = 0.3f;
		punchAudio.volume = 0.1f;
		MaxHealth ();
	}

	void Awake () {
		anim = GetComponent <Animator> ();
		audioSource = GetComponent <AudioSource> ();
		currentHealth = startingHealth;
		healthSlider.value = currentHealth;
		isDead = false;
		CameraController.restartGame += MaxHealth;
		CameraController.restartGame += Revive;
	}


	void Update () {}

	// Modify Player Health if Positive, Health is Subtracted, if Negative, health is Added
	public void TakeDamage (int amount) {
		if(amount < 0)
			audioSource.Play();
		else
			punchAudio.Play();
		currentHealth -= amount;
		if (currentHealth < 0)
			currentHealth = 0;
		else if (currentHealth > 100)
			currentHealth = 100;
		else if (currentHealth <= 25)
			ChangeSliderColor (Color.red);
		else if (currentHealth <= 50)
			ChangeSliderColor (new Color (255, 153, 0));
		else
			ChangeSliderColor (Color.green);

		healthSlider.value = currentHealth;
		ChangeSliderText(currentHealth.ToString());

		if (currentHealth == 0 && !isDead)
			Die ();
		if (amount > 0) {
			GetComponentInChildren<ParticleSystem> ().Play ();
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraController> ().FlashColor (Color.red);
		} else {
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraController> ().FlashColor (Color.green);
		}
	}

	// Restore Health to Max
	public void MaxHealth() {
		currentHealth = startingHealth;
		healthSlider.value = currentHealth;
		ChangeSliderColor (Color.green);
		ChangeSliderText(currentHealth.ToString());
	}

	// Kill Player
	void Die () {
		anim.SetTrigger ("Die");
		gameObject.GetComponent<PlayerController> ().Lock ();
		isDead = true;
		GameController.LEVEL = 1;
		onDeath.Invoke ();
	}

	// Set Player as Alive
	void Revive() { isDead = false; }
	// Set Player as Dead
	void Kill() { isDead = true; }
	// Change HealthBar Background Color
	void ChangeSliderColor(Color color) { healthSlider.transform.Find ("FillArea/Fill").GetComponent<Image> ().color = color; }
	// Change HealthBar Text Amount of Health
	void ChangeSliderText(string text) { healthSlider.transform.Find ("Text").GetComponent<Text> ().text = text; }

}
