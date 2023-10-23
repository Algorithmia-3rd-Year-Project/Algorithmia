using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankManager : MonoBehaviour, IDataPersistence
{

    [SerializeField] private SimManager simulationManager;

    private bool hasAJob;

    [SerializeField] private GameObject loanDefault;
    [SerializeField] private GameObject loanInfoWindow;
    [SerializeField] private GameObject loanFailWindow;
    
    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        
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

    IEnumerator HideLoanFailText()
    {
        yield return new WaitForSeconds(3f);
        loanFailWindow.SetActive(false);
    }
    
}
