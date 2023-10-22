using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject accountSelectionScreen;

    [SerializeField] private TMP_InputField guestNameInput;
    [SerializeField] private TMP_Text loggedUsernameText;

    [SerializeField] private GameObject newGameObjects;
    [SerializeField] private GameObject continueGameObjects;

    public string currentUsername;
    
    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            accountSelectionScreen.SetActive(true);
            continueGameObjects.SetActive(false);
            newGameObjects.SetActive(true);
        } else if (DataPersistenceManager.instance.HasGameData())
        {
            continueGameObjects.SetActive(true);
            newGameObjects.SetActive(false);
        }
        loggedUsernameText.text = currentUsername;
    }

    public void SaveGuestName()
    {
        currentUsername = guestNameInput.text;
        loggedUsernameText.text = currentUsername;
        //changing the folder name of the saved file creating
        //DataPersistenceManager.instance.selectedProfileId = currentUsername;
        PlayerPrefs.SetString("PlayerID", "");
        PlayerPrefs.SetString("PlayerName", currentUsername);
        PlayerPrefs.Save();
    }

    public void PlayNewGame()
    {
        //Load the cutscene for new players or the simulation screen for old players
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Scenes/CutScenes/Introduction");
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
        Application.Quit();
        Debug.Log("Exiting");
    }
    
    public void LoadData(GameData data)
    {
        this.currentUsername = data.username;
    }

    public void SaveData(ref GameData data)
    {
        //data.username = PlayerPrefs.GetString("PlayerName");
    }
}
