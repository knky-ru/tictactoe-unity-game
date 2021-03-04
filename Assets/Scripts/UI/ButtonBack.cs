using System;
using CryoDI;
using UnityEngine;

namespace UI
{
    public class ButtonBack : CryoBehaviour
    {
        [Dependency]
        private EventEmitter Events { get; set; }

        private void Start()
        {
            Hide();
            Events.OnSetFigure += Event_OnSetFigure;
            Events.OnGameRestart += Event_OnGameRestart;
        }

        private void Event_OnSetFigure(object sender, EventEmitter.FigureEventArgs e)
        {
            Show();
        }

        private void Event_OnGameRestart(object sender, EventArgs e)
        {
            Hide();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnClick()
        {
            Events.OnBackToMain_Event();
            Events.OnGameRestart_Event();
        }
    
    }
}
