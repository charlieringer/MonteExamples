using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;
using Monte;

public class GameController : MonoBehaviour {
	//The current game
	protected Game currentGame;
	//Prefab Graphical game objects
	public GameObject preFabTile;
	public GameObject AIThinking;
	public GameObject EndGame;
	public Text winlose;
	//Which game is being played
	public int gameIndx;

	void Start()
	{
		//Redirect the console output (for debugging purposes).
		UnitySystemConsoleRedirector.Redirect();
		//Make a new agent
		AIAgent agent;
		//And model
		Model model = new Model(GameData.modelFiles [gameIndx]);
		//Set the settings
		string settings = GameData.settingsFiles [gameIndx];
		//And init the right agent with the model and settings above.
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
		default:
			agent = new MCTSSimpleAgent (settings);
			break;
		}
		//Init the correct game
		switch (gameIndx) 
		{
		case 0:
			currentGame = gameObject.AddComponent<TicTacToe> ();
			break;
		case 1:
			currentGame = gameObject.AddComponent<OrderAndChaos> ();
			break;
		case 2:
			currentGame = gameObject.AddComponent<Hex> ();
			break;
		}
		//Once the game has been made set the rest of the game up
		currentGame.ai = agent;
		currentGame.playerIndx = GameData.playerIndex;;
		currentGame.preFabTile = preFabTile;
		currentGame.AIThinking = AIThinking;
		currentGame.EndGame = EndGame;
		currentGame.winlose = winlose;
	}

	//On update run the game
	void Update() { if(currentGame!=null) currentGame.runGame (); }
}