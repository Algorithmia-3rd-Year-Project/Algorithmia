using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class IntroCutScene : MonoBehaviour, IDataPersistence
{
    private string username;

    private int energyLevel;
    private int happinessLevel;
    private int intelligenceLevel;

    private void Start()
    {
        energyLevel = Random.Range(51, 60);
        happinessLevel = Random.Range(40, 80);
        intelligenceLevel = 1;
    }

    public void LoadData(GameData data)
    {
        this.username = data.username;
    }

    public void SaveData(ref GameData data)
    {
        data.username = PlayerPrefs.GetString("PlayerName");
        data.energyLevel = this.energyLevel;
        data.happinessLevel = this.happinessLevel;
        data.intelligenceLevel = this.intelligenceLevel;
    }
    
    public void ContinueToGame()
    {
        SceneManager.LoadSceneAsync("Dialogue1");
    }
}
