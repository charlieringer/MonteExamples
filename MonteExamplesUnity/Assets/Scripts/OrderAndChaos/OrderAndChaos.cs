using System;
using Monte;
using UnityEngine;
using System.Collections.Generic;

public class OrderAndChaos : Game
{
    private int[] lastMovePlayed;
	private int pieceSelected = 0;

    public OrderAndChaos()
    {
        latestAIState = new OCAIState();
    }

	public void Update()
	{
		if (Input.GetKeyDown ("space")) {
			pieceSelected = (pieceSelected + 1) % 2;
		}
	}
		
	// Play is called once per tick
	public override int checkAI () {
		//If the game is running and it is time for the AI to play
		if (!currentAI.started)
		{
			AIState currentState = new OCAIState((currentPlayersTurn+1)%2, null, 0, latestStateRep, lastMovePlayed, numbMovesPlayed);
			currentAI.run(currentState);
		}
		else if (currentAI.done)
		{
			OCAIState nextAIState = (OCAIState)currentAI.next;
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
		if (numbMovesPlayed == 36) return 2;
		return latestAIState.getWinner();
	}

	// Update is called once per frame
	public override int checkSimulation () {
	    //If the game is running and it is time for the AI to play
	    if (!currentAI.started)
	    {
	        AIState currentState = new OCAIState((currentPlayersTurn+1)%2, null, 0, latestAIState.stateRep, lastMovePlayed, numbMovesPlayed);
	        //AIState currentState = new OCAIState(currentPlayersTurn, null, 0, latestAIState.stateRep, lastMovePlayed, numbMovesPlayed);
	        currentAI.run(currentState);
	    }
	    else if (currentAI.done)
	    {
	        OCAIState nextAIState = (OCAIState)currentAI.next;
	        if (nextAIState == null) return 2;
            latestAIState = nextAIState;
            currentAI.reset();
            currentPlayersTurn = (currentPlayersTurn + 1) % 2;
            currentAI = ais[currentPlayersTurn];
            lastMovePlayed = nextAIState.lastPiecePlayed;
            numbMovesPlayed++;
	    }
	    return latestAIState.getWinner();
	}

    public override void reset()
    {
        latestAIState = new OCAIState();
        numbMovesPlayed = 0;
        currentPlayersTurn = 0;
        currentAI = ais[currentPlayersTurn];
    }

	public override void initBoard()
	{
		latestStateRep = new int[36];
		//Creates the new tiles
		board = new List<GameObject>();
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 6; j++) {
				//create the game object 
				float x = i+(0.1f*i);
				float y = j+(0.1f*j);
				GameObject tile = (GameObject)Instantiate(preFabTile, new Vector3 (x, y, 0), Quaternion.identity);
				tile.GetComponent<Tile>().setMaster(this);
				tile.GetComponent<Tile > ().setXY (i, j);
				tile.GetComponent<Tile> ().setRealXY (x, y);
				board.Add(tile);
				tile.GetComponent<Tile>().preFabCounter0 = preFabCounter0;
				tile.GetComponent<Tile>().preFabCounter1 = preFabCounter1;
			}
		} 
	}

	public override int getPlayerColour()
	{
		return pieceSelected;
	}

	public override void handlePlayerAt(int x, int y)
	{
		latestStateRep[x*boardWidth+y] = pieceSelected == 0 ? 2 : 1;
		currentPlayersTurn = (currentPlayersTurn + 1) % 2;
		int[] lastMovePlayed = { x * boardWidth + y, pieceSelected == 0 ? 2 : 1 };
		numbMovesPlayed++;
		latestAIState = new OCAIState(playerIndx, null, 0, latestStateRep, lastMovePlayed, numbMovesPlayed);
		int result = latestAIState.getWinner ();
		if (result >= 0) {
			if (result == 2) {
				Debug.Log ("Draw");
			} else if (result == playerIndx) {
				Debug.Log ("Win");
			} else {
				Debug.Log ("Loss");
			}
			gamePlaying = false;
		}
	}
}