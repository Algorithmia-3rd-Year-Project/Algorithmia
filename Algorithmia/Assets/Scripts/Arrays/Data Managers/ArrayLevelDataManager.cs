using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrayLevelDataManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string levelName;
    
    private int energyLevel;

    [SerializeField] private GameObject energyLostMenu;

    private float totalTime;
    private float sceneStartTime = 0.0f;
    private float scenePlayTime = 0.0f;

    private bool levelCompletionStatus;
    
    private void Start()
    {
        sceneStartTime = Time.time;
    }

    public void LoadData(GameData data)
    {
        this.energyLevel = data.energyLevel;
        this.totalTime = data.totalPlayTime;
        
        //Retrieving whether this level has already completed
        data.levelsCompleted.TryGetValue(levelName, out levelCompletionStatus);
    }

    public void SaveData(ref GameData data)
    {
        data.energyLevel = this.energyLevel;
        data.totalPlayTime += scenePlayTime;

        //Remove if a record exists for this level and add a new one with levelName
        if (data.levelsCompleted.ContainsKey(levelName))
        {
            data.levelsCompleted.Remove(levelName);
        }
        data.levelsCompleted.Add(levelName, levelCompletionStatus);
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
        levelCompletionStatus = true;
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
