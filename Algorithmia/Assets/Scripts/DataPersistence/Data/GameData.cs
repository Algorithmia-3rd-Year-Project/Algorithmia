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
    //Parent related Activities
    public float lastSpentTimeWithMother;
    public float lastRequestMoneyFromMother;
    public float lastOfferMoneyToMother;
    public float lastSpentTimeWithFather;
    public float lastRequestMoneyFromFather;
    public float lastOfferMoneyToFather;
    
    //Leisure Activities
    public float lastBookReadTime;
    public float lastGamePlayedTime;
    public float lastMovieWatchedTime;

    //Subscription based tasks
    public bool enrolledAtLibrary;
    public float enrolledAtLibraryTime;

    public SerializableDictionary<string, bool> levelsCompleted;
    public SerializableDictionary<string, int> levelTrophies;
    
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

        this.lastSpentTimeWithMother = 0.0f;
        this.lastRequestMoneyFromMother = 0.0f;
        this.lastOfferMoneyToMother = 0.0f;
        this.lastSpentTimeWithFather = 0.0f;
        this.lastRequestMoneyFromFather = 0.0f;
        this.lastOfferMoneyToFather = 0.0f;
        

        this.enrolledAtLibrary = false;
        this.enrolledAtLibraryTime = 0.0f;

        this.levelsCompleted = new SerializableDictionary<string, bool>();
        this.levelTrophies = new SerializableDictionary<string, int>();
    }
}
