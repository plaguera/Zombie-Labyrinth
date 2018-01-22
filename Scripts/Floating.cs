using UnityEngine;
using System.Collections;

/**
 * Floating Component
 **/
public class Floating : MonoBehaviour {

	public float degreesPerSecond = 15.0f;		// Degrees GameObject will rotate each second.
	public float amplitude = 0.5f;				// Floating Amplitude
	public float frequency = 1f;				// Number of 'Amplitudes' moved each second

	// Position Storage Variables
	Vector3 posOffset = new Vector3();
	Vector3 tempPos = new Vector3();

	// Use this for initialization
	void Start () {
		posOffset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

		tempPos = posOffset;
		tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

		transform.position = tempPos;
	}
}
