using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class tileObjectController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		mousedOverSprite = Resources.Load<Sprite>("Graphics/gridTest32");
		baseSprite = Resources.Load<Sprite>("Graphics/gridEmpty32");
		fullSprite = Resources.Load<Sprite>("Graphics/gridFull32");
		sprRenderer = (SpriteRenderer)GetComponent<Renderer>();
	}

	public Sprite baseSprite, mousedOverSprite, fullSprite;
	public SpriteRenderer sprRenderer;
	public Text value;
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MousedOver() {
		sprRenderer.sprite = mousedOverSprite;
	}

	public void MousedOut() {
		sprRenderer.sprite = baseSprite;
	}
		
	public void PlacePentomino () {
		sprRenderer.sprite = fullSprite;
	}

	public void updateTileValue (string val) {
		value.text = val;
	}

	public void updateTextColour (){
		value.color = new Color(255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f);
	}

	public void returnTextColour (){
		value.color = new Color(0f/255.0f, 0f/255.0f, 0f/255.0f);
	}
}
