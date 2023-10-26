using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;

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

    [SerializeField] private GameObject computerScreen;
    [SerializeField] private GameObject arrayQuestTree;
    
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
    private int initialIntroPlayTimeReached;
    [SerializeField] private GameObject okayButton;
    [SerializeField] private GameObject instructionBox;
    [SerializeField] private GameObject directToSimulationIntroTrigger;
    [SerializeField] private GameObject questTreeIntroduction;
    private bool questTreeIntroduced;
    public bool assignmentCutScenePlayed;
    private bool assignmentEmailShown;
    [SerializeField] private GameObject emailWindow;
    public bool afterAssignmentCutScenePlayed;
    private bool freelanceWindowShown;
    [SerializeField] private GameObject freelanceWindow;
    private bool memoryGameUnlockedMessageShown;
    private bool memoryGameUnlocked;
    [SerializeField] private GameObject memoryGameUnlockPanel;
    [SerializeField] private GameObject memoryGameApp;

    private bool optionalQuestIntroMessageShown;
    [SerializeField] private GameObject optionalQuestIntroMessageBox;

    public bool hasAJob;
    public bool hasGraphicCard;
    [SerializeField] private GameObject graphicCardMissingBox;
    [SerializeField] private GameObject gamesListPanel;

    [SerializeField] private GameObject menu;

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
    
    
    
    //Variables to access via ArrayQuestTree
    public SerializableDictionary<string, bool> levelCompletionStatus = new SerializableDictionary<string, bool>();
    public SerializableDictionary<string, int> levelAchievedTrophies = new SerializableDictionary<string, int>();

    private string playerName;
    private string playerID;
    
    //Dictionary To Store transaction data
    public List<string> transactionsNames;
    public List<string> transactionsCosts;

    public List<string> skillsList;
    public List<string> myJobs;

    public int[] memoryGameLevels;
    
    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        anyMenuOpened = false;
        prevDate = (int)totalPlayTime / 900;
        dailyMessageText.text = dailyMessage;

        int loadArrayTree = PlayerPrefs.GetInt("LoadArrayTree");
        if (loadArrayTree == 1)
        {
            computerScreen.SetActive(true);
            arrayQuestTree.SetActive(true);
        }

        if (!initialIntroPlayed && initialIntroPlayTimeReached == 1)
        {
            instructionManager.SetActive(true);
        }

        if (!questTreeIntroduced)
        {
            questTreeIntroduction.SetActive(true);
            questTreeIntroduced = true;
        }

        if (!assignmentEmailShown && assignmentCutScenePlayed)
        {
            anyMenuOpened = true;
            assignmentEmailShown = true;
            computerScreen.SetActive(true);
            emailWindow.SetActive(true);
        }

        if (!freelanceWindowShown && afterAssignmentCutScenePlayed)
        {
            anyMenuOpened = true;
            freelanceWindowShown = true;
            computerScreen.SetActive(true);
            freelanceWindow.SetActive(true);
        }

        if (memoryGameUnlocked && !memoryGameUnlockedMessageShown)
        {
            memoryGameUnlockPanel.SetActive(true);
            memoryGameUnlockedMessageShown = true;
            memoryGameApp.SetActive(true);
        }

        if (memoryGameUnlocked && memoryGameUnlockedMessageShown)
        {
            memoryGameApp.SetActive(true);
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

        if (computerScreen.activeSelf)
        {
            anyMenuOpened = true;
        }
        
        if (arrayQuestTree.activeSelf && optionalQuestIntroMessageShown)
        {
            optionalQuestIntroMessageBox.SetActive(true);
            optionalQuestIntroMessageShown = false;
        }

        //Open and close the menu depending on the time of the menu when pressing the escape key
        if (Input.GetKeyDown(KeyCode.Escape) && !menu.activeSelf)
        {
            menu.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Escape) && menu.activeSelf)
        {
            menu.SetActive(false);
        }
        
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
        this.assignmentEmailShown = data.assignmentEmailShown;
        this.assignmentCutScenePlayed = data.assignmentCutScenePlayed;
        this.afterAssignmentCutScenePlayed = data.afterAssignmentCutScenePlayed;
        this.freelanceWindowShown = data.freelanceWindowShown;

        this.optionalQuestIntroMessageShown = data.optionalQuestIntroMessage;

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
        this.initialIntroPlayTimeReached = data.simulationIntroPlayTimeReached;
        this.questTreeIntroduced = data.questTreeIntroDisplayed;

        this.memoryGameUnlocked = data.memoryGameUnlocked;
        this.memoryGameUnlockedMessageShown = data.memoryGameUnlockedMessageShown;

        this.hasAJob = data.hasAJob;
        this.hasGraphicCard = data.hasGraphicCard;

        this.levelCompletionStatus = data.levelsCompleted;
        this.levelAchievedTrophies = data.levelTrophies;

        this.playerName = data.username;
        this.playerID = data.playerId;

        this.transactionsNames = data.transactionNames;
        this.transactionsCosts = data.transactionCosts;

        this.skillsList = data.skills;
        this.myJobs = data.jobs;

        this.memoryGameLevels = data.memoryGameVictoryLevels;
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
        data.assignmentEmailShown = this.assignmentEmailShown;
        data.assignmentCutScenePlayed = this.assignmentCutScenePlayed;
        data.afterAssignmentCutScenePlayed = this.afterAssignmentCutScenePlayed;
        data.freelanceWindowShown = this.freelanceWindowShown;

        data.optionalQuestIntroMessage = this.optionalQuestIntroMessageShown;

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
        data.simulationIntroPlayTimeReached = this.initialIntroPlayTimeReached;
        data.questTreeIntroDisplayed = this.questTreeIntroduced;

        data.hasAJob = this.hasAJob;

        data.transactionNames = this.transactionsNames;
        data.transactionCosts = this.transactionsCosts;

        data.skills = this.skillsList;
        data.jobs = this.myJobs;

        data.memoryGameVictoryLevels = this.memoryGameLevels;
        
        data.memoryGameUnlocked = this.memoryGameUnlocked;
        data.memoryGameUnlockedMessageShown = this.memoryGameUnlockedMessageShown;
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
        energyLevel -= 20;
        initialIntroPlayTimeReached = 2;
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

    //Set conditions for displaying the simulation intro tutorial for the player
    //Displays trigger, proceed button, and instruction box when the player has run out of energy for the first time of his gameplay
    public void InitialIntroPlayTrigger()
    {
        if (initialIntroPlayTimeReached == 0)
        {
            initialIntroPlayTimeReached = 1;
            directToSimulationIntroTrigger.SetActive(true);
            okayButton.SetActive(true);
            instructionBox.SetActive(true);
        }
    }

    //Redirect the player into simulation scene after the above function's requirements achieved
    public void DirectToSimulationIntro()
    {
        PlayerPrefs.SetInt("LoadArrayTree", 0);
        SceneManager.LoadSceneAsync("Scenes/Simulation");
    }

    public void CheckGraphicCardForPlayingGames()
    {
        if (!hasGraphicCard)
        {
            graphicCardMissingBox.SetActive(true);
        }
        else
        {
            gamesListPanel.SetActive(true);
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync("Scenes/Main Menu");
    }

    public void ExitGame()
    {
        //Application.Quit();
        if (playerID == "")
        {
            Debug.Log("local quit");
            Application.Quit();
        }
        else
        {
            StartCoroutine(TryProgressSaving());
        }
    }
    
    //Saving player save data in mongodb database
    //problem - only save the data in file upto coming to the simulation scene last time
    private IEnumerator TryProgressSaving()
    {
        string username = playerName;
        string content = "wefwgwrg";
        string loginEndPoint = "localhost:4000/api/user/playersave";

        string saveFilePath = Application.persistentDataPath + "/data.game";
        if (File.Exists(saveFilePath))
        {
            content = File.ReadAllText(saveFilePath);
        }
        //string password = passwordInput.text;

        WWWForm form = new WWWForm();
        form.AddField("email", username);
        form.AddField("saveContent", content);

        UnityWebRequest request = UnityWebRequest.Post(loginEndPoint, form);
        var handler = request.SendWebRequest();
        
        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if (startTime > 10.0f)
            {
                break;
            }

            yield return null;
        }
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            /*
            PlayerAccount returnedPlayer = JsonUtility.FromJson<PlayerAccount>(request.downloadHandler.text);
            loginInterface.SetActive(false);
            Debug.Log(request.downloadHandler.text + " from db" + returnedPlayer._id + " " + returnedPlayer.email);

            currentUsername = returnedPlayer.email;
            loggedUsernameText.text = currentUsername;
            PlayerPrefs.SetString("PlayerID", returnedPlayer._id);
            PlayerPrefs.SetString("PlayerName", returnedPlayer.email);
            PlayerPrefs.Save();*/
            Debug.Log("Successsssssss");
            Application.Quit();
            
        } else if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(loginEndPoint);
            Debug.Log("Error connecting to the server with yasintha");
            /*loginButton.interactable = true;*/
        }
        else
        {
            Debug.Log("Failure" + request.downloadHandler.text);
            /*loginButton.interactable = true;*/
        }

        
    }

    //Temporary Functions
    public void spendMoney(float amount)
    {
        coins -= amount;
    }
}
