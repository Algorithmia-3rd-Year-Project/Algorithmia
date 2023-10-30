using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayQuestTree : MonoBehaviour
{

    [SerializeField] private SimManager simulationManager;
    [SerializeField] private List<GameObject> questTree;


    [SerializeField] private List<Transform> levelIconsList;
    
    private void Start()
    {
        QuestTreeManagement();
        CheckCompleted();
    }

    private void QuestTreeManagement()
    {
        
        //If has completed one adaptive learning quest, check the status of the other relevant adaptive learning quest
        //If it has also been completed with above Bronze Trophy unlock the rest of the area for player
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 7 A_2") &&
            simulationManager.levelCompletionStatus["Array Level 7 A_2"] == true)
        {
            if (simulationManager.levelAchievedTrophies.ContainsKey("Array Level 7 A_1"))
            {
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 7 A_1", out int trophy);
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 7 A_2", out int currentTrophies);
                if (trophy < 2 && currentTrophies < 2)
                {
                    UnlockQuestTree(32);
                    return;
                }
            }
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 7 A_1") &&
            simulationManager.levelCompletionStatus["Array Level 7 A_1"] == true)
        {
            if (simulationManager.levelAchievedTrophies.ContainsKey("Array Level 7 A_2"))
            {
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 7 A_2", out int trophy);
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 7 A_1", out int currentTrophies);
                if (trophy < 2 && currentTrophies < 2)
                {
                    UnlockQuestTree(32);
                    return;
                }
            }
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 7") && simulationManager.levelCompletionStatus["Array Level 7"] == true)
        {
            if (simulationManager.levelAchievedTrophies.TryGetValue("Array Level 7", out int trophy)) ;

            if (trophy == 0)
            {
                UnlockQuestTree(32);
                return;
            } else if (trophy == 1 || trophy == 2)
            {
                UnlockQuestTree(28);
                return;
            } 
        }
        
        
        //If has completed one adaptive learning quest, check the status of the other relevant adaptive learning quest
        //If it has also been completed with above Bronze Trophy unlock the rest of the area for player
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 6 A_2") &&
            simulationManager.levelCompletionStatus["Array Level 6 A_2"] == true)
        {
            if (simulationManager.levelAchievedTrophies.ContainsKey("Array Level 6 A_1"))
            {
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 6 A_1", out int trophy);
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 6 A_2", out int currentTrophies);
                if (trophy < 2 && currentTrophies < 2)
                {
                    UnlockQuestTree(24);
                    return;
                }
            }
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 6 A_1") &&
            simulationManager.levelCompletionStatus["Array Level 6 A_1"] == true)
        {
            if (simulationManager.levelAchievedTrophies.ContainsKey("Array Level 6 A_2"))
            {
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 6 A_2", out int trophy);
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 6 A_1", out int currentTrophies);
                if (trophy < 2 && currentTrophies < 2)
                {
                    UnlockQuestTree(24);
                    return;
                }
            }
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 6") && simulationManager.levelCompletionStatus["Array Level 6"] == true)
        {
            if (simulationManager.levelAchievedTrophies.TryGetValue("Array Level 6", out int trophy)) ;

            if (trophy == 0)
            {
                UnlockQuestTree(24);
                return;
            } else if (trophy == 1 || trophy == 2)
            {
                UnlockQuestTree(20);
                return;
            } 
        }
        
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 5") && simulationManager.levelCompletionStatus["Array Level 5"] == true)
        {
            UnlockQuestTree(16);
            return;
        }

        //If has completed one adaptive learning quest, check the status of the other relevant adaptive learning quest
        //If it has also been completed with above Bronze Trophy unlock the rest of the area for player
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 4 A_2") &&
            simulationManager.levelCompletionStatus["Array Level 4 A_2"] == true)
        {
            if (simulationManager.levelAchievedTrophies.ContainsKey("Array Level 4 A_1"))
            {
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 4 A_1", out int trophy);
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 4 A_2", out int currentTrophies);
                if (trophy < 2 && currentTrophies < 2)
                {
                    UnlockQuestTree(12);
                    return;
                }
            }
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 4 A_1") &&
            simulationManager.levelCompletionStatus["Array Level 4 A_1"] == true)
        {
            if (simulationManager.levelAchievedTrophies.ContainsKey("Array Level 4 A_2"))
            {
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 4 A_2", out int trophy);
                simulationManager.levelAchievedTrophies.TryGetValue("Array Level 4 A_1", out int currentTrophies);
                if (trophy < 2 && currentTrophies < 2)
                {
                    UnlockQuestTree(12);
                    return;
                }
            }
        }
        

        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 4") && simulationManager.levelCompletionStatus["Array Level 4"] == true)
        {
            if (simulationManager.levelAchievedTrophies.TryGetValue("Array Level 4", out int trophy)) ;

            if (trophy == 0)
            {
                UnlockQuestTree(12);
                return;
            } else if (trophy == 1 || trophy == 2)
            {
                UnlockQuestTree(8);
                return;
            } 
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

    private void CheckCompleted()
    {
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 3") &&
            simulationManager.levelCompletionStatus["Array Level 3"] == true)
        {
            HideEnergyDrain(levelIconsList[0]);
        }

        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 4") &&
            simulationManager.levelCompletionStatus["Array Level 4"] == true)
        {
            HideEnergyDrain(levelIconsList[1]);
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 4 A_1") &&
            simulationManager.levelCompletionStatus["Array Level 4 A_1"] == true)
        {
            HideEnergyDrain(levelIconsList[2]);
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 4 A_2") &&
            simulationManager.levelCompletionStatus["Array Level 4 A_2"] == true)
        {
            HideEnergyDrain(levelIconsList[3]);
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 5") &&
            simulationManager.levelCompletionStatus["Array Level 5"] == true)
        {
            HideEnergyDrain(levelIconsList[4]);
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 6") &&
            simulationManager.levelCompletionStatus["Array Level 6"] == true)
        {
            HideEnergyDrain(levelIconsList[5]);
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 6 A_1") &&
            simulationManager.levelCompletionStatus["Array Level 6 A_1"] == true)
        {
            HideEnergyDrain(levelIconsList[6]);
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 6 A_2") &&
            simulationManager.levelCompletionStatus["Array Level 6 A_2"] == true)
        {
            HideEnergyDrain(levelIconsList[7]);
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 7") &&
            simulationManager.levelCompletionStatus["Array Level 7"] == true)
        {
            HideEnergyDrain(levelIconsList[8]);
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 7 A_1") &&
            simulationManager.levelCompletionStatus["Array Level 7 A_1"] == true)
        {
            HideEnergyDrain(levelIconsList[9]);
        }
        
        if (simulationManager.levelCompletionStatus.ContainsKey("Array Level 7 A_2") &&
            simulationManager.levelCompletionStatus["Array Level 7 A_2"] == true)
        {
            HideEnergyDrain(levelIconsList[10]);
        }
    }

    private void HideEnergyDrain(Transform levelIcon)
    {
        GameObject energyDetails = levelIcon.Find("Cost").gameObject;
        energyDetails.SetActive(false);
    }
}
