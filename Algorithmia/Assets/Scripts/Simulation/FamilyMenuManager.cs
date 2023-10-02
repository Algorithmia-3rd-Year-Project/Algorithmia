using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FamilyMenuManager : MonoBehaviour
{
    [SerializeField] private SimManager simulationManager;
    
    //Buttons related to mother
    [SerializeField] private Button spentTimeWithMotherButton;
    [SerializeField] private Button askMoneyFromMotherButton;
    [SerializeField] private Button offerMoneyToMotherButton;
    
    //Buttons related to father
    [SerializeField] private Button spentTimeWithFatherButton;
    [SerializeField] private Button askMoneyFromFatherButton;
    [SerializeField] private Button offerMoneyToFatherButton;

    [SerializeField] private GameObject parentTaskPanel;
    [SerializeField] private TMP_Text parentTaskName;
    [SerializeField] private TMP_Text parentTaskResult;
    
    private void Update()
    {
        SpentTimeWithMother();
        AskMoneyFromMother();
        OfferMoneyToMother();
    }

    private void SpentTimeWithMother()
    {
        if ((simulationManager.lastSpentTimeWithMother == 0f) || (Time.time - simulationManager.lastSpentTimeWithMother) > 20f)
        {
            spentTimeWithMotherButton.interactable = true;
        }
        else
        {
            spentTimeWithMotherButton.interactable = false;
        }
    }

    public void SpentTimeWithMotherOnClicked()
    {
        int randomNo = Random.Range(0, 10);
        parentTaskPanel.SetActive(true);
        parentTaskName.text = "You Spent time with mother";
        simulationManager.happinessLevel += randomNo;
        simulationManager.lastSpentTimeWithMother = Time.time;
        parentTaskResult.text = "Increased happiness by " + randomNo;

    }

    private void AskMoneyFromMother()
    {
        if ((simulationManager.lastRequestMoneyFromMother == 0f) || (Time.time - simulationManager.lastRequestMoneyFromMother) > 20f)
        {
            askMoneyFromMotherButton.interactable = true;
        }
        else
        {
            askMoneyFromMotherButton.interactable = false;
        }
    }

    public void AskMoneyFromMotherOnClicked()
    {
        int randomNo = Random.Range(0, 10);
        parentTaskPanel.SetActive(true);
        parentTaskName.text = "You asked money from mother";
        simulationManager.lastRequestMoneyFromMother = Time.time;

        if (randomNo % 5 == 0)
        {
            parentTaskResult.text = "Mother gave you 50 coins";
            simulationManager.coins += 50;
        }
        else
        {
            parentTaskResult.text = "She said No";
        }
    }

    private void OfferMoneyToMother()
    {
        if ((simulationManager.lastOfferMoneyToMother == 0f) || (Time.time - simulationManager.lastOfferMoneyToMother) > 20f)
        {
            offerMoneyToMotherButton.interactable = true;
        }
        else
        {
            offerMoneyToMotherButton.interactable = false;
        }
    }

    public void OfferMoneyToMotherOnClicked()
    {
        
    }
}
