using Monte;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Game : MonoBehaviour {
	//Stores all the variables needed for the Game that are shared between the two.
	public AIAgent currentAI;
	//For simulations
    protected AIAgent[] ais;
	public bool gamePlaying = true;
	public int currentPlayersTurn;
    protected int numbMovesPlayed = 0;
    protected AIState latestAIState = null;
	public int playerIndx;
	protected int[] latestStateRep;
	protected List<GameObject> board;
	public GameObject preFabTile;
	public GameObject preFabCounter0;
	public GameObject preFabCounter1;
	public GameObject AIThinking;
	public GameObject EndGame;
	public Text winlose;
	public int boardWidth;
		
	void Start()
	{
		gamePlaying = true;
		currentPlayersTurn = 0;
	}
		

	public void runGame()
	{
		playerIndx = GameData.playerIndex;
		if (!(currentPlayersTurn == playerIndx) && gamePlaying) {
			AIThinking.SetActive(true);
			int result = checkAI ();
			if (result >= 0) {
				if (result == 2) {
					winlose.text = "You drew!";
				} else if (result == playerIndx) {
					winlose.text = "You won!";
				} else {
					winlose.text = "You lost!";
				}
				EndGame.SetActive (true);
				gamePlaying = false;
			}
		} else AIThinking.SetActive(false);
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