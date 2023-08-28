using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrayBlockManager : MonoBehaviour
{

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject arrayPrefab;

    [SerializeField]
    GameObject currentObj;

    private float startPosX;

    private float startPosY;

    private int workspaceLayer;

    private ArrayBlockList arrayblocksList;

    [SerializeField]
    private ArrayLevelManager levelManager;

    [SerializeField]
    private LayerMask anotherLayer;

    [SerializeField]
    private LayerMask dataLayer;

    [SerializeField]
    private Transform dataParentObj;

    //For generating pseudo codes
    [SerializeField]
    private GameObject codeParent;

    [SerializeField]
    private GameObject codeArrayInstance;

    [SerializeField] 
    private GameObject codeDataInstance;
    
    [SerializeField]
    private GameObject codePrintInstance;

    [SerializeField]
    private GameObject codeReverseInstance;

    [SerializeField]
    private GameObject codeInsertionInstance;

    [SerializeField]
    private GameObject codeDeletionInstance;

    [SerializeField]
    private float typingSpeed;

    private void Start()
    {
        workspaceLayer = LayerMask.NameToLayer("Workspace");
        arrayblocksList = GetComponent<ArrayBlockList>();
        currentObj = null;

    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);


            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero, 20f, layerMask);

            if (hit.collider != null)
            {

                if (hit.collider.name == "Empty Array Block")
                {
                    currentObj = Instantiate(arrayblocksList.blockList["Empty Array Block"], new Vector3(mousePos.x, mousePos.y - 0.7f, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;

                }
                else if (hit.collider.name == "Array Print Function")
                {
                    currentObj = Instantiate(arrayblocksList.blockList["Array Print Function"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;

                }
                else if (hit.collider.name == "Array Reverse Function")
                {
                    currentObj = Instantiate(arrayblocksList.blockList["Array Reverse Function"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
                else if (hit.collider.name == "Array Insertion Function")
                {
                    currentObj = Instantiate(arrayblocksList.blockList["Array Insertion Function"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
                else if (hit.collider.name == "Array Deletion Function")
                {
                    currentObj = Instantiate(arrayblocksList.blockList["Array Deletion Function"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
                else if (hit.collider.name == "Long Array Block")
                {
                    currentObj = Instantiate(arrayblocksList.blockList["Long Array Block"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }

            }
            else
            {
                RaycastHit2D dataHit = Physics2D.Raycast(mousePos, Vector3.zero, 20f, dataLayer);

                if (dataHit.collider != null)
                {
                    currentObj = dataHit.collider.gameObject;

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
                else
                {
                    RaycastHit2D[] allhits = Physics2D.RaycastAll(mousePos, Vector3.zero, Mathf.Infinity, anotherLayer);

                    if (allhits.Length > 1)
                    {
                        foreach (RaycastHit2D singleHit in allhits)
                        {
                            if (singleHit.collider.CompareTag("Data"))
                            {
                                currentObj = singleHit.collider.gameObject;

                                if (currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().blockName == "Empty Array")
                                {
                                    currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().dataElementCount -= 1;
                                }
                                
                            }

                            if (singleHit.collider.CompareTag("Title"))
                            {
                                GameObject dataTypes = singleHit.collider.transform.parent.Find("Data Type Menu").gameObject;

                                if (dataTypes.activeSelf)
                                {
                                    dataTypes.SetActive(false);
                                } else
                                {
                                    dataTypes.SetActive(true);
                                }
                                
                            }

                            if (singleHit.collider.CompareTag("Data Type"))
                            {
                                GameObject hitObject = singleHit.collider.gameObject;

                                if (hitObject.name == "Character")
                                {
                                    hitObject.transform.parent.parent.Find("Character").gameObject.SetActive(true);
                                    hitObject.transform.parent.parent.Find("Number").gameObject.SetActive(false);
                                    hitObject.transform.parent.parent.Find("Boolean").gameObject.SetActive(false);
                                    hitObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>start</color> = <color=#F88379>0</color>%<color=yellow>end</color> = <color=#F88379>0</color>%while <color=yellow>start</color> < <color=yellow>end</color>%      <color=green>Character</color> temp = Array[<color=yellow>start</color>]%      Array[<color=yellow>start</color>] = Array[<color=yellow>end</color>]%      Array[<color=yellow>end</color>] = temp%      <color=yellow>start</color> = <color=yellow>start</color> + 1%      <color=yellow>end</color> = <color=yellow>end</color> - 1%end while";


                                }
                                else if (hitObject.name == "Number")
                                {
                                    hitObject.transform.parent.parent.Find("Number").gameObject.SetActive(true);
                                    hitObject.transform.parent.parent.Find("Character").gameObject.SetActive(false);
                                    hitObject.transform.parent.parent.Find("Boolean").gameObject.SetActive(false);
                                    hitObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>start</color> = <color=#F88379>0</color>%<color=yellow>end</color> = <color=#F88379>0</color>%while <color=yellow>start</color> < <color=yellow>end</color>%      <color=green>Number</color> temp = Array[<color=yellow>start</color>]%      Array[<color=yellow>start</color>] = Array[<color=yellow>end</color>]%      Array[<color=yellow>end</color>] = temp%      <color=yellow>start</color> = <color=yellow>start</color> + 1%      <color=yellow>end</color> = <color=yellow>end</color> - 1%end while";

                                }
                                else if (hitObject.name == "Boolean")
                                {
                                    hitObject.transform.parent.parent.Find("Boolean").gameObject.SetActive(true);
                                    hitObject.transform.parent.parent.Find("Number").gameObject.SetActive(false);
                                    hitObject.transform.parent.parent.Find("Character").gameObject.SetActive(false);
                                    hitObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>start</color> = <color=#F88379>0</color>%<color=yellow>end</color> = <color=#F88379>0</color>%while <color=yellow>start</color> < <color=yellow>end</color>%      <color=green>Boolean</color> temp = Array[<color=yellow>start</color>]%      Array[<color=yellow>start</color>] = Array[<color=yellow>end</color>]%      Array[<color=yellow>end</color>] = temp%      <color=yellow>start</color> = <color=yellow>start</color> + 1%      <color=yellow>end</color> = <color=yellow>end</color> - 1%end while";
                                }

                                hitObject.transform.parent.gameObject.SetActive(false);

                                GameObject codePosition = hitObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;
                                string pseudoText = hitObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                                string[] pseudoSubstrings = pseudoText.Split('%');


                                StartCoroutine(TypingMultipleCode(pseudoSubstrings, codePosition));

                            }
                        }
                    }
                    else if (allhits.Length == 1)
                    {

                        if (allhits[0].collider.CompareTag("Inventory"))
                        {
                            currentObj = allhits[0].collider.gameObject;
                        }

                        
                        if (allhits[0].collider.CompareTag("Data Type"))
                        {

                            GameObject hitObject = allhits[0].collider.gameObject;

                            if (hitObject.name == "Character")
                            {
                                hitObject.transform.parent.parent.Find("Array : Character").gameObject.SetActive(true);
                                hitObject.transform.parent.parent.Find("Array : Number").gameObject.SetActive(false);
                                hitObject.transform.parent.parent.Find("Array : Boolean").gameObject.SetActive(false);
                                hitObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().dataType = "Character";

                            } else if (hitObject.name == "Number")
                            {
                                hitObject.transform.parent.parent.Find("Array : Number").gameObject.SetActive(true);
                                hitObject.transform.parent.parent.Find("Array : Character").gameObject.SetActive(false);
                                hitObject.transform.parent.parent.Find("Array : Boolean").gameObject.SetActive(false);
                                hitObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().dataType = "Number";

                            } else if (hitObject.name == "Boolean")
                            {
                                hitObject.transform.parent.parent.Find("Array : Boolean").gameObject.SetActive(true);
                                hitObject.transform.parent.parent.Find("Array : Number").gameObject.SetActive(false);
                                hitObject.transform.parent.parent.Find("Array : Character").gameObject.SetActive(false);
                                hitObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().dataType = "Boolean";
                            }

                            hitObject.transform.parent.parent.Find("Title").gameObject.SetActive(false);
                            hitObject.transform.parent.gameObject.SetActive(false);

                            //update the pseudo code in the editor
                            string updatedText = "<color=green>" + hitObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().dataType + "</color> " + hitObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            GameObject codePosition = hitObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement.transform.Find("Code").gameObject;
                            StartCoroutine(TypingCode(updatedText, codePosition));

                        }

                    }

                    if (currentObj != null)
                    {
                        startPosX = mousePos.x - currentObj.transform.position.x;
                        startPosY = mousePos.y - currentObj.transform.position.y;
                    }

                }

            }
        }

        if (Input.GetMouseButton(0))
        {

            if (currentObj != null)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                if (currentObj.CompareTag("Data"))
                {
                    if (currentObj.GetComponent<DataBlock>().snapped == true)
                    {
                        //Execute as a data block is being removed from the print function
                        if (currentObj.transform.parent.gameObject.name == "Print - Start Point")
                        {
                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint = "s";

                            string endValue = (currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint == "") ? "e" : currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint;

                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "for index=<color=yellow>" + currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint + "</color> to <color=yellow>" + endValue + "</color>%	      print Array[index]%end for";
                            GameObject codeObject = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        //Execute as a data block is being removed from the print function's end point
                        if (currentObj.transform.parent.gameObject.name == "Print - End Point")
                        {
                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint = "e";

                            string startValue = (currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint == "") ? "s" : currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint;

                            currentObj.transform.parent.parent.parent.GetComponent<ArrayBlock>().pseudoCode = "for index=<color=yellow>" + startValue + "</color> to <color=yellow>" + currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint + "</color>%	      print Array[index]%end for";
                            GameObject codeObject = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }


                        //Execute as a data block is being removed from the reverse function's start parameter
                        if (currentObj.transform.parent.gameObject.name == "Reverse - Start Point")
                        {
                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint = "0";

                            string endValue = (currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint == "") ? "0" : currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint;

                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>start</color> = <color=#F88379>" + currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint + "</color>%<color=yellow>end</color> = <color=#F88379>" + endValue + "</color>%while <color=yellow>start</color> < <color=yellow>end</color>%      <color=green>Number</color> temp = Array[<color=yellow>start</color>]%      Array[<color=yellow>start</color>] = Array[<color=yellow>end</color>]%      Array[<color=yellow>end</color>] = temp%      <color=yellow>start</color> = <color=yellow>start</color> + 1%      <color=yellow>end</color> = <color=yellow>end</color> - 1%end while";
                            GameObject codeObject = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        //Execute as a data block is being removed from the reverse function's end parameter
                        if (currentObj.transform.parent.gameObject.name == "Reverse - End Point")
                        {
                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint = "0";

                            string startValue = (currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint == "") ? "0" : currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint;

                            currentObj.transform.parent.parent.parent.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>start</color> = <color=#F88379>" + startValue + "</color>%<color=yellow>end</color> = <color=#F88379>" + currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint + "</color>%while <color=yellow>start</color> < <color=yellow>end</color>%      <color=green>Number</color> temp = Array[<color=yellow>start</color>]%      Array[<color=yellow>start</color>] = Array[<color=yellow>end</color>]%      Array[<color=yellow>end</color>] = temp%      <color=yellow>start</color> = <color=yellow>start</color> + 1%      <color=yellow>end</color> = <color=yellow>end</color> - 1%end while";
                            GameObject codeObject = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }


                        //Execute as a data block is being removed from the insertion function's position value
                        if (currentObj.transform.parent.gameObject.name == "Position Point")
                        {
                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().positionPoint = "0";

                            string elementValue = (currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().elementPoint == "") ? "0" : currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().elementPoint;

                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>pos</color> = <color=#F88379>" + currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().positionPoint + "</color>%<color=yellow>element</color> = <color=#F88379>" + elementValue + "</color>%for i = 0 to <color=yellow>pos</color> - 1%      newArray[i] = array[i]%end for%newArray[<color=yellow>pos</color>] = <color=yellow>element</color>%for i = <color=yellow>pos</color> + 1 to size(newArray) - 1%      newArray[i] = array[i-1]%end for";
                            GameObject codeObject = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        //Execute as a data block is being removed from the insertion function's element value
                        if (currentObj.transform.parent.gameObject.name == "Value Point")
                        {
                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().elementPoint = "0";

                            string positionValue = (currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().positionPoint == "") ? "0" : currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().positionPoint;

                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>pos</color> = <color=#F88379>" + positionValue + "</color>%<color=yellow>element</color> = <color=#F88379>" + currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().elementPoint + "</color>%for i = 0 to <color=yellow>pos</color> - 1%      newArray[i] = array[i]%end for%newArray[<color=yellow>pos</color>] = <color=yellow>element</color>%for i = <color=yellow>pos</color> + 1 to size(newArray) - 1%      newArray[i] = array[i-1]%end for";
                            GameObject codeObject = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        //Execute as a data block is being removed from the deletion function's index value
                        if (currentObj.transform.parent.gameObject.name == "Index Point")
                        {
                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().indexPoint = "0";

                            string lengthValue = (currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().lengthPoint == "") ? "0" : currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().lengthPoint;

                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>index</color> = <color=#F88379>" + currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().indexPoint + "</color>%<color=yellow>length</color> = <color=#F88379>" + lengthValue + "</color>%for i = <color=yellow>index</color> to <color=yellow>length</color> - 2%      array[i] = array[i+1]%end for%array[<color=yellow>length</color>-1] = null";
                            GameObject codeObject = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }


                        //Execute as a data block is being removed from the deletion function's length value
                        if (currentObj.transform.parent.gameObject.name == "Length Point")
                        {
                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().lengthPoint = "0";

                            string indexValue = (currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().indexPoint == "") ? "0" : currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().indexPoint;

                            currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>index</color> = <color=#F88379>" + indexValue + "</color>%<color=yellow>length</color> = <color=#F88379>" + currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().lengthPoint + "</color>%for i = <color=yellow>index</color> to <color=yellow>length</color> - 2%      array[i] = array[i+1]%end for%array[<color=yellow>length</color>-1] = null";
                            GameObject codeObject = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = currentObj.transform.parent.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }


                    }

                    currentObj.GetComponent<DataBlock>().snapped = false;
                    currentObj.transform.SetParent(dataParentObj);
                    Destroy(currentObj.GetComponent<DataBlock>().pseudoElement);
                    currentObj.GetComponent<SpriteRenderer>().sortingOrder = 8;
                    Transform dataText = currentObj.transform.Find("a-data");
                    dataText.GetComponent<SpriteRenderer>().sortingOrder = 9;
                } else if (currentObj.CompareTag("Inventory"))
                {
                    HighlightColorBlockLines(true);
                }

                currentObj.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<ArrayBlock>().inWorkspace == true)
            {
                HighlightColorBlockLines(false);

                if (currentObj.layer != workspaceLayer)
                {

                    TrackSnapPoints(currentObj);
                    TrackLinePoints(currentObj);
                    ChangeBlockLayer(currentObj.transform, "Workspace");
                    levelManager.blockCount += 1;


                    if (currentObj.GetComponent<ArrayBlock>().blockName == "Empty Array")
                    {
                        GameObject codeObject = Instantiate(codeArrayInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                        codeObject.transform.SetParent(codeParent.transform);

                        currentObj.GetComponent<ArrayBlock>().pseudoElement = codeObject;

                        GameObject code = codeObject.transform.Find("Code").gameObject;
                        string pseudoText = "<color=green>" + currentObj.GetComponent<ArrayBlock>().dataType + "</color> " + currentObj.GetComponent<ArrayBlock>().pseudoCode;
                        //string pseudoText = $"<color=green>{currentObj.GetComponent<ArrayBlock>().dataType}</color> {currentObj.GetComponent<ArrayBlock>().pseudoCode}";

                        StartCoroutine(TypingCode(pseudoText, code));

                    }

                    if (currentObj.GetComponent<ArrayBlock>().blockName == "Long Array")
                    {
                        GameObject codeObject = Instantiate(codeArrayInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                        codeObject.transform.SetParent(codeParent.transform);

                        currentObj.GetComponent<ArrayBlock>().pseudoElement = codeObject;

                        GameObject code = codeObject.transform.Find("Code").gameObject;
                        string pseudoText = "<color=green>" + currentObj.GetComponent<ArrayBlock>().dataType + "</color> " + currentObj.GetComponent<ArrayBlock>().pseudoCode;

                        StartCoroutine(TypingCode(pseudoText, code));

                    }

                    if (currentObj.GetComponent<ArrayBlock>().blockName == "Array Print")
                    {
                        GameObject codeObject = Instantiate(codePrintInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                        codeObject.transform.SetParent(codeParent.transform);

                        string pseudoText = currentObj.GetComponent<ArrayBlock>().pseudoCode;
                        string[] pseudoSubstrings = pseudoText.Split('%');

                        currentObj.GetComponent<ArrayBlock>().pseudoElement = codeObject;

                        StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                    }

                    if (currentObj.GetComponent<ArrayBlock>().blockName == "Array Reverse")
                    {
                        GameObject codeObject = Instantiate(codeReverseInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                        codeObject.transform.SetParent(codeParent.transform);

                        string pseudoText = currentObj.GetComponent<ArrayBlock>().pseudoCode;
                        string[] pseudoSubstrings = pseudoText.Split('%');

                        currentObj.GetComponent<ArrayBlock>().pseudoElement = codeObject;

                        StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));
                    }

                    if (currentObj.GetComponent<ArrayBlock>().blockName == "Array Insertion")
                    {
                        GameObject codeObject = Instantiate(codeInsertionInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                        codeObject.transform.SetParent(codeParent.transform);

                        string pseudoText = currentObj.GetComponent<ArrayBlock>().pseudoCode;
                        string[] pseudoSubstrings = pseudoText.Split('%');

                        currentObj.GetComponent<ArrayBlock>().pseudoElement = codeObject;

                        StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));
                    }

                    if (currentObj.GetComponent<ArrayBlock>().blockName == "Array Deletion")
                    {
                        GameObject codeObject = Instantiate(codeDeletionInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                        codeObject.transform.SetParent(codeParent.transform);

                        string pseudoText = currentObj.GetComponent<ArrayBlock>().pseudoCode;
                        string[] pseudoSubstrings = pseudoText.Split('%');

                        currentObj.GetComponent<ArrayBlock>().pseudoElement = codeObject;

                        StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));
                    }

                }

                currentObj = null;

            }
            else if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<ArrayBlock>().inWorkspace == false)
            {
                DestroyBlocks(currentObj);

            }
            else if (currentObj != null && currentObj.CompareTag("Data"))
            {

                float _snapRadius = currentObj.GetComponent<DataBlock>().snapRadius;

                currentObj.GetComponent<DataBlock>().snapped = false;

                for (int i = 0; i < levelManager.correctForms.Count; i++)
                {
                    if ((Mathf.Abs(currentObj.transform.position.x - levelManager.correctForms[i].transform.position.x) <= _snapRadius &&
                    Mathf.Abs(currentObj.transform.position.y - levelManager.correctForms[i].transform.position.y) <= _snapRadius) &&
                    levelManager.correctForms[i].transform.childCount == 0)
                    {
                        currentObj.transform.position = new Vector3(levelManager.correctForms[i].transform.position.x, levelManager.correctForms[i].transform.position.y, 0f);
                        currentObj.GetComponent<DataBlock>().snapped = true;

                        ChangeBlockLayer(currentObj.transform, "Workspace");

                        //make the data element a child of the snapped point
                        currentObj.transform.SetParent(levelManager.correctForms[i].transform);

                        currentObj.transform.localScale = new Vector3(1f, 1f, 0f);

                        //Execute only if data is snapped into an array snap point
                        if (levelManager.correctForms[i].name == "0" || levelManager.correctForms[i].name == "1" || levelManager.correctForms[i].name == "2" || levelManager.correctForms[i].name == "3")
                        {
                            //Initializing data into the array
                            GameObject codeObject = Instantiate(codeDataInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                            codeObject.transform.SetParent(codeParent.transform);

                            currentObj.GetComponent<DataBlock>().pseudoElement = codeObject;

                            GameObject code = codeObject.transform.Find("Code").gameObject;
                            string pseudoText = currentObj.GetComponent<DataBlock>().dataValue;

                            string fullPseudoText = "Array[" + levelManager.correctForms[i].name + "]" + " = " + pseudoText;

                            StartCoroutine(TypingCode(fullPseudoText, code));

                            //To track elements count of an Array object
                            if (levelManager.correctForms[i].gameObject.transform.parent.parent.gameObject.GetComponent<ArrayBlock>().blockName == "Empty Array")
                            {
                                levelManager.correctForms[i].gameObject.transform.parent.parent.GetComponent<ArrayBlock>().dataElementCount += 1;
                            }
                                
                        }


                        //Excute only if data is snapped into print function's start snap point
                        if (levelManager.correctForms[i].name == "Print - Start Point")
                        {
                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint = currentObj.GetComponent<DataBlock>().dataValue;
                            string endValue = (levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint == "") ? "e" : levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint;

                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "for index=<color=yellow>" + levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint + "</color> to <color=yellow>" + endValue + "</color>%	      print Array[index]%end for";
                            GameObject codeObject = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        //Excute only if data is snapped into print function's end snap point
                        if (levelManager.correctForms[i].name == "Print - End Point")
                        {
                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint = currentObj.GetComponent<DataBlock>().dataValue;
                            string startValue = (levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint == "") ? "s" : levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint;

                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "for index=<color=yellow>" + startValue + "</color> to <color=yellow>" + levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint + "</color>%	      print Array[index]%end for";
                            GameObject codeObject = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        //Excute only if data is snapped into reverse function's start snap point
                        if (levelManager.correctForms[i].name == "Reverse - Start Point")
                        {
                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint = currentObj.GetComponent<DataBlock>().dataValue;
                            string endValue = (levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint == "") ? "0" : levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint;

                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>start</color> = <color=#F88379>" + levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint + "</color>%<color=yellow>end</color> = <color=#F88379>" + endValue + "</color>%while <color=yellow>start</color> < <color=yellow>end</color>%      <color=green>Number</color> temp = Array[<color=yellow>start</color>]%      Array[<color=yellow>start</color>] = Array[<color=yellow>end</color>]%      Array[<color=yellow>end</color>] = temp%      <color=yellow>start</color> = <color=yellow>start</color> + 1%      <color=yellow>end</color> = <color=yellow>end</color> - 1%end while";
                            GameObject codeObject = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        //Excute only if data is snapped into reverse function's end snap point
                        if (levelManager.correctForms[i].name == "Reverse - End Point")
                        {
                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint = currentObj.GetComponent<DataBlock>().dataValue;
                            string startValue = (levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint == "") ? "0" : levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().startPoint;

                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>start</color> = <color=#F88379>" + startValue + "</color>%<color=yellow>end</color> = <color=#F88379>" + levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().endPoint + "</color>%while <color=yellow>start</color> < <color=yellow>end</color>%      <color=green>Number</color> temp = Array[<color=yellow>start</color>]%      Array[<color=yellow>start</color>] = Array[<color=yellow>end</color>]%      Array[<color=yellow>end</color>] = temp%      <color=yellow>start</color> = <color=yellow>start</color> + 1%      <color=yellow>end</color> = <color=yellow>end</color> - 1%end while";
                            GameObject codeObject = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        //Excute only if data is snapped into insertion function's position snap point
                        if (levelManager.correctForms[i].name == "Position Point")
                        {
                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().positionPoint = currentObj.GetComponent<DataBlock>().dataValue;
                            string elementValue = (levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().elementPoint == "") ? "0" : levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().elementPoint;

                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>pos</color> = <color=#F88379>" + levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().positionPoint + "</color>%<color=yellow>element</color> = <color=#F88379>" + elementValue + "</color>%for i = 0 to <color=yellow>pos</color> - 1%      newArray[i] = array[i]%end for%newArray[<color=yellow>pos</color>] = <color=yellow>element</color>%for i = <color=yellow>pos</color> + 1 to size(newArray) - 1%      newArray[i] = array[i-1]%end for";
                            GameObject codeObject = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        //Excute only if data is snapped into insertion function's element snap point
                        if (levelManager.correctForms[i].name == "Value Point")
                        {
                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().elementPoint = currentObj.GetComponent<DataBlock>().dataValue;
                            string positionValue = (levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().positionPoint == "") ? "0" : levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().positionPoint;

                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>pos</color> = <color=#F88379>" + positionValue + "</color>%<color=yellow>element</color> = <color=#F88379>" + levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().elementPoint + "</color>%for i = 0 to <color=yellow>pos</color> - 1%      newArray[i] = array[i]%end for%newArray[<color=yellow>pos</color>] = <color=yellow>element</color>%for i = <color=yellow>pos</color> + 1 to size(newArray) - 1%      newArray[i] = array[i-1]%end for";
                            GameObject codeObject = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        //Excute only if data is snapped into deletion function's index point
                        if (levelManager.correctForms[i].name == "Index Point")
                        {
                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().indexPoint = currentObj.GetComponent<DataBlock>().dataValue;
                            string lengthValue = (levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().lengthPoint == "") ? "0" : levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().lengthPoint;

                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>index</color> = <color=#F88379>" + levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().indexPoint + "</color>%<color=yellow>length</color> = <color=#F88379>" + lengthValue + "</color>%for i = <color=yellow>index</color> to <color=yellow>length</color> - 2%      array[i] = array[i+1]%end for%array[<color=yellow>length</color>-1] = null";
                            GameObject codeObject = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        //Excute only if data is snapped into deletion function's length point
                        if (levelManager.correctForms[i].name == "Length Point")
                        {
                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().lengthPoint = currentObj.GetComponent<DataBlock>().dataValue;
                            string indexValue = (levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().indexPoint == "") ? "0" : levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().indexPoint;

                            levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>index</color> = <color=#F88379>" + indexValue + "</color>%<color=yellow>length</color> = <color=#F88379>" + levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().lengthPoint + "</color>%for i = <color=yellow>index</color> to <color=yellow>length</color> - 2%      array[i] = array[i+1]%end for%array[<color=yellow>length</color>-1] = null";
                            GameObject codeObject = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = levelManager.correctForms[i].transform.parent.parent.gameObject.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');


                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));

                        }

                        break;
                    }
                }

                if (currentObj.GetComponent<DataBlock>().snapped == false)
                {
                    Vector3 currentResetPos = currentObj.GetComponent<DataBlock>().resetPosition;
                    currentObj.transform.position = new Vector3(currentResetPos.x, currentResetPos.y, currentResetPos.z);
                    

                    currentObj.transform.SetParent(dataParentObj);
                    ChangeBlockLayer(currentObj.transform, "Data");
                    currentObj.transform.localScale = currentObj.GetComponent<DataBlock>().originalScale;

                    Destroy(currentObj.GetComponent<DataBlock>().pseudoElement);

                    currentObj.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    Transform dataText = currentObj.transform.Find("a-data");
                    dataText.GetComponent<SpriteRenderer>().sortingOrder = 4;
                }

                currentObj = null;
            }

        }

    }

    //Used to add all snap points of an array block to the level managers list when the array first placed in workspace
    private void TrackSnapPoints(GameObject parentObj)
    {
        Transform snapPointsListObj = parentObj.transform.Find("Snap Points");

        if (snapPointsListObj != null)
        {
            Transform[] snapPointsList = snapPointsListObj.GetComponentsInChildren<Transform>(includeInactive: false);

            foreach (Transform snapPoint in snapPointsList)
            {
                if (snapPoint != snapPointsListObj)
                {
                    levelManager.correctForms.Add(snapPoint.gameObject);
                }

            }
        }
    }

    private void TrackLinePoints(GameObject parentObj)
    {

        Transform linePoints = parentObj.transform.Find("Line Points");

        if (linePoints != null)
        {
            Transform endPoint = linePoints.Find("Line End");
            if (endPoint != null)
            {
                levelManager.lineEndPoints.Add(endPoint);
            }

            //for adding parameter array end point
            Transform parameterArray = linePoints.Find("Parameter Array");
            if (parameterArray != null)
            {
                levelManager.lineEndPoints.Add(parameterArray);
            }

        }

    }

    //To change the layer of each objects when they are being dragged from one camera view to another
    private void ChangeBlockLayer(Transform currentObj, string layerName)
    {
        if (currentObj != null)
        {
            currentObj.gameObject.layer = LayerMask.NameToLayer(layerName);
        }

        for (int i = 0; i < currentObj.childCount; i++)
        {
            Transform childTransform = currentObj.GetChild(i);
            ChangeBlockLayer(childTransform, layerName);
        }
    }

    //Have to delete both the block and its snap points that was assigned to the level manager
    private void DestroyBlocks(GameObject currentObj)
    {
        Debug.Log("Block Destroed");
        Transform snapPointsListObj = currentObj.transform.Find("Snap Points");
        Transform linePointsObj = currentObj.transform.Find("Line Points");
        int deletedCount = 0;
        int deletedLines = 0;

        if (snapPointsListObj != null)
        {
            Transform[] snapPointsList = snapPointsListObj.GetComponentsInChildren<Transform>(includeInactive: false);


            for (int i = 0; i < snapPointsList.Length; i++)
            {
                for (int j = 0; j < levelManager.correctForms.Count; j++)
                {
                    if (snapPointsList[i].gameObject == levelManager.correctForms[j])
                    {
                        levelManager.correctForms.RemoveAt(j);
                        deletedCount += 1;
                    }
                }
            }
        }

        if (linePointsObj != null)
        {
            Transform[] linePointsList = linePointsObj.GetComponentsInChildren<Transform>(includeInactive: false);

            for (int i = 0; i < linePointsList.Length; i++)
            {
                for (int j = 0; j < levelManager.lineEndPoints.Count; j++)
                {
                    if (linePointsList[i] == levelManager.lineEndPoints[j])
                    {
                        levelManager.lineEndPoints.RemoveAt(j);
                        deletedLines += 1;
                    }
                }
            }
        }


        levelManager.blockCount -= 1;
        Destroy(currentObj.GetComponent<ArrayBlock>().pseudoElement);
        Destroy(currentObj);
    }

    private IEnumerator TypingCode(string codeText, GameObject code)
    {
        code.GetComponent<TMP_Text>().text = "";

        for (int i=0; i<codeText.Length; i++)
        {

            if (codeText[i] == '<') {

                do
                {
                    code.GetComponent<TMP_Text>().text += codeText[i];
                    i += 1;                    
                }
                while (codeText[i] != '>'); 

            }

            code.GetComponent<TMP_Text>().text += codeText[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public IEnumerator TypingMultipleCode (string[] pseudoSubstrings, GameObject codeObject)
    {
        //Checks whether the code instance object has been destroyed while the pseudo code is being printed to the editor
        if (codeObject == null)
        {
            yield break;
        }

        int codeInstanceLength = codeObject.transform.childCount;
        
        for (int i = 0; i < codeInstanceLength; i++)
        {
            if (codeObject == null)
            {
                yield break;
            }
            
            GameObject tempObject = codeObject.transform.GetChild(i).gameObject;

            if (tempObject == null)
            {
                yield break;
            }
            
            if (tempObject.name != "Code")
            {
                continue;
            }
            else
            {
                tempObject.GetComponent<TMP_Text>().text = "";

                for (int j=0; j < pseudoSubstrings[i].Length; j++)
                {

                    if (pseudoSubstrings[i][j] == '<' && pseudoSubstrings[i][j+1] != ' ')
                    {

                        do
                        {
                            if (tempObject == null)
                            {
                                yield break;
                            }
                            
                            tempObject.GetComponent<TMP_Text>().text += pseudoSubstrings[i][j];
                            j += 1;
                        }
                        while (pseudoSubstrings[i][j] != '>');

                    }

                    if (tempObject == null)
                    {
                        yield break;
                    }
 
                    tempObject.GetComponent<TMP_Text>().text += pseudoSubstrings[i][j];

                    
                    yield return new WaitForSeconds(0.03f);
                }
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //Hightlights the pseudo code when player drags it when it is already in workspace
    private void HighlightColorBlockLines(bool visibility)
    {
        GameObject codeInstance = currentObj.GetComponent<ArrayBlock>().pseudoElement;

        if (codeInstance != null)
        {
            GameObject image = codeInstance.transform.Find("Image").gameObject;
            if (image != null)
            {
                image.SetActive(visibility);
            }
        } 
        
    }

}
