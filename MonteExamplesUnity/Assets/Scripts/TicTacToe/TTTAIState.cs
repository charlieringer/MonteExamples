using System.Collections.Generic;
using Monte;

public class TTTAIState : AIState
{
    public TTTAIState()
    {
        stateRep = new int[9];
        playerIndex = 0;
        parent = null;
        depth = 0;
        numbPieceTypes = 2;
    }

    public TTTAIState(int pIndex, AIState _parent, int _depth, int[] _stateRep) : base(pIndex, _parent, _depth,
        _stateRep, 2)
    {
        numbPieceTypes = 2;
    }

    public override List<AIState> generateChildren()
    {
        List<AIState> children = new List<AIState> ();

        if (getWinner () >= 0) {
            this.children = children;
            return children;
        }

        int newPIndx = (playerIndex == 0) ? 1 : 0;

        for (int i = 0; i < 9; i++) {
        if (stateRep[i] == 0) {
            int[] newBoard = (int[])stateRep.Clone ();
            newBoard [i] = playerIndex+1;
            TTTAIState childAIState = new TTTAIState (newPIndx, this, depth + 1, newBoard);
            children.Add (childAIState);
            }
        }
        this.children = children;
        return children;
    }

    public override int getWinner()
    {
        if (stateRep[0] == stateRep[3] && stateRep[3] == stateRep[6] && stateRep[0] != 0) return playerIndex;
        if (stateRep[1] == stateRep[4] && stateRep[4] == stateRep[7] && stateRep[1] != 0) return playerIndex;
        if (stateRep[2] == stateRep[5] && stateRep[5] == stateRep[8] && stateRep[2] != 0) return playerIndex;
        if (stateRep[0] == stateRep[1] && stateRep[1] == stateRep[2] && stateRep[0] != 0) return playerIndex;
        if (stateRep[3] == stateRep[4] && stateRep[4] == stateRep[5] && stateRep[3] != 0) return playerIndex;
        if (stateRep[6] == stateRep[7] && stateRep[7] == stateRep[8] && stateRep[6] != 0) return playerIndex;
        if (stateRep[0] == stateRep[4] && stateRep[4] == stateRep[8] && stateRep[0] != 0) return playerIndex;
        if (stateRep[2] == stateRep[4] && stateRep[4] == stateRep[6] && stateRep[2] != 0) return playerIndex;

        //Check draw
        bool drawn = true;
        foreach (var position in stateRep)if (position == 0) drawn = false;
        if (drawn) return 2;
        //Game still going on
        return -1;
    }
}