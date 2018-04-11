using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

//TODO: Next tile preview. 

public class mouseController : MonoBehaviour {



	// Use this for initialization
	void Start () {
		GameController = GameObject.FindObjectOfType<gameController>();
		lastTile = GameController.getTileAt(0, 0);
		inverted = 0;
	}

	gameController GameController;
	pentomino currentPentomino;
	public GameObject centreLaserPrefab;

	
	// Update is called once per frame
	void Update () {
		Vector3 currentMouse = getMousePosition ();
		xPos = (int)Mathf.Floor (currentMouse [0]);
		yPos = (int)Mathf.Floor (currentMouse [1]);
		if (Input.GetKeyDown("space")){
			if (inverted == 3){
				inverted = 0;
			} else {
				inverted++;
			}
		}
		if (xPos >= 0 && yPos >= 0 && xPos <= 14 && yPos <= 14) {
			if (mouseInGrid ()) 
			{
				if (Input.GetMouseButtonDown (0)) 
				{
					placePentomino ();
				}
			}
		}

	}

	int xPos, yPos;
	gridTile lastTile;
	gridTile [] shapeTiles;
	int inverted;

	void logPosition ()
	{
		Vector3 cameraPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Debug.Log (cameraPos);
	}

	public Vector3 getMousePosition ()
	{
		Vector3 cameraPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		return cameraPos;
	}

	bool mouseInGrid ()
	{
		gridTile currentTile = GameController.getTileAt(xPos, yPos);
		currentPentomino = GameController.parsePentomino ();
		int[,] currentPentominoShape = currentPentomino.shape;


		//TODO: Currently loops over and replaces all tiles - would be more efficient if it would work with only those not empty
		if (lastTile != currentTile) 
		{
			for (int i = 0; i < 15; i++) 
			{
				for (int j = 0; j < 15; j++) 
				{
					gridTile TileToEmpty = GameController.getTileAt (i, j);
					if (TileToEmpty.empty) 
					{
						GameObject GOtoEmpty = GameController.getTileGO (TileToEmpty);
						GOtoEmpty.GetComponentInChildren<Canvas> ().enabled = false;
						tileObjectController ControllerToEmpty = GOtoEmpty.GetComponent<tileObjectController> ();
						ControllerToEmpty.MousedOut ();
					}
				}
			}
		}

		for (int i = 0; i < currentPentominoShape.GetLength (0); i++) 
		{
			gridTile tmpTile = GameController.getTileAt (xPos + currentPentominoShape [i, 0], yPos + currentPentominoShape [i, 1]);
			if (inverted == 3) {
				tmpTile = GameController.getTileAt (xPos + (currentPentominoShape [i, 1]*-1), yPos + currentPentominoShape [i, 0]);
			}
			if (inverted == 2) {
				tmpTile = GameController.getTileAt (xPos + (currentPentominoShape [i, 0]*-1), yPos + (currentPentominoShape [i, 1]*-1));
			}
			if (inverted == 1) {
				tmpTile = GameController.getTileAt (xPos + currentPentominoShape [i, 1], yPos + (currentPentominoShape [i, 0]*-1));
			}
			if (tmpTile == null || tmpTile.empty == false) 
			{
				return false;
			}
		}

		for (int i = 0; i < currentPentominoShape.GetLength (0); i++) 
		{
			gridTile tmpTile = GameController.getTileAt (xPos + currentPentominoShape [i, 0], yPos + currentPentominoShape [i, 1]);
			if (inverted == 3) {
				tmpTile = GameController.getTileAt (xPos + (currentPentominoShape [i, 1]*-1), yPos + currentPentominoShape [i, 0]);
			}
			if (inverted == 2) {
				tmpTile = GameController.getTileAt (xPos + (currentPentominoShape [i, 0]*-1), yPos + (currentPentominoShape [i, 1]*-1));
			}
			if (inverted == 1) {
				tmpTile = GameController.getTileAt (xPos + currentPentominoShape [i, 1], yPos + (currentPentominoShape [i, 0]*-1));
			}
			GameObject tmpGO = GameController.getTileGO (tmpTile);
			tmpGO.GetComponentInChildren<Canvas> ().enabled = true;
			tileObjectController tmpController = tmpGO.GetComponent<tileObjectController> ();
			tmpController.MousedOver ();
			//tmpController.updateTileValue ("1");
			tmpController.updateTileValue (currentPentomino.values[i].ToString());
		}

		return true;
	}

	void placePentomino ()
	{
		int[,] currentPentominoShape = currentPentomino.shape;
		int[,] parsedShape = new int[5,2]; 

		for (int i = 0; i < currentPentominoShape.GetLength (0); i++) 
		{
			int tmpX = xPos + currentPentominoShape [i, 0];
			int tmpY = yPos + currentPentominoShape [i, 1];

			if (inverted == 3) {
				tmpX = xPos + (currentPentominoShape [i, 1] * -1);
				tmpY = yPos + currentPentominoShape [i, 0];
			}
			if (inverted == 2) {
				tmpX = xPos + (currentPentominoShape [i, 0]*-1);
				tmpY = yPos + (currentPentominoShape [i, 1]*-1);
			}
			if (inverted == 1) {
				tmpX = xPos + currentPentominoShape [i, 1];
				tmpY = yPos + (currentPentominoShape [i, 0]*-1);
			}
			parsedShape [i, 0] = tmpX;
			parsedShape [i, 1] = tmpY;
			gridTile tmpTile = GameController.getTileAt (tmpX, tmpY);
			tmpTile.empty = false;
			tmpTile.currentValue = currentPentomino.values [i];
			GameObject tmpGO = GameController.getTileGO (tmpTile);
			tileObjectController tmpController = tmpGO.GetComponent<tileObjectController> ();
			tmpController.PlacePentomino ();
			tmpController.updateTextColour ();
			//updateTotals (tmpX, tmpY, currentPentomino.values [i]);
			totalsTile tmpTotal = GameController.getTotalTile (tmpX, 0);
			tmpTotal.checkForMatch ();
			tmpTotal = GameController.getTotalTile (tmpY, 2);
			tmpTotal.checkForMatch ();
		}
		//checkMatch (parsedShape);
		updateTotals();
		while (checkMatch ()) {
			updateTotals ();
		}
			
		GameController.getNewPentomino ();
	}

	void updateTotals()
	{
		for (int i = 0; i < 15; i++) {
			int colTotal = 0;
			int rowTotal = 0;
			for (int j = 0; j < 15; j++) {

				gridTile colTile = GameController.getTileAt (i, j);
				gridTile rowTile = GameController.getTileAt (j, i);

				colTotal += colTile.currentValue;
				rowTotal += rowTile.currentValue;
			}

			totalsTile bottom = GameController.getTotalTile (i, 0);
			totalsTile top = GameController.getTotalTile (i, 1);
			totalsTile right = GameController.getTotalTile (i, 2);
			totalsTile left = GameController.getTotalTile (i, 3);

			bottom.currentValue = colTotal;
			top.currentValue = colTotal;
			right.currentValue = rowTotal;
			left.currentValue = rowTotal;

		}

	}

	bool checkMatch()
	{
		List<int> xMatches = new List<int> ();
		List<int> yMatches = new List<int> ();
		for (int i = 0; i <15; i++) {
			totalsTile bottom = GameController.getTotalTile (i, 0);
			totalsTile right = GameController.getTotalTile (i, 2);

			if (bottom.currentValue == bottom.currentTarget) {
				xMatches.Add (i);
			}
			if (right.currentValue == right.currentTarget) {
				yMatches.Add (i);
			}

		}
			
		foreach (var xM in xMatches) 
		{
			for (int i = 0; i < 15; i++) {
				gridTile g = GameController.getTileAt (xM, i);
				g.empty = true;
				g.currentValue = 0;
				GameObject clearGO = GameController.getTileGO (g);
				tileObjectController clearCtrl = clearGO.GetComponent<tileObjectController> ();
				clearCtrl.MousedOut ();
				clearCtrl.returnTextColour ();

			}

			for (int i = 0; i < 8; i++) {
				
				//Instantiate laser animations
				if (i == 0) {
					Vector3 pos = new Vector3 (xM, 7);

					GameObject CentreLaser = (GameObject)Instantiate (
						centreLaserPrefab,
						pos,
						Quaternion.identity,
						this.transform
					);

					Animator LaserAnimator = CentreLaser.GetComponent<Animator>();
					LaserAnimator.Play ("centreLaser");

					Destroy (CentreLaser, 0.6f);

				} else {
					Vector3 pos1 = new Vector3 (xM, 7+i);
					Vector3 pos2 = new Vector3 (xM, 7-i);

					GameObject Laser1 = (GameObject)Instantiate (
						centreLaserPrefab,
						pos1,
						Quaternion.identity,
						this.transform
					);

					GameObject Laser2 = (GameObject)Instantiate (
						centreLaserPrefab,
						pos2,
						Quaternion.identity,
						this.transform
					);

					Animator Laser1Animator = Laser1.GetComponent<Animator>();
					Laser1Animator.Play ("centreLaser");

					Animator Laser2Animator = Laser2.GetComponent<Animator>();
					Laser2Animator.Play ("centreLaser");

					Destroy (Laser1, 0.6f);
					Destroy (Laser2, 0.6f);

				}

			}
			totalsTile bottom = GameController.getTotalTile (xM, 0);
			totalsTile top = GameController.getTotalTile (xM, 1);
			bottom.currentTarget = bottom.currentTarget * 2;
			top.currentTarget = bottom.currentTarget;
			bottom.currentValue = 0;
			top.currentValue = 0;
			GameObject bGO = GameController.getTotalsGO (bottom);
			GameObject tGO = GameController.getTotalsGO (top);
			tileObjectController bController = bGO.GetComponent<tileObjectController> ();
			tileObjectController tController = tGO.GetComponent<tileObjectController> ();
			bController.updateTileValue (bottom.currentTarget.ToString ());
			tController.updateTileValue (bottom.currentTarget.ToString ());
			if (yMatches.Count > 0) {
				GameController.score += bottom.currentTarget;
				Text scoreText = GameController.scoreText;
				scoreText.text = "Score: " + GameController.score.ToString ();
			} else {
				GameController.score += bottom.currentTarget / 2;
				Text scoreText = GameController.scoreText;
				scoreText.text = "Score: " + GameController.score.ToString ();
			}
		}

		foreach (var yM in yMatches) 
		{
			for (int i = 0; i < 15; i++) {
				gridTile g = GameController.getTileAt (i, yM);
				g.empty = true;
				g.currentValue = 0;
				GameObject clearGO = GameController.getTileGO (g);
				tileObjectController clearCtrl = clearGO.GetComponent<tileObjectController> ();
				clearCtrl.MousedOut ();
				clearCtrl.returnTextColour ();
			}

			for (int i = 0; i < 8; i++) {

				//Instantiate laser animations
				if (i == 0) {
					Vector3 pos = new Vector3 (7, yM);

					GameObject CentreLaser = (GameObject)Instantiate (
						centreLaserPrefab,
						pos,
						Quaternion.identity,
						this.transform
					);
					Animator LaserAnimator = CentreLaser.GetComponent<Animator>();
					LaserAnimator.Play ("centreLaser");
					//LaserAnimator.StartPlayback ();
					Destroy(CentreLaser, 0.6f);
				} else {
					Vector3 pos1 = new Vector3 (7+i, yM);
					Vector3 pos2 = new Vector3 (7-i, yM);

					GameObject Laser1 = (GameObject)Instantiate (
						centreLaserPrefab,
						pos1,
						Quaternion.identity,
						this.transform
					);

					GameObject Laser2 = (GameObject)Instantiate (
						centreLaserPrefab,
						pos2,
						Quaternion.identity,
						this.transform
					);

					Animator Laser1Animator = Laser1.GetComponent<Animator>();
					Laser1Animator.Play ("centreLaser");
					Animator Laser2Animator = Laser2.GetComponent<Animator>();
					Laser2Animator.Play ("centreLaser");

					Destroy (Laser1, 0.6f);
					Destroy (Laser2, 0.6f);
				}

			}
			totalsTile right = GameController.getTotalTile (yM, 2);
			totalsTile left = GameController.getTotalTile (yM, 3);
			right.currentTarget = right.currentTarget * 2;
			left.currentTarget = right.currentTarget;
			right.currentValue = 0;
			left.currentValue = 0;
			GameObject rGO = GameController.getTotalsGO (right);
			GameObject lGO = GameController.getTotalsGO (left);
			tileObjectController rController = rGO.GetComponent<tileObjectController> ();
			tileObjectController lController = lGO.GetComponent<tileObjectController> ();
			rController.updateTileValue (right.currentTarget.ToString ());
			lController.updateTileValue (right.currentTarget.ToString ());
			if (xMatches.Count > 0) {
				GameController.score += right.currentTarget;
				Text scoreText = GameController.scoreText;
				scoreText.text = "Score: " + GameController.score.ToString ();
			} else {
				GameController.score += right.currentTarget / 2;
				Text scoreText = GameController.scoreText;
				scoreText.text = "Score: " + GameController.score.ToString ();
			}
		}

		if (xMatches.Count > 0 || yMatches.Count > 0) {
			//Debug.Log (xMatches.Count + ", " + yMatches.Count);
			return true;
		}

		return false;

	}

	void blockMatch (int index, int rc, int val)
	{
		//ARGS: row/column index [0-14]; row=0, col=1; value that has been matched (for scoring purposes)
		for (int i = 0; i < 15; i++) {
			if (rc == 0) {
				//Clear Row
				gridTile toClear = GameController.getTileAt (i, index);
				toClear.empty = true;
				GameObject clearGO = GameController.getTileGO (toClear);
				tileObjectController clearCtrl = clearGO.GetComponent<tileObjectController> ();
				clearCtrl.MousedOut ();
			} else {
				gridTile toClear = GameController.getTileAt (index, i);
				toClear.empty = true;
				GameObject clearGO = GameController.getTileGO (toClear);
				tileObjectController clearCtrl = clearGO.GetComponent<tileObjectController> ();
				clearCtrl.MousedOut ();
			}
		}
	}
}
