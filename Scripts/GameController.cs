using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Game Controller Script
 **/
public class GameController : MonoBehaviour {

	public SpawnController spawnController;						// Spawn Controller Reference
	public MazeLoader mazeGenerator;							// Maze Generator Reference


	public delegate void DestroyInstances ();					// Delegate Called to Destroy Current Maze and Spawned Instances
	public static event DestroyInstances destroyInstances;

	public delegate void OnPause ();					// Delegate Called to Destroy Current Maze and Spawned Instances
	public static event OnPause onPause;

	public delegate void OnResume ();					// Delegate Called to Destroy Current Maze and Spawned Instances
	public static event OnResume onResume;

	MazeAlgorithm maze;											// Current Maze Object
	public static uint LEVEL = 1;								// Current Level

	[HideInInspector]public static GameObject player, camera;

	bool isPaused = false;
	bool hasBegun = false;

	// Use this for initialization
	void Start () {
		maze = mazeGenerator.GenerateMaze ();
		player = GameObject.FindGameObjectWithTag ("Player");
		camera = GameObject.FindGameObjectWithTag ("MainCamera");

		spawnController.SetDimensions (mazeGenerator.mazeRows, mazeGenerator.mazeRows, mazeGenerator.size);
		spawnController.Spawn ();

		SetDelegates ();
		//player.GetComponent<PlayerController> ().Lock ();
		//AudioListener.volume = 0f;
		//UnfreezeGame()
	}

	private void SetDelegates() {
		CameraController.restartGame += destroyInstances.Invoke;
		CameraController.restartGame += GenerateNewMaze;
		CameraController.restartGame += spawnController.Spawn;
		NextLevelScript.onNextLevel += destroyInstances.Invoke;
		NextLevelScript.onNextLevel += GenerateNewMaze;
		NextLevelScript.onNextLevel += spawnController.Spawn;

		onPause += camera.GetComponent<CameraController> ().EnablePauseMenu;
		onPause += player.GetComponent<PlayerController> ().Lock;
		onPause += FreezeGame;

		onResume += camera.GetComponent<CameraController> ().DisablePauseMenu;
		onResume += player.GetComponent<PlayerController> ().Unlock;
		onResume += UnfreezeGame;
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) || Cardboard.SDK.BackButtonPressed)
			PauseToggle ();
	}

	// Generate New Random Maze
	public void GenerateNewMaze() {
		maze = mazeGenerator.GenerateMaze ();
	}

	public static void StartGame() {
		camera.GetComponent<CameraController> ().DisableStartScreen ();
		camera.GetComponent<CameraController> ().EnableHUD ();
		player.GetComponent<PlayerController> ().Unlock ();
		AudioListener.volume = 1f;
		UnfreezeGame ();
	}

	// Toggle Pause Menu
	public void PauseToggle() {
		if (isPaused) {
			onResume.Invoke ();
			isPaused = false;
		} else {
			onPause.Invoke ();
			isPaused = true;
		}
	}

	// Quit Application
	public void Quit() { Application.Quit (); }

	public static void FreezeGame() { Time.timeScale = 0f; }

	public static void UnfreezeGame() { Time.timeScale = 1f; }

	public void FreezeAllAudio() {
		AudioSource[] audios = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource audio in audios) {
			audio.Stop ();
		}
	}

	public void UnfreezeAllAudio() {
		AudioSource[] audios = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource audio in audios) {
			audio.Play ();
		}
	}
}
