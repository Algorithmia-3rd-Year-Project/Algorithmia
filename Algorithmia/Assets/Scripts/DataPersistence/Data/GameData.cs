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
    
    //These values would be default values the game starts with when there's no data to load
    public GameData()
    {
        this.coinAmount = 150;

        this.energyLevel = 0;
        this.happinessLevel = 0;
        this.intelligenceLevel = 0;

        this.username = "";
        
        this.totalPlayTime = 0;
    }
}
