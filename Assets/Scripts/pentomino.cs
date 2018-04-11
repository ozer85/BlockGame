using UnityEngine;
using System.Collections;

public class pentomino {

	public pentomino (int height, int width, int[,] shape, float lowerVal, float upperVal)
	{
		this.height = height;
		this.width = width;
		this.shape = shape;
		assignGridValues (lowerVal, upperVal);
	}

	public int height, width;
	public int[,] shape;
	public int[] values;

	public int[,] getPentominoShape()
	{
		return this.shape;
	}

	void assignGridValues (float startRange, float endRange)
	{
		this.values = new int[5] {
			(int)Mathf.Floor (Random.Range (startRange, endRange)),
			(int)Mathf.Floor (Random.Range (startRange, endRange)),
			(int)Mathf.Floor (Random.Range (startRange, endRange)),
			(int)Mathf.Floor (Random.Range (startRange, endRange)),
			(int)Mathf.Floor (Random.Range (startRange, endRange))
		};
	}
}
