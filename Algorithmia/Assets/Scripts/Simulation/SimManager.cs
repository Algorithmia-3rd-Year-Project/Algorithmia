using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimManager : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private Texture2D computerCursor;

    public bool anyMenuOpened;

    //temporary code
    [SerializeField]
    private Slider energyBar;
    [SerializeField]
    private TMP_Text energyPercent;

    public float coins;

    [Header("Progress Bars")]
    public float energyLevel;
    public float happinessLevel;
    public float intelligenceLevel;

    private string username;
    
    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        anyMenuOpened = false;
    }

    public void LoadData(GameData data)
    {
        this.coins = data.coinAmount;
        this.energyLevel = data.energyLevel;
        this.happinessLevel = data.happinessLevel;
        this.intelligenceLevel = data.intelligenceLevel;
        this.username = data.username;
    }

    public void SaveData(ref GameData data)
    {
        data.coinAmount = this.coins;
        data.energyLevel = this.energyLevel;
        data.happinessLevel = this.happinessLevel;
        data.intelligenceLevel = this.intelligenceLevel;
        data.username = PlayerPrefs.GetString("PlayerName");
    }

    public void ChangeMouseCursor(bool computerCursorEnabled)
    {

        if (computerCursorEnabled)
        {
            Cursor.SetCursor(computerCursor, Vector2.zero, CursorMode.Auto);
        } else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        
    }

    public void MenuCloseDetection()
    {
        anyMenuOpened = false;
    }

    //Temporary Functions
    public void IncreaseEnergy()
    {
        energyBar.value = 0.08f;
        energyPercent.text = "4%";
    }

    public void spendMoney(float amount)
    {
        coins -= amount;
    }
}
