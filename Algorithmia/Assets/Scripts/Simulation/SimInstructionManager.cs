using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimInstructionManager : MonoBehaviour
{
    [SerializeField] private GameObject instruction1;
    [SerializeField] private GameObject instruction2;
    [SerializeField] private GameObject instruction3;
    [SerializeField] private GameObject instruction4;
    [SerializeField] private GameObject instruction5;
    [SerializeField] private GameObject instruction6;
    [SerializeField] private GameObject instruction7;

    [SerializeField] private GameObject triggerCollection;
    
    
    private void Start()
    {
        triggerCollection.SetActive(true);
        instruction1.SetActive(true);
    }
}
