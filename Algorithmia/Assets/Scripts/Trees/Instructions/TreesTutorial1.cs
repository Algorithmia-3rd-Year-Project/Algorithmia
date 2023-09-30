using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesTutorial1 : MonoBehaviour
{

    [SerializeField]
    private GameObject instruction1;

    [SerializeField]
    private GameObject instruction2;

    [SerializeField]
    private GameObject instruction3;

    [SerializeField]
    private GameObject instruction4;

    [SerializeField]
    private GameObject instruction5;

    [SerializeField]
    private TreeLevelManager levelManager;

    private bool instruction1Executed;
    private bool instruction2Executed;
    private bool instruction3Executed = false;
    private bool instruction4Executed;
    private bool instruction5Executed;

    [SerializeField]
    private GameObject instructionOverlay;

    [SerializeField]
    private GameObject topUIInstructionOverlay;

    [SerializeField]
    private GameObject runButtonOverlay;

    [SerializeField]
    private GameObject guide1;

    [SerializeField]
    private GameObject guide2;

    [SerializeField]
    private GameObject guide3;

    [SerializeField]
    private GameObject guide4;

    private void Start()
    {
        instruction1.SetActive(false);
        instruction2.SetActive(true);
        instruction3.SetActive(false);
        instruction4.SetActive(false);
        guide1.SetActive(true);
        instructionOverlay.SetActive(true);
        topUIInstructionOverlay.SetActive(true);
        runButtonOverlay.SetActive(true);
    }

    private void Update()
    {
        if (instruction3Executed == false && levelManager.blockCount != 0)
        {
            instruction2.SetActive(false);
            guide1.SetActive(false);
            guide2.SetActive(true);
            instruction3.SetActive(true);
            instruction3Executed = true;
        }

        if (levelManager.dataSnapPoints.Count > 0)
        {
            if (!instruction4Executed && levelManager.dataSnapPoints[0].transform.childCount != 0)
            {
                instruction3.SetActive(false);
                instruction4.SetActive(true);
                guide2.SetActive(false);
                guide3.SetActive(true);
                instruction4Executed = true;
            }
        }

        if (levelManager.dataSnapPoints.Count > 1) 
        { 

            if (!instruction5Executed && levelManager.dataSnapPoints[1].transform.childCount != 0)
            {
                instruction4.SetActive(false);
                instruction5.SetActive(true);
                guide3.SetActive(false);
                guide4.SetActive(true);
                instruction5Executed = true;

            }
        }
        
    }

}
