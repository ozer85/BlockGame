using UnityEngine;
using System.Collections;

public class gridTile {

	public gridTile (int x, int y)
	{
		this.X = x;
		this.Y = y;
		this.currentValue = 0;
		this.empty = true;

	}

	public readonly int X, Y;
	public int currentValue;
	public bool empty;

}
