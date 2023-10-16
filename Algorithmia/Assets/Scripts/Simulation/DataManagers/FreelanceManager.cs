using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FreelanceManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TMP_InputField freelanceNameInput;
    [SerializeField] private TMP_InputField freelanceUsernameInput;
    [SerializeField] private TMP_InputField freelanceBioInput;

    private string freelanceName;
    private string freelanceUsername;
    private string freelanceBio;
    private bool hasFreelanceAccount;

    //pages
    [SerializeField] private GameObject freelanceMainPage;
    [SerializeField] private GameObject freelanceSignupPage;
    
    //Main page items
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text bioText;
    
    public void LoadData(GameData data)
    {
        this.nameText.text = data.freelanceName;
        this.bioText.text = data.freelanceBio;
        this.hasFreelanceAccount = data.hasFreelanceAccount;
    }

    public void SaveData(ref GameData data)
    {
        data.freelanceName = this.freelanceName;
        data.freelanceUsername = this.freelanceUsername;
        data.freelanceBio = this.freelanceBio;
        data.hasFreelanceAccount = this.hasFreelanceAccount;
    }

    public void SignUpButtonOnClicked()
    {
        freelanceName = freelanceNameInput.text;
        freelanceUsername = freelanceUsernameInput.text;
        freelanceBio = freelanceBioInput.text;
        
        freelanceSignupPage.SetActive(false);
        freelanceMainPage.SetActive(true);

        nameText.text = freelanceName;
        bioText.text = freelanceBio;

        hasFreelanceAccount = true;
    }

    public void FreelanceAppOnClicked()
    {
        if (hasFreelanceAccount)
        {
            freelanceSignupPage.SetActive(false);
            freelanceMainPage.SetActive(true);
        }
        else
        {
            freelanceSignupPage.SetActive(true);
            freelanceMainPage.SetActive(false);
        }
    }
    
}
