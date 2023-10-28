using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class Linkedlistblockmanager : MonoBehaviour
{
    [SerializeField]
    private List<string> blockNames;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject linkedListPrefab;

    [SerializeField]
    GameObject currentObj;

    [SerializeField]
    private LayerMask anotherLayer;

    [SerializeField]
    private LayerMask dataLayer;

    [SerializeField]
    private Transform dataParentObj;

    [SerializeField]
    private GameObject codeDataInstance;

    [SerializeField]
    private GameObject codeParent;

    private float startPosX;

    private float startPosY;

    private int workspaceLayer;

    private ArrayBlockList linkedListblocksList;

    [SerializeField]
    private LinkedListLevelManager levelManager;

    [SerializeField]
    private bool setTriggers;

    [SerializeField]
    private float typingSpeed;

    [SerializeField]
    private GameObject codeNodeInstance;

    [SerializeField]
    private GameObject codeHeadNodeInstance;

    [SerializeField] private ScrollRect scrollRect;

    [SerializeField]
    private GameObject codeInsertInstance;


    // Start is called before the first frame update
    void Start()
    {
        workspaceLayer = LayerMask.NameToLayer("Workspace");
        linkedListblocksList = GetComponent<ArrayBlockList>();
        currentObj = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);


            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero, 20f, layerMask);

            if (hit.collider != null)
            {
                if (hit.collider.name == "LL Normal node")
                {
                    currentObj = Instantiate(linkedListblocksList.blockList["LL Normal node"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
                else if(hit.collider.name == "Insert Block")
                {
                    currentObj = Instantiate(linkedListblocksList.blockList["Insert Block"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
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
                    currentObj.GetComponent<SpriteRenderer>().sortingOrder = 8;
                    Transform dataText = currentObj.transform.Find("a-data");
                    dataText.GetComponent<SpriteRenderer>().sortingOrder = 9;
                }


                currentObj.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<LinkedListBlock>().inWorkspace == true)
            {
                if (currentObj.name == "Insert Block(Clone)")
                {
                    float _snapRadius = 1f;

                    currentObj.GetComponent<LinkedListBlock>().snapped = false;

                    for (int i = 0; i < levelManager.functionSnapPoints.Count; i++)
                    {
                        if ((Mathf.Abs(currentObj.transform.position.x -
                                       levelManager.functionSnapPoints[i].transform.position.x) <= _snapRadius &&
                             Mathf.Abs(currentObj.transform.position.y -
                                       levelManager.functionSnapPoints[i].transform.position.y) <= _snapRadius) &&
                            levelManager.functionSnapPoints[i].transform.childCount == 0)
                        {
                           // Debug.Log(levelManager.functionSnapPoints[i].name + " " + _snapRadius);
                            currentObj.transform.position = new Vector3(levelManager.functionSnapPoints[i].transform.position.x, levelManager.functionSnapPoints[i].transform.position.y, 0f);
                            
                            currentObj.GetComponent<LinkedListBlock>().snapped = true;


                            //ChangeBlockLayer(currentObj.transform, "Workspace");

                            //make the data element a child of the snapped point
                            currentObj.transform.SetParent(levelManager.functionSnapPoints[i]);

                            for(int j = 0; j<levelManager.fixLines.Count; j++)
                            {
                                if (levelManager.functionSnapPoints[i].name == levelManager.fixLines[j].name)
                                {
                                    levelManager.fixLines[j].SetActive(false);
                                    levelManager.triggerNodes[j].SetActive(true);
                                    levelManager.triggerNodes[j].GetComponent<LinkedListBlock>().triggerNode = currentObj;
                                    break;
                                }
                            }
                            GameObject codeObject = Instantiate(codeInsertInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                            codeObject.transform.SetParent(codeParent.transform);

                            string pseudoText = currentObj.GetComponent<LinkedListBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');

                            currentObj.GetComponent<LinkedListBlock>().pseudoElement = codeObject;

                            StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));
                               

                            //currentObj.transform.localScale = new Vector3(1f, 1f, 0f);

                            //currentObj.transform.SetParent(currentObj.transform);
                        }
                    }
                    if (currentObj.GetComponent<LinkedListBlock>().snapped == false)
                    {
                        DestroyBlocks(currentObj);
                    }
                }
                else
                {
                    if (currentObj.layer != workspaceLayer)
                    {
                        TrackSnapPoints(currentObj);
                        TrackLinePoints(currentObj);
                        ChangeBlockLayer(currentObj.transform, "Workspace");
                        ActivateLineSnapPoints(currentObj);
                        levelManager.blockCount += 1;

                        currentObj.GetComponent<LinkedListBlock>().addedBlock = true;

                        if (currentObj.GetComponent<LinkedListBlock>().blockName == "LL Normal Node")
                        {
                            if (levelManager.blockCount == 1)
                            {
                                GameObject codeObject = Instantiate(codeHeadNodeInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                                codeObject.transform.SetParent(codeParent.transform);

                                string pseudoText = currentObj.GetComponent<LinkedListBlock>().pseudoCode;
                                string[] pseudoSubstrings = pseudoText.Split('%');

                                currentObj.GetComponent<LinkedListBlock>().pseudoElement = codeObject;

                                StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));
                            }
                            else
                            {
                                GameObject codeObject = Instantiate(codeNodeInstance, new Vector3(codeParent.transform.position.x, codeParent.transform.position.y, 0f), Quaternion.identity);
                                codeObject.transform.SetParent(codeParent.transform);

                                string variableName = currentObj.GetComponent<LinkedListBlock>().variableName + levelManager.blockCount;
                                string pseudoText = "Node *" + variableName + " = NULL%" + variableName + " = allocateMemory()";
                                string[] pseudoSubstrings = pseudoText.Split('%');

                                currentObj.GetComponent<LinkedListBlock>().pseudoElement = codeObject;

                                StartCoroutine(TypingMultipleCode(pseudoSubstrings, codeObject));
                            }
                        }
                    }
                }
                currentObj = null;


                /* if (setTriggers)
                 {
                     if (levelManager.additionalSnapPositions.Count > 0 && (Mathf.Abs(currentObj.transform.position.x - levelManager.additionalSnapPositions[0].transform.position.x) <= 0.5f &&
                     Mathf.Abs(currentObj.transform.position.y - levelManager.additionalSnapPositions[0].transform.position.y) <= 0.5f))
                     {
                         currentObj.transform.position = new Vector3(levelManager.additionalSnapPositions[0].transform.position.x, levelManager.additionalSnapPositions[0].transform.position.y, 0f);
                         currentObj = null;
                     }
                     else
                     {
                         DestroyBlocks(currentObj);
                     }
                 }
                 else
                 {
                     currentObj = null;
                 }
                 // }*/



            }
            else if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<LinkedListBlock>().inWorkspace == false)
            {
                DestroyBlocks(currentObj);

            }
            else if (currentObj != null && currentObj.CompareTag("Data"))
            {

                float _snapRadius = currentObj.GetComponent<LLDataBlock>().snapRadius;

                currentObj.GetComponent<LLDataBlock>().snapped = false;

                for (int i = 0; i < levelManager.correctForms.Count; i++)
                {
                    if ((Mathf.Abs(currentObj.transform.position.x -
                                   levelManager.correctForms[i].transform.position.x) <= _snapRadius &&
                         Mathf.Abs(currentObj.transform.position.y -
                                   levelManager.correctForms[i].transform.position.y) <= _snapRadius) &&
                        levelManager.correctForms[i].transform.childCount == 0)
                    {
                        Debug.Log(levelManager.correctForms[i].name+" "+_snapRadius);
                        currentObj.transform.position = new Vector3(levelManager.correctForms[i].transform.position.x, levelManager.correctForms[i].transform.position.y, 0f);
                        currentObj.GetComponent<LLDataBlock>().snapped = true;

                        ChangeBlockLayer(currentObj.transform, "Workspace");

                        //make the data element a child of the snapped point
                        currentObj.transform.SetParent(levelManager.correctForms[i].transform);

                        currentObj.transform.localScale = new Vector3(1f, 1f, 0f);

                        //currentObj.transform.SetParent(currentObj.transform);
                    }

                    
                }
                
                if (currentObj.GetComponent<LLDataBlock>().snapped == false)
                {
                    Vector3 currentResetPos = currentObj.GetComponent<LLDataBlock>().resetPosition;
                    currentObj.transform.position = new Vector3(currentResetPos.x, currentResetPos.y, currentResetPos.z);
                    currentObj.transform.SetParent(dataParentObj);
                    ChangeBlockLayer(currentObj.transform, "Data");
                    currentObj.transform.localScale = currentObj.GetComponent<LLDataBlock>().originalScale;

                    currentObj.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    Transform dataText = currentObj.transform.Find("a-data");
                    dataText.GetComponent<SpriteRenderer>().sortingOrder = 4;


                }

                currentObj = null;
            }
        }
    }

        private void TrackSnapPoints(GameObject parentObj)
        {
            Transform snapPointsListObj = parentObj.transform.Find("Snap Points");
            Debug.Log(snapPointsListObj);

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
        //save all the end points of nodes in an array 

            Transform linePoints = parentObj.transform.Find("Line End Points");

            if (linePoints != null)
            {
                for(int i = 0;  i < linePoints.childCount; i++)
                { 
                    Transform lineEndPoint = linePoints.GetChild(i);
                    if(lineEndPoint.name != "Line End Points")
                    {
                        levelManager.lineEndPoints.Add(lineEndPoint);
                    }
                }   
            }
        }

        private void ActivateLineSnapPoints(GameObject currentObj)
        {
            Transform linePoints = currentObj.transform.Find("Line Points");
            Transform lineEndPoints = currentObj.transform.Find("Line End Points");
            foreach (Transform linePoint in linePoints)
            {
                currentObj.gameObject.SetActive(true);
            }
            foreach (Transform lineEndPoint in lineEndPoints)
            {
                currentObj.gameObject.SetActive(true);
            }
        }

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


        if (this.currentObj.GetComponent<LinkedListBlock>().addedBlock == true)
        {
            levelManager.blockCount -= 1;
            //levelManager.blocks.Remove(this.currentObj);
        }

        Destroy(currentObj.GetComponent<LinkedListBlock>().pseudoElement);
        
        Destroy(currentObj);

    }

    public IEnumerator TypingMultipleCode(string[] pseudoSubstrings, GameObject codeObject)
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

                for (int j = 0; j < pseudoSubstrings[i].Length; j++)
                {

                    if (pseudoSubstrings[i][j] == '<' && pseudoSubstrings[i][j + 1] != ' ')
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

        ScrollToBottom();
    }

    private void ScrollToBottom()
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }

}


