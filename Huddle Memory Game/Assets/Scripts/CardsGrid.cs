using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsGrid : MonoBehaviour {

    #region Public Variables

    [Header("Tamanho das Cartas (3 à 4 pares)")]
    [Range(100, 110)]
    public int cardSizeShortPairs;

    [Header("Tamanho das Cartas (5 à 7 pares)")]
    [Range(80, 90)]
    public int cardSizeMediumPairs;

    [Header("Tamanho das Cartas (8 à 10 pares)")]
    [Range(60, 70)]
    public int cardSizeManyPairs;

    public int _cardPairs;

    #endregion

    public void OrganizeGrid () {

        if (_cardPairs == 3)
        {    
            GetComponent<GridLayoutGroup>().constraintCount = 3;
            GetComponent<GridLayoutGroup>().spacing = new Vector2(90, 90);
            GetComponent<GridLayoutGroup>().cellSize = new Vector2(cardSizeShortPairs, cardSizeShortPairs);
        }

        else if ((_cardPairs == 4) || (_cardPairs == 6) || (_cardPairs == 8))
        {
            GetComponent<GridLayoutGroup>().constraintCount = 4;

            if (_cardPairs == 4)
            {
                GetComponent<GridLayoutGroup>().spacing = new Vector2(90, 90);
                GetComponent<GridLayoutGroup>().cellSize = new Vector2(cardSizeShortPairs, cardSizeShortPairs);
            }

            else if (_cardPairs == 6)
            {
                GetComponent<GridLayoutGroup>().spacing = new Vector2(70, 70);
                GetComponent<GridLayoutGroup>().cellSize = new Vector2(cardSizeMediumPairs, cardSizeMediumPairs);
            }

            else if (_cardPairs == 8)
            {
                GetComponent<GridLayoutGroup>().spacing = new Vector2(70, 60);
                GetComponent<GridLayoutGroup>().cellSize = new Vector2(cardSizeManyPairs, cardSizeManyPairs);
            }
        }

        else if ((_cardPairs == 5) || (_cardPairs == 7) || (_cardPairs == 10))
        {
            GetComponent<GridLayoutGroup>().constraintCount = 5;

            if (_cardPairs == 5)
            {
                GetComponent<GridLayoutGroup>().spacing = new Vector2(70, 70);
                GetComponent<GridLayoutGroup>().cellSize = new Vector2(cardSizeMediumPairs, cardSizeMediumPairs);
            }

            else if (_cardPairs == 7)
            {
                GetComponent<GridLayoutGroup>().spacing = new Vector2(70, 70);
                GetComponent<GridLayoutGroup>().cellSize = new Vector2(cardSizeMediumPairs, cardSizeMediumPairs);
            }

            else if (_cardPairs == 10)
            {
                GetComponent<GridLayoutGroup>().spacing = new Vector2(70, 60);
                GetComponent<GridLayoutGroup>().cellSize = new Vector2(cardSizeManyPairs, cardSizeManyPairs);
            }
        }

        else if (_cardPairs == 9)
        {
            GetComponent<GridLayoutGroup>().constraintCount = 6;
            GetComponent<GridLayoutGroup>().spacing = new Vector2(70, 70);
            GetComponent<GridLayoutGroup>().cellSize = new Vector2(cardSizeManyPairs, cardSizeManyPairs);
        }
    }
}
