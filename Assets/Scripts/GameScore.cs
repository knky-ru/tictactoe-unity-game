using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore
{
    private int playerScores;
    private int robotScores;

    public GameScore()
    {
        playerScores = 0;
        robotScores = 0;
    }

    public void RobotWins()
    {
        robotScores++;
    }

    public void PlayerWins()
    {
        playerScores++;
    }

    public int GetRobotScore()
    {
        return robotScores;
    }

    public int GetPlayerScore()
    {
        return playerScores;
    }
}
