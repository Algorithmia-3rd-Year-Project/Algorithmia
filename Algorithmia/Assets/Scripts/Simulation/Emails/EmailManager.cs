using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EmailManager : MonoBehaviour
{
    [SerializeField] private SimManager simulationManager;

    //Email content fields
    public GameObject openedEmail;
    public TMP_Text emailTitleText;
    public TMP_Text emailBodyText;
    
    private string targetLevelName;

    [SerializeField] private GameObject singleEmailPrefab;
    [SerializeField] private Transform mailWrapper;

    [SerializeField] private GameObject emailNotification;
    [SerializeField] private TMP_Text notificationCountText;

    [SerializeField] private List<GameObject> emailList;

    public GameObject emailWindowBackButton;
    public GameObject singleEmailBackButton;
    
    private void Start()
    {
        if (simulationManager.emailStatus.Count == 0)
        {
            simulationManager.emailStatus.Add(emailList[0].GetComponent<SingleEmail>().readMail);
        }
        
        if (simulationManager.assignmentCutScenePlayed)
        {
            GameObject newEmail = Instantiate(singleEmailPrefab, new Vector3(mailWrapper.position.x, mailWrapper.position.y, mailWrapper.position.z), Quaternion.identity);
            RectTransform newEmailUI = newEmail.GetComponent<RectTransform>();
            SingleEmail newEmailScript = newEmail.GetComponent<SingleEmail>();
            
            emailList.Add(newEmail);
            
            if (simulationManager.emailStatus.Count == 1)
            {
                simulationManager.emailStatus.Add(emailList[1].GetComponent<SingleEmail>().readMail);
            }

            newEmailScript.emailID = 1;
            newEmailScript.emailTitle = "Array Assignment Homework";
            newEmailScript.emailBody = "PLz help me do this";
            targetLevelName = "Array Assignment Level";
            
            newEmail.transform.SetParent(mailWrapper);
            newEmailUI.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            
        }
        
        foreach (GameObject email in emailList)
        {
            int id = email.GetComponent<SingleEmail>().emailID;
            email.GetComponent<SingleEmail>().readMail = simulationManager.emailStatus[id];
        }
        
        ReverseVerticalLayoutGroup();
        NotificationVisibility();
    }

    private void ReverseVerticalLayoutGroup()
    {
        int childCount = mailWrapper.transform.childCount;
        Transform[] childTransforms = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            childTransforms[i] = mailWrapper.transform.GetChild(i);
        }

        for (int i = 0; i < childCount / 2; i++)
        {
            int j = childCount - 1 - i;
            childTransforms[i].SetSiblingIndex(j);
        }
    }

    public void PrivateEmailProceedButtonOnClicked()
    {
        SceneManager.LoadSceneAsync(targetLevelName);
    }

    public void NotificationVisibility()
    {
        int unreadCount = 0;
        for (int i = 0; i < mailWrapper.childCount; i++)
        {
            SingleEmail singleMail = mailWrapper.GetChild(i).gameObject.GetComponent<SingleEmail>();
            if (!singleMail.readMail)
            {
                unreadCount++;
            }
        }

        if (unreadCount > 0)
        {
            emailNotification.SetActive(true);
            notificationCountText.text = unreadCount.ToString();
        }
        else
        {
            emailNotification.SetActive(false);
        }

    }
    
}
