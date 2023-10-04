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
    public int currentTrophy;
    
    private void Start()
    {
        sceneStartTime = Time.time;
        currentTrophy = 3;
    }

    public void LoadData(GameData data)
    {
        this.energyLevel = data.energyLevel;
        this.totalTime = data.totalPlayTime;
        
        //Retrieving whether this level has already completed
        data.levelsCompleted.TryGetValue(levelName, out levelCompletionStatus);
        
        //Retrieving the current trophy player has for a certain level
        data.levelTrophies.TryGetValue(levelName, out currentTrophy);
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

        //If the level has already received a trophy check what it has and update it if player plays same level again and acquired a trophy better than first one
        if (data.levelTrophies.ContainsKey(levelName))
        {
            if (data.levelTrophies[levelName] > currentTrophy)
            {
                data.levelTrophies.Remove(levelName);
                data.levelTrophies.Add(levelName, currentTrophy);
            }
        }
        else
        {
            data.levelTrophies.Add(levelName, currentTrophy);
        }
        
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
