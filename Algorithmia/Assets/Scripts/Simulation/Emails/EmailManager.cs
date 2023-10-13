using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmailManager : MonoBehaviour
{
    [SerializeField] private SimManager simulationManager;

    //Email content fields
    public GameObject openedEmail;
    public TMP_Text emailTitleText;
    public TMP_Text emailBodyText;

    [SerializeField] private GameObject singleEmailPrefab;
    [SerializeField] private Transform mailWrapper;
    
    private void Start()
    {
        if (simulationManager.assignmentEmailShown)
        {
            GameObject newEmail = Instantiate(singleEmailPrefab, new Vector3(mailWrapper.position.x, mailWrapper.position.y, mailWrapper.position.z), Quaternion.identity);
            RectTransform newEmailUI = newEmail.GetComponent<RectTransform>();
            SingleEmail newEmailScript = newEmail.GetComponent<SingleEmail>();

            newEmailScript.emailTitle = "Array Assignment Homework";
            newEmailScript.emailBody = "PLz help me do this";
            
            newEmail.transform.SetParent(mailWrapper);
            newEmailUI.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            
        }
        
        ReverseVerticalLayoutGroup();
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
    
}
