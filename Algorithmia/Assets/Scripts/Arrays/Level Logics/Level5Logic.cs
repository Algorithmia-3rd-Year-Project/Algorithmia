using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level5Logic : MonoBehaviour
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
    
    [SerializeField] private Image trophyPlaceholder;
    [SerializeField] private List<Sprite> trophyImages;
    [SerializeField] private TMP_Text expectedMsg;
    [SerializeField] private TMP_Text resultMsg;
    [SerializeField] private TMP_Text objectiveStatus;
    [SerializeField] private GameObject proceedButton;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private Stopwatch stopwatch;

    private List<string> outputArray = new List<string>();
    
    [SerializeField] private ArrayLevelDataManager levelDataManager;
    
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

            GameObject arrayObject = null;

            for (int i = 0; i < levelManager.blocks.Count; i++)
            {
                if (levelManager.blocks[i].name == "Advanced Array Block(Clone)")
                {
                    arrayObject = levelManager.blocks[i];
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
                                return "Incompatible data type passed to the array";
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
                                return "Incompatible data type passed to the array";
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
                        return "Undeclared variable s";
                    }
                    
                    if (!char.IsDigit(s))
                    {
                        Debug.Log("Invalid data type for s");
                        return "Invalid data type for s";
                    }
                    
                    if (currentLine.Contains(" to e"))
                    {
                        Debug.Log("Undeclared variable e");
                        return "Undeclared variable e";
                    }
            
                    if (!char.IsDigit(e))
                    {
                        Debug.Log("Invalid data type for e");
                        return "Invalid data type for e";
                    }
                }

                //Check the print function code is trying to get executed only if there is an array data structure
                if (currentLine.Contains("print Array[index]") && arrayObject == null)
                {
                    Debug.Log("Array not found");
                    return "Array not found";
                }
                
                //checking out start variable of reversal function has valid syntax
                if (currentLine.Contains("start =") && currentLine.Length == 9)
                {
                    if (!char.IsDigit(currentLine[8]))
                    {
                        Debug.Log("Passed data is invalid type");
                        return "Passed data is invalid type";
                    }
                }
                
                //checking out end variable of reversal function has valid syntax
                if (currentLine.Contains("end =") && currentLine.Length == 7)
                {
                    if (!char.IsDigit(currentLine[6]))
                    {
                        Debug.Log("Passed data is invalid type");
                        return "Passed data is invalid type";
                    }
                }
                
                //checking whether the reversal function uses correct data type when checking array elements
                if (currentLine.Contains("temp = Array"))
                {
                    if (arrayObject == null)
                    {
                        Debug.Log("No array found");
                        return "No array found";
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
                                return "Incompatible data type passed for temp variable";
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
                                return "Incompatible data type passed for the temp variable";
                            }
                        }
                    }
                }
                
            }
            
            string result = string.Join("", currentArray);

            string output = result;
            
            
            int x = 0;
            int start = 0;
            int end = 0;
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
                        return "Invalid range for Print Function";
                    }
                    
                    output = output.Substring(s, phraseLength);
                    outputArray.Add(output);
                    x += 3;
                } else if (codes[x].Contains("start</color> =") && codes[x].Length == 54)
                {
                    start = int.Parse(codes[x][45] + "");
                    x += 1;
                } else if (codes[x].Contains("end</color> =") && codes[x].Length == 52)
                {
                    end = int.Parse(codes[x][43] + "");

                    if (!(start >= 0 && end < output.Length && start <= end))
                    {
                        Debug.Log("Invalid range for array reversal");
                        return "Invalid range for array reversal";
                    }
                    
                    output = ReverseString(output, start, end);
                    x += 8;
                }
                else
                {
                    x += 1;
                }
            }

            Debug.Log(output);
            Debug.Log(outputArray.Count);
            
            compilationSuccess = true;
            return "Compiled Successfully";

            /*
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
            }*/


            /*
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

            if (errorMessage.Contains("for index=") && (char.IsDigit(s) || char.IsDigit(e)))
            {
                errorMessage = "";
            }

            if (errorMessage != "")
            {
                Debug.Log(errorMessage);
                return;
            }*/

            /*
            if (errorMessage == "")
            {
                string expectedResult = "art";

                int begin = int.Parse(s.ToString());
                int end = int.Parse(e.ToString());

                if (begin < 0 || end >= result.Length || begin >= result.Length || end < 0)
                {
                    Debug.Log("Index is outside the bounds of array");
                    return;
                }

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

        return "Dummy Message";
    }

    private string ReverseString(string input, int start, int end)
    {
        
        char[] charArray = input.ToCharArray();
        
        while (start < end)
        {
            char temp = charArray[start];
            charArray[start] = charArray[end];
            charArray[end] = temp;

            start += 1;
            end -= 1;
        }

        return new string(charArray);
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
            VictoryMenuDetails();
            victoryMenu.SetActive(true);
        }
    }
    
    private void VictoryMenuDetails()
    {
        float currentTime = stopwatch.currentTime;
        expectedMsg.text = "step pets";

        if (outputArray.Count == 2)
        {
            if (outputArray[0] == "step" && outputArray[1] == "pets" && currentTime <= 30f)
            {
                trophyPlaceholder.sprite = trophyImages[0];
                levelDataManager.currentTrophy = 0;
                resultMsg.text = outputArray[0] + " " + outputArray[1];
                objectiveStatus.text = "Objective complete";
                proceedButton.SetActive(true);
                retryButton.SetActive(false);
            } else if (outputArray[0] == "step" && outputArray[1] == "pets" && currentTime <= 60f)
            {
                trophyPlaceholder.sprite = trophyImages[1];
                levelDataManager.currentTrophy = 1;
                resultMsg.text = outputArray[0] + " " + outputArray[1];
                objectiveStatus.text = "Objective complete";
                proceedButton.SetActive(true);
                retryButton.SetActive(false);
            } else if (outputArray[0] == "step" && outputArray[1] == "pets" && currentTime > 60f)
            {
                trophyPlaceholder.sprite = trophyImages[2];
                levelDataManager.currentTrophy = 2;
                resultMsg.text = outputArray[0] + " " + outputArray[1];
                objectiveStatus.text = "Objective complete";
                proceedButton.SetActive(true);
                retryButton.SetActive(false);
            }
        } else 
        {
            trophyPlaceholder.sprite = trophyImages[3];
            levelDataManager.currentTrophy = 3;
            resultMsg.text = "something else";
            objectiveStatus.text = "Objective is not met";
            proceedButton.SetActive(false);
            retryButton.SetActive(true);
        }
        
    }

    public void TryAgain()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
