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
    
    [SerializeField] private GameObject blockDropTrigger;
    [SerializeField] private GameObject dataDropTrigger1;
    [SerializeField] private GameObject dataDropTrigger2;
    [SerializeField] private GameObject dataDropTrigger3;
    [SerializeField] private GameObject dataDropTrigger4;
    [SerializeField] private GameObject lineDrawTrigger;

    public int lineCount;
    
    private void Start()
    {
        instructionOverlay.SetActive(true);
        instruction1.SetActive(true);

        instruction1Executed = false;
        blockDropTrigger.SetActive(true);
    }


    private void Update()
    {
        if (levelManager.blockCount > 0)
        {
            blockDropTrigger.SetActive(false);
        }

        if (levelManager.correctForms.Count > 0 && levelManager.correctForms[0].transform.childCount > 0)
        {
            dataDropTrigger1.SetActive(false);
            dataDropTrigger2.SetActive(true);
        }
        
        if (levelManager.correctForms.Count > 0 && levelManager.correctForms[1].transform.childCount > 0)
        {
            dataDropTrigger2.SetActive(false);
            dataDropTrigger3.SetActive(true);
        }
        
        if (levelManager.correctForms.Count > 0 && levelManager.correctForms[2].transform.childCount > 0)
        {
            dataDropTrigger3.SetActive(false);
            dataDropTrigger4.SetActive(true);
        }
        
        if (levelManager.correctForms.Count > 0 && levelManager.correctForms[3].transform.childCount > 0)
        {
            dataDropTrigger4.SetActive(false);
        }

        if (lineCount > 0)
        {
            lineDrawTrigger.SetActive(false);
        }
        
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

    public void DataDropTriggerPoints()
    {
        dataDropTrigger1.SetActive(true);
    }

    public void LineDrawTriggerPoints()
    {
        lineDrawTrigger.SetActive(true);
    }

}
