using System;
using CryoDI;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Placeholder : CryoBehaviour
    {

        [Dependency]
        private EventEmitter Events { get; set; }

        [Dependency]
        private TicTacToe Game { get; set; }

        [SerializeField] private int index;
        [SerializeField] private Sprite empty;
        [SerializeField] private Sprite cross;
        [SerializeField] private Sprite circle;
        

        private FigureType figure = FigureType.EMPTY;

        private void Start()
        {
            Events.OnGameRestart += Event_OnGameRestart;
            Events.OnRobotClickPlaceholder += Event_OnRobotClickPlaceholder;
        }
        
        private void Event_OnGameRestart(object sender, EventArgs e)
        {
            Reset();
        }

        private void Event_OnRobotClickPlaceholder(object sender, EventEmitter.IntegerEventArgs e)
        {
            if (e.Value == index)
                OnRobotClick();
        }

        public void OnClick()
        {
            if (figure != FigureType.EMPTY || Game.IsWaitForMove()) return;
            SetFigure();
        }

        public void OnRobotClick()
        {
            if (figure != FigureType.EMPTY) return;
            SetFigure();
        }

        private void SetFigure()
        {
            figure = Game.CurrentFigure();
            Events.OnClickPlaceholder_Event(index);
            ChangeIcon(figure);
        }

        private void Reset()
        {
            figure = FigureType.EMPTY;
            ChangeIcon(FigureType.EMPTY);
        }

        private void ChangeIcon(FigureType type)
        {
            Image image = GetComponent<Image>();
            switch (type)
            {
                case FigureType.CROSS:
                    image.sprite = cross;
                    break;
                case FigureType.CIRCLE:
                    image.sprite = circle;
                    break;
                case FigureType.EMPTY:
                    image.sprite = empty;
                    break;
            }
        }
    }
}