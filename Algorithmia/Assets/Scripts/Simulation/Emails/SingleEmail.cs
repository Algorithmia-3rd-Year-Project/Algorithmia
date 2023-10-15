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
    
    private void Awake()
    {
        emailManager = FindObjectOfType<EmailManager>();
        
    }

    private void Start()
    {
        readButton.onClick.AddListener(OpenEmail);
        emailTitleUnOpenedText.text = emailTitle;
    }

    private void OpenEmail()
    {
        //Debug.Log(emailTitle);
        emailManager.openedEmail.SetActive(true);
        this.transform.parent.gameObject.SetActive(false);
        emailManager.emailTitleText.text = emailTitle;
        emailManager.emailBodyText.text = emailBody;
    }
}