using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject achievementUnlockWindow;
    [SerializeField] private SimManager simulationManager;
    [SerializeField] private BankManager bankManager;
    
    private List<string> unlockedAchievementList;
    
    private void Update()
    {
        if (simulationManager.hasAJob && !unlockedAchievementList.Contains("Finally Employed!"))
        {
            achievementUnlockWindow.SetActive(true);
            unlockedAchievementList.Add("Finally Employed!");
            achievementUnlockWindow.transform.Find("Name").GetComponent<TMP_Text>().text = "Finally Employed!";
            StartCoroutine(HideAchievementWindow());
        }

        if (bankManager.hasOnGoingLoan && !unlockedAchievementList.Contains("Got Fund You"))
        {
            achievementUnlockWindow.SetActive(true);
            unlockedAchievementList.Add("Got Fund You");
            achievementUnlockWindow.transform.Find("Name").GetComponent<TMP_Text>().text = "Got Fund You";
            StartCoroutine(HideAchievementWindow());
        }
        
    }

    public void LoadData(GameData data)
    {
        this.unlockedAchievementList = data.achievementList;
    }

    public void SaveData(ref GameData data)
    {
        data.achievementList = this.unlockedAchievementList;
    }

    private IEnumerator HideAchievementWindow()
    {
        yield return new WaitForSeconds(2f);
        achievementUnlockWindow.SetActive(false);
    }
}
