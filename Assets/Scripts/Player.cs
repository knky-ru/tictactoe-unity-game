using System.Collections;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UnityEngine;

public class Player : IPlayer
{
    private string playerName;
    private FigureType figure;
    protected bool isRobot;

    public Player(string playerName, FigureType figure)
    {
        this.playerName = playerName;
        this.figure = figure;
        isRobot = false;
    }

    public string PlayerName()
    {
        return playerName;
    }
    
    public FigureType PlayerFigure()
    {
        return figure;
    }

    public bool IsRobot()
    {
        return isRobot;
    }

    public virtual int RobotMove(Difficulty difficulty, GameField field)
    {
        return 0;
    }
}
