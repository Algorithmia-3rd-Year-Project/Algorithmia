using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleEmail : MonoBehaviour
{

    public int emailID;
    public string sender;

    public string emailTitle;
    public string emailBody;

    [SerializeField] private Button readButton;
    [SerializeField] private TMP_Text emailTitleUnOpenedText;
    
    private EmailManager emailManager;
    private SimManager simulationManager;
    
    
    public bool readMail;
    
    private void Awake()
    {
        emailManager = FindObjectOfType<EmailManager>();
        simulationManager = FindObjectOfType<SimManager>();

    }

    private void Start()
    {
        readButton.onClick.AddListener(OpenEmail);
        emailTitleUnOpenedText.text = emailTitle;

        /*
        if (simulationManager.emailStatus.Count > emailID)
        {
            readMail = simulationManager.emailStatus[emailID];
        }*/
        
    }

    private void OpenEmail()
    {
        //Debug.Log(emailTitle);
        emailManager.openedEmail.SetActive(true);
        this.transform.parent.gameObject.SetActive(false);
        emailManager.emailTitleText.text = emailTitle;
        emailManager.emailBodyText.text = emailBody;
        readMail = true;
        simulationManager.emailStatus[emailID] = readMail;
    }
}
