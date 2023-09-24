using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class Level4Logic : MonoBehaviour
{
    [SerializeField] private GameObject codeEditor;

    [SerializeField] private List<GameObject> codeOrder;

    [SerializeField] private List<string> correctCodeOrder;

    [SerializeField] private ArrayLevelManager levelManager;

    [SerializeField] private List<string> codes = new List<string>();

    [SerializeField] private string[] ww;
    
    private bool compilationSuccess;
    
    [SerializeField] private TMP_Text compileMessage;
    [SerializeField] private GameObject compilationMenu;
    [SerializeField] private GameObject victoryMenu;

    public string OptimalAnswer()
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
            return "Could not compile the program";
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
                return "Undefined variables in functions";
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
            

            if (errorMessage.Contains("for index=s to "))
            {
                Debug.Log("Undeclared variable s");
                return "Undeclared variable s" + "\n" + errorMessage;
            }
            
            if (errorMessage.Contains(" to e"))
            {
                Debug.Log("Undeclared variable e");
                return "Undeclared variable e" + "\n" + errorMessage;
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
                        return "Invalid data types as elements" + "\n" + errorMessage;
                    }
                }
            } else if (arrayBlockType == "Character")
            {
                foreach (string letter in currentArray)
                {
                    if (int.TryParse(letter, out _))
                    {
                        Debug.Log("Invalid data types as elements");
                        return "Invalid data types as elements" + "\n" + errorMessage;
                    }
                }
            }

            char s = printBlockCode[24];
            char e = printBlockCode[51];

            if (!char.IsDigit(s))
            {
                Debug.Log("Invalid data type for s");
                return "Invalid data type for s" + "\n" + errorMessage;
            }
            
            if (!char.IsDigit(e))
            {
                Debug.Log("Invalid data type for e");
                return "Invalid data type for e" + "\n" + errorMessage;
            }
            
            int begin = int.Parse(s.ToString());
            int end = int.Parse(e.ToString());
                
            if (begin < 0 || end >= result.Length || begin >= result.Length || end < 0)
            {
                Debug.Log("Index is outside the bounds of array");
                return "Index is outside the bounds of array" + "\n" + errorMessage;
            }

            if (errorMessage != "")
            {
                Debug.Log(errorMessage);
                return errorMessage;
            }
            
            /*
            if (errorMessage == "")
            {
                string expectedResult = "art";

                string receivedOutput = "";
                
                for (int i = begin; i <= end; i++)
                {
                    receivedOutput += result[i];
                }
                
                if (receivedOutput != expectedResult)
                {
                    Debug.Log("Expected art but got " + receivedOutput);
                    return;
                }
                
                Debug.Log("Victory");
            }*/
            
        }
        return "dummy message";
    }
    
    public void Compile(bool compilation)
    {
        compilationSuccess = false;
        string returnedMessage = OptimalAnswer();
        if (!compilationSuccess)
        {
            compileMessage.text = returnedMessage;
            compilationMenu.SetActive(true);
            return;
        }

        if (compilation)
        {
            compileMessage.text = "Compilation Successful";
            compilationMenu.SetActive(true);
        }
    }
    
    public void Build()
    {
        Compile(false);
        if (compilationSuccess)
        {
            victoryMenu.SetActive(true);
        }
    }
    
}
