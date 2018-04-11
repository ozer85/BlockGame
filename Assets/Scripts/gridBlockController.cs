using UnityEngine;
using System.Collections;

public class gridBlockController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 coords = returnPosition ();
		xPos = (int)Mathf.Floor(coords [0]);
		yPos = (int)Mathf.Floor(coords [1]);
		mousedOverSprite = Resources.Load<Sprite>("gridTest32");
		sprRenderer = (SpriteRenderer)GetComponent<Renderer>();
		//mouseControls = GameObject.FindObjectOfType<mouseController>();

	}

	//mouseController mouseControls;
	int xPos, yPos;
	Sprite mousedOverSprite;
	SpriteRenderer sprRenderer;

	// Update is called once per frame
	void Update () {
		//Debug.Log (this.coords);
		//Vector3 currentMouse = mouseControls.getMousePosition ();

	}

	Vector3 returnPosition ()
	{
		Vector3 coords = transform.position;
		return coords;
	}

	public bool checkMouseOver (int x , int y)
	{
		Debug.Log (this.xPos + ", " + this.yPos + ", " + x + ", " + y + ", " + transform.parent);
		if (x == this.xPos && y == this.yPos) {

			//Change the sprite of mousedover tile
			sprRenderer.sprite = mousedOverSprite;
			return true;
		}

		return false;
	}
}
