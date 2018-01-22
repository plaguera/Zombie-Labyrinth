using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Enemy Health Script
 **/
public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.

	Animator anim;                              // Reference to the animator.
	ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
	CapsuleCollider capsuleCollider;            // Reference to the capsule collider.

	void Awake () {
		anim = GetComponent <Animator> ();
		hitParticles = GetComponentInChildren <ParticleSystem> ();
		currentHealth = startingHealth;
	}

	void Update () {}

	// Subtract health from enemy, play bleed particle system and die if health reaches 0
	public void TakeDamage (int amount) {
		currentHealth -= amount;
		GetComponentInChildren<ParticleSystem> ().Play();
		if(currentHealth <= 0)
			Death ();
	}

	// Play Death Animation
	void Death () { anim.SetTrigger ("Die"); }
}
