using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public bool DO_NOT = false;

    public int _state;
    public int _cardValue;

    public bool _initialized = false;
    public bool _hasBeenShown = false;

    public Sprite _cardBack;
    public Sprite _cardFace;


    void Start()
    {
        _state = 0;
    }

    public void setupGraphics()
    {
        _cardBack = GameManager.instance.GetCardBack();
        _cardFace = GameManager.instance.GetCardFace(_cardValue);
    }

    public void flipCard()
    {
        if (_state == 0)
            _state = 1;
        else if (_state == 1)
            _state = 0;

        if (_state == 0 && !DO_NOT)
        {
            GetComponent<Image>().sprite = _cardBack;
            GetComponent<Button>().interactable = true;
        }

        else if (_state == 1 && !DO_NOT)
        {
            GetComponent<Image>().sprite = _cardFace;
            GetComponent<Button>().interactable = false;
        }

        if (!(_hasBeenShown))
            _hasBeenShown = true;

        GameManager.instance.CheckCards();
    }

    public int cardValue
    {
        get { return _cardValue; }
        set { _cardValue = value; }
    }

    public void cardFace()
    {
        GetComponent<Image>().sprite = _cardFace;
        GetComponent<Button>().interactable = false;
    }

    public void cardBack()
    {
        GetComponent<Image>().sprite = _cardBack;
        GetComponent<Button>().interactable = true;
    }


    public int state
    {
        get { return _state; }
        set { _state = value; }
    }

    public bool initialized
    {
        get { return _initialized; }
        set { _initialized = value; }
    }

    public void falseCheck()
    {
        StartCoroutine(pause());
    }

    IEnumerator pause()
    {
        yield return new WaitForSeconds(0.5f);

        if (_state == 0)
        {
            GetComponent<Image>().sprite = _cardBack;
            GetComponent<Button>().interactable = true;
        }
        else if (_state == 1)
            GetComponent<Image>().sprite = _cardFace;

        DO_NOT = false;
    }
}
