using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class Level2Logic : MonoBehaviour
{
    //[SerializeField] private GameObject codeEditor;

    //[SerializeField] private List<string> codeOrder;

    //[SerializeField] private List<string> correctCodeOrder;

    [SerializeField] private ArrayLevelManager levelManager;

    //Checking the optimal answer using pseudo codes
    /*public void OptimalPseudoCodeAnswer()
    {
        //Clear the code order list if it is already populated
        if (codeOrder.Count > 0)
        {
            codeOrder.Clear();
        }
        
        //Populate the code order list with the codes in the pseudo code editor
        for (int i = 0; i < codeEditor.transform.childCount; i++)
        {
            if (codeEditor.transform.GetChild(i).name != "Blank Space")
            {
                GameObject codeInstance = codeEditor.transform.GetChild(i).gameObject;

                for (int j = 0; j < codeInstance.transform.childCount; j++)
                {
                    if (codeInstance.transform.GetChild(j).name == "Code")
                    {
                        GameObject code = codeInstance.transform.GetChild(j).gameObject;
                        codeOrder.Add(code.GetComponent<TMP_Text>().text);
                    }
                }
                
            }
        }
        
        //Check whether the codeOrder matches with the correctCodeOrder
        string errorMessage = "";
        
        for (int i = 0; i < codeOrder.Count; i++)
        {
            if (codeOrder[i] != correctCodeOrder[i])
            {
                string errorLine = Regex.Replace(codeOrder[i], "<.*?>", string.Empty); 
                errorMessage = "Line " + i.ToString() + " : " + errorLine;
                break;
            }
        }
        
        Debug.Log(errorMessage);

    }*/

    public void OptimalAnswer()
    {
        if (levelManager.lines.Count == 0)
        {
            Debug.Log("No Program to run");
            return;
        }
        
        if (levelManager.blockCount == 0)
        {
            Debug.Log("No Array Declared");
            return;
        }

        if (levelManager.blockCount > 1)
        {
            Debug.Log("Duplicate array declaration");
            return;
        }

        const string correctOrder = "cool";
        string receivedOrder = "";
        
        if (levelManager.correctForms.Count > 0)
        {
            
            for (int i = 0; i < levelManager.correctForms.Count; i++)
            {
                GameObject attachedChild = (levelManager.correctForms[i].transform.childCount > 0)
                    ? levelManager.correctForms[i].transform.GetChild(0).gameObject
                    : null;

                if (attachedChild != null)
                {
                    receivedOrder += attachedChild.GetComponent<DataBlock>().dataValue;
                }
            }

            
        }

        if (receivedOrder == "")
        {
            Debug.Log("No Message to display");
            return;
        }

        if (receivedOrder != "")
        {
            string dataType = levelManager.blocks[0].GetComponent<ArrayBlock>().dataType;

            switch (dataType)
            {
                case "Number":
                    Debug.Log("Invalid Data type");
                    return;
                case "Boolean":
                    Debug.Log("Invalid Data type bool");
                    return;
            }
        }
        
        if (receivedOrder == correctOrder)
        {
           Debug.Log("Correct Message");
        }
        else
        {
            Debug.Log("Incorrect message");
        }
    }
}
