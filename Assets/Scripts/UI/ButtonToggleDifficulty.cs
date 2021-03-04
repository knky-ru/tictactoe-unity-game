using System;
using CryoDI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonToggleDifficulty : CryoBehaviour
    {
        [Dependency]
        private EventEmitter Events { get; set; }

        private int index = 0;
        private GameObject label;
        private readonly string[] labels = new[] { "Easy squizy", "Medium fortress", "Don't even try" };

        private void Start()
        {
            label = GameObject.Find("ButtonToggleDifficultyText");
        }

        public void OnClick()
        {
            index++;
            if (index >= 3) index = 0;
            Events.OnDifficultyChange_Event(index);
            ChangeText(index);
        }

        private void ChangeText(int index)
        {
            label.GetComponent<Text>().text = labels[index];
        }

        
    }
}
