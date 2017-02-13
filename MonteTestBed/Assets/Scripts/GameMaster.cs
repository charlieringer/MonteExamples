﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;
using Monte;

abstract public class GameMaster : MonoBehaviour {
	//Stores all the variables needed for the Game that are shared between the two.
	protected BasicMCTS brain = new BasicMCTS();
	protected bool gamePlaying = true;
	protected int playerIndx;

	public int selectedColour;
	public GameObject preFabTile;
	public GameObject preFabCounter;
	public int x;
	public int y;
	public int tileSize;
	public int gap;
	public List<GameObject> tiles;
	public bool playersTurn = true;

	public GameObject thinkingPopup;
	public GameObject endGameMenu;

	public abstract void spawn (int x, int y);
}