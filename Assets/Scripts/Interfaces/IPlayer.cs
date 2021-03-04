using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface IPlayer
    {
        string PlayerName();
        FigureType PlayerFigure();
        bool IsRobot();
        int RobotMove(Difficulty difficulty, GameField field);
    }
}