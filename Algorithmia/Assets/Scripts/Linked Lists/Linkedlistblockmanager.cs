using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

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
                if (hit.collider.name == "LL Head node")
                {
                    currentObj = Instantiate(linkedListblocksList.blockList["LL Head node"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
                else if (hit.collider.name == "LL Normal node")
                {
                    currentObj = Instantiate(linkedListblocksList.blockList["LL Normal node"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
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
                                //currentObj = singleHit.collider.gameObject;
                                currentObj = Instantiate(linkedListblocksList.blockList["Left Arrow"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
                                startPosX = mousePos.x - currentObj.transform.position.x;
                                startPosY = mousePos.y - currentObj.transform.position.y;
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

                TrackSnapPoints(currentObj);
                TrackLinePoints(currentObj);
                ActivateLineSnapPoints(currentObj);
                ChangeBlockLayer(currentObj.transform, "Workspace");
                levelManager.blockCount += 1;

                if (setTriggers)
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
                // }



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
            //Transform linePointsObj = currentObj.transform.Find("Line Points");
            int deletedCount = 0;
            //int deletedLines = 0;

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
        }
    }


