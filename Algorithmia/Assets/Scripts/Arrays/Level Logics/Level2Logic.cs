using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class Level2Logic : MonoBehaviour
{
    [SerializeField] private GameObject codeEditor;

    [SerializeField] private List<string> codeOrder;

    [SerializeField] private List<string> correctCodeOrder;

    public void OptimalAnswer()
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
                GameObject code = null;
                if (codeInstance.name == "Code Data Instance(Clone)")
                {
                    code = codeInstance.transform.GetChild(0).gameObject;
                    codeOrder.Add("_" + code.GetComponent<TMP_Text>().text);
                }
                else
                {
                    for (int j = 0; j < codeInstance.transform.childCount; j++)
                    {
                        if (codeInstance.transform.GetChild(j).name == "Code")
                        {
                            code = codeInstance.transform.GetChild(j).gameObject;
                            codeOrder.Add(code.GetComponent<TMP_Text>().text);
                        }
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

    }

}
