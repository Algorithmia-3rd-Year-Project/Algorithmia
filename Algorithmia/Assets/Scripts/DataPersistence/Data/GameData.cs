using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Things that needed to be saved
    public float coinAmount;

    //These values would be default values the game starts with when there's no data to load
    public GameData()
    {
        this.coinAmount = 150;
    }
}
