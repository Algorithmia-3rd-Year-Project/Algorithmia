using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private SimManager simulationManager;
    
    private Leisure leisureData;
    
    private void Start()
    {
        string jsonPath = Application.streamingAssetsPath + "/Leisure.json";
        string json = File.ReadAllText(jsonPath);
        leisureData = JsonUtility.FromJson<Leisure>(json);
        simulationManager.skillsList = leisureData.skills;
    }
}
