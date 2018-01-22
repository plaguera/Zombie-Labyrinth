using UnityEngine;
using System.Collections;

/**
 * Maze Generation Algorithm Abstract Parent Class
 **/
public abstract class MazeAlgorithm {
	protected MazeCell[,] mazeCells;		// 2D Array (Table) of Visual Cells
	protected int mazeRows, mazeColumns;	// Maze Width and Height

	protected MazeAlgorithm(MazeCell[,] mazeCells) : base() {
		this.mazeCells = mazeCells;
		mazeRows = mazeCells.GetLength(0);
		mazeColumns = mazeCells.GetLength(1);
	}

	public abstract void CreateMaze ();
}
