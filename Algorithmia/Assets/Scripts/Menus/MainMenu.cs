using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private bool hasLogged;

    private bool alreadyGuest;

    [SerializeField] private GameObject accountSelectionScreen;

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
    }

    public void PlayGame()
    {
        //Load the cutscene for new players or the simulation screen for old players
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
