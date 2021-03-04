using System;
using CryoDI;
using Enums;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreUIElements : CryoBehaviour
    {
        [Dependency]
        private EventEmitter Events { get; set; }

        [Dependency]
        private TicTacToe Game { get; set; }

        private GameObject resultMessageText;
        private GameObject scoreYouCount;
        private GameObject scoreRobotCount;
            
        private void Start()
        {
            resultMessageText = GameObject.Find("ResultMessageText");
            scoreYouCount = GameObject.Find("ScoreYouCount");
            scoreRobotCount = GameObject.Find("ScoreRobotCount");
            
            Events.OnShowScore += Event_OnShowScore;
        }

        private void Event_OnShowScore(object sender, EventArgs e)
        {
            FillResults();
        }

        private void Reset()
        {
            resultMessageText.GetComponent<Text>().text = "";
            scoreYouCount.GetComponent<Text>().text = "00";
            scoreRobotCount.GetComponent<Text>().text = "00";
        }

        private void FillResults()
        {
            UpdateWinner();
            UpdateScores();
        }

        private void UpdateWinner()
        {
            resultMessageText.GetComponent<Text>().text = Game.GetGameResult().GetResultMessage();
        }

        private void UpdateScores()
        {
            scoreYouCount.GetComponent<Text>().text = Game.GetScore().GetPlayerScore().ToString();
            scoreRobotCount.GetComponent<Text>().text = Game.GetScore().GetRobotScore().ToString();
        }
    }
}