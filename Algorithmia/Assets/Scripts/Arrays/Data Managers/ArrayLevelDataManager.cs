using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrayLevelDataManager : MonoBehaviour, IDataPersistence
{
    private int energyLevel;

    [SerializeField] private GameObject energyLostMenu;

    private float totalTime;
    private float sceneStartTime = 0.0f;
    private float scenePlayTime = 0.0f;

    private void Start()
    {
        sceneStartTime = Time.time;
    }

    public void LoadData(GameData data)
    {
        this.energyLevel = data.energyLevel;
        this.totalTime = data.totalPlayTime;
    }

    public void SaveData(ref GameData data)
    {
        data.energyLevel = this.energyLevel;
        data.totalPlayTime += scenePlayTime;
    }

    public void LoadNextLevel(string sceneName)
    {
        if (energyLevel - 20 <= 0)
        {
            energyLostMenu.SetActive(true);
            return;
        }
        energyLevel -= 20;
        scenePlayTime = Time.time - sceneStartTime; 
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ExitToMenu()
    {
        scenePlayTime = Time.time - sceneStartTime; 
        SceneManager.LoadSceneAsync("Scenes/Main Menu");
    }

    public void DirectToSimulation()
    {
        scenePlayTime = Time.time - sceneStartTime; 
        SceneManager.LoadSceneAsync("Scenes/Simulation");
    }
    
}
