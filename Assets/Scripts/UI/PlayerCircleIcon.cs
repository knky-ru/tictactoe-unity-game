using System;
using CryoDI;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerCircleIcon : CryoBehaviour
    {
        [Dependency]
        private EventEmitter Events { get; set; }

        [Dependency]
        private TicTacToe Game { get; set; }

        private const FigureType representFigure = FigureType.CIRCLE;

        private void Start()
        {
            Events.OnSetFigure += Event_OnSetFigure;
            Events.OnGameRestart += Event_OnGameRestart;
        }

        private void Event_OnGameRestart(object sender, EventArgs e)
        {
            Activate();
        }

        private void Event_OnSetFigure(object sender, EventEmitter.FigureEventArgs e)
        {
            if (e.Figure == representFigure)
                Activate();
            else
                Deactivate();
        }

        public void OnClick()
        {
            if (Game.IsPlayerSelected()) return;
            Events.OnSelectFigure_Event(FigureType.CIRCLE);
            Events.OnSetFigure_Event(FigureType.CROSS);
        }
        
        private void Activate()
        {
            gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
        }

        private void Deactivate()
        {
            gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.1f);
        }


    }
}
