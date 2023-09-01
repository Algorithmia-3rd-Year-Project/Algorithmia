using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Logic : MonoBehaviour
{

    [SerializeField] private ArrayLevelManager levelManager;

    public void CheckAnswer()
    {
        bool programConnected = false;
        
        for (int i = 0; i < levelManager.lines.Count; i++)
        {
            if (levelManager.lines[i].GetComponent<ArrayLine>().endPos.name == "PC")
            {
                programConnected = true;
                break;
            }
        }

        if (!programConnected)
        {
            Debug.Log("Program is not connected");
            return;
        }

        int hasArray = 0;
        int hasPrint = 0;

        for (int i = 0; i < levelManager.blocks.Count; i++)
        {
            string blockName = levelManager.blocks[i].GetComponent<ArrayBlock>().blockName;
            if (blockName == "Empty Array")
            {
                hasArray += 1;
            } else if (blockName == "Array Print")
            {
                hasPrint += 1;
            }
        }

        if (hasArray == 0)
        {
            Debug.Log("No Array is declared");
            return;
        }

        if (hasArray > 1)
        {
            Debug.Log("Duplicate arrays");
            return;
        }
        
        const string correctOrder = "cool";
        string receivedOrder = "";
        string rangeStart = "";
        string rangeEnd = "";
        

        
        if (levelManager.correctForms.Count > 0)
        {
            
            for (int i = 0; i < levelManager.correctForms.Count; i++)
            {
                GameObject attachedChild = (levelManager.correctForms[i].transform.childCount > 0)
                    ? levelManager.correctForms[i].transform.GetChild(0).gameObject
                    : null;

                if (attachedChild != null)
                {
                    if (levelManager.correctForms[i].name == "0" || levelManager.correctForms[i].name == "1" ||
                        levelManager.correctForms[i].name == "2" || levelManager.correctForms[i].name == "3")
                    {
                        receivedOrder += attachedChild.GetComponent<DataBlock>().dataValue;
                    } else if (levelManager.correctForms[i].name == "Print - Start Point")
                    {
                        rangeStart = attachedChild.GetComponent<DataBlock>().dataValue;
                    } else if (levelManager.correctForms[i].name == "Print - End Point")
                    {
                        rangeEnd = attachedChild.GetComponent<DataBlock>().dataValue;
                    }
                }
            }
        }
        
        if (hasPrint == 1 && (rangeStart == "" || rangeEnd == "")) 
        {
            Debug.Log("Undeclared variables in print");
            return;
        }

        if (receivedOrder == "")
        {
            Debug.Log("No Message to display");
            return;
        }

        if (receivedOrder == correctOrder && hasPrint == 1)
        {
            Debug.Log("Correct Answer");
        }
    }

}
