using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour, IDataPersistence
{
    [Header("Hints")] 
    [SerializeField] private List<string> hints;
    [SerializeField] private List<TMP_Text> hintsTexts;
    [SerializeField] private string answer;
    [SerializeField] private TMP_Text answerText;

    [Header("Hint 1")] 
    [SerializeField] private GameObject hint1Cover;
    [SerializeField] private GameObject hint1Instruction;

    [Header("Hint 2")] 
    [SerializeField] private GameObject hint2Cover;
    [SerializeField] private GameObject hint2Instruction;
    [SerializeField] private GameObject hint2Lock;
    
    [Header("Hint 3")] 
    [SerializeField] private GameObject hint3Cover;
    [SerializeField] private GameObject hint3Instruction;
    [SerializeField] private GameObject hint3Lock;

    [Header("Answer")] 
    [SerializeField] private GameObject unlockButton;
    [SerializeField] private GameObject answerPanel;
    [SerializeField] private GameObject insufficientCoinsPanel;
    

    private int happinessLevel;
    private float coinsCount;
    
    private bool hint1;
    private bool hint2;

    private void Start()
    {
        for (int i = 0; i < hints.Count; i++)
        {
            hintsTexts[i].text = hints[i];
        }

        answerText.text = answer;
    }

    public void UnlockHint1()
    {
        if (happinessLevel - 2 > 0)
        {
            hint1Cover.SetActive(false);
            hint2Lock.SetActive(false);
            hint2Instruction.SetActive(true);
            happinessLevel -= 2;
            hint1 = true;
        }
        
    }

    public void UnlockHint2()
    {
        if (happinessLevel - 4 > 0 && hint1)
        {
            hint2Cover.SetActive(false);
            hint3Lock.SetActive(false);
            hint3Instruction.SetActive(true);
            happinessLevel -= 4;
            hint2 = true;
        }
    }

    public void UnlockHint3()
    {
        if (happinessLevel - 6 > 0 && hint2)
        {
            hint3Cover.SetActive(false);
            happinessLevel -= 6;
        }
    }

    public void UnlockAnswer()
    {
        if (coinsCount - 40 > 0)
        {
            unlockButton.SetActive(false);
            answerPanel.SetActive(true);
            coinsCount -= 40;
        }
        else
        {
            insufficientCoinsPanel.SetActive(true);
        }
    }
    
    public void LoadData(GameData data)
    {
        this.happinessLevel = data.happinessLevel;
        this.coinsCount = data.coinAmount;
    }

    public void SaveData(ref GameData data)
    {
        data.happinessLevel = this.happinessLevel;
        data.coinAmount = this.coinsCount;
    }

}
