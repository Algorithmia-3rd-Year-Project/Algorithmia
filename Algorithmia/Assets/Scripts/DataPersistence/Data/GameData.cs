using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Things that needed to be saved
    public float coinAmount;

    public float energyLevel;
    public float happinessLevel;
    public float intelligenceLevel;

    public string username;
    
    //These values would be default values the game starts with when there's no data to load
    public GameData()
    {
        this.coinAmount = 150;

        //energyLevel = Random.Range(60f, 75f);
        //happinessLevel = Random.Range(40f, 90f);
        intelligenceLevel = 0f;

        username = "";
    }
}
