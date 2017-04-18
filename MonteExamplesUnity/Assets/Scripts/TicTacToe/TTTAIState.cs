using System.Collections.Generic;
using Monte;

public class TTTAIState : AIState
{
    //Makes a new state
    public TTTAIState()
    {
        stateRep = new int[9];
        playerIndex = 0;
        parent = null;
        depth = 0;
        numbPieceTypes = 2;
    }

    //Makes a state as a child of another one.
    public TTTAIState(int pIndex, AIState _parent, int _depth, int[] _stateRep) : base(pIndex, _parent, _depth,
        _stateRep, 2)
    {
        numbPieceTypes = 2;
    }

    //Generates all children (results of all moves) from this state.
    public override List<AIState> generateChildren()
    {
        //List of children
        List<AIState> children = new List<AIState> ();
        //If the game is already over there are no children
        if (getWinner () >= 0) {
            this.children = children;
            return children;
        }
        //Swap the player
        int newPIndx = (playerIndex == 0) ? 1 : 0;
        //Loop through all of the board pieces
        for (int i = 0; i < 9; i++)
        {
            //if it is 0 (therefore empty)
            if (stateRep[i] == 0)
            {
                //We have a possible peice to play so clone the board
                int[] newBoard = (int[])stateRep.Clone ();
                //and simululate playing a piece
                newBoard [i] = playerIndex+1;
                TTTAIState childAIState = new TTTAIState (newPIndx, this, depth + 1, newBoard);
                //And add this state as a child
                children.Add (childAIState);
            }
        }
        //Set the children of this node
        this.children = children;
        //Also return it.
        return children;
    }

    public override int getWinner()
    {
        //Check to see there are any wins
        if (stateRep[0] == stateRep[3] && stateRep[3] == stateRep[6] && stateRep[0] != 0) return playerIndex;
        if (stateRep[1] == stateRep[4] && stateRep[4] == stateRep[7] && stateRep[1] != 0) return playerIndex;
        if (stateRep[2] == stateRep[5] && stateRep[5] == stateRep[8] && stateRep[2] != 0) return playerIndex;
        if (stateRep[0] == stateRep[1] && stateRep[1] == stateRep[2] && stateRep[0] != 0) return playerIndex;
        if (stateRep[3] == stateRep[4] && stateRep[4] == stateRep[5] && stateRep[3] != 0) return playerIndex;
        if (stateRep[6] == stateRep[7] && stateRep[7] == stateRep[8] && stateRep[6] != 0) return playerIndex;
        if (stateRep[0] == stateRep[4] && stateRep[4] == stateRep[8] && stateRep[0] != 0) return playerIndex;
        if (stateRep[2] == stateRep[4] && stateRep[4] == stateRep[6] && stateRep[2] != 0) return playerIndex;

        //Check draw (draw if all spots are taken and the game has not finished)
        bool drawn = true;
        foreach (var position in stateRep)if (position == 0) drawn = false;
        if (drawn) return 2;
        //Game still going on
        return -1;
    }
}