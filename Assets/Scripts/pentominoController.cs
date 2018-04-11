using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pentominoController {

	public pentominoController()
	{
		GameController = GameObject.FindObjectOfType<gameController>();
		allPentominos = new Dictionary<int, pentomino>();
		BuildPentominoDict ();
	}

	private Dictionary<int, pentomino> allPentominos; 
	gameController GameController; 


	public void BuildPentominoDict ()
	{
		pentomino p1 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 0, 2 }, { 0, -1 }, { 0, -2 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [0] = p1;
		pentomino p2 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 1, 1 }, { 0, -1 }, { -1, 0 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [1] = p2;
		pentomino p3 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { -1, 1 }, { 0, -1 }, { 1, 0 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [2] = p3;
		pentomino p4 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 0, 2 }, { 0, -1 }, { 1, -1 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [3] = p4;
		pentomino p5 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 0, 2 }, { 0, -1 }, { -1, -1 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [4] = p5;
		pentomino p6 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { 0, -1 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [5] = p6;
		pentomino p7 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 0, -1 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [6] = p7;
		pentomino p8 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 0, 2 }, { -1, 0 }, { -1, -1 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [7] = p8;
		pentomino p9 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 0, 2 }, { 1, 0 }, { 1, -1 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [8] = p9;
		pentomino p10 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { -1, 1 }, { 1, 1 }, { 0, -1 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [9] = p10;
		pentomino p11 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 1, 0 }, { -1, 0 }, { -1, 1 }, { 1, 1 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [10] = p11;
		pentomino p12 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 0, 2 }, { -1, 0 }, { -2, 0 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [11] = p12;
		pentomino p13 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 1, 0 }, { 1, 1 }, { 0, -1 }, { -1, -1 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [12] = p13;
		pentomino p14 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [13] = p14;
		pentomino p15 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 }, { 0, -2 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [14] = p15;
		pentomino p16 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { 0, -2 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [15] = p16;
		pentomino p17 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 0, 2 }, { -1, 0 }, { -2, 0 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [16] = p17;
		pentomino p18 = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 1, 1 }, { 0, -1 }, { -1, -1 } }, GameController.tileValueLower, GameController.tileValueUpper);
		allPentominos [17] = p18;

	}

	public pentomino selectRandomPentomino ()
	{
		int selector = (int)Mathf.Floor(Random.Range (0f, 18f) );
		pentomino selectedPentomino = allPentominos [selector];
		return selectedPentomino;
	}


}
