using System;
using System.Collections.Generic;
using Monte;

public class OCAIState : AIState
{
    public int[] lastPiecePlayed;
    public int numbPiecesPlayed;

    public OCAIState()
    {
        stateRep = new int[37];
        stateRep[36] = 2;
        playerIndex = 0;
        parent = null;
        depth = 0;
        numbPiecesPlayed = 0;
    }

    public OCAIState(int pIndex, AIState _parent, int _depth, int[] _stateRep, int[] _lastPiecePlayed,
        int _numbPiecesPlayed) : base(pIndex, _parent, _depth,
        _stateRep)
    {
        lastPiecePlayed = _lastPiecePlayed;
        numbPiecesPlayed = _numbPiecesPlayed;
    }

	public override List<AIState> generateChildren()
	{
		//Generates all possible child states from this state
		List<AIState> children = new List<AIState> ();
		//Swap the player
	    int newPIndx = (playerIndex + 1) % 2;
		//Increment the number of peices played
		int newNumbPieces = numbPiecesPlayed+1;
		//Loop through all of the board pieces
		for (int i = 0; i < stateRep.Length; i++) {
            //if it is 0 (therefore empty)
            if (stateRep[i] == 0) {
                //We have a possible peice to play so clone the board
                int[] newStateRep = (int[])stateRep.Clone ();
                int[] move = {i, 1};
                //and simululate playing a white piece
                newStateRep [i] = 1;
                OCAIState childAIState = new OCAIState (newPIndx, this, depth+1, newStateRep, move, newNumbPieces);
                //And add this state as a child
                children.Add (childAIState);
                //Then simululate playing a black piece
                int[] newStateRep2 = (int[])stateRep.Clone ();
                newStateRep2 [i] = 2;
                int[] move2 = {i, 1};
                OCAIState childAIState2 = new OCAIState (newPIndx, this, depth+1, newStateRep2, move2, newNumbPieces);
                //And add this state as a child
                children.Add (childAIState2);
            }
        }
		//Set the children of this node
		this.children = children;
		//Also return it.
		return children;
	}

	public override int getWinner()
	{
	    if (lastPiecePlayed == null) return -1;
	    if (numbPiecesPlayed == 36) return 1;
	    int boardWidth = 6;
	    int location = lastPiecePlayed [0];
	    int locModBoard = location % boardWidth;
	    int rowStart =  (int) Math.Floor((double)(location / boardWidth))*boardWidth;
	    int colourPlayed = lastPiecePlayed [1];
	    int countX = 0;
	    int countY = 0;
	    int countD1 = 0;
	    int countD2 = 0;

	    for (int i = 0; i < 6; i++) {
	        //Check orthognal directions
	        //Vertical
	        if (stateRep [locModBoard+(boardWidth*i)] == colourPlayed) //If we find a match
	            countX++; //Increment the count
	        else if (countX >= 1 && countX < 5) //If we find a break and have not completed the row
	            countX = 0; //It is impossible to win so set count to 0

	        //Horizontal
	        if (stateRep[rowStart + i] == colourPlayed)  //If we find a match
	            countY++; //Increment the count
	        else if (countY >= 1 && countY < 5) //If we find a break and have not completed the row
	            countY = 0; //It is impossible to win so set count to 0

	        //Check diagonal directions
	        int diag1Loc = location % 7 + i + i * boardWidth;
	        bool hasWraparoundD1 = diag1Loc % boardWidth < locModBoard && location < diag1Loc;
	        if (diag1Loc == colourPlayed && !hasWraparoundD1) countD1++;
	        else if (countD1 >= 1 && countD1 < 5) countD1 = 0;

	        int diag2Loc = location % 5 - i + i * boardWidth;
	        bool hasWraparoundD2 = diag2Loc % boardWidth > locModBoard && location < diag2Loc;
	        if (diag2Loc == colourPlayed &&  !hasWraparoundD2) countD2++;
	        else if (countD2 >= 1 && countD2 < 5) countD2 = 0;

	    }
	    //if either direction is at least 5 the game is over (Order wins)
	    if (countX >= 5 || countY >= 5 || countD1 >= 5 || countD2 >= 5) return 0;
	    //Otherwise game is still going on.
	    return -1;
	}
}