using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreelanceManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TMP_InputField freelanceNameInput;
    [SerializeField] private TMP_InputField freelanceUsernameInput;
    [SerializeField] private TMP_InputField freelanceBioInput;

    private string freelanceName;
    private string freelanceUsername;
    private string freelanceBio;
    private bool hasFreelanceAccount;
    private bool afterAssignmentCutScenePlayed;

    //pages
    [SerializeField] private GameObject freelanceMainPage;
    [SerializeField] private GameObject freelanceSignupPage;
    
    //Main page items
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text bioText;

    [SerializeField] private GameObject lockedApp;
    
    [SerializeField] private GameObject noEnergyMenu;
    [SerializeField] private SimManager simulationManager;

    private bool freelance1Done;

    [SerializeField] private GameObject freelance1Completed;
    [SerializeField] private GameObject freelance1AcceptButton;
    [SerializeField] private GameObject freelance1ReviewButton;

    private void Start()
    {
        if (freelance1Done)
        {
            freelance1AcceptButton.SetActive(false);
            freelance1ReviewButton.SetActive(true);
            freelance1Completed.SetActive(true);
        }
    }

    public void LoadData(GameData data)
    {
        this.nameText.text = data.freelanceName;
        this.bioText.text = data.freelanceBio;
        this.hasFreelanceAccount = data.hasFreelanceAccount;
        this.afterAssignmentCutScenePlayed = data.afterAssignmentCutScenePlayed;

        this.freelanceName = data.freelanceName;
        this.freelanceUsername = data.freelanceUsername;
        this.freelanceBio = data.freelanceBio;

        this.freelance1Done = data.freelance1Done;
    }

    public void SaveData(ref GameData data)
    {
        data.freelanceName = this.freelanceName;
        data.freelanceUsername = this.freelanceUsername;
        data.freelanceBio = this.freelanceBio;
        data.hasFreelanceAccount = this.hasFreelanceAccount;

        data.freelance1Done = this.freelance1Done;
    }

    public void SignUpButtonOnClicked()
    {
        freelanceName = freelanceNameInput.text;
        freelanceUsername = freelanceUsernameInput.text;
        freelanceBio = freelanceBioInput.text;
        
        freelanceSignupPage.SetActive(false);
        freelanceMainPage.SetActive(true);

        nameText.text = freelanceName;
        bioText.text = freelanceBio;

        hasFreelanceAccount = true;
    }

    public void FreelanceAppOnClicked()
    {
        if (afterAssignmentCutScenePlayed)
        {
            if (hasFreelanceAccount)
            {
                freelanceSignupPage.SetActive(false);
                freelanceMainPage.SetActive(true);
            }
            else
            {
                freelanceSignupPage.SetActive(true);
                freelanceMainPage.SetActive(false);
            }
        }
        else
        {
            lockedApp.SetActive(true);
        }
        
        
    }
    
    public void LoadFreelanceLevels(string levelName)
    {
        if (simulationManager.energyLevel - 20 <= 0)
        {
            noEnergyMenu.SetActive(true);
            return;
        }

        //Todo - playtime saving
        simulationManager.energyLevel -= 20;
        SceneManager.LoadSceneAsync(levelName);
    }

    public void ReviewLevels(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
    }
    
}
