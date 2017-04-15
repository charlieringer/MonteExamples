using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;
using Monte;

public class GameController : MonoBehaviour {
	
	protected Game currentGame;
	public int playerIndx;
	public GameObject preFabTile;
	public GameObject preFabCounterWhite;
	public GameObject preFabCounterBlack;
	public GameObject AIThinking;
	public GameObject EndGame;
	public Text winlose;
	public int gameindx;


	void Start()
	{
		switch (gameindx) 
		{
		case 1:
			currentGame = gameObject.AddComponent<TicTacToe> ();
			currentGame.currentAI = new MCTSSimpleAgent (1000, 1.4, 9, 0.5);
			currentGame.boardWidth = 3;
			break;
		case 2:
			currentGame = gameObject.AddComponent<OrderAndChaos> ();
			currentGame.currentAI = new MCTSSimpleAgent (1000, 1.4, 36, 0.5);
			currentGame.boardWidth = 6;
			break;
		case 3:
			currentGame = gameObject.AddComponent<Hex> ();
			currentGame.currentAI = new MCTSSimpleAgent (1000, 1.4, 36, 0.5);
			currentGame.boardWidth = 9;
			break;
		}
		currentGame.playerIndx = playerIndx;
		currentGame.preFabTile = preFabTile;
		currentGame.preFabCounter0 = preFabCounterWhite;
		currentGame.preFabCounter1 = preFabCounterBlack;
		currentGame.AIThinking = AIThinking;
		currentGame.EndGame = EndGame;
		currentGame.winlose = winlose;
		currentGame.initBoard ();
	}

	void Update()
	{
		currentGame.runGame ();
	}
}