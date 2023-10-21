using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatusManager : MonoBehaviour, IDataPersistence
{
    
    [SerializeField] private List<ArrayQuestNode> levels;

    [SerializeField] private List<Sprite> trophyIcons;

    public void LoadData(GameData data)
    {
        
       foreach (ArrayQuestNode level in levels)
       {
           string levelName = level.GetComponent<ArrayQuestNode>().levelName;
           bool completionStatus;
           int trophyNumber = 3;
           data.levelsCompleted.TryGetValue(levelName, out completionStatus);
           if (completionStatus)
           {
               level.GetComponent<ArrayQuestNode>().completionIcon.SetActive(true);
               level.GetComponent<ArrayQuestNode>().trophyIcon.gameObject.SetActive(true);
               data.levelTrophies.TryGetValue(levelName, out trophyNumber);
               level.GetComponent<ArrayQuestNode>().trophyIcon.sprite = trophyIcons[trophyNumber];
           }

       }
       
    }

    public void SaveData(ref GameData data)
    {
        
    }
}
