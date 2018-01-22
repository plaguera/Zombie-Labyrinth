﻿using UnityEngine;
using System.Collections;

/**
 * Maze Loader Component
 **/
public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;			// Maze Dimensions
	public GameObject wall;						// GameObject used for Walls, Floor, Ceiling
	public float size = 6f;						// Size of the Wall Object's side

	private MazeCell[,] mazeCells;

	// Use this for initialization
	void Start () {}
	// Update is called once per frame
	void Update () {}

	public MazeAlgorithm GenerateMaze() {
		InitializeMaze ();
		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();
		return ma;
	}

	// Instantiate floor and ceiling tiles and surround tile with walls
	private void InitializeMaze() {
		mazeCells = new MazeCell[mazeRows,mazeColumns];

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				mazeCells [r, c] = new MazeCell ();

				mazeCells [r, c] .floor = Instantiate (wall, new Vector3 (r*size, -(size/2f), c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

				mazeCells [r, c] .floor = Instantiate (wall, new Vector3 (r*size, (size/2f), c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c] .floor.name = "Ceiling " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

				if (c == 0) {
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)), Quaternion.identity) as GameObject;
					mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
				}

				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)), Quaternion.identity) as GameObject;
				mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;

				if (r == 0) {
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size), Quaternion.identity) as GameObject;
					mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
				}

				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);
				GameController.destroyInstances += mazeCells [r, c].Destroy;
			}
		}
	}
}
