using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Spawn Controller Script
 **/
public class SpawnController : MonoBehaviour {

	public GameObject key;			// GameObject Key to access Teleport Pad
	public GameObject player;		// GameObject Player
	public GameObject teleport;		// GameObject Teleport Pad for NextLevel
	public GameObject[] enemies;	// GameObject Enemies to Spawn
	public GameObject[] lights;		// GameObject Lights to Spawn
	public GameObject[] potions;	// GameObject Potions to Spawn

	int w, h;						// Width, Height of current Maze
	float s;						// Floor Tile Size (Side)
	List<Vector2> usedCoordinates = new List<Vector2> ();	// Store already used coordinates in current spawn operation

	public void SetDimensions(int w, int h, float s) {
		this.w = w;
		this.h = h;
		this.s = s;
	}

	// Use this for initialization
	void Start () {}
	// Update is called once per frame
	void Update () {}

	// Spawn all GameObjects
	public void Spawn() {
		SpawnSingletonMove (player, 0f);
		SpawnSingletonMove (teleport, -1.5f);
		SpawnSingleton (key, -1.5f);

		int n_enemy = (int)(GameConstants.N_ENEMIES * GameController.LEVEL);
		int n_powerup = (int)(GameConstants.N_POTIONS / GameController.LEVEL);
		SpawnMultiple (enemies, n_enemy);
		SpawnMultiple (potions, n_powerup);
		SpawnMultiple (lights, GameConstants.N_LIGHTS);
	}

	// Spawn Single GameObject Prefab by Instantiating
	void SpawnSingleton(GameObject obj, float height) {
		Vector2 pos = RandomCoordinates ();
		while(usedCoordinates.Contains (pos))
			pos = RandomCoordinates();
		Spawn (obj, pos.x, height, pos.y);
	}

	// Spawn Single Existing GameObject by Moving it
	void SpawnSingletonMove(GameObject obj, float height) {
		Vector2 pos = RandomCoordinates ();
		while(usedCoordinates.Contains (pos))
			pos = RandomCoordinates();
		obj.transform.position = new Vector3 (pos.x, height, pos.y);
	}

	// Spawn Multiple GameObject Prefabs by Instantiating
	void SpawnMultiple(GameObject[] objs, int n) {
		Vector2 pos;
		for (int i = 0; i < n; i++) {
			int k = Random.Range(0, objs.GetLength(0));
			do {
				pos = RandomCoordinates() + RandomNoise();
			} while(usedCoordinates.Contains (pos));
			Spawn(objs[k], pos.x, pos.y);
		}
	}

	// Spawn GameObject at X,Y,Z coordinates
	void Spawn(GameObject obj, float x, float y, float z) {
		obj.transform.position = new Vector3 (x, obj.transform.position.y, z);
		usedCoordinates.Add(new Vector2(x, z));
		GameObject instance = Instantiate (obj) as GameObject;
		//instance.AddComponent<DestroyScript> ();
		//GameController.destroyInstances += instance.GetComponent<DestroyScript>().Destroy;
	}

	// Spawn GameObject at X,Y coordinates, setting height to 0
	void Spawn(GameObject obj, float x, float z) { Spawn(obj, x, 0f, z); }
	// Get Random Coordinates within Dimensions
	Vector2 RandomCoordinates() { return new Vector2 (Random.Range (0, w) * s, Random.Range (0, h) * s); }
	// Get Random Noise within Floor Tile Size
	Vector2 RandomNoise() { return new Vector2 (Random.Range (-s/2, s/2), Random.Range (-s/2, s/2)); }
}
