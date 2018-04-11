using UnityEngine;
using System.Collections;

public class totalsTile {

	public totalsTile (int x, int y)
	{
		this.X = x;
		this.Y = y;
		this.currentValue = 0;
		this.currentTarget = 0;
		this.matched = false;

	}

	public readonly int X, Y;
	public int currentValue, currentTarget;
	public bool matched;

	public int addToTotal (int toAdd)
	{
		this.currentValue += toAdd;
		return this.currentValue;
	}

	public void checkForMatch ()
	{
		if (this.currentValue == this.currentTarget){
			this.matched = true;
		} else {
			this.matched = false;
		}
	}

}
