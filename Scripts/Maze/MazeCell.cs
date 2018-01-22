using UnityEngine;

/**
 * Maze Cell Class (Visual GameObject)
 **/
public class MazeCell {
	public bool visited = false;
	public GameObject northWall, southWall, eastWall, westWall, floor, ceiling;
	public void Destroy() {
		GameController.destroyInstances -= Destroy;
		Object.Destroy (northWall);
		Object.Destroy (southWall);
		Object.Destroy (eastWall);
		Object.Destroy (westWall);
		Object.Destroy (floor);
		Object.Destroy (ceiling);
	}
}
