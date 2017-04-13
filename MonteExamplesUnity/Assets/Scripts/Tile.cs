using UnityEngine;
using System.Collections;

//Class for the tile
public class Tile : MonoBehaviour {

	public Renderer rend;
	private Game master;
	public bool canPress;
	public float realX;
	public float realY;
	public int x;
	public int y;
	public GameObject preFabCounter0;
	public GameObject preFabCounter1;
	public GameObject counter0;
	public GameObject counter1;

	public Texture tileTexture;

	// Use this for initialization
	void Start () {
		rend.material.mainTexture =  tileTexture;
		counter0 = (GameObject)Instantiate(preFabCounter0, new Vector3 (realX, realY,0 ), Quaternion.Euler(new Vector3(90, 0, 0)));
		counter0.SetActive (false);
		counter0.GetComponent<Collider> ().enabled = false;

		counter1 = (GameObject)Instantiate(preFabCounter1, new Vector3 (realX, realY,0 ), Quaternion.Euler(new Vector3(90, 0, 0)));
		counter1.SetActive (false);
		counter1.GetComponent<Collider> ().enabled = false;
	}

	//When you mouse over 
	void OnMouseOver()
	{
		rend.material.mainTexture =  tileTexture;
		//If it can be pressed (therefore an empty space)
		if (canPress) {
			//Set the colour to green
			rend.material.color = Color.green;
			//And set the counter to active
			if (master.getPlayerColour() == 0) counter0.SetActive (true);
			else counter1.SetActive (true);
		}
	}

	void OnMouseDown()
	{
		//If it is the players turn and they can click this space
		if (master.currentPlayersTurn == master.playerIndx && canPress) {
			//Turn of the highlight counter (as a real counter will be spawned
			if (master.getPlayerColour() == 0) counter0.SetActive (true);
			else counter1.SetActive (true);
			canPress = false;
			//And spawn the real counter ... 
			master.handlePlayerAt (x, y);
		}
	}

	void OnMouseExit()
	{
		//When we mouse off the tile we reset the highligh and hide the counter
		rend.material.mainTexture =  tileTexture;
		rend.material.color = Color.white;
		if (canPress) {
			counter0.SetActive (false);
			counter1.SetActive (false);
		}
	}

	public void setMaster(Game _master)
	{
		//Sets which game master we are using.
		master = _master;
	}

	public void setRealXY(float _rX, float _rY)
	{
		realX = _rX;
		realY = _rY;
	}

	public void setXY(int _x, int _y)
	{
		//Sets the XY values of the tile
		x = _x;
		y = _y;
	}

	public void playHere(int pieceIndx)
	{
		if (!canPress)return;
		//Function to play the AI move here
		if (pieceIndx == 2) counter0.SetActive (true);
		else counter1.SetActive (true);
		canPress = false;
	}
}
