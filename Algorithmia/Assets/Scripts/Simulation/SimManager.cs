using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

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

    [Header("Introductions")]
    [SerializeField] private GameObject instructionManager;
    private bool initialIntroPlayed;

    [Header("Family")] 
    [SerializeField] private TMP_Text motherNameText;
    [SerializeField] private TMP_Text motherAgeText;
    [SerializeField] private TMP_Text motherOccupationText;
    [SerializeField] private TMP_Text fatherNameText;
    [SerializeField] private TMP_Text fatherAgeText;
    [SerializeField] private TMP_Text fatherOccupationText;
    
    //Variables related to parents
    [HideInInspector] public float lastSpentTimeWithMother;
    [HideInInspector] public float lastRequestMoneyFromMother;
    [HideInInspector] public float lastOfferMoneyToMother;
    [HideInInspector] public float lastSpentTimeWithFather;
    [HideInInspector] public float lastRequestMoneyFromFather;
    [HideInInspector] public float lastOfferMoneyToFather;
    
    //Variables related to tasks
    [HideInInspector] public float lastBookReadTime;
    [HideInInspector] public float lastGamePlayedTime;
    [HideInInspector] public float lastMovieWatchedTime;
    
    [HideInInspector] public bool enrolledAtLibrary;
    [HideInInspector] public float enrolledAtLibraryTime;
    
    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        anyMenuOpened = false;
        prevDate = (int)totalPlayTime / 900;
        dailyMessageText.text = dailyMessage;

        if (!initialIntroPlayed)
        {
            instructionManager.SetActive(true);
        }
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
        this.initialIntroPlayed = data.simulationIntroPlayed;

        this.motherNameText.text = data.motherName;
        this.motherAgeText.text = data.motherAge.ToString();
        this.motherOccupationText.text = data.motherOccupation;
        this.fatherNameText.text = data.fatherName;
        this.fatherAgeText.text = data.fatherAge.ToString();
        this.fatherOccupationText.text = data.fatherOccupation;

        this.lastSpentTimeWithMother = data.lastSpentTimeWithMother;
        this.lastRequestMoneyFromMother = data.lastRequestMoneyFromMother;
        this.lastOfferMoneyToMother = data.lastOfferMoneyToMother;
        this.lastSpentTimeWithFather = data.lastSpentTimeWithFather;
        this.lastRequestMoneyFromFather = data.lastRequestMoneyFromFather;
        this.lastOfferMoneyToFather = data.lastOfferMoneyToFather;

        this.lastBookReadTime = data.lastBookReadTime;
        this.lastGamePlayedTime = data.lastGamePlayedTime;
        this.lastMovieWatchedTime = data.lastMovieWatchedTime;
        
        this.enrolledAtLibrary = data.enrolledAtLibrary;
        this.enrolledAtLibraryTime = data.enrolledAtLibraryTime;
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
        data.simulationIntroPlayed = this.initialIntroPlayed;

        data.lastSpentTimeWithMother = this.lastSpentTimeWithMother;
        data.lastRequestMoneyFromMother = this.lastRequestMoneyFromMother;
        data.lastOfferMoneyToMother = this.lastOfferMoneyToMother;
        data.lastSpentTimeWithFather = this.lastSpentTimeWithFather;
        data.lastRequestMoneyFromFather = this.lastRequestMoneyFromFather;
        data.lastOfferMoneyToFather = this.lastOfferMoneyToFather;

        data.lastBookReadTime = this.lastBookReadTime;
        data.lastGamePlayedTime = this.lastGamePlayedTime;
        data.lastMovieWatchedTime = this.lastMovieWatchedTime;
        
        data.enrolledAtLibrary = this.enrolledAtLibrary;
        data.enrolledAtLibraryTime = this.enrolledAtLibraryTime;
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
    
    public void FinishIntroduction()
    {
        initialIntroPlayed = true;
        SceneManager.LoadSceneAsync("Level 4 Array");
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
