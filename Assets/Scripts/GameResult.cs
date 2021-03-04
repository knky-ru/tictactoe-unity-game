using System.Collections;
using System.Collections.Generic;
using Enums;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

public class GameResult : IGameResult
{
    [CanBeNull] private IPlayer winner;
    private bool isDraw;

    public GameResult(bool isDraw, IPlayer winner)
    {
        this.winner = winner;
        this.isDraw = isDraw;
    }
    
    public string GetResultMessage()
    {
        if (isDraw)
            return "Draw!";
        else
            return winner.PlayerName() + " won!";

    }
}
