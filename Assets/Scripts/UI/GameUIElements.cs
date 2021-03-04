using System;
using CryoDI;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUIElements : CryoBehaviour
    {
        [Dependency]
        private EventEmitter Events { get; set; }

        private GameObject infoText;
        private GameObject playerCrossText;
        private GameObject playerCircleText;
        private GameObject gameField;
            
        private void Start()
        {
            infoText = GameObject.Find("InfoText");
            playerCrossText = GameObject.Find("PlayerCrossText");
            playerCircleText = GameObject.Find("PlayerCircleText");
            gameField = GameObject.Find("GameField");

            Reset();
            
            Events.OnGameStart += Event_OnGameStart;
            Events.OnSelectFigure += Event_OnSelectFigure;
            Events.OnGameRestart += Event_OnGameRestart;
        }

        private void Event_OnGameRestart(object sender, EventArgs e)
        {
            Reset();
        }

        private void Event_OnSelectFigure(object sender, EventEmitter.FigureEventArgs e)
        {
            ChangePlayerNames(e.Figure);
            ChangeInfoTitle();
            ShowGameField();
        }

        private void Event_OnGameStart(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            infoText.GetComponent<Text>().text = "Select player";
            playerCrossText.GetComponent<Text>().text = "First";
            playerCircleText.GetComponent<Text>().text = "Second";
            gameField.SetActive(false);
        }

        private void ShowGameField()
        {
            gameField.SetActive(true);
        }

        private void ChangeInfoTitle()
        {
            infoText.GetComponent<Text>().text = "";
        }

        private void ChangePlayerNames(FigureType playerFigure)
        {
            playerCrossText.GetComponent<Text>().text = (playerFigure == FigureType.CROSS) ? Dictionary.PlayerNames["PLAYER"] : Dictionary.PlayerNames["AI"];
            playerCircleText.GetComponent<Text>().text = (playerFigure == FigureType.CIRCLE) ? Dictionary.PlayerNames["PLAYER"] : Dictionary.PlayerNames["AI"];
        }
    }
}