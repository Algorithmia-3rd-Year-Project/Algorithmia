using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool hasLogged;

    private bool alreadyGuest;

    [SerializeField] private GameObject accountSelectionScreen;

    [SerializeField] private TMP_InputField guestNameInput;
    [SerializeField] private TMP_Text loggedUsernameText;

    [SerializeField] private GameObject newGameObjects;
    [SerializeField] private GameObject continueGameObjects;

    public string currentUsername;
    
    private void Awake()
    {
        //need to retrieve this from log files
        hasLogged = false;
        alreadyGuest = false;
    }

    private void Start()
    {
        if (!hasLogged && !alreadyGuest)
        {
            accountSelectionScreen.SetActive(true);
        }

        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameObjects.SetActive(false);
            newGameObjects.SetActive(true);
        } else if (DataPersistenceManager.instance.HasGameData())
        {
            continueGameObjects.SetActive(true);
            newGameObjects.SetActive(false);
        }
    }

    public void SaveGuestName()
    {
        currentUsername = guestNameInput.text;
        loggedUsernameText.text = currentUsername;
    }

    public void PlayNewGame()
    {
        //Load the cutscene for new players or the simulation screen for old players
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Scenes/Simulation");
    }

    public void ContinueGame()
    {
        SceneManager.LoadSceneAsync("Scenes/Simulation");
    }

    public void LoadAchievements()
    {
        //Load Achievements
    }

    public void LoadLeaderboard()
    {
        //Load leaderboard
    }

    public void QuitGame()
    {
        //Quit from the application
    }
}
