using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrayLevelDataManager : MonoBehaviour, IDataPersistence
{
    private int energyLevel;

    [SerializeField] private GameObject energyLostMenu;
    
    public void LoadData(GameData data)
    {
        this.energyLevel = data.energyLevel;
    }

    public void SaveData(ref GameData data)
    {
        data.energyLevel = this.energyLevel;
    }

    public void LoadNextLevel(string sceneName)
    {
        if (energyLevel - 20 <= 0)
        {
            energyLostMenu.SetActive(true);
            return;
        }
        energyLevel -= 20;
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadSceneAsync("Scenes/Main Menu");
    }

    public void DirectToSimulation()
    {
        SceneManager.LoadSceneAsync("Scenes/Simulation");
    }
    
}
