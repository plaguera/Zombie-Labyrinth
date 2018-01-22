using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Enemy Controller Script
 **/
public class EnemyController : MonoBehaviour {

	public float speed = 0.01f;					// Walking Speed
	public float runSpeed = 0.1f;				// Running Speed
	public float rotationSpeed = 15.0f;			// Rotation Speed
	public int attackDamage = 10;               // The amount of health taken away per attack.
	public bool isAttacking = false;			// Is Attacking Animation Currently Playing
	public bool alreadyAttacked = false;		// Has Enemy Attacked Already in the Current Animation

	Animator anim;                              // Reference to the animator component.
	GameObject player;                          // Reference to the player GameObject.
	EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	PlayerHealth playerHealth;                  // Reference to the player's health.

	void Awake () {
		anim = GetComponent <Animator> ();
		enemyHealth = GetComponent<EnemyHealth>();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
	}

	void Update () {
		float distance = DistanceToPlayer ();
		if (distance != -1.0f) ChasePlayer ();
		else RandomMovement ();
		anim.SetFloat ("DistanceToPlayer", distance);
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player" && !alreadyAttacked && isAttacking) {
			alreadyAttacked = true;
			playerHealth.TakeDamage (attackDamage + Random.Range(0, 5));
		}
	}

	// Raycast to Player and return the distance if there are no obstacles between them
	float DistanceToPlayer() {
		RaycastHit hit;
		Vector3 origin = GetComponent<CapsuleCollider>().transform.position + GetComponent<CapsuleCollider>().center;
		Vector3 target = player.GetComponent<CapsuleCollider>().transform.position + player.GetComponent<CapsuleCollider>().center;
		Vector3 direction = target - origin;

		if (Physics.Raycast (origin, direction, out hit) && hit.transform.tag == "Player") {
			Debug.DrawLine (origin, target, Color.red);
			float step = rotationSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards (transform.forward, direction, step, 0.0f);
			transform.rotation = Quaternion.LookRotation (newDir);
			return hit.distance;
		}
		return -1.0f;
	}

	// Rotate to face the player and run forward
	void ChasePlayer() {
		Vector3 direction = player.transform.position - transform.position;
		float step = rotationSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, direction, step, 0.0f);
		transform.rotation = Quaternion.LookRotation (newDir);
		transform.Translate (Vector3.forward * runSpeed * Time.timeScale);
	}

	// Wander, Move and Rotate Randomly
	void RandomMovement() {
		float turnProb = Random.Range (0.0f, 1.0f);
		transform.Translate (Vector3.forward * speed * Time.timeScale);
		if (turnProb < 0.9999f)
			return;

		float angle = Random.Range (-180.0f, 180.0f);
		for (int i = 0; i < Mathf.Abs (angle); i++) {
			transform.Rotate (Vector3.up, rotationSpeed * Time.timeScale);
		}
	}

}
