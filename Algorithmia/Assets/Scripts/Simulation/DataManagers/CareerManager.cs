using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CareerManager : MonoBehaviour
{

    public CareerList careerList;

    [SerializeField] private GameObject careerPrefab;

    [SerializeField] private Transform careerParent;

    private List<Button> careerButtonList;
    
    //Job Info Panel
    [SerializeField] private GameObject careerInfoWindow;
    [SerializeField] private TMP_Text jobNameText;
    [SerializeField] private TMP_Text jobFieldText;
    [SerializeField] private TMP_Text jobSalaryText;
    [SerializeField] private TMP_Text jobCompanyText;
    [SerializeField] private Transform requirementHandle;
    
    private void Awake()
    {
        careerButtonList = new List<Button>();
        
        //string jsonPath = Application.dataPath + "/Data/Careers.json";
        string jsonPath = Application.streamingAssetsPath + "/Careers.json";
        string json = File.ReadAllText(jsonPath);

        careerList = JsonUtility.FromJson<CareerList>(json);

        foreach (CareerData career in careerList.careers)
        {
            GameObject singleCareer = Instantiate(careerPrefab, careerParent);
            SingleJob singleCareerScript = singleCareer.GetComponent<SingleJob>();
            Button singleJobButton = singleCareer.GetComponent<Button>();
            careerButtonList.Add(singleJobButton);
            
            //Assigning data to the prefab
            singleCareerScript.jobNameText.text = career.jobName;
            singleCareerScript.fieldNameText.text = career.field;
            singleCareerScript.salaryText.text = career.salary;
            singleCareerScript.jobName = career.jobName;
            singleCareerScript.fieldName = career.field;
            singleCareerScript.salaryAmount = career.salary;
            singleCareerScript.companyName = career.employer;
            singleCareerScript.requirements = career.requirements;

            string imagePath = "Careers/" + career.jobIcon;
            Sprite loadedImage = LoadSprite(imagePath);

            singleCareerScript.jobIconImage.sprite = loadedImage;
            

            //Debug.Log($"Career ID: {career.id}, jobname: {career.jobName}, Field: {career.field}");
        }
        
    }

    private void Start()
    {
        if (careerButtonList.Count > 0)
        {
            AddListeners();
        }
        
    }

    private Sprite LoadSprite(string path)
    {
        Sprite loadedSprite = Resources.Load<Sprite>(path);
        return loadedSprite;
    }

    private void AddListeners()
    {
        foreach (Button btn in careerButtonList)
        {
            btn.onClick.AddListener(() => ClickedOnJob());
        }
    }

    public void ClickedOnJob()
    {
        GameObject jobObject = EventSystem.current.currentSelectedGameObject;
        SingleJob job = jobObject.GetComponent<SingleJob>();
        Debug.Log(job.jobName);
        
        careerInfoWindow.SetActive(true);
        jobNameText.text = job.jobName;
        jobFieldText.text = job.fieldName;
        jobSalaryText.text = job.salaryAmount;
        jobCompanyText.text = job.companyName;
        
        //Adding requirements
        List<TMP_Text> requirementGameObjects = new List<TMP_Text>();
        for (int i = 0; i < requirementHandle.childCount; i++)
        {
            requirementGameObjects.Add(requirementHandle.GetChild(i).GetComponent<TMP_Text>());
        }
        
        for(int i=0; i < job.requirements.Length; i++)
        {
            requirementGameObjects[i].text = "- " + job.requirements[i];
        }
    }
}
