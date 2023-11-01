using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayQuiz : MonoBehaviour
{

    [SerializeField] private List<Transform> questions;

    private void Start()
    {
        //AnswerTracker();
    }

    private void AnswerTracker()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            for (int j = 0; j < questions[i].childCount; j++)
            {
                if (questions[i].GetChild(j).gameObject.name != "Question")
                {
                    Button answerButton = questions[i].GetChild(j).gameObject.GetComponent<Button>();
                    answerButton.onClick.AddListener(() => RecordAnswer());
                }

            }
        }
    }

    public void RecordAnswer()
    {
        Debug.Log("Fewfw");
    }


}
