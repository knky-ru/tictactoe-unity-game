using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Figure
{
    private int index;
    private FigureType type;

    public Figure(int index, FigureType type)
    {
        this.index = index;
        this.type = type;
    }
}