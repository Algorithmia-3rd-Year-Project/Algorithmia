using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreelanceCutSceneManager : MonoBehaviour, IDataPersistence
{

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject refreshButton;
    [SerializeField] private GameObject gotoFreelanceButton;
    
    private int buttonPressedCount;
    private bool afterAssignmentCutScenePlayed;

    private void Update()
    {
        
        if (buttonPressedCount == 10)
        {
            refreshButton.SetActive(false);
            gotoFreelanceButton.SetActive(true);
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
        } else if (buttonPressedCount == 9)
        {
            animator.SetTrigger("Clip10");
            buttonPressedCount += 1;
        } 
    }

    
    public void LoadData(GameData data)
    {
        this.afterAssignmentCutScenePlayed = data.afterAssignmentCutScenePlayed;
    }

    public void SaveData(ref GameData data)
    {
        data.afterAssignmentCutScenePlayed = this.afterAssignmentCutScenePlayed;
    }

    public void ContinueToFreelance()
    {
        afterAssignmentCutScenePlayed = true;
        SceneManager.LoadSceneAsync("Scenes/Simulation");
    }
    
}