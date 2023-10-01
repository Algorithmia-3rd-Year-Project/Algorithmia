using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TasksMenuManager : MonoBehaviour
{

    [SerializeField] private SimManager simulationManager;
    [SerializeField] private TaskManager taskManager;
    
    [SerializeField] private Button bookReadButton;
    [SerializeField] private GameObject bookReadPanel;
    [SerializeField] private TMP_Text bookNameText;
    
    [SerializeField] private GameObject outOfMoneyPanel;

    private void Update()
    {
        ReadABookButton();
    }

    private void ReadABookButton()
    {
        if (simulationManager.lastBookReadTime > Time.time || (Time.time - simulationManager.lastBookReadTime) > 20f)
        {
            bookReadButton.interactable = true;
        }
        else
        {
            bookReadButton.interactable = false;
        }
    }

    public void ReadABookOnClicked()
    {
        if (simulationManager.coins > 40)
        {
            simulationManager.coins -= 40;
            simulationManager.energyLevel += 10;
            simulationManager.lastBookReadTime = Time.time;
            bookReadPanel.SetActive(true);
            bookNameText.text = "You have read " + taskManager.BookName() + ". ";
        }
        else
        {
            outOfMoneyPanel.SetActive(true);
        }
        
        
    }

}
