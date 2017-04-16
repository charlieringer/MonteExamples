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
	public int gameIndx;


	void Start()
	{
		UnitySystemConsoleRedirector.Redirect();
		AIAgent agent = new MCTSSimpleAgent (1000, 1.4, 9, 0.5);
		Model model = new Model(GameData.modelFiles [gameIndx]);
		string settings = GameData.settingsFiles [gameIndx];
		switch (GameData.selectedAgent) 
		{
		case 0:
			agent = new RandomAgent ();
			break;
		case 1:
			agent = new ModelBasedAgent (model);
			break;
		case 2:
			agent = new MCTSSimpleAgent (settings);
			break;
		case 3:
			agent = new MCTSWithPruning (model, settings);
			break;
		case 4:
			agent = new MCTSWithSoftPruning (model, settings);
			break;
		case 5:
			agent = new MCTSWithLearning (model, settings);
			break;
		}

		switch (gameIndx) 
		{
		case 0:
			currentGame = gameObject.AddComponent<TicTacToe> ();
			currentGame.currentAI = agent;
			currentGame.boardWidth = 3;
			break;
		case 1:
			currentGame = gameObject.AddComponent<OrderAndChaos> ();
			currentGame.currentAI = agent;
			currentGame.boardWidth = 6;
			break;
		case 2:
			currentGame = gameObject.AddComponent<Hex> ();
			currentGame.currentAI = agent;
			currentGame.boardWidth = 9;
			break;
		}
		currentGame.playerIndx = playerIndx;
		currentGame.preFabTile = preFabTile;
		currentGame.AIThinking = AIThinking;
		currentGame.EndGame = EndGame;
		currentGame.winlose = winlose;
	}

	void Update() {
		currentGame.runGame ();
	}
}