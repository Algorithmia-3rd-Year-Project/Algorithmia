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
    [SerializeField] private GameObject guestNameInputPanel;

    [SerializeField] private GameObject newGameObjects;
    [SerializeField] private GameObject continueGameObjects;

    public string currentUsername;
    
    [SerializeField] private GameObject loginSuccessfulMessage;
    [SerializeField] private GameObject errorConnectingToServerMessage;
    [SerializeField] private GameObject errorMessageText;
    
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

    private void Update()
    {
        if (loginSuccessfulMessage.activeSelf)
        {
            StartCoroutine(HideMessage(loginSuccessfulMessage));
        }

        if (errorConnectingToServerMessage.activeSelf)
        {
            StartCoroutine(HideMessage(errorConnectingToServerMessage));
        }

        if (errorMessageText.activeSelf)
        {
            StartCoroutine(HideMessage(errorMessageText));
        }
    }

    public void SaveGuestName()
    {
        currentUsername = guestNameInput.text;
        if (currentUsername == "")
        {
            Debug.Log("Username cannot be empty");
            errorMessageText.SetActive(true);
            errorMessageText.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Username cannot be empty";
            return;
        } else if (currentUsername.Length < 5)
        {
            Debug.Log("Username must have at least 5 letters");
            errorMessageText.SetActive(true);
            errorMessageText.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Must have atleast 5 letters";
            return;
        }
        
        loggedUsernameText.text = currentUsername;
        //changing the folder name of the saved file creating
        //DataPersistenceManager.instance.selectedProfileId = currentUsername;
        PlayerPrefs.SetString("PlayerID", "");
        PlayerPrefs.SetString("PlayerName", currentUsername);
        PlayerPrefs.Save();
        guestNameInputPanel.SetActive(false);
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

    public void NavigateToExternalURL(string url)
    {
        Application.OpenURL(url);
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
    
    private IEnumerator HideMessage(GameObject currentObj)
    {
        yield return new WaitForSeconds(3f);
        currentObj.SetActive(false);
        
    }
}
