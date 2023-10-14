using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Random = UnityEngine.Random;

public class TaskManager : MonoBehaviour
{
    private Leisure leisureData;
    
    private void Start()
    {
        string jsonPath = Application.dataPath + "/Data/Leisure.json";
        string json = File.ReadAllText(jsonPath);
        leisureData = JsonUtility.FromJson<Leisure>(json);
    }

    public string BookName()
    {
        string bookName = GetRandomValueFromList(leisureData.books);
        return bookName;
    }

    public string GameName()
    {
        string gameName = GetRandomValueFromList(leisureData.games);
        return gameName;
    }

    public string MovieName()
    {
        string movieName = GetRandomValueFromList(leisureData.movies);
        return movieName;
    }
    
    private T GetRandomValueFromList<T>(List<T> list)
    {
        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}
