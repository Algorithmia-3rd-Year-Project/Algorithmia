using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimInstructionManager : MonoBehaviour
{
    [SerializeField] private GameObject instruction1;
    [SerializeField] private GameObject instruction2;
    [SerializeField] private GameObject instruction3;
    [SerializeField] private GameObject instruction4;
    [SerializeField] private GameObject instruction5;
    [SerializeField] private GameObject instruction6;
    [SerializeField] private GameObject instruction7;

    [SerializeField] private GameObject triggerCollection;

    [SerializeField] private GameObject readingBooksMenu;
    
    [SerializeField] private GameObject trigger4;
    [SerializeField] private GameObject trigger5;

    [SerializeField] private SimManager simulationManager;
    private bool tempCondition;
    
    private void Start()
    {
        triggerCollection.SetActive(true);
        instruction1.SetActive(true);
    }

    private void Update()
    {
        
        if (simulationManager.anyMenuOpened && tempCondition)
        {
            trigger4.SetActive(false);
            instruction5.SetActive(false);
            instruction6.SetActive(true);
            trigger5.SetActive(true);
        }

        if (instruction1.activeSelf || instruction2.activeSelf || instruction3.activeSelf || instruction4.activeSelf)
        {
            simulationManager.anyMenuOpened = true;
        }

        if (instruction5.activeSelf)
        {
            simulationManager.anyMenuOpened = false;
            tempCondition = true;
        }
        
    }

    public void ChangeTempCondition()
    {
        tempCondition = false;
    }
}
