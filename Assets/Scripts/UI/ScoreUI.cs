using System;
using CryoDI;
using UnityEngine;

namespace UI
{
    public class ScoreUI : CryoBehaviour
    {
        [Dependency]
        private EventEmitter Events { get; set; }

        private void Start()
        {
            Hide();
            MoveToScreen();
            Events.OnShowScore += Event_OnShowScore;
            Events.OnGameRestart += Event_OnGameRestart;
        }
        
        private void Event_OnShowScore(object sender, EventArgs e)
        {
            Show();
        }

        private void Event_OnGameRestart(object sender, EventArgs e)
        {
            Hide();
        }

        private void MoveToScreen()
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.position = new Vector3(0, 0, 0);
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }
    }
}