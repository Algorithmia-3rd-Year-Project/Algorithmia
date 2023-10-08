using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayQuestTree : MonoBehaviour
{

    [SerializeField] private SimManager simulationManager;
    [SerializeField] private List<GameObject> questTree;

    private void Start()
    {
        QuestTreeManagement();
    }

    private void QuestTreeManagement()
    {
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 5") && simulationManager.levelCompletionStatus["Array Level 5"] == true)
        {
            UnlockQuestTree(10);
            return;
        }
        

        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 4"))
        {
            //choose between adaptive levels also
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 3") && simulationManager.levelCompletionStatus["Array Level 3"] == true)
        {
            UnlockQuestTree(4);
            return;
        }

        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 2") && simulationManager.levelCompletionStatus["Array Level 2"] == true)
        {
            UnlockQuestTree(2);
            return;
        }
    }

    private void UnlockQuestTree(int end)
    {
        for (int i = 0; i < end; i++)
        {
            questTree[i].SetActive(true);
        }
    }
}
