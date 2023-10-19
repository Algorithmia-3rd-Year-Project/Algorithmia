using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionLevel1 : MonoBehaviour
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
    private GameObject instruction6;

    [SerializeField] 
    private GameObject instruction7;

    [SerializeField] private GameObject instruction8;
    
    [SerializeField]
    private GameObject instructionOverlay;

    [SerializeField]
    private ArrayLevelManager levelManager;

    [SerializeField]
    private GameObject pseudoCodeTrigger;


    private bool instruction1Executed;
    private bool instruction3Executed;
    private bool instruction4Executed;
    private bool instruction5Executed;
    private bool instruction6Executed;
    
    
    [SerializeField] private TMP_Text compileMessage;
    [SerializeField] private GameObject compilationMenu;
    [SerializeField] private GameObject victoryMenu;
    
    private void Start()
    {
        instructionOverlay.SetActive(true);
        instruction1.SetActive(true);

        instruction1Executed = false;
    }


    private void Update()
    {
        if (levelManager.blockCount != 0 && instruction1Executed == false)
        {
            instruction1.SetActive(false);
            instruction1Executed = true;
            instruction2.SetActive(true);
            pseudoCodeTrigger.SetActive(true);
        }

        if (!instruction3Executed && levelManager.correctForms.Count > 0 && levelManager.correctForms[0].transform.childCount != 0)
        {
            instruction3.SetActive(false);
            instructionOverlay.SetActive(false);
            instruction3Executed = true;
            //set rest 3 of data block's guidance points to set as active
        }

        if (!instruction4Executed && levelManager.correctForms.Count > 0)
        {
            int elementCount = 0;

            for (int i=0; i < 4; i++)
            {
                if (levelManager.correctForms[i].transform.childCount != 0)
                {
                    elementCount += 1;
                }
            }

            if (elementCount == 4)
            {
                instructionOverlay.SetActive(true);
                instruction4.SetActive(true);
                instruction4Executed = true;
            }
        }

        if (!instruction5Executed && levelManager.lines.Count > 0)
        {
            instruction5.SetActive(false);
            instruction6.SetActive(true);
            instruction5Executed = true;
        }
   
    }
    
    public void Compile(string compiledMsg)
    {
        if (instruction5Executed)
        {
            compileMessage.text = compiledMsg;
            compilationMenu.SetActive(true);
            instruction6.SetActive(false);
            instruction6Executed = true;
        }

    }

    public void Build()
    {
        if (instruction6Executed)
        {
            instruction7.SetActive(false);
            victoryMenu.SetActive(true);
            //instructionOverlay.SetActive(true);
            instruction8.SetActive(true);
        }
    }

}
