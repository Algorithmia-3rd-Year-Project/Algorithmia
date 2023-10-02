using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Things that needed to be saved
    public float coinAmount;

    public int energyLevel;
    public int happinessLevel;
    public int intelligenceLevel;

    public string username;

    public float totalPlayTime;
    public string dailyMessage;

    public bool simulationIntroPlayed; 
    
    //Player family details
    public string motherName;
    public int motherAge;
    public string motherOccupation;
    
    public string fatherName;
    public int fatherAge;
    public string fatherOccupation;
    
    //Simulation task executed times
    public float lastBookReadTime;
    public float lastGamePlayedTime;
    public float lastMovieWatchedTime;

    public bool enrolledAtLibrary;
    public float enrolledAtLibraryTime;
    
    //These values would be default values the game starts with when there's no data to load
    public GameData()
    {
        this.coinAmount = 150;

        this.energyLevel = 0;
        this.happinessLevel = 0;
        this.intelligenceLevel = 0;

        this.username = "";
        
        this.totalPlayTime = 0;
        this.dailyMessage = "It's a cool day";

        this.simulationIntroPlayed = false;

        this.motherName = "";
        this.motherAge = 0;
        this.motherOccupation = "";
        this.fatherName = "";
        this.fatherAge = 0;
        this.fatherOccupation = "";

        this.lastBookReadTime = 0.0f;
        this.lastGamePlayedTime = 0.0f;
        this.lastMovieWatchedTime = 0.0f;

        this.enrolledAtLibrary = false;
        this.enrolledAtLibraryTime = 0.0f;
    }
}
