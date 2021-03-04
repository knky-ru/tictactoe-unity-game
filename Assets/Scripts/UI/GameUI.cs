using System;
using CryoDI;
using UnityEngine;

namespace UI
{
    public class GameUI : CryoBehaviour
    {
        [Dependency]
        private EventEmitter Events { get; set; }

        private void Start()
        {
            Hide();
            MoveToScreen();
            Events.OnGameStart += Event_OnGameStart;
            Events.OnBackToMain += Event_OnBackToMain;
        }
        
        private void Event_OnGameStart(object sender, EventArgs e)
        {
            Show();
        }

        private void Event_OnBackToMain(object sender, EventArgs e)
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