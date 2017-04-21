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
		if	   (GameData.selectedAgent == 0) agent = new RandomAgent ();
		else if(GameData.selectedAgent == 1) agent = new ModelBasedAgent (model);
		else if(GameData.selectedAgent == 2) agent = new MCTSSimpleAgent (settings);
		else if(GameData.selectedAgent == 3) agent = new MCTSWithPruning (model, settings);
		else if(GameData.selectedAgent == 4) agent = new MCTSWithSoftPruning (model, settings);
		else if(GameData.selectedAgent == 5) agent = new MCTSWithLearning (model, settings);
		else agent = new MCTSSimpleAgent (settings);

		//Init the correct game
		if(gameIndx == 0) currentGame = gameObject.AddComponent<TicTacToe> ();
		else if (gameIndx == 1) currentGame = gameObject.AddComponent<OrderAndChaos> ();
		else if (gameIndx == 2) currentGame = gameObject.AddComponent<Hex> ();

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