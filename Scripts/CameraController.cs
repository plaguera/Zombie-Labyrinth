using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Camera Controller Class
 **/
public class CameraController : MonoBehaviour {

	// Countdown duration before restarting game after death
	public static readonly float COUNTDOWN_SECONDS = 5.0f;
	// Camera auxiliar background image, used for flashing colors when taking damage
	public Image background;
	// Background Music Clips
	public AudioClip[] clips;

	// Starting Color when taking damage
	Color flashColor = Color.red;
	// End Color when taking damage
	Color transparent = new Color(0f, 0f, 0f, 0f);
	// Should camera show background color?
	bool flash = false;
	// Time transcurred after first flash
	float time = 0f;

	// Paused Game and Game Over Canvases
	[HideInInspector]public Canvas canvasPause, canvasGameOver, canvasStart, canvasHUD;
	// Camera Audio Source for Background Music
	AudioSource audioSource;
	// Time Counter when restarting Game
	float timeLeft = COUNTDOWN_SECONDS;

	// Delegate that will be called when player dies
	public delegate void RestartGame ();
	public static event RestartGame restartGame;


	// Use this for initialization
	void Start () {
		canvasStart = transform.Find ("CanvasStart").gameObject.GetComponent<Canvas> ();
		canvasPause = transform.Find ("CanvasPauseMenu").gameObject.GetComponent<Canvas> ();
		canvasGameOver = transform.Find ("CanvasGameOver").gameObject.GetComponent<Canvas> ();
		canvasHUD = transform.Find ("HUD").gameObject.GetComponent<Canvas> ();
		audioSource = gameObject.GetComponent<AudioSource> ();
		canvasHUD.enabled = false;
		canvasStart.enabled = false;
		canvasPause.enabled = false;
		canvasGameOver.enabled = false;

		ChangeMusic (GameController.LEVEL);
		PlayerHealth.onDeath += EnableGameOver;
		//GameController.FreezeGame ();
		GameController.StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		// Fade Out Color after damage is taken
		if (flash) {
			background.color = Color.Lerp(flashColor, transparent, time);
			if (time >= 1f) {
				flash = false;
				time = 0f;
			} else {
				time += Time.deltaTime;
			}
		}
		// Count Down after Game Over Canvas is enabled
		if (canvasGameOver.enabled) {
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0f) {
				DisableGameOver ();
				timeLeft = COUNTDOWN_SECONDS;
				restartGame.Invoke ();
			} else {
				string text = "Restarting in " + (int)timeLeft + " seconds";
				canvasGameOver.transform.Find ("Panel/Countdown").GetComponent<Text> ().text = text;
			}
		}
	}

	public void FlashColor(Color color) {
		flashColor = new Color(color.r / 3f, color.g / 3f, color.b / 3f);
		flash = true;
	}

	// Play Background Music Track depending on Level
	public void ChangeMusic(uint level) {
		if (level <= 10) {
			audioSource.clip = clips [0];
		} else if (level <= 20) {
			audioSource.clip = clips [1];
		} else {
			audioSource.clip = clips [2];
		}
		audioSource.Play ();
	}

	// Toggle Pause Menu Canvas
	public void PauseMenuToggle() { canvasPause.enabled = !canvasPause.enabled; }
	// Toggle Game Over Canvas
	public void GameOverToggle() { canvasGameOver.enabled = !canvasGameOver.enabled; }
	// Toggle Start Canvas
	public void StartScreenToggle() { canvasStart.enabled = !canvasStart.enabled; }
	// Enable Pause Menu Canvas
	public void EnablePauseMenu() { canvasPause.enabled = true; }
	// Disable Pause Menu Canvas
	public void DisablePauseMenu() { canvasPause.enabled = false; }
	// Enable Game Over Canvas
	public void EnableGameOver() { canvasGameOver.enabled = true; }
	// Disable Game Over Canvas
	public void DisableGameOver() { canvasGameOver.enabled = false; }
	// Enable Start Canvas
	public void EnableStartScreen() { canvasStart.enabled = true; }
	// Disable Start Canvas
	public void DisableStartScreen() { canvasStart.enabled = false; }
	// Enable HUD
	public void EnableHUD() { canvasHUD.enabled = true; }
	// Disable HUD
	public void DisableHUD() { canvasHUD.enabled = false; }

}
