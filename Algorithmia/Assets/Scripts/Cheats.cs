using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private SimManager simManagaer;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            simManagaer.coins += 1000;
            Debug.Log("Cheat Done");
        }
    }

    public void LoadData(GameData data)
    {
        this.simManagaer.coins = data.coinAmount;
    }

    public void SaveData(ref GameData data)
    {
        data.coinAmount = this.simManagaer.coins;
    }
}
