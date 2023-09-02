using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class Level3Logic : MonoBehaviour
{
    [SerializeField] private GameObject codeEditor;

    [SerializeField] private List<GameObject> codeOrder;

    [SerializeField] private List<string> correctCodeOrder;

    [SerializeField] private ArrayLevelManager levelManager;

    private List<string> codes = new List<string>();

    [SerializeField] private string[] ww;

    /*
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
    }*/

    public void OptimalAnswer()
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
        }
        else
        {
            //Clear the code order list if it is already populated
            if (codeOrder.Count > 0)
            {
                codeOrder.Clear();
            }

            if (codes.Count > 0)
            {
                codes.Clear();
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

            ww = currentArray;
            string errorMessage = "";
            
            //For checking whether the workspace have unconnected blocks
            for (int i = 0; i < levelManager.blocks.Count; i++)
            {
                ArrayLine line = levelManager.blocks[i].transform.Find("Line").GetComponent<ArrayLine>();
                if (line.startPos == null && line.endPos == null)
                {
                    errorMessage = "Unconnected Blocks";
                    break;
                }
            }
            
            if (errorMessage == "Unconnected Blocks")
            {
                Debug.Log("There are random wanderers");
                return;
            }
            
            for (int i = 0; i < codeOrder.Count; i++)
            {
                if (codeOrder[i].transform.parent.gameObject.name != "Code Data Instance(Clone)")
                {
                    string currentText = codeOrder[i].GetComponent<TMP_Text>().text;
                    
                    codes.Add(currentText);
                }
            }
            
            for (int i = 0; i < codes.Count; i++)
            {
                
                if (codes[i] != correctCodeOrder[i])
                {
                    string errorLine = Regex.Replace(codes[i], "<.*?>", string.Empty); 
                    errorMessage = "Line " + i.ToString() + " : " + errorLine;
                    break;
                }
                
            }
            
            string result = string.Join("", currentArray);


            
            if (errorMessage.Contains("Number Array[4]") && result == "")
            {
                Debug.Log("Nothing to output");
                return;
            }

            if (errorMessage.Contains("for index=s to "))
            {
                Debug.Log("Undeclared variable s");
                return;
            }
            
            if (errorMessage.Contains(" to e"))
            {
                Debug.Log("Undeclared variable e");
                return;
            }

            string printBlockCode = "";
            string arrayBlockType = "";

            for (int i = 0; i < levelManager.blocks.Count; i++)
            {
                if (levelManager.blocks[i].name == "Array Print Function(Clone)")
                {
                    printBlockCode = levelManager.blocks[i].GetComponent<ArrayBlock>().pseudoCode;
                }

                if (levelManager.blocks[i].name == "Advanced Array Block(Clone)")
                {
                    arrayBlockType = levelManager.blocks[i].GetComponent<ArrayBlock>().dataType;
                }

            }
            
            //check for array data type errors
            if (arrayBlockType == "Number")
            {
                foreach (string letter in currentArray)
                {
                    if (!int.TryParse(letter, out _))
                    {
                        Debug.Log("Invalid data types as elements");
                        return;
                    }
                }
            } else if (arrayBlockType == "Character")
            {
                foreach (string letter in currentArray)
                {
                    if (int.TryParse(letter, out _))
                    {
                        Debug.Log("Invalid data types as elements");
                        return;
                    }
                }
            }
            
            

            char s = printBlockCode[24];
            char e = printBlockCode[51];

            if (!char.IsDigit(s))
            {
                Debug.Log("Invalid data type for s");
                return;
            }
            
            if (!char.IsDigit(e))
            {
                Debug.Log("Invalid data type for e");
                return;
            }

            if (errorMessage != "")
            {
                Debug.Log(errorMessage);
                return;
            }

            if (errorMessage == "")
            {
                string expectedResult = "cool";

                int begin = int.Parse(s.ToString());
                int end = int.Parse(e.ToString());
                
                if (begin < 0 || end >= result.Length || begin >= result.Length || end < 0)
                {
                    Debug.Log("Index is outside the bounds of array");
                    return;
                }
                
                for (int i = begin; i < end; i++)
                {
                    if (result[i] != expectedResult[i])
                    {
                        Debug.Log("Expected cool but got " + result);
                        return;
                    }
                }
                
                Debug.Log("Victory");
            }
            
            
        }
    }
    
}
