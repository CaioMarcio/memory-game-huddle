using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float time;
    public float timeAmount;
    public Text timeText;

    private Image fillImage;
    private float displayMinutes;
    private float displaySeconds;

    void Start()
    {
        fillImage = GetComponent<Image>();
        timeAmount = time;
    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            fillImage.fillAmount = time / timeAmount;
            displayMinutes = Mathf.FloorToInt(time / 60.0f);
            displaySeconds = Mathf.FloorToInt(time % 60.0f);
            timeText.text = string.Format("{0:00}:{1:00}", displayMinutes, (int)displaySeconds);
        }
    }

}
