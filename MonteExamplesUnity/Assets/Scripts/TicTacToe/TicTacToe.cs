using System;
using Monte;
using UnityEngine;
using System.Collections.Generic;

public class TicTacToe : Game
{
	public TicTacToe()
    {
        latestAIState = new TTTAIState();
    }

	// Play is called once per tick
	public override int checkAI () {
		//If the game is running and it is time for the AI to play
		if (!currentAI.started)
		{
			AIState currentState = new TTTAIState((currentPlayersTurn+1)%2, null, 0, latestStateRep);
			currentAI.run(currentState);
		}
		else if (currentAI.done)
		{
			TTTAIState nextAIState = (TTTAIState)currentAI.next;
			if (nextAIState == null) reset();
			else
			{
				latestAIState = nextAIState;
				latestStateRep = nextAIState.stateRep;
				currentAI.reset();
				currentPlayersTurn = (currentPlayersTurn + 1) % 2;
				updateBoard ();
			}
			numbMovesPlayed++;
		}
		if (numbMovesPlayed == 9) return 2;
		return latestAIState == null ? -1 :(latestAIState.getWinner());
	}

    // Play is called once per tick
    public override int checkSimulation () {
        //If the game is running and it is time for the AI to play
        if (!currentAI.started)
        {
            AIState currentState = new TTTAIState((currentPlayersTurn+1)%2, null, 0, latestStateRep);
            //AIState currentState = new TTTAIState(currentPlayersTurn, null, 0, latestAIState.stateRep);
            currentAI.run(currentState);
        }
        else if (currentAI.done)
        {
            TTTAIState nextAIState = (TTTAIState)currentAI.next;
            if (nextAIState == null) return 2;
            latestAIState = nextAIState;
            currentAI.reset();
            currentPlayersTurn = (currentPlayersTurn + 1) % 2;
            currentAI = ais[currentPlayersTurn];
            numbMovesPlayed++;
        }
        return latestAIState.getWinner();
    }

    public override void reset()
    {
        latestAIState = new TTTAIState();
        numbMovesPlayed = 0;
        currentPlayersTurn = 0;
        currentAI = ais[currentPlayersTurn];
    }

	public override void initBoard()
	{
		latestStateRep = new int[9];
		//Creates the new tiles
		board = new List<GameObject>();
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				//create the game object 
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

	public override int getPlayerColour()
	{
		return playerIndx;
	}

	public override void handlePlayerAt(int x, int y)
	{
		latestStateRep[x*boardWidth+y] = playerIndx == 0 ? 2 : 1;
		currentPlayersTurn = (currentPlayersTurn + 1) % 2;
		numbMovesPlayed++;
		latestAIState = new TTTAIState(playerIndx, null, 0, latestStateRep);
		int result = latestAIState.getWinner ();
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
