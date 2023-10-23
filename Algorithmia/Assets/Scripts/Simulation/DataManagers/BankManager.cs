using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BankManager : MonoBehaviour, IDataPersistence
{

    [SerializeField] private SimManager simulationManager;

    private bool hasAJob;

    [SerializeField] private GameObject loanDefault;
    [SerializeField] private GameObject loanInfoWindow;
    [SerializeField] private GameObject loanFailWindow;
    [SerializeField] private GameObject onGoingLoanWindow;

    [SerializeField] private TMP_Text loanAmountText;
    [SerializeField] private Slider loanAmountSlider;
    [SerializeField] private TMP_Text loanTermText;
    [SerializeField] private Slider loanTermSlider;

    private float loanRoundValue;

    private bool hasOnGoingLoan;
    private float loanAmount;
    private int loanTerm;

    [SerializeField] private TMP_Text loanAmountTextAfter;
    [SerializeField] private TMP_Text loanTermTextAfter;

    private void Start()
    {
        if (hasOnGoingLoan)
        {
            onGoingLoanWindow.SetActive(true);
            loanDefault.SetActive(false);
        } 
    }

    private void Update()
    {
        //Increase the loan amount slider by 500 per each increase
        loanRoundValue = Mathf.Round(loanAmountSlider.value / 500) * 500;
        loanAmountText.text = loanRoundValue.ToString("F0");

        loanTermText.text = loanTermSlider.value.ToString("F0");
        loanTerm = (int)loanTermSlider.value;

        if (onGoingLoanWindow.activeSelf)
        {
            loanAmountTextAfter.text = loanAmount.ToString("F0");
            loanTermTextAfter.text = loanTerm.ToString();
        }
    }

    public void LoadData(GameData data)
    {
        this.hasOnGoingLoan = data.hasOngoingLoan;
        this.loanAmount = data.loanAmount;
        this.loanTerm = data.loanTerm;
    }

    public void SaveData(ref GameData data)
    {
        data.hasOngoingLoan = this.hasOnGoingLoan;
        data.loanAmount = this.loanAmount;
        data.loanTerm = this.loanTerm;
    }

    public void ApplyForALoan()
    {
        if (simulationManager.hasAJob)
        {
            loanDefault.SetActive(false);
            loanInfoWindow.SetActive(true);
        }
        else
        {
            loanFailWindow.SetActive(true);
            StartCoroutine(HideLoanFailText());
        }
    }

    public void GetALoan()
    {
        if (!hasOnGoingLoan)
        {
            simulationManager.coins += loanRoundValue;
            hasOnGoingLoan = true;
            loanAmount = loanRoundValue;
            loanInfoWindow.SetActive(false);
            onGoingLoanWindow.SetActive(true);
        }
    }

    public void ResolveLoan()
    {
        //logic to payout the the existing loan with interest
        Debug.Log("Paying Out");
    }

    IEnumerator HideLoanFailText()
    {
        yield return new WaitForSeconds(3f);
        loanFailWindow.SetActive(false);
    }
    
}
