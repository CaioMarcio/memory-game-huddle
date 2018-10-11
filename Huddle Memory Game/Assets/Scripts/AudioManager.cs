using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [Header("Som dos botões da UI")]
    public Sound clickButton;

    [Header("Som do Par Encontrado")]
    public Sound pairFound;

    [Header("Som de Vitória")]
    public Sound winner;

    public static AudioManager instance;

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

    public void clickTheButton()
    {
        GetComponent<AudioSource>().volume = clickButton.volume;
        GetComponent<AudioSource>().pitch = clickButton.pitch ;
        GetComponent<AudioSource>().PlayOneShot(clickButton.clip);
    }

    public void pairFoundSound()
    {
        GetComponent<AudioSource>().volume = pairFound.volume;
        GetComponent<AudioSource>().pitch = pairFound.pitch;
        GetComponent<AudioSource>().PlayOneShot(pairFound.clip);
    }

    public void winnerSound()
    {
        GetComponent<AudioSource>().volume = winner.volume;
        GetComponent<AudioSource>().pitch = winner.pitch;
        GetComponent<AudioSource>().PlayOneShot(winner.clip);
    }


}
