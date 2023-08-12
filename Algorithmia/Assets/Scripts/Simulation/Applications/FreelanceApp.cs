using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreelanceApp : MonoBehaviour
{

    [SerializeField]
    private GameObject signUpPage;

    private bool hasSignUp;

    private void Start()
    {
        hasSignUp = false;
    }

    public void OpenFreelanceApp()
    {
        if (!hasSignUp)
        {
            signUpPage.SetActive(true);
        }
    }

}
