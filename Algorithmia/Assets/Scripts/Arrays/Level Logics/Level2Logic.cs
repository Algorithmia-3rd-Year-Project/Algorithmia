using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class Level2Logic : MonoBehaviour
{
    [SerializeField] private GameObject codeEditor;

    [SerializeField] private List<GameObject> codeOrder;

    [SerializeField] private List<string> correctCodeOrder;

    [SerializeField] private ArrayLevelManager levelManager;
    
    //Checking the optimal answer using pseudo codes
    public void OptimalPseudoCodeAnswer()
    {
        if (levelManager.lines.Count == 1)
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
                            codeOrder.Add(code);
                        }
                    }
                    
                }
            }

            GameObject dataStructureBlock = null;
            
            foreach (GameObject block in levelManager.blocks)
            {
                if (block.GetComponent<ArrayBlock>().blockName == "Empty Array")
                {
                    dataStructureBlock = block;
                    break;
                }
                
            }
            
            string[] currentArray = new string[4];
            
            for (int i = 0; i < codeOrder.Count; i++)
            {
                if (codeOrder[i].transform.parent.gameObject.name == "Code Data Instance(Clone)")
                {
                    string word = codeOrder[i].GetComponent<TMP_Text>().text;
                    string pattern = @"\[(.*?)\]";
                    Match match = Regex.Match(word, pattern);

                    if (match.Success && match.Groups.Count > 1)
                    {
                        string extractedText = "" + match.Groups[0].Value[1];
                        int index = int.Parse(extractedText);
                        currentArray[index] = word[word.Length - 1] + "";
                    }
                }
            }
            
            //Check whether the codeOrder matches with the correctCodeOrder
            string errorMessage = "";
            
            //Return error if there are more than one array declared
            if (levelManager.blocks.Count > 1)
            {
                Debug.Log("More arrays");
                return;
            }
            
            for (int i = 0; i < codeOrder.Count; i++)
            {
                if (codeOrder[i].transform.parent.gameObject.name != "Code Data Instance(Clone)")
                {
                    string currentText = codeOrder[i].GetComponent<TMP_Text>().text;
                    
                    if (currentText != correctCodeOrder[i])
                    {
                        string errorLine = Regex.Replace(currentText, "<.*?>", string.Empty); 
                        errorMessage = "Line " + i.ToString() + " : " + errorLine;
                        break;
                    }
                }
            }

            
            string result = string.Join("", currentArray);
            
            if (errorMessage != "" && result != "")
            {
                Debug.Log(errorMessage + "\n" + "Incompatible data type");
                return;
            }


            if (result == "cool")
            {
                Debug.Log("Correct output");
            }
            else
            {
                Debug.Log("cool was expected. but got " + result);
            }
        }
        else
        {
            Debug.Log("Program is not connected");
        }
        


    }

    //Checking the optimal answer using blocks connect
    /*
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
    }*/
}
