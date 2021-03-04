using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CryoDI;
using Enums;
using Interfaces;
using UnityEngine;

public class TicTacToe : CryoBehaviour
{
    private GameField field;
    private Difficulty difficulty;
    private IPlayer playerCircle;
    private IPlayer playerCross;
    private IPlayer playerCurrentTurn;
    private IGameResult result;
    private bool playerSelected;
    private bool isWaitForMove;
    private bool gameStopped;
    private GameScore score;
    
    [Dependency]
    private EventEmitter Events { get; set; }

    private void Start()
    {
        score = new GameScore();
        difficulty = Difficulty.EASY;

        Events.OnSelectFigure += Event_OnSelectFigure;
        Events.OnClickPlaceholder += Event_OnClickPlaceholder;
        Events.OnGameEnded += Event_OnGameEnded;
        Events.OnGameRestart += Event_OnGameRestart;
        Events.OnDifficultyChange += Event_OnDifficultyChange;
        Events.OnBackToMain += Event_OnBackToMain;
    }
    
    private void Event_OnGameEnded(object sender, EventEmitter.GameResultEventArgs e)
    {
        StopGame();
        result = new GameResult(e.IsDraw, e.Winner); 
        if (e.Winner != null && e.Winner.GetType().Equals(typeof(Player)))
            score.PlayerWins();
        if (e.Winner != null && e.Winner.GetType().Equals(typeof(PlayerAI)))
            score.RobotWins();
        Events.OnShowScore_Event();
    }

    private void Event_OnSelectFigure(object sender, EventEmitter.FigureEventArgs e)
    {
        NewGame(e.Figure);
        if (playerCurrentTurn.IsRobot())
            StartCoroutine("WaitForRobotMove");
    }

    private void Event_OnClickPlaceholder(object sender, EventEmitter.IntegerEventArgs e)
    {
        MakeMove(e.Value);
    }

    private void Event_OnDifficultyChange(object sender, EventEmitter.IntegerEventArgs e)
    {
        switch (e.Value)
        {
            case 0:
                difficulty = Difficulty.EASY;
                break;
            case 1:
                difficulty = Difficulty.MEDIUM;
                break;
            case 2:
                difficulty = Difficulty.HARD;
                break;
            default:
                difficulty = Difficulty.EASY;
                break;
        }
    }

    private void Event_OnGameRestart(object sender, EventArgs e)
    {
        Restart();
    }

    private void Event_OnBackToMain(object sender, EventArgs e)
    {
        Restart();
    }

    private void Restart()
    {
        field = null;
        playerCircle = null;
        playerCross = null;
        playerCurrentTurn = null;
        result = null;
        playerSelected = false;
    }

    private void MakeMove(int index)
    {
        field.AddMove(index, CurrentFigure());
        SwapPlayer();
        Events.OnSetFigure_Event(CurrentFigure());
        if (playerCurrentTurn.IsRobot() && !gameStopped)
        {
            isWaitForMove = true;
            StartCoroutine("WaitForRobotMove");
        }
    }

    IEnumerator WaitForRobotMove()
    {
        yield return new WaitForSeconds(1f);
        int placeholderIndex = playerCurrentTurn.RobotMove(difficulty, field);
        Events.OnRobotClickPlaceholder_Event(placeholderIndex);
        isWaitForMove = false;
    }

    private void SwapPlayer()
    {
        playerCurrentTurn = (playerCurrentTurn == playerCross) ? playerCircle : playerCross;
    }

    private void NewGame(FigureType playerFigure)
    {
        field = new GameField();
        if (playerFigure == FigureType.CIRCLE)
        {
            playerCircle = new Player(Dictionary.PlayerNames["PLAYER"], FigureType.CIRCLE);
            playerCross = new PlayerAI(Dictionary.PlayerNames["AI"], FigureType.CROSS);
        } else {
            playerCircle = new PlayerAI(Dictionary.PlayerNames["AI"], FigureType.CIRCLE);
            playerCross = new Player(Dictionary.PlayerNames["PLAYER"], FigureType.CROSS);
        }
        playerCurrentTurn = playerCross;
        playerSelected = true;
        isWaitForMove = false;
        gameStopped = false;
    }
    
    public bool IsPlayerSelected()
    {
        return playerSelected;
    }

    public FigureType CurrentFigure()
    {
        return (playerCurrentTurn == playerCross) ? FigureType.CROSS : FigureType.CIRCLE;
    }

    public GameField GetField()
    {
        return field;
    }

    public IPlayer GetPlayerCross()
    {
        return playerCross;
    }

    public IPlayer GetPlayerCircle()
    {
        return playerCircle;
    }

    public GameScore GetScore()
    {
        return score;
    }

    public IGameResult GetGameResult()
    {
        return result;
    }

    public bool IsWaitForMove()
    {
        return isWaitForMove;
    }

    public void StopGame()
    {
        gameStopped = true;
    }

}