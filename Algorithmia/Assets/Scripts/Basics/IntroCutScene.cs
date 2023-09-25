using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutScene : MonoBehaviour, IDataPersistence
{
    private string username;
    
    public void LoadData(GameData data)
    {
        this.username = data.username;
    }

    public void SaveData(ref GameData data)
    {
        data.username = PlayerPrefs.GetString("PlayerName");
    }
    
    public void ContinueToGame()
    {
        SceneManager.LoadSceneAsync("Scenes/Simulation");
    }
}
