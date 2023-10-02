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

    [SerializeField] private Button enrollLibraryButton;
    [SerializeField] private GameObject cancelMembershipButton;
    private bool membershipRenewal;
    private bool canRenew = true;
    private float renewalTime;
    
    private void Update()
    {
        ReadABookButton();
        LibraryMembership();
        //UpdateLibraryMembership();
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
        if (simulationManager.coins >= 40)
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

    public void EnrollAtLibraryOnClicked()
    {
        if (simulationManager.coins >= 500)
        {
            simulationManager.coins -= 500;
            simulationManager.energyLevel += 50;
            simulationManager.enrolledAtLibrary = true;
            simulationManager.enrolledAtLibraryTime = simulationManager.totalPlayTime;
            enrollLibraryButton.interactable = false;
            cancelMembershipButton.SetActive(true);
        }
        else
        {
            outOfMoneyPanel.SetActive(true);
        }
    }

    //To control the players ability to enroll or cancel out from enrollment depending on saved data
    private void LibraryMembership()
    {
        if (simulationManager.enrolledAtLibrary)
        {
            cancelMembershipButton.SetActive(true);
            enrollLibraryButton.interactable = false;
        }
        else
        {
            cancelMembershipButton.SetActive(false);
            enrollLibraryButton.interactable = true;
        }
    }

    //To cancel out from an ongoing membership at a library
    public void CancelLibraryMembership()
    {
        simulationManager.enrolledAtLibrary = false;
        simulationManager.enrolledAtLibraryTime = 0.0f;
    }

    //This function is used to renew the library membership if the player has already enrolled at a library
    //But this does not work properly yet. Not getting updated correctly once a week passed
    private void UpdateLibraryMembership()
    {
        if (!simulationManager.enrolledAtLibrary)
        {
            return;
        }

        if (simulationManager.enrolledAtLibraryTime < simulationManager.totalPlayTime + 10f)
        {
            return;
        }
        
        int enrolledTime = (int)simulationManager.totalPlayTime - (int)simulationManager.enrolledAtLibraryTime;
        if (enrolledTime % 6300 == 0)
        {
            membershipRenewal = true;
        }

        if (membershipRenewal && canRenew)
        {
            simulationManager.coins -= 500;
            simulationManager.energyLevel += 50;
            //Display library membership has updated message on daily log
            membershipRenewal = false;
            canRenew = false;
            renewalTime = Time.time;
        }

        if ((int)renewalTime == 90)
        {
            canRenew = true;
        }
        
    }

}
