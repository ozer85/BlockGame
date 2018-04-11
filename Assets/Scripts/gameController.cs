using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GenerateGrid ();
		tileValueLower = 1f;
		tileValueUpper = 6f;
		PentominoController = new pentominoController ();
		currentPentomino = getNewPentomino ();
	}

	public int gridSize = 15;
	public int score = 0;
	private gridTile[,] tiles; 
	private totalsTile[,] totalTiles;
	public GameObject tilePrefab, totalsBottomPrefab, totalsTopPrefab, totalsRightPrefab, totalsLeftPrefab, totalsCornerBL, totalsCornerBR, totalsCornerTL, totalsCornerTR;
	private Dictionary<gridTile, GameObject> tileToGameObject; 
	private Dictionary<totalsTile, GameObject> totalsToGameObject; 
	pentominoController PentominoController;
	pentomino currentPentomino;
	public float tileValueLower,tileValueUpper;
	public Text scoreText;
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateGrid ()
	{
		tiles = new gridTile[gridSize, gridSize];
		totalTiles = new totalsTile[gridSize, 4];
		tileToGameObject = new Dictionary<gridTile, GameObject>();
		totalsToGameObject = new Dictionary<totalsTile, GameObject> ();

		for (int column = 0; column < gridSize; column++) 
		{
			for (int row = 0; row < gridSize; row++) 
			{
				gridTile g = new gridTile (column, row);
				tiles [column, row] = g;

				Vector3 pos = new Vector3 (column, row);

				GameObject tileGO = (GameObject)Instantiate (
					                    tilePrefab,
					                    pos,
					                    Quaternion.identity,
					                    this.transform
				                    );
				tileGO.name = string.Format ("Tile: {0},{1}", column, row);
				tileToGameObject [g] = tileGO;
			}
		}

		//Add totals tiles
		for (int column = 0; column < gridSize; column++) 
		{
			//BOTTOM
			totalsTile b = new totalsTile (column, -1);
			if (column < 8){
				b.currentTarget = column + 1;
			} else {
				b.currentTarget = 14 - (column - 1);
			}
			Vector3 pos = new Vector3 (column, -1);
			GameObject bottomGO = (GameObject)Instantiate (
				totalsBottomPrefab,
				pos,
				Quaternion.identity,
				this.transform
			);
			tileObjectController bottomController = bottomGO.GetComponent<tileObjectController> ();
			bottomController.updateTileValue (b.currentTarget.ToString());
			totalTiles [column, 0] = b;
			totalsToGameObject [b] = bottomGO;


			//TOP
			totalsTile t = new totalsTile (column, 15);
			if (column < 8){
				t.currentTarget = column + 1;
			} else {
				t.currentTarget = 14 - (column - 1);
			}
			pos = new Vector3 (column, 15);
			GameObject topGO = (GameObject)Instantiate (
				totalsTopPrefab,
				pos,
				Quaternion.identity,
				this.transform
			);
			tileObjectController topController = topGO.GetComponent<tileObjectController> ();
			topController.updateTileValue (t.currentTarget.ToString());
			totalTiles [column, 1] = t;
			totalsToGameObject [t] = topGO;

			//RIGHT
			totalsTile r = new totalsTile (15, column);
			if (column < 8){
				r.currentTarget = column + 1;
			} else {
				r.currentTarget = 14 - (column - 1);
			}
			pos = new Vector3 (15, column);
			GameObject rightGO = (GameObject)Instantiate (
				totalsRightPrefab,
				pos,
				Quaternion.identity,
				this.transform
			);
			tileObjectController rightController = rightGO.GetComponent<tileObjectController> ();
			rightController.updateTileValue (r.currentTarget.ToString());
			totalTiles [column, 2] = r;
			totalsToGameObject [r] = rightGO;

			//LEFT
			totalsTile l = new totalsTile (-1, column);
			if (column < 8){
				l.currentTarget = column + 1;
			} else {
				l.currentTarget = 14 - (column - 1);
			}
			pos = new Vector3 (-1, column);
			GameObject leftGO = (GameObject)Instantiate (
				totalsLeftPrefab,
				pos,
				Quaternion.identity,
				this.transform
			);
			tileObjectController leftController = leftGO.GetComponent<tileObjectController> ();
			leftController.updateTileValue (l.currentTarget.ToString());
			totalTiles [column, 3] = l;
			totalsToGameObject [l] = leftGO;

			//CORNERS
			pos = new Vector3 (-1, -1);
			GameObject cornerBLGO = (GameObject)Instantiate (
				totalsCornerBL,
				pos,
				Quaternion.identity,
				this.transform
			);
			pos = new Vector3 (-1, 15);
			GameObject cornerBRGO = (GameObject)Instantiate (
				totalsCornerTL,
				pos,
				Quaternion.identity,
				this.transform
			);
			pos = new Vector3 (15, 15);
			GameObject cornerTRGO = (GameObject)Instantiate (
				totalsCornerTR,
				pos,
				Quaternion.identity,
				this.transform
			);
			pos = new Vector3 (15, -1);
			GameObject cornerTLGO = (GameObject)Instantiate (
				totalsCornerBR,
				pos,
				Quaternion.identity,
				this.transform
			);
		}

	}

	public gridTile getTileAt (int x, int y)
	{
		if (tiles == null) {
			//Debug.LogError ("No tiles instantiated!");
			return null;
		}

		if (x > 14 || x < 0 || y > 14 || y < 0) 
		{
			return null;
		}
		return tiles [x, y];
	}

	public totalsTile getTotalTile (int x, int y)
	{
		if (totalTiles == null) {
			return null;
		}
		return totalTiles [x, y];
	}

	public GameObject getTileGO (gridTile tile)
	{
		if (tileToGameObject.ContainsKey(tile) ) {
			return tileToGameObject [tile];
		}
		return null;
	}

	public GameObject getTotalsGO (totalsTile tile)
	{
		if (totalsToGameObject.ContainsKey (tile)) {
			return totalsToGameObject [tile];
		}
		return null;
	}

	public pentomino getNewPentomino ()
	{
		pentomino p = new pentomino (5, 1, new int[5, 2]{ { 0, 0 }, { 0, 1 }, { 0, 2 }, { 0, -1 }, { 0, -2 } }, tileValueLower, tileValueUpper);
		p = PentominoController.selectRandomPentomino ();
		currentPentomino = p;
		return p;
	}

	public pentomino parsePentomino ()
	{
		return currentPentomino;
	}
}
