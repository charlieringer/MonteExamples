using System;
using Monte;
using UnityEngine;
using System.Collections.Generic;

//Class for the Hex game which derives from the Game base class
public class Hex : Game
{
    public Hex()
    {
		//Make a blank state
        latestAIState = new HexAIState();
		currentPlayersTurn = 0;
    }

	//Called each time the update loop checks the AI progress
	public override int checkAI () {
		//If the AI has not stated
		if (!ai.started)
		{
			//Start it with the current state.
			AIState currentState = new HexAIState((currentPlayersTurn+1)%2, null, 0, latestStateRep, numbMovesPlayed);
			ai.run(currentState);
		}
		//Otherwise if the AI is done 
		else if (ai.done)
		{
			//Get the next state (after the AI has moved)
			HexAIState nextAIState = (HexAIState)ai.next;
			//Unpack the state 
			latestAIState = nextAIState;
			latestStateRep = nextAIState.stateRep;
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
		latestStateRep = new int[81];
		//Creates the new tiles
		board = new List<GameObject>();
		for (int i = 0; i < 9; i++) 
		{
			for (int j = 0; j < 9; j++) 
			{
				//create a tile here. There is a realX and realY (real world location) and X Y (board position) 
				//due to the offset needed for Hex
				float x = (i+(0.1f*i))+((float)j/2);
				float y = -(j + (0.1f * j));
				GameObject tile = (GameObject)Instantiate(preFabTile, new Vector3 (x, y, 0), Quaternion.identity);
				tile.GetComponent<Tile>().setMaster(this);
				tile.GetComponent<Tile> ().setXY (i, j);
				tile.GetComponent<Tile> ().setRealXY (x, y);
				board.Add(tile);
			}
		} 
	}

	//Get which colour the player is playing
	public override int getPlayerColour()
	{
		return playerIndx; //Which in this case is just the player index
	}

	//Handles the player clicking a tile.
	public override void handlePlayerAt(int x, int y)
	{
		//Get the staterep location and updating it with the correct value
		latestStateRep[x*9+y] = playerIndx == 0 ? 2 : 1;
		//Change the players turn
		currentPlayersTurn = (currentPlayersTurn + 1) % 2;
		//Update the number of moves
		numbMovesPlayed++;
		//Set up the last state
		latestAIState = new HexAIState(playerIndx, null, 0, latestStateRep, numbMovesPlayed);
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
