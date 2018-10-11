using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    #region Public Variables

    [Header("TELAS")]
    [Header("Menu Principal")]
    public GameObject MainMenuScreen;

    [Header("Tela de escolha de Pares")]
    public GameObject PairsScreen;

    [Header("Tela Principal do Jogo")]
    public GameObject GameScreen;

    [Header("Tela de Pausa")]
    public GameObject PauseScreen;

    [Header("Tela do Resultado Final")]
    public GameObject ResultScreen;

    [Header("Tela para enviar Resultado Final")]
    public GameObject SendResultScreen;

    [Header("Tela de Resultado Enviado")]
    public GameObject SentResultScreen;

    [Header("UI (Textos) (Game)")]
    [Header("Combo")]
    public Text comboText;

    [Header("Pontuação do Jogador")]
    public Text playerScoreText;

    [Header("Pontuação Final do Jogador")]
    public Text playerFinalScoreText;

    [Header("Nome do Jogador")]
    public Text playerNameText;

    [Header("Texto com o Tempo da Partida")]
    public Text timerText;

    [Header("UI (Textos) (Send Result)")]
    [Header("Pontuação Final do Jogador")]
    public Text playerSendFinalScoreText;

    [Header("Status do Jogo")]
    public Text resultText;

    [Header("UI")]
    [Header("UI das Cartas na tela")]
    public GameObject CardsUI;

    [Header("UI do Tempo na tela")]
    public GameObject TimerUI;

    [Header("UI (Textos) (Pair Choice)")]
    [Header("Texto do número de pares escolhidos")]
    public Text pairNumberText;

    public static MenuManager instance;

    #endregion

    #region Private Variables

    private bool timeEnded = false;

    #endregion

    #region Awake and FixedUpdate

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (!timeEnded)
        {
            if (timerText.text == "00:00")
            {
                TimeIsUpScreen();
                timeEnded = true;
            }
        }
    }

    #endregion

    #region Resume and Pause

    public void Resume()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pausing()
    {
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    #endregion

    #region Screen Transitions

    public void PlayGame()
    {
        CardsUI.GetComponent<CardsGrid>()._cardPairs = int.Parse(pairNumberText.text);
        GameManager.instance.cardPairs = int.Parse(pairNumberText.text);
        GameManager.instance.StartGame();
    
        PairsScreen.SetActive(false);
        GameScreen.SetActive(true);
    }

    public void PairChooseScreen()
    {
        MainMenuScreen.SetActive(false);
        PairsScreen.SetActive(true);
    }

    public void TimeIsUpScreen()
    {
        AudioManager.instance.winnerSound();
        ResultScreen.SetActive(true);
        resultText.text = "time is up!";
        Time.timeScale = 0f;
    }

    public void WinnerScreen()
    {
        AudioManager.instance.winnerSound();
        ResultScreen.SetActive(true);
        resultText.text = " you win!";
        Time.timeScale = 0f;
    }

    public void SendingResultScreen()
    {
        GameScreen.SetActive(false);
        ResultScreen.SetActive(false);
        SendResultScreen.SetActive(true);
        Time.timeScale = 1f;
    }

    public void SentResult()
    {
        AudioManager.instance.winnerSound();
        SendResultScreen.SetActive(false);
        SentResultScreen.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    #endregion

    #region RefreshScoreText

    public void RefreshScoreText()
    {
        playerScoreText.text = GameManager.instance.playerScore.ToString();
        playerFinalScoreText.text = "Score: " + playerScoreText.text;
        playerSendFinalScoreText.text = "Score: " + playerScoreText.text;
    }

    #endregion
}