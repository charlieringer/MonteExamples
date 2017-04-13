using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;
using Monte;

public class GameController : MonoBehaviour {
	
	protected AIAgent brain;
	protected bool gamePlaying = true;
	public int playerIndx;
	protected Game currentGame;
	public int tileSize;
	public int gap;
	private bool playersTurn;
	protected List<GameObject> board;
	public GameObject preFabTile;
	public GameObject preFabCounterWhite;
	public GameObject preFabCounterBlack;
	public int gameindx;

	void Start()
	{
		switch (gameindx) 
		{
		case 1:
			currentGame = gameObject.AddComponent<TicTacToe> ();
			currentGame.currentAI = new MCTSSimpleAgent (100, 1.4, 9, 0.5);
			currentGame.boardWidth = 3;
			break;
		case 2:
			currentGame = gameObject.AddComponent<OrderAndChaos> ();
			currentGame.currentAI = new MCTSSimpleAgent (1000, 1.4, 36, 0.5);
			currentGame.boardWidth = 6;
			break;
		case 3:
			currentGame = gameObject.AddComponent<Hex> ();
			currentGame.currentAI = new MCTSSimpleAgent ();
			currentGame.boardWidth = 9;
			break;
		}
		currentGame.preFabTile = preFabTile;
		currentGame.preFabCounter0 = preFabCounterWhite;
		currentGame.preFabCounter1 = preFabCounterBlack;
		currentGame.initBoard ();
	}

	void Update()
	{
		currentGame.runGame ();
	}
}