using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrayLevels : MonoBehaviour
{
    [SerializeField] private GameObject noEnergyMenu;
    [SerializeField] private SimManager simulationManager;
    
    public void LoadLevels(string levelName)
    {
        if (simulationManager.energyLevel - 20 <= 0)
        {
            noEnergyMenu.SetActive(true);
            return;
        }

        //Todo - playtime saving
        simulationManager.energyLevel -= 20;
        SceneManager.LoadSceneAsync(levelName);
    }

    public void ChangeLoadTreePrefs()
    {
        PlayerPrefs.SetInt("LoadArrayTree", 0);
    }
}
