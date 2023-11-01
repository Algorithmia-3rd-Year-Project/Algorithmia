using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StackBlockManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject stackPrefab;

    [SerializeField]
    GameObject currentObj;

    private float startPosX;

    private float startPosY;

    private int workspaceLayer;

    private ArrayBlockList stackblocksList;

    [SerializeField]
    private StackLevelManager levelManager;

    [SerializeField]
    private LayerMask anotherLayer;

    [SerializeField]
    private LayerMask dataLayer;

    [SerializeField]
    private Transform dataParentObj;


    private void Start()
    {
        workspaceLayer = LayerMask.NameToLayer("Workspace");
        stackblocksList = GetComponent<ArrayBlockList>();
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

                if (hit.collider.name == "Empty Stack Block")
                {
                    currentObj = Instantiate(stackblocksList.blockList["Empty Stack Block"], new Vector3(mousePos.x, mousePos.y - 0.7f, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;

                }
                else if (hit.collider.name == "Push Function")
                {
                    currentObj = Instantiate(stackblocksList.blockList["Push Function"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

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
                                currentObj.transform.parent.parent.parent.gameObject.GetComponent<StackBlock>().dataElementCount -= 1;
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
                else if (currentObj.CompareTag("Inventory"))
                {
                    //
                }

                currentObj.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<StackBlock>().inWorkspace == true)
            {

                if (currentObj.layer != workspaceLayer)
                {
                    ChangeBlockLayer(currentObj.transform, "Workspace");
                    levelManager.blockCount += 1;

                    Debug.Log(currentObj.name);
                    if (currentObj.name == "Push Function(Clone)")
                    {
                        Debug.Log("Push function");
                        TrackSnapPoints(currentObj);
                        TrackLinePoints(currentObj);
                    }

                }

                currentObj = null;

            }
            else if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<StackBlock>().inWorkspace == false)
            {
                DestroyBlocks(currentObj);

            }
            else if (currentObj != null && currentObj.CompareTag("Data"))
            {

                float _snapRadius = currentObj.GetComponent<DataBlock>().snapRadius;

                currentObj.GetComponent<DataBlock>().snapped = false;

                for (int i = 0; i < levelManager.snapPoints.Count; i++)
                {
                    if ((Mathf.Abs(currentObj.transform.position.x - levelManager.snapPoints[i].transform.position.x) <= _snapRadius &&
                    Mathf.Abs(currentObj.transform.position.y - levelManager.snapPoints[i].transform.position.y) <= _snapRadius) &&
                    levelManager.snapPoints[i].transform.childCount == 0)
                    {
                        currentObj.transform.position = new Vector3(levelManager.snapPoints[i].transform.position.x, levelManager.snapPoints[i].transform.position.y, 0f);
                        currentObj.GetComponent<DataBlock>().snapped = true;

                        ChangeBlockLayer(currentObj.transform, "Workspace");

                        //make the data element a child of the snapped point
                        currentObj.transform.SetParent(levelManager.snapPoints[i].transform);

                        currentObj.transform.localScale = new Vector3(0.64f, 0.44f, 1f);

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
        
        levelManager.blockCount -= 1;
        Destroy(currentObj.GetComponent<StackBlock>().pseudoElement);
        Destroy(currentObj);
    }

    //Used to add all snap points of an array block to the level managers list when the array first placed in workspace
    private void TrackSnapPoints(GameObject currentObj)
    {
        Transform snapPointsListObj = currentObj.transform.Find("Snap Points");

        if (snapPointsListObj != null)
        {
            Transform[] snapPointsList = snapPointsListObj.GetComponentsInChildren<Transform>(includeInactive: false);

            foreach (Transform snapPoint in snapPointsList)
            {
                if (snapPoint != snapPointsListObj)
                {
                    levelManager.snapPoints.Add(snapPoint.gameObject);
                }

            }
        }
    }

    private void TrackLinePoints(GameObject currentObj)
    {

        Transform linePoints = currentObj.transform.Find("Line Points");

        if (linePoints != null)
        {
            Transform endPoint = linePoints.Find("Line End");
            if (endPoint != null)
            {
                levelManager.lineEndPoints.Add(endPoint);
            }

        }

    }

}


