using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksMenuManager : MonoBehaviour
{

    [SerializeField] private SimManager simulationManager;

    [SerializeField] private Button bookReadButton;

    private void Update()
    {
        ReadABookButton();
    }

    private void ReadABookButton()
    {
        if (Mathf.Abs(Time.time - simulationManager.lastBookReadTime) > 20f)
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
        simulationManager.coins -= 40;
        simulationManager.energyLevel += 10;
        simulationManager.lastBookReadTime = Time.time;
    }

}
