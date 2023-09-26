using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Random = UnityEngine.Random;

public class FamilyManager : MonoBehaviour, IDataPersistence
{
    private Family familyData;

    private string motherName;
    private string fatherName;
    
    private void Start()
    {
        
        string jsonPath = Application.dataPath + "/Data/Family.json";
        string json = File.ReadAllText(jsonPath);
        familyData = JsonUtility.FromJson<Family>(json);
        
        motherName = GetRandomValueFromList(familyData.mother);
        fatherName = GetRandomValueFromList(familyData.father);
        
    }
    
    public void LoadData(GameData data)
    {
        //this.username = data.username;
    }

    public void SaveData(ref GameData data)
    {
        data.motherName = this.motherName;
        data.fatherName = this.fatherName;
    }
    
    private T GetRandomValueFromList<T>(List<T> list)
    {
        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}
