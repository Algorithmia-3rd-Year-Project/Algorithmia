using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class CareerManager : MonoBehaviour
{

    public CareerList careerList;

    [SerializeField] private GameObject careerPrefab;

    [SerializeField] private Transform careerParent;
    
    private void Start()
    {
        string jsonPath = Application.dataPath + "/Data/Careers.json";
        string json = File.ReadAllText(jsonPath);

        careerList = JsonUtility.FromJson<CareerList>(json);

        foreach (CareerData career in careerList.careers)
        {
            GameObject singleCareer = Instantiate(careerPrefab, careerParent);
            SingleJob singleCareerScript = singleCareer.GetComponent<SingleJob>();
            
            //Assigning data to the prefab
            singleCareerScript.jobNameText.text = career.jobName;
            singleCareerScript.fieldNameText.text = career.field;
            singleCareerScript.salaryText.text = career.salary;
            
            //Debug.Log($"Career ID: {career.id}, jobname: {career.jobName}, Field: {career.field}");
        }
    }
}
