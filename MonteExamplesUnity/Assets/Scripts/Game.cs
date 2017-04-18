using Monte;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Game : MonoBehaviour {
	//Stores all the variables needed for the Game that are shared between the two.
	//The AI you are playing against
	public AIAgent ai;
	//Bool for if the game is playing
	public bool gamePlaying = true;
	//Which player starts
	public int currentPlayersTurn = 0;
	//How many moves have been played
    protected int numbMovesPlayed = 0;
	//The latest board state
    protected AIState latestAIState = null;
	//Which side the player is playing as.
	public int playerIndx;
	//The
	protected int[] latestStateRep;
	//The graphical board
	protected List<GameObject> board;
	//The board tile
	public GameObject preFabTile;
	//Text for various popups
	public GameObject AIThinking;
	public GameObject EndGame;
	public Text winlose;
		
	//When Start is called init the board.
	void Start(){ initBoard (); }
		
	//Fro when games are run
	public void runGame()
	{
		//If it is not the human player's turn and the game is playing
		if (!(currentPlayersTurn == playerIndx) && gamePlaying) {
			//Turn on the AIThinking popup
			AIThinking.SetActive(true);
			//Check the AI
			int result = checkAI ();
			//If it is >= 0 the game is over
			if (result >= 0) {
				//If 2 then a draw
				if (result == 2) {
					winlose.text = "You drew!";
				//If playerIndx then win
				} else if (result == playerIndx) {
					winlose.text = "You won!";
				//Otherwise loss
				} else {
					winlose.text = "You lost!";
				}
				//Pop up the end game menu
				EndGame.SetActive (true);
				//Game is over
				gamePlaying = false;
			}
			//Otherwise turn the AI thinking pop up off (and wait for the player to make a move)
		} else AIThinking.SetActive(false);
	}

	//For catching what move the AI has made.
	public void updateBoard()
	{
		//Safety check
		if (latestStateRep == null) return;
		//Loop through the game state
		for (int i = 0; i < latestStateRep.Length; i++) 
		{
			//And make sure if a move has been played here it has been visually updated.
			if (latestStateRep [i] > 0) board [i].GetComponent<Tile>().playHere(latestStateRep [i]);
		}
	}

	//All of these functions need to be implemented by the games
	public abstract void handlePlayerAt (int x, int y);
	public abstract int checkAI();
	public abstract void initBoard();
	public abstract int getPlayerColour();
}