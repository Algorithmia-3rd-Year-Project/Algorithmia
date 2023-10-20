using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2Logic : MonoBehaviour
{
    [SerializeField] private GameObject codeEditor;

    [SerializeField] private List<GameObject> codeOrder;

    [SerializeField] private List<string> correctCodeOrder;

    [SerializeField] private ArrayLevelManager levelManager;
    
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
    
    private string _result;
    private bool compilationSuccess;

    [SerializeField] private ArrayLevelDataManager levelDataManager;

    [SerializeField] private GameObject helpInstructions;
    private bool helpInstructionShown;

    private void Start()
    {
        compilationSuccess = false;
    }

    //Checking the optimal answer using pseudo codes
    public string OptimalPseudoCodeAnswer()
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
                return "Duplicate Arrays";
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

            
            _result = string.Join("", currentArray);
            
            if (errorMessage != "" && _result != "")
            {
                Debug.Log(errorMessage + "\n" + "Incompatible data type");
                return "Incompatible data type" + "\n" + errorMessage;
            }

            compilationSuccess = true;
            return "Compiled Successfully";
            

            if (_result == "cool")
            {
                Debug.Log("Correct output");
                return "cool";
            }
            else
            {
                Debug.Log("cool was expected. but got " + _result);
                return _result;
            }
        }
        else
        {
            Debug.Log("Program is not connected");
            return "Could not compile the program";
        }
    }

    public void Compile(bool compilation)
    {
        compilationSuccess = false;
        string returnedMessage = OptimalPseudoCodeAnswer();
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
        expectedMsg.text = "Array of cool";
        
        if (_result == "cool" && currentTime <= 30f)
        {
            trophyPlaceholder.sprite = trophyImages[0];
            levelDataManager.currentTrophy = 0;
            resultMsg.text = "Array of cool";
            objectiveStatus.text = "Objective complete";
            proceedButton.SetActive(true);
            retryButton.SetActive(false);
        } else if (_result == "cool" && currentTime <= 60f)
        {
            trophyPlaceholder.sprite = trophyImages[1];
            levelDataManager.currentTrophy = 1;
            resultMsg.text = "Array of cool";
            objectiveStatus.text = "Objective complete";
            proceedButton.SetActive(true);
            retryButton.SetActive(false);
        } else if (_result == "cool" && currentTime > 60f)
        {
            trophyPlaceholder.sprite = trophyImages[2];
            levelDataManager.currentTrophy = 2;
            resultMsg.text = "Array of cool";
            objectiveStatus.text = "Objective complete";
            proceedButton.SetActive(true);
            retryButton.SetActive(false);
        } else if (_result != "cool")
        {
            trophyPlaceholder.sprite = trophyImages[3];
            levelDataManager.currentTrophy = 3;
            resultMsg.text = "Array of " + _result;
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

    public void CloseObjective()
    {
        if (!helpInstructionShown)
        {
            helpInstructions.SetActive(true);
            helpInstructionShown = true;
        }
    }

}
