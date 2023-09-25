using System;
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

    

    [Header("Progress Bars")]
    public int energyLevel;
    public int happinessLevel;
    public int intelligenceLevel;
    [Space] 
    [SerializeField] private TMP_Text energyPercentageText;
    [SerializeField] private TMP_Text happinessPercentageText;
    [SerializeField] private TMP_Text intelligencePercentageText;
    [Space] 
    [SerializeField] private Slider energySlider;
    [SerializeField] private Slider happinessSlider;
    [SerializeField] private Slider intelligenceSlider;
    
    [Header("Currency")]
    public float coins;
    [SerializeField] private TMP_Text currencyText;
    
    private string username;
    
    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        anyMenuOpened = false;
    }

    private void Update()
    {
        //Update progression bar percentages
        energyPercentageText.text = energyLevel.ToString() + "%";
        happinessPercentageText.text = happinessLevel.ToString() + "%";
        intelligencePercentageText.text = intelligenceLevel.ToString() + "%";

        //Update progression bar fill
        energySlider.value = energyLevel;
        happinessSlider.value = happinessLevel;
        intelligenceSlider.value = intelligenceLevel;
        
        //Update current coin amount
        currencyText.text = coins.ToString();
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

    public void LoadTopUpCenter(string url)
    {
        Application.OpenURL(url);
    }

    public void MenuCloseDetection()
    {
        anyMenuOpened = false;
    }

    //Temporary Functions
    public void spendMoney(float amount)
    {
        coins -= amount;
    }
}
