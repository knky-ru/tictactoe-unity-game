using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

public class PlayerAI : Player
{
    private System.Random random;
    
    private Dictionary<int, int[]> map = new Dictionary<int, int[]>();
    
    public PlayerAI(string playerName, FigureType figure) : base(playerName, figure)
    {
        isRobot = true;
        random = new System.Random();
        
        map.Add(1, new []{ 2, 5, 4 });
        map.Add(2, new []{ 1, 3, 4, 5, 6 });
        map.Add(3, new []{ 2, 5, 6 });
        map.Add(4, new []{ 1, 2, 5, 8, 7 });
        map.Add(5, new []{ 1, 2, 3, 4, 6, 7, 8, 9 });
        map.Add(6, new []{ 2, 3, 5, 8, 9 });
        map.Add(7, new []{ 4, 5, 8 });
        map.Add(8, new []{ 4, 5, 6, 7, 9 });
        map.Add(9, new []{ 5, 6, 8 });
    }

    public override int RobotMove(Difficulty difficulty, GameField field)
    {
        switch (difficulty)
        {
            case Difficulty.EASY:
                return RandomMove(field);
            case Difficulty.MEDIUM:
                return StopMove(field);
            case Difficulty.HARD:
                return WinMove(field);
        }

        return 0;
    }

    private int RandomMove(GameField field)
    {
        List<int> emptyFigures = new List<int>();
        foreach (KeyValuePair<int, FigureType> kvp in field.GetFigures())
        {
            if (kvp.Value == FigureType.EMPTY)
                emptyFigures.Add(kvp.Key);
        }

        int randomIndex = random.Next(emptyFigures.Count);
        return emptyFigures[randomIndex];
    }

    private int StopMove(GameField field)
    {
        List<int> emptyFigures = new List<int>();
        FigureType enemyFigure = (PlayerFigure() == FigureType.CROSS) ? FigureType.CIRCLE : FigureType.CROSS;

        IDictionary<int, FigureType> fieldFigures = field.GetFigures();
        foreach (KeyValuePair<int, FigureType> kvp in fieldFigures)
        {
            if (kvp.Value == FigureType.EMPTY)
                emptyFigures.Add(kvp.Key);
        }
        
        Dictionary<int, int> weights = new Dictionary<int, int>();
        weights = GetStopWeights(emptyFigures, fieldFigures, enemyFigure);
        
        Dictionary<int, int> maxWeights = weights.Where(x => x.Value == weights.Values.Max()).ToDictionary(x => x.Key, x => x.Value);
        maxWeights = maxWeights.OrderBy(x => random.Next())
            .ToDictionary(x => x.Key, x => x.Value);
        
        if (maxWeights.Count == 0) return random.Next(1, 10);
        
        int maxIndex = maxWeights.First().Key;
        return maxIndex;
    }

    private int WinMove(GameField field)
    {
        List<int> emptyFigures = new List<int>();
        FigureType enemyFigure = (PlayerFigure() == FigureType.CROSS) ? FigureType.CIRCLE : FigureType.CROSS;

        IDictionary<int, FigureType> fieldFigures = field.GetFigures();
        foreach (KeyValuePair<int, FigureType> kvp in fieldFigures)
        {
            if (kvp.Value == FigureType.EMPTY)
                emptyFigures.Add(kvp.Key);
        }
        
        Dictionary<int, int> weights = new Dictionary<int, int>();
        weights = GetStopWeights(emptyFigures, fieldFigures, enemyFigure);
        weights = GetWinWeights(weights, emptyFigures, fieldFigures, PlayerFigure());
        
        Dictionary<int, int> maxWeights = weights.Where(x => x.Value == weights.Values.Max()).ToDictionary(x => x.Key, x => x.Value);
        maxWeights = maxWeights.OrderBy(x => random.Next())
            .ToDictionary(x => x.Key, x => x.Value);

        if (maxWeights.Count == 0) return random.Next(1, 10);

        int maxIndex = maxWeights.First().Key;
        return maxIndex;
    }

    private Dictionary<int, int> GetStopWeights(List<int> emptyFigures, IDictionary<int, FigureType> fieldFigures, FigureType enemyFigure)
    {
        Dictionary<int, int> weights = new Dictionary<int, int>();
        foreach (int emptyFigIndex in emptyFigures)
        {
            foreach (int nearCellIndex in map[emptyFigIndex])
            {
                if (fieldFigures[nearCellIndex] == enemyFigure)
                {
                    if (weights.ContainsKey(emptyFigIndex))
                        weights[emptyFigIndex]++;
                    else
                        weights.Add(emptyFigIndex, 1);
                    
                    if (IsOppositeCellIsSame(emptyFigIndex, nearCellIndex, fieldFigures, enemyFigure))
                        weights[emptyFigIndex] += 5;
                }
            }
        }

        return weights;
    }
    
    private Dictionary<int, int> GetWinWeights(Dictionary<int, int> weights, List<int> emptyFigures, IDictionary<int, FigureType> fieldFigures, FigureType myFigure)
    {
        foreach (int emptyFigIndex in emptyFigures)
        {
            foreach (int nearCellIndex in map[emptyFigIndex])
            {
                if (fieldFigures[nearCellIndex] == myFigure)
                {
                    if (weights.ContainsKey(emptyFigIndex))
                        weights[emptyFigIndex]++;
                    else
                        weights.Add(emptyFigIndex, 1);
                    
                    if (IsOppositeCellIsSame(emptyFigIndex, nearCellIndex, fieldFigures, myFigure))
                        weights[emptyFigIndex] += 20;
                }
            }
        }

        return weights;
    }
    
    private bool IsOppositeCellIsSame(int emptyFigIndex, int nearCellIndex, IDictionary<int, FigureType> fieldFigures, FigureType enemyFigure)
    {
        if (emptyFigIndex == 1 && nearCellIndex == 2 && fieldFigures[3] == enemyFigure) return true;
        if (emptyFigIndex == 1 && nearCellIndex == 4 && fieldFigures[7] == enemyFigure) return true;
        if (emptyFigIndex == 1 && nearCellIndex == 5 && fieldFigures[9] == enemyFigure) return true;

        if (emptyFigIndex == 2 && nearCellIndex == 5 && fieldFigures[8] == enemyFigure) return true;

        if (emptyFigIndex == 3 && nearCellIndex == 2 && fieldFigures[1] == enemyFigure) return true;
        if (emptyFigIndex == 3 && nearCellIndex == 5 && fieldFigures[7] == enemyFigure) return true;
        if (emptyFigIndex == 3 && nearCellIndex == 6 && fieldFigures[9] == enemyFigure) return true;

        if (emptyFigIndex == 6 && nearCellIndex == 5 && fieldFigures[4] == enemyFigure) return true;

        if (emptyFigIndex == 9 && nearCellIndex == 5 && fieldFigures[1] == enemyFigure) return true;
        if (emptyFigIndex == 9 && nearCellIndex == 6 && fieldFigures[3] == enemyFigure) return true;
        if (emptyFigIndex == 9 && nearCellIndex == 8 && fieldFigures[7] == enemyFigure) return true;

        if (emptyFigIndex == 8 && nearCellIndex == 5 && fieldFigures[2] == enemyFigure) return true;

        if (emptyFigIndex == 7 && nearCellIndex == 4 && fieldFigures[1] == enemyFigure) return true;
        if (emptyFigIndex == 7 && nearCellIndex == 5 && fieldFigures[3] == enemyFigure) return true;
        if (emptyFigIndex == 7 && nearCellIndex == 8 && fieldFigures[9] == enemyFigure) return true;

        if (emptyFigIndex == 4 && nearCellIndex == 5 && fieldFigures[6] == enemyFigure) return true;

        if (emptyFigIndex == 5 && nearCellIndex == 1 && fieldFigures[9] == enemyFigure) return true;
        if (emptyFigIndex == 5 && nearCellIndex == 2 && fieldFigures[8] == enemyFigure) return true;
        if (emptyFigIndex == 5 && nearCellIndex == 3 && fieldFigures[7] == enemyFigure) return true;
        if (emptyFigIndex == 5 && nearCellIndex == 6 && fieldFigures[4] == enemyFigure) return true;
        if (emptyFigIndex == 5 && nearCellIndex == 9 && fieldFigures[1] == enemyFigure) return true;
        if (emptyFigIndex == 5 && nearCellIndex == 8 && fieldFigures[2] == enemyFigure) return true;
        if (emptyFigIndex == 5 && nearCellIndex == 7 && fieldFigures[3] == enemyFigure) return true;
        if (emptyFigIndex == 5 && nearCellIndex == 4 && fieldFigures[6] == enemyFigure) return true;

        return false;
    }


}
