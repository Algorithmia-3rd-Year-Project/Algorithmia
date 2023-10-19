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

    [SerializeField] private List<GameObject> panelList;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject continueButton;
    
    private int panelNo;
    
    private void Start()
    {
        energyLevel = Random.Range(51, 60);
        happinessLevel = Random.Range(40, 80);
        intelligenceLevel = 1;
        
        LoadNextPanel();
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

    public void LoadNextPanel()
    {
        panelList[panelNo].SetActive(true);
        panelNo++;

        if (panelNo == 3)
        {
            continueButton.SetActive(true);
            nextButton.SetActive(false);
        }
    }
}
