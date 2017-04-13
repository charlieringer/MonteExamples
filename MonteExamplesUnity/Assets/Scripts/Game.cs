using Monte;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game : MonoBehaviour {
	//Stores all the variables needed for the Game that are shared between the two.
	public AIAgent currentAI;
	//For simulations
    protected AIAgent[] ais;
	protected bool gamePlaying = true;
	public int currentPlayersTurn;
    protected int numbMovesPlayed = 0;
    protected AIState latestAIState = null;
	public int playerIndx;
	protected int[] latestStateRep;
	protected List<GameObject> board;
	public GameObject preFabTile;
	public GameObject preFabCounter0;
	public GameObject preFabCounter1;
	public int boardWidth;
		
	void Start()
	{
		gamePlaying = true;
		currentPlayersTurn = 0;
		playerIndx = 0;
	}
		

	public void runGame()
	{
		if (!(currentPlayersTurn == playerIndx) && gamePlaying) {
			int result = checkAI ();
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

    public void runGameSimulations(int numbGames, AIAgent _ai1, AIAgent _ai2)
    {
        ais = new AIAgent[2];
        ais[0] = _ai1;
        ais[1] = _ai2;
        currentAI = ais[currentPlayersTurn];

        int gamesPlayed = 0;
        int[] wins = new int[3];
        while (gamesPlayed < numbGames)
        {
            int result = checkSimulation();
            if (result >= 0)
            {
                gamesPlayed++;
                wins[result]++;
                reset();
				currentAI = ais[currentPlayersTurn];
            }
        }
        Console.WriteLine("Player 0 wins: " + wins[0] + "     Player 1 wins: " + wins[1] + "     Draws: " + wins[2]);
    }

	public void updateBoard()
	{
		for (int i = 0; i < latestStateRep.Length; i++) {
			if (latestStateRep [i] > 0)
				board [i].GetComponent<Tile>().playHere(latestStateRep [i]);
		}
	}

	public abstract void handlePlayerAt (int x, int y);
	public abstract int checkAI();
    public abstract int checkSimulation();
    public abstract void reset();
	public abstract void initBoard();
	public abstract int getPlayerColour();
}