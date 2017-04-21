using System;
using Monte;
using UnityEngine;
using System.Collections.Generic;

//Class for the Hex game which derives from the Game base class
public class OrderAndChaos : Game
{
	//Need to know the last move for Order and Chaos
    private int[] lastMovePlayed;
	//Also the piece selected changes
	private int pieceSelected = 0;

    public OrderAndChaos()
    {
		//Make a blank state
        latestAIState = new OCAIState();
		currentPlayersTurn = 0;
    }

	//Handle key input
	public void Update()
	{
		//If space is pressed
		if (Input.GetKeyDown ("space")) {
			//Change the piece selected
			pieceSelected = (pieceSelected + 1) % 2;
		}
	}
		
	//Called each time the update loop checks the AI progress
	public override int checkAI () {
		//If the AI has not stated
		if (!ai.started)
		{
			//Start it with the current state.
			AIState currentState = new OCAIState((currentPlayersTurn+1)%2, null, 0, latestStateRep, lastMovePlayed, numbMovesPlayed);
			ai.run(currentState);
		}
		//Otherwise if the AI is done 
		else if (ai.done)
		{
			//Get the next state (after the AI has moved)
			OCAIState nextAIState = (OCAIState)ai.next;
			//Unpack the state 
			latestAIState = nextAIState;
			latestStateRep = nextAIState == null ? null : nextAIState.stateRep;
			//Reset the AI
			ai.reset();
			//Switch which player is playing
			currentPlayersTurn = (currentPlayersTurn + 1) % 2;
			//Update the graphical rep of the board
			updateBoard ();
			//And increment the number of moves
			numbMovesPlayed++;
		}
		//Return who the winner is
		return latestAIState.getWinner();
	}

	//Instantiates the tiles for the graphical rep of the board
	public override void initBoard()
	{
		//Make an empty state rep
		latestStateRep = new int[36];
		//Creates the new tiles
		board = new List<GameObject>();
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 6; j++) {
				//create a tile here.
				float x = i+(0.1f*i);
				float y = j+(0.1f*j);
				GameObject tile = (GameObject)Instantiate(preFabTile, new Vector3 (x, y, 0), Quaternion.identity);
				tile.GetComponent<Tile>().setMaster(this);
				tile.GetComponent<Tile > ().setXY (i, j);
				tile.GetComponent<Tile> ().setRealXY (x, y);
				board.Add(tile);
			}
		} 
	}

	//Get which colour the player is playing
	public override int getPlayerColour()
	{
		return pieceSelected;//because Order and Chaos has peices changing colour get pieceSelected.
	}

	//Handles the player clicking a tile.
	public override void handlePlayerAt(int x, int y)
	{
		//Get the staterep location and updating it with the correct value
		latestStateRep[x*6+y] = pieceSelected == 0 ? 2 : 1;
		//Change the players turn
		currentPlayersTurn = (currentPlayersTurn + 1) % 2;
		//Make a move array for the lastest move
		lastMovePlayed = new int[]{ x * 6 + y, pieceSelected == 0 ? 2 : 1 };
		//Update the number of moves
		numbMovesPlayed++;
		//Set up the last state
		latestAIState = new OCAIState(playerIndx, null, 0, latestStateRep, lastMovePlayed, numbMovesPlayed);
		//Find out the result of the board
		int result = latestAIState.getWinner ();
		//And the end game as such
		if (result >= 0) {
			if (result == 2) {
				winlose.text = "You drew!";
			} else if (result == playerIndx) {
				winlose.text = "You won!";
			} else {
				winlose.text = "You lost!";
			}
			gamePlaying = false;
			EndGame.SetActive (true);
		}
	}
}