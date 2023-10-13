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
    }
    
}
