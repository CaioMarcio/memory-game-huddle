using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    #region Public Variables

    [Header("Prefab da Carta")]
    public GameObject cardPrefab;

    [Header("Sprite das Costas da Carta")]
    public Sprite spriteCardBack;

    [Header("Sprites da Frente da Carta")]
    public Sprite[] spriteCardFace;

    [Header("TEMPOS")]
    [Header("Tempo mínimo do jogo (em segundos)")]
    [Range(5, 60)]
    public int minimumTime;

    [Header("Tempo adicionado para um novo par (em segundos)")]
    [Range(5, 20)]
    public int timeAddedByPair;

    [Header("Tempo para desvirar as Cartas (Inicio) (em segundos)")]
    [Range(2, 5)]
    public float firstUncoverTime;

    [Header("Tempo para desvirar as Cartas (Jogo) (em segundos)")]
    [Range(0.5f, 2f)]
    public float cardUncoverTime;

    [Header("PONTUAÇÃO")]
    [Header("Pontos por Par Correto")]
    [Range(10, 20)]
    public int pointsByCorrectPair;

    [HideInInspector]
    public int playerScore = 0;

    [HideInInspector]
    public int cardPairs;

    [HideInInspector]
    public int cardAmount = 0;

    public static GameManager instance;

    #endregion

    #region Private Variables

    // Número Total de Cartas
    private int totalNumberOfCards;
    
    // Lista de Cartas do Jogo
    private GameObject[] cards;

    // Tempo Total da Partida
    private int totalTime;

    // Número de Combos
    private int combo = 0;

    // Número minimo de pares possiveis
    private int minimumCardPair = 3;

    #endregion

    #region Awake

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

    #endregion

    #region Starting the Game

    public void StartGame()
    {
        totalNumberOfCards = (cardPairs * 2);
        cards = new GameObject[totalNumberOfCards];

        CreatingCards();
        InitializeCards();
        OrganizeGrid();
        AddTime();
        FirstView();
    }

    void CreatingCards()
    {
        for (int i = 0; i < totalNumberOfCards; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, transform.position, transform.rotation);
            newCard.transform.SetParent(MenuManager.instance.CardsUI.transform);
            cards[i] = newCard;
        }     
    }

    private void InitializeCards()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 1; j < (cardPairs + 1); j++)
            {
                bool cardInitialized = false;
                int choice = 0;
                while (!cardInitialized)
                {
                    choice = Random.Range(0, cards.Length);
                    cardInitialized = !(cards[choice].GetComponent<Card>().initialized);
                }
                cards[choice].GetComponent<Card>().cardValue = j;
                cards[choice].GetComponent<Card>().initialized = true;
            }
        }

        foreach (GameObject c in cards)
        {
            c.GetComponent<Card>().setupGraphics();
        }
    }

    void OrganizeGrid()
    {
        MenuManager.instance.CardsUI.GetComponent<CardsGrid>().OrganizeGrid();
    }

    void AddTime()
    {
        if (minimumCardPair != cardPairs)
        {
            for (int i = 0; i < (cardPairs - minimumCardPair); i++)
            {
                totalTime += timeAddedByPair;
            }
        }

        totalTime += minimumTime;

        MenuManager.instance.TimerUI.GetComponent<Timer>().time = totalTime;
    }

    void FirstView()
    {
        foreach (GameObject c in cards)
        {
            c.GetComponent<Card>().cardFace();
        }

        StartCoroutine(FirstUncoverCards());
    }

    IEnumerator FirstUncoverCards()
    {
        yield return new WaitForSeconds(firstUncoverTime);

        foreach (GameObject c in cards)
        {
            c.GetComponent<Card>().cardBack();
        }

        MenuManager.instance.TimerUI.SetActive(true);
    }



    #endregion

    public Sprite GetCardBack()
    {
        return spriteCardBack;
    }

    public Sprite GetCardFace(int i)
    {
        return spriteCardFace[i - 1];
    }

    public void CheckCards()
    {
        List<int> c = new List<int>();

        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<Card>().state == 1)
                c.Add(i);
        }

        if (c.Count == 2)
        {
            int _cardAmount = cardAmount;

            CardComparison(c);

            StartCoroutine(Pause(_cardAmount));
        }
    }

    IEnumerator Pause(int _cardAmount)
    {
        yield return new WaitForSeconds(cardUncoverTime);
    }

    private void CardComparison(List<int> c)
    {
        int x = 0;

        if (cards[c[0]].GetComponent<Card>().cardValue == cards[c[1]].GetComponent<Card>().cardValue)
        {
            x = 2;
            cardAmount++;
            combo++;
            AddScore();
            AudioManager.instance.pairFoundSound();
            if (cardAmount == cardPairs)
                CheckWinner();
        } else
        {
            combo = 0;
        }

        MenuManager.instance.comboText.text = "Combo: " + combo;

        for (int i = 0; i < c.Count; i++)
        {
            cards[c[i]].GetComponent<Card>().state = x;
            cards[c[i]].GetComponent<Card>().falseCheck();
        }
    }

    public void AddScore()
    {
        playerScore += (pointsByCorrectPair * combo);
        MenuManager.instance.RefreshScoreText();
    }

    private void CheckWinner()
    {
        playerScore += Mathf.RoundToInt(MenuManager.instance.TimerUI.GetComponent<Timer>().time);
        MenuManager.instance.RefreshScoreText();
        MenuManager.instance.WinnerScreen();
    } 
}