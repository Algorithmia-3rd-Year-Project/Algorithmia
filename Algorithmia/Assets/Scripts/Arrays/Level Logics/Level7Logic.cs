using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class Level7Logic : MonoBehaviour
{
    [SerializeField] private GameObject codeEditor;

    [SerializeField] private List<GameObject> codeOrder;

    [SerializeField] private List<string> correctCodeOrder;

    [SerializeField] private ArrayLevelManager levelManager;

    [SerializeField] private List<string> codes = new List<string>();

    [SerializeField] private string[] ww;

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

            //Clear the codes list if it is already populated
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

            GameObject arrayObject = null;
            GameObject newArrayObject = null;

            for (int i = 0; i < levelManager.blocks.Count; i++)
            {
                if (levelManager.blocks[i].name == "Empty Array Block")
                {
                    arrayObject = levelManager.blocks[i];
                }

                if (levelManager.blocks[i].name == "Advanced Array Block(Clone)")
                {
                    newArrayObject = levelManager.blocks[i];
                }
            }

            for (int i = 0; i < codes.Count; i++)
            {
                string currentLine = Regex.Replace(codes[i], "<.*?>", string.Empty);
                
                //checking whether the array has the right type of data in it
                if (currentLine.Contains("Array[4]"))
                {
                    if (currentLine.Contains("Number Array[4]"))
                    {
                        foreach (string letter in currentArray)
                        {
                            string checker = letter;
                            
                            if (checker == null)
                            {
                                checker = "0";
                            }
                            
                            if (!int.TryParse(checker, out _))
                            {
                                Debug.Log("Incompatible data type passed to the array");
                                return;
                            }
                        }
                    }
                    
                    if (currentLine.Contains("Character Array[4]"))
                    {
                        foreach (string letter in currentArray)
                        {
                            string checker = letter;
                            
                            if (int.TryParse(checker, out _))
                            {
                                Debug.Log("Incompatible data type passed to the array");
                                return;
                            }
                        }
                    }
                }
                
                //checking the values passed for array printing range are syntactically correct or not
                if (currentLine.Contains("for index="))
                {
                    char s = currentLine[10];
                    char e = currentLine[15]; 
                    
                    if (currentLine.Contains("for index=s to "))
                    {
                        Debug.Log("Undeclared variable s");
                        return;
                    }
                    
                    if (!char.IsDigit(s))
                    {
                        Debug.Log("Invalid data type for s");
                        return;
                    }
                    
                    if (currentLine.Contains(" to e"))
                    {
                        Debug.Log("Undeclared variable e");
                        return;
                    }
            
                    if (!char.IsDigit(e))
                    {
                        Debug.Log("Invalid data type for e");
                        return;
                    }
                }

                //Check the print function code is trying to get executed only if there is an array data structure
                if (currentLine.Contains("print Array[index]") && arrayObject == null)
                {
                    Debug.Log("Array not found");
                    return;
                }
                
                //checking out start variable of reversal function has valid syntax
                if (currentLine.Contains("start =") && currentLine.Length == 9)
                {
                    if (!char.IsDigit(currentLine[8]))
                    {
                        Debug.Log("Passed data is invalid type");
                        return;
                    }
                }
                
                //checking out end variable of reversal function has valid syntax
                if (currentLine.Contains("end =") && currentLine.Length == 7)
                {
                    if (!char.IsDigit(currentLine[6]))
                    {
                        Debug.Log("Passed data is invalid type");
                        return;
                    }
                }
                
                //checking whether the reversal function uses correct data type when checking array elements
                if (currentLine.Contains("temp = Array"))
                {
                    if (arrayObject == null)
                    {
                        Debug.Log("No array found");
                        return;
                    }
                    
                    if (currentLine.Contains("Number temp = "))
                    {
                        foreach (string letter in currentArray)
                        {
                            string checker = letter;
                            
                            if (checker == null)
                            {
                                checker = "0";
                            }
                            
                            if (!int.TryParse(checker, out _))
                            {
                                Debug.Log("Incompatible data type passed for temp variable");
                                return;
                            }
                        }
                    }

                    if (currentLine.Contains("Character temp = "))
                    {
                        foreach (string letter in currentArray)
                        {
                            string checker = letter;
                            
                            if (int.TryParse(checker, out _))
                            {
                                Debug.Log("Incompatible data type passed for the temp variable");
                                return;
                            }
                        }
                    }
                }

                //checking whether the array insertion function's position variable has correct data type passed
                if (currentLine.Contains("pos ="))
                {
                    if (!char.IsDigit(currentLine[6]))
                    {
                        Debug.Log("Invalid data type passed for position");
                        return;
                    }
                }
                
                //checking whether the array insertion function's element variable has correct data type passed
                if (currentLine.Contains("element ="))
                {
                    if (arrayObject == null)
                    {
                        Debug.Log("No Array found");
                        return;
                    }
                    
                    if (newArrayObject == null)
                    {
                        Debug.Log("No Parameter Array Found");
                        return;
                    }

                    if (newArrayObject.GetComponent<ArrayBlock>().dataType != arrayObject.GetComponent<ArrayBlock>().dataType)
                    {
                        Debug.Log("Incompatible data conversion try");
                        return;
                    }
                    
                    if (newArrayObject.GetComponent<ArrayBlock>().dataType == "Number")
                    {
                        if (!char.IsDigit(currentLine[10]))
                        {
                            Debug.Log("Invalid data type passed for element");
                            return;
                        }
                    } else if (newArrayObject.GetComponent<ArrayBlock>().dataType == "Character")
                    {
                        if (char.IsDigit(currentLine[10]))
                        {
                            Debug.Log("Invalid data type passed for element");
                            return;
                        }
                    }
                }
                
                //checking whether the array deletion function's index variable has correct data type passed
                if (currentLine.Contains("index ="))
                {
                    if (!char.IsDigit(currentLine[8]))
                    {
                        Debug.Log("Invalid data type passed for index");
                        return;
                    }
                }
                
                //checking whether the array deletion function's length variable has correct data type passed
                if (currentLine.Contains("length ="))
                {
                    if (!char.IsDigit(currentLine[9]))
                    {
                        Debug.Log("Invalid data type passed for length");
                        return;
                    }
                }
                
                //Check the deletion function code is trying to get executed only if there is an array data structure
                if (currentLine.Contains("array[i]") && arrayObject == null)
                {
                    Debug.Log("Array not found for deletion function");
                    return;
                }
                
            }
            
            
            string result = string.Join("", currentArray);

            string output = result;
            List<string> outputArray = new List<string>();
            
            int x = 0;
            int position = 0;
            
            while (x < codes.Count)
            {
                if (codes[x].Contains("for index"))
                {
                    int s = int.Parse(codes[x][24] + "");
                    int e = int.Parse(codes[x][51] + "");
                    int phraseLength = (e - s) + 1;

                    if (!(s >= 0 && e >= s && e < output.Length))
                    {
                        Debug.Log("Invalid range for Print Function");
                        return;
                    }
                    
                    string printOutput = output.Substring(s, phraseLength);
                    outputArray.Add(printOutput);
                    x += 3;
                } else if (codes[x].Contains("pos</color> =") && codes[x].Length == 52)
                {
                    position = int.Parse(codes[x][43] + "");
                    x += 1;
                } else if (codes[x].Contains("element</color> =") && codes[x].Length == 56)
                {
                    var element = codes[x][47];

                    if (position < 0 || position > output.Length)
                    {
                        Debug.Log("Position is out of range");
                        return;
                    }
                    
                    output = InsertElement(output, position, element);
                    x += 8;
                } else if (codes[x].Contains("index</color> ="))
                {
                    position = int.Parse(codes[x][45] + "");
                    x += 1;
                } else if (codes[x].Contains("length</color> ="))
                {
                    var length = codes[x][46];

                    if (position < 0 || position > length)
                    {
                        Debug.Log("Position is out of range for deletion");
                        return;
                    }

                    output = RemoveCharacter(output, position);
                    x += 5;

                }
                else
                {
                    x += 1;
                }
            }

            Debug.Log(output);
            Debug.Log(outputArray.Count);


        }
    }

    private string InsertElement(string original, int index, char letter)
    {
        string leftSubstring = original.Substring(0, index);
        string rightSubstring = original.Substring((index));

        return leftSubstring + letter + rightSubstring;
    }

    private string RemoveCharacter(string original, int index)
    {
        string modifiedString = original.Substring(0, index) + original.Substring(index + 1);
        return modifiedString;
    }
}
