using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

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
    
    [Header("Play Time")]
    public float totalPlayTime;
    [SerializeField] private TMP_Text currentDateText;
    [SerializeField] private TMP_Text currentWeekText;
    
    [Header("Daily Logs")]
    [SerializeField] private string[] randomDailyLogs;
    private int prevDate;
    private string dailyMessage;
    [SerializeField] private TMP_Text dailyMessageText;
    
    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        anyMenuOpened = false;
        prevDate = (int)totalPlayTime / 900;
        dailyMessageText.text = dailyMessage;
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
        
        //Keep track of time, date, and week
        totalPlayTime += Time.deltaTime;
        CalculateDayAndWeek(totalPlayTime);
        
        //Update daily default message
        AddDailyMessage();

    }

    public void LoadData(GameData data)
    {
        this.coins = data.coinAmount;
        this.energyLevel = data.energyLevel;
        this.happinessLevel = data.happinessLevel;
        this.intelligenceLevel = data.intelligenceLevel;
        this.username = data.username;
        this.totalPlayTime = data.totalPlayTime;
        this.dailyMessage = data.dailyMessage;
    }

    public void SaveData(ref GameData data)
    {
        data.coinAmount = this.coins;
        data.energyLevel = this.energyLevel;
        data.happinessLevel = this.happinessLevel;
        data.intelligenceLevel = this.intelligenceLevel;
        data.username = PlayerPrefs.GetString("PlayerName");
        data.totalPlayTime = this.totalPlayTime;
        data.dailyMessage = this.dailyMessage;
    }

    private void CalculateDayAndWeek(float totalTime)
    {
        int totalDaysCount = (int) totalTime / 900;
        int weekNumber = (totalDaysCount / 7) + 1;
        int weekDays = (totalDaysCount % 7) + 1;

        currentWeekText.text = weekNumber.ToString();
        currentDateText.text = weekDays.ToString();
    }

    private void AddDailyMessage()
    {
        if ((int)totalPlayTime / 900 != prevDate)
        {
            int randomNo = Random.Range(0, 3);
            dailyMessage = randomDailyLogs[randomNo];
            dailyMessageText.text = dailyMessage;
            prevDate = (int)totalPlayTime / 900;
        }
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
