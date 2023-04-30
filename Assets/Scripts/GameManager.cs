using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // manage the player's amount of turns left
    [SerializeField] private int turnsLeft;
    [SerializeField] private int scoreToMatch;
    [SerializeField] private Text turnsLeftText;
    [SerializeField] private Text gameResultText;
    [SerializeField] private GameObject gameOverPanelGameObject;
    [SerializeField] private GameObject matchesFoundGameObject;

    private bool gameOver = false;// ends the game
    private int matchesFound = 0;

    private void Start()
    {
        gameOverPanelGameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameOver)// checking that it is not game over
        {
            if (turnsLeft <= 0)
            {
                gameOver = true;
                turnsLeftText.GetComponent<Text>().text = "0";
            }
            else
            {
                turnsLeftText.GetComponent<Text>().text = turnsLeft.ToString();
            }

            matchesFoundGameObject.GetComponent<Text>().text = matchesFound.ToString();
        }
        else
        {
            gameOverPanelGameObject.SetActive(true);
            if (matchesFound >= scoreToMatch)
            {
                gameResultText.text = "YAAAAAAAAYYYY!!! YOU WON";
            }
            else
            {
                gameResultText.text = "BAAAAHAHAHAHAHA, YOU LOST LMAO";
            }
        }
    }

    public void SubtractOneFromTurnsLeft()
    {
        turnsLeft -= 1;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void AddOneToMatchesFound()
    {
        matchesFound += 1;
    }

    public void ReloadScene()
    {
        ReloadScene();
    }
}