using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player Controller Script
 **/
public class PlayerController : MonoBehaviour {

	Animator anim;								// Player GameObject Animator
	AudioSource audio;							// Player GameObject Audio Source
	public float speedWalk = 3.0f;				// Walking Speed
	public float speedRun = 9.0f;				// Running Speed
	public float rotationSpeed = 100.0f;		// Rotation Speed

	[HideInInspector]public bool isAttacking = false;			// Is Player currently executing Melee Animation
	[HideInInspector]public bool hasKey = false;					// Does Player have the key to the current level
	[HideInInspector]public bool locked = false;					// Lock Player Controls

	[HideInInspector]public Texture keyImage;
	[HideInInspector]public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
		audio = GetComponent<AudioSource> ();
		PlayerHealth.onDeath += Lock;
		CameraController.restartGame += Unlock;
		CameraController.restartGame += anim.Rebind;
		NextLevelScript.onNextLevel += RemoveKey;
		keyImage = Resources.Load ("key-security") as Texture;

		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = Resources.Load("keys") as AudioClip;
		GetComponent<Rigidbody> ().freezeRotation = true;
	}

	// Update is called once per frame
	void Update () {
		if (locked)
			return;
		
		Vector3 look = Cardboard.SDK.HeadPose.Orientation.eulerAngles;
		transform.Rotate(0f, look.y, 0f);
		Cardboard.SDK.Recenter ();
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) ||
		   Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D)) {
			Move ();
		} else {
			anim.SetFloat ("speed", 0f);
		}
		if (Input.GetButton("Fire1") && !isAttacking) {
			Attack ();
		}

	}

	// Move Player in Direction of Pressed WASD and Run if LEFT SHIFT Pressed
	void Move() {
		float speed = speedWalk;
		if (Input.GetKey (KeyCode.LeftShift) || Cardboard.SDK.Triggered) {
			speed = speedRun;
		}
		float moveH = Input.GetAxis ("Horizontal");
		float moveV = Input.GetAxis ("Vertical");
		Vector3 direction = new Vector3 (moveH, 0f, moveV);
		transform.Translate (direction * speed * Time.deltaTime);
		anim.SetFloat ("speed", speed);
	}

	void Attack() {
		anim.SetTrigger ("Melee");
		isAttacking = true;
	}

	// Toggle Lock Key Controls
	public void ToggleLock() { locked = !locked; }

	// Lock Key and Mouse Controls and freeze all physic movement and rotation
	public void Lock() {
		locked = true;
		GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
	}

	// Unlock Key and Mouse Controls and freeze only X and Z Axis rotation
	public void Unlock() {
		locked = false;
		GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}

	// Give Key to Player
	public void GiveKey() { hasKey = true; audioSource.Play();}
	// Take Key Away from Player
	public void RemoveKey() { hasKey = false; }

	void OnGUI() {
		if(hasKey == true)
			GUI.DrawTexture(new Rect(50,200,50,50),keyImage);
	}
}
