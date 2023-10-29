using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class CareerManager : MonoBehaviour
{

    public CareerList careerList;

    [SerializeField] private GameObject careerPrefab;

    [SerializeField] private Transform careerParent;

    private List<Button> careerButtonList;

    [SerializeField] private SimManager simManager;
    
    //Job Info Panel
    [SerializeField] private GameObject careerInfoWindow;
    [SerializeField] private TMP_Text jobNameText;
    [SerializeField] private TMP_Text jobFieldText;
    [SerializeField] private TMP_Text jobSalaryText;
    [SerializeField] private TMP_Text jobCompanyText;
    [SerializeField] private Transform requirementHandle;
    [SerializeField] private Button applyButton;
    [SerializeField] private Button leaveJobButton;

    [SerializeField] private GameObject requirementNotMetWindow;
    [SerializeField] private GameObject jobSelectedWindow;
    [SerializeField] private GameObject alreadyHaveAJobWindow;

    private List<Transform> childList = new List<Transform>();
    
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
            childList.Add(singleCareer.transform);
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
        
        //Making the current job as the topmost job on the list
        if (simManager.myJobs.Count > 0 && simManager.hasAJob)
        {
            /*
            if (career.jobName == simManager.myJobs[0])
            {
                singleCareer.transform.SetAsFirstSibling();
            }*/
            Debug.Log("frew");
            int index = simManager.myJobs.Count - 1;
            string currentJob = simManager.myJobs[index];
            
            for (int i = 0; i < childList.Count; i++)
            {
                string jobName = childList[i].gameObject.GetComponent<SingleJob>().jobName;
                if (jobName == currentJob)
                {
                    childList[i].gameObject.GetComponent<SingleJob>().occupied = true;
                    childList[i].SetSiblingIndex(1);
                }
            }
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
        
        applyButton.onClick.RemoveAllListeners();
        applyButton.onClick.AddListener(() => ApplyJobOnClicked(job.requirements, job.jobName));
        
        //Adding requirements
        List<TMP_Text> requirementGameObjects = new List<TMP_Text>();
        for (int i = 0; i < requirementHandle.childCount; i++)
        {
            requirementHandle.GetChild(i).GetComponent<TMP_Text>().text = "";
            requirementGameObjects.Add(requirementHandle.GetChild(i).GetComponent<TMP_Text>());
        }
        
        for(int i=0; i < job.requirements.Length; i++)
        {
            requirementGameObjects[i].text = "- " + job.requirements[i];
        }


        //Display leave job option for players for the job that they are currently working
        if (simManager.hasAJob)
        {
            if (jobObject.GetComponent<SingleJob>().occupied)
            {
                applyButton.gameObject.SetActive(false);
                leaveJobButton.gameObject.SetActive(true);
            }
            else
            {
                applyButton.gameObject.SetActive(true);
                leaveJobButton.gameObject.SetActive(false);
            }
        }
        else
        {
            applyButton.gameObject.SetActive(true);
            leaveJobButton.gameObject.SetActive(false);
        }
        
    }

    public void ApplyJobOnClicked(string[] requirements, string jobName)
    {
        if (!simManager.hasAJob)
        {
            bool achieved = false;
            foreach (string requirement in requirements)
            {
            
                for (int i = 0; i < simManager.skillsList.Count; i++)
                {
                    if (requirement == simManager.skillsList[i])
                    {
                        achieved = true;
                    }
                }

                if (!achieved)
                {
                    requirementNotMetWindow.SetActive(true);
                    return;
                }
            }

            if (achieved)
            {
                simManager.hasAJob = true;
                simManager.myJobs.Add(jobName);
                jobSelectedWindow.SetActive(true);
            }
        }
        else
        {
            alreadyHaveAJobWindow.SetActive(true);
        }
    }
    
    //Refresh Available Job List
    public void ShuffleJobs()
    {
        for (int i = 0; i < childList.Count; i++)
        {
            int randomIndex = Random.Range(i, childList.Count);
            Transform temp = childList[i];
            childList[i] = childList[randomIndex];
            childList[randomIndex] = temp;
        }

        // Reorder the child objects
        for (int i = 1; i < childList.Count; i++)
        {
            childList[i].SetSiblingIndex(i);
        }
    }
}
