using System;
using Enums;
using Interfaces;
using JetBrains.Annotations;

public class EventEmitter
{
    public event EventHandler OnGameStart;
    public event EventHandler OnGameRestart;
    public event EventHandler OnBackToMain;
    public event FigureEventHandler OnSelectFigure;
    public event FigureEventHandler OnSetFigure;
    public event IntegerEventHandler OnClickPlaceholder;
    public event IntegerEventHandler OnRobotClickPlaceholder;
    public event IntegerEventHandler OnDifficultyChange;
    public event GameResultEventHandler OnGameEnded;
    public event EventHandler OnShowScore;

    public void OnBackToMain_Event()
    {
        OnBackToMain?.Invoke(this, EventArgs.Empty);
    }

    public void OnShowScore_Event()
    {
        OnShowScore?.Invoke(this, EventArgs.Empty);
    }

    public void OnGameStart_Event()
    {
        OnGameStart?.Invoke(this, EventArgs.Empty);
    }

    public void OnGameRestart_Event()
    {
        OnGameRestart?.Invoke(this, EventArgs.Empty);
    }

    public delegate void FigureEventHandler(object sender, FigureEventArgs e);
    public void OnSelectFigure_Event(FigureType figure)
    {
        FigureEventArgs args = new FigureEventArgs(figure);
        OnSelectFigure?.Invoke(this, args);
    }
    public void OnSetFigure_Event(FigureType figure)
    {
        FigureEventArgs args = new FigureEventArgs(figure);
        OnSetFigure?.Invoke(this, args);
    }
    public class FigureEventArgs : EventArgs
    {
        public FigureType Figure { get; set; }
        public FigureEventArgs(FigureType figure)
        {
            this.Figure = figure;
        }
    }

    public delegate void IntegerEventHandler(object sender, IntegerEventArgs e);
    public void OnClickPlaceholder_Event(int value)
    {
        IntegerEventArgs args = new IntegerEventArgs(value);
        OnClickPlaceholder?.Invoke(this, args);
    }
    public void OnRobotClickPlaceholder_Event(int value)
    {
        IntegerEventArgs args = new IntegerEventArgs(value);
        OnRobotClickPlaceholder?.Invoke(this, args);
    }
    public void OnDifficultyChange_Event(int value)
    {
        IntegerEventArgs args = new IntegerEventArgs(value);
        OnDifficultyChange?.Invoke(this, args);
    }
    public class IntegerEventArgs : EventArgs
    {
        public int Value { get; set; }
        public IntegerEventArgs(int value)
        {
            this.Value = value;
        }
    }

    public delegate void GameResultEventHandler(object sender, GameResultEventArgs e);
    public void OnGameEnded_Event(bool isDraw, IPlayer winner)
    {
        GameResultEventArgs args = new GameResultEventArgs(isDraw, winner);
        OnGameEnded?.Invoke(this, args);
    }
    public class GameResultEventArgs : EventArgs
    {
        public bool IsDraw { get; set; }
        [CanBeNull] public IPlayer Winner { get; set; }
        public GameResultEventArgs(bool isDraw, IPlayer winner)
        {
            this.IsDraw = isDraw;
            this.Winner = winner;
        }
    }

}
