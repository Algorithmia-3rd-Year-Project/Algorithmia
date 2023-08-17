using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private GameObject codePrintInstance;

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
                    currentObj = Instantiate(arrayblocksList.blockList["Empty Array Block"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

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
                            }
                        }
                    }
                    else if (allhits.Length == 1)
                    {

                        if (allhits[0].collider.CompareTag("Inventory"))
                        {
                            currentObj = allhits[0].collider.gameObject;
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
                    currentObj.GetComponent<DataBlock>().snapped = false;
                    currentObj.transform.SetParent(dataParentObj);
                    Destroy(currentObj.GetComponent<DataBlock>().pseudoElement);
                    currentObj.GetComponent<SpriteRenderer>().sortingOrder = 8;
                    Transform dataText = currentObj.transform.Find("a-data");
                    dataText.GetComponent<SpriteRenderer>().sortingOrder = 9;
                }


                currentObj.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<ArrayBlock>().inWorkspace == true)
            {

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
                        string pseudoText = currentObj.GetComponent<ArrayBlock>().pseudoCode;

                        StartCoroutine(TypingCode(pseudoText, code));

                        
                    }

                    if (currentObj.GetComponent<ArrayBlock>().blockName == "Array Print")
                    {
                        GameObject codeObject = Instantiate(codePrintInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                        codeObject.transform.SetParent(codeParent.transform);

                        string pseudoText = currentObj.GetComponent<ArrayBlock>().pseudoCode;
                        string[] pseudoSubstrings = pseudoText.Split('_');

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

                        //Initializing data into the array
                        GameObject codeObject = Instantiate(codeArrayInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                        codeObject.transform.SetParent(codeParent.transform);

                        currentObj.GetComponent<DataBlock>().pseudoElement = codeObject;

                        GameObject code = codeObject.transform.Find("Code").gameObject;
                        string pseudoText = currentObj.GetComponent<DataBlock>().dataValue;

                        string fullPseudoText = "Array[" + i.ToString() + "]" + " = " + pseudoText;

                        StartCoroutine(TypingCode(fullPseudoText, code));


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
        foreach (char letter in codeText)
        {
            code.GetComponent<TMP_Text>().text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private IEnumerator TypingMultipleCode (string[] pseudoSubstrings, GameObject codeObject)
    {
        for (int i = 0; i < codeObject.transform.childCount; i++)
        {
            GameObject tempObject = codeObject.transform.GetChild(i).gameObject;
            if (tempObject.name != "Code")
            {
                continue;
            }
            else
            {
                foreach (char letter in pseudoSubstrings[i])
                {
                    tempObject.GetComponent<TMP_Text>().text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

}
