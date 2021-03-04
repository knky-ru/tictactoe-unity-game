using System;
using System.Collections.Generic;
using CryoDI;
using Enums;
using Interfaces;
using UnityEditor;
using UnityEngine;

public class GameChecker : CryoBehaviour
{
 
    [Dependency]
    private EventEmitter Events { get; set; }

    [Dependency]
    private TicTacToe Game { get; set; }
    
    private readonly int[][] combinations = new []
    {
        new []{ 1, 1, 1, 0, 0, 0, 0, 0, 0, },
        new []{ 0, 0, 0, 1, 1, 1, 0, 0, 0, },
        new []{ 0, 0, 0, 0, 0, 0, 1, 1, 1, },
        new []{ 1, 0, 0, 1, 0, 0, 1, 0, 0, },
        new []{ 0, 1, 0, 0, 1, 0, 0, 1, 0, },
        new []{ 0, 0, 1, 0, 0, 1, 0, 0, 1, },
        new []{ 1, 0, 0, 0, 1, 0, 0, 0, 1, },
        new []{ 0, 0, 1, 0, 1, 0, 1, 0, 0, },
    };

    private void Start()
    {
        Events.OnSetFigure += Event_OnSetFigure;
    }
    
    private void Event_OnSetFigure(object sender, EventEmitter.FigureEventArgs e)
    {
        Check();
    }

    private void Check()
    {
        List<int> circleFigures = new List<int>();
        List<int> crossFigures = new List<int>();
        List<int> emptyFigures = new List<int>();
        foreach( KeyValuePair<int, FigureType> kvp in Game.GetField().GetFigures() )
        {
            if (kvp.Value == FigureType.CROSS)
            {
                crossFigures.Add(1);
                circleFigures.Add(0);
            } else if (kvp.Value == FigureType.CIRCLE)
            {
                crossFigures.Add(0);
                circleFigures.Add(1);
            }
            else
            {
                crossFigures.Add(0);
                circleFigures.Add(0);
                emptyFigures.Add(1);
            }
        }

        int[] circles = circleFigures.ToArray();
        int[] crosses = crossFigures.ToArray();
        int[] empties = emptyFigures.ToArray();

        foreach (int[] combination in combinations)
        {
            if (IsCombinationWins(combination, crosses))
            {
                Events.OnGameEnded_Event(false, Game.GetPlayerCross());
                return;
            }
            if (IsCombinationWins(combination, circles))
            {
                Events.OnGameEnded_Event(false, Game.GetPlayerCircle());
                return;
            }
        }
        if (empties.Length == 0)
            Events.OnGameEnded_Event(true, null);
    }

    private bool IsCombinationWins(int[] combination, int[] values)
    {
        int matches = 0;
        
        for (var i = 1; i <= combination.Length; i++)
        {
            matches += combination[i - 1] * values[i - 1];
        }

        return (matches == 3);
    }
    
}
