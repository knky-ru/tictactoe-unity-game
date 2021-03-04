using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class GameField
{
 //   private List<Figure> figures;
    
    private Dictionary<int, FigureType> figures;

    public GameField()
    {
        figures = new Dictionary<int, FigureType>();
        for (int i = 1; i <= 9; i++)
        {
            figures.Add(i, FigureType.EMPTY);
        }
    }

    public void AddMove(int index, FigureType type)
    {
        figures[index] = type;
    }

    public IDictionary<int, FigureType> GetFigures()
    {
        return figures;
    }
}
