using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrayQuiz : MonoBehaviour
{

    [SerializeField] private List<Transform> questions;

    [SerializeField] private List<string> answerList;

    [SerializeField] private GameObject singleCorrectAnswer;
    [SerializeField] private GameObject singleWrongAnswer;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject continueButton;

    private int questionIndex;
    [SerializeField] private int correctAnswerCount;

    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TMP_Text correctAnswerCountText;
    
    private void Start()
    {
        AnswerTracker();
    }

    private void AnswerTracker()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            for (int j = 0; j < questions[i].childCount; j++)
            {
                if (questions[i].GetChild(j).gameObject.name != "Question")
                {
                    Transform currentQuestion = questions[i];
                    Button answerButton = questions[i].GetChild(j).gameObject.GetComponent<Button>();
                    answerButton.onClick.AddListener(() => RecordAnswer(answerButton.gameObject, currentQuestion));
                }

            }
        }
    }

    public void RecordAnswer(GameObject answerObject, Transform questionObject)
    {
        //Check the status of the selected answer for the question 1
        if (questionObject.gameObject.name == "Question 1" &&
            EventSystem.current.currentSelectedGameObject.name == "Answer 4")
        {
            correctAnswerCount += 1;
            singleCorrectAnswer.SetActive(true);
        } else if (questionObject.gameObject.name == "Question 1" &&
                   EventSystem.current.currentSelectedGameObject.name != "Answer 4")
        {
            singleWrongAnswer.SetActive(true);
        }
        
        
        //Check the status of the selected answer for the question 2
        if (questionObject.gameObject.name == "Question 2" &&
            EventSystem.current.currentSelectedGameObject.name == "Answer 3")
        {
            correctAnswerCount += 1;
            singleCorrectAnswer.SetActive(true);
        } else if (questionObject.gameObject.name == "Question 2" &&
                   EventSystem.current.currentSelectedGameObject.name != "Answer 3")
        {
            singleWrongAnswer.SetActive(true);
        }
        
        //Check the status of the selected answer for the question 3
        if (questionObject.gameObject.name == "Question 3" &&
            EventSystem.current.currentSelectedGameObject.name == "Answer 2")
        {
            correctAnswerCount += 1;
            singleCorrectAnswer.SetActive(true);
        } else if (questionObject.gameObject.name == "Question 3" &&
                   EventSystem.current.currentSelectedGameObject.name != "Answer 2")
        {
            singleWrongAnswer.SetActive(true);
        }
        

        //Activate answer statuses for the current question
        for (int i = 0; i < questionObject.childCount; i++)
        {
            if (questionObject.GetChild(i).gameObject.name != "Question")
            {
                Transform resultObject = questionObject.GetChild(i).transform.Find("Result");
                resultObject.gameObject.SetActive(true);
            }
        }
        
        //make next question button or continue button visible
        if (questionIndex == 2)
        {
            continueButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
        }
    }

    public void NextQuestion()
    {
        questions[questionIndex].gameObject.SetActive(false);
        questionIndex += 1;
        questions[questionIndex].gameObject.SetActive(true);
        nextButton.SetActive(false);
        singleCorrectAnswer.SetActive(false);
        singleWrongAnswer.SetActive(false);
        
    }

    public void LoadVictoryMenu()
    {
        victoryPanel.SetActive(true);
        correctAnswerCountText.text = correctAnswerCount.ToString();
    }


}
