using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject refreshButton;
    [SerializeField] private GameObject downloadButton;
    
    private int buttonPressedCount;

    private void Update()
    {
        if (buttonPressedCount == 9)
        {
            refreshButton.SetActive(false);
            downloadButton.SetActive(true);
        }
    }

    public void TrackKeys()
    {
        if (buttonPressedCount == 0)
        {
            animator.SetTrigger("Clip1");
            buttonPressedCount += 1;
        } else if (buttonPressedCount == 1)
        {
            animator.SetTrigger("Clip2");
            buttonPressedCount += 1;
        } else if (buttonPressedCount == 2)
        {
            animator.SetTrigger("Clip3");
            buttonPressedCount += 1;
        } else if (buttonPressedCount == 3)
        {
            animator.SetTrigger("Clip4");
            buttonPressedCount += 1;
        } else if (buttonPressedCount == 4)
        {
            animator.SetTrigger("Clip5");
            buttonPressedCount += 1;
        } else if (buttonPressedCount == 5)
        {
            animator.SetTrigger("Clip6");
            buttonPressedCount += 1;
        } else if (buttonPressedCount == 6)
        {
            animator.SetTrigger("Clip7");
            buttonPressedCount += 1;
        } else if (buttonPressedCount == 7)
        {
            animator.SetTrigger("Clip8");
            buttonPressedCount += 1;
        } else if (buttonPressedCount == 8)
        {
            animator.SetTrigger("Clip9");
            buttonPressedCount += 1;
        }
    }

    public void ContinueToGame()
    {
        SceneManager.LoadSceneAsync("Level 1 Array");
    }
    
}