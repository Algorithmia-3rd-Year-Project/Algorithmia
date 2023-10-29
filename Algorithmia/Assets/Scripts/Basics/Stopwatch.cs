using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{

    [SerializeField]
    private TMP_Text stopwatchText;

    public float currentTime;

    public bool isPaused;
    
    private void Start()
    {
        currentTime = 0f;
    }
    

    private void Update()
    {
        if (!isPaused)
        {
            if (currentTime >= 0)
            {
                currentTime += Time.deltaTime;

                int minutes = Mathf.FloorToInt(currentTime / 60);
                int seconds = Mathf.FloorToInt(currentTime % 60);

                string timeFormatted = string.Format("{0:00}:{1:00}", minutes, seconds);
                stopwatchText.text = timeFormatted;
            }
        }
    }

}
