using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBlockManager : MonoBehaviour
{

    [SerializeField]
    private LayerMask blockLayer;

    private ArrayBlockList treeblocksList;

    GameObject currentObj;

    private float startPosY;
    private float startPosX;

    [SerializeField]
    private TreeLevelManager levelManager;

    public GameObject machinePrefab;

    public GameObject arrayPrefab;

    [SerializeField]
    private Transform dataParentObj;

    [SerializeField]
    private LayerMask dataLayer;

    private void Start()
    {
        treeblocksList = GetComponent<ArrayBlockList>();
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            //Sending a raycast to inventory objects
            RaycastHit2D inventoryHit = Physics2D.Raycast(mousePos, Vector3.zero, Mathf.Infinity, blockLayer);

            if (inventoryHit.collider != null)
            {
                if (inventoryHit.collider.gameObject.name == "Tree Node")
                {
                    //Create a new tree node
                    currentObj = Instantiate(treeblocksList.blockList["Tree Node"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
                if (inventoryHit.collider.gameObject.name == "Insert Function")
                {
                    //Create a new insert function
                    currentObj = Instantiate(treeblocksList.blockList["Insert Function"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
                if (inventoryHit.collider.gameObject.name == "Pre Order Traversal")
                {
                    //Create a new insert function
                    currentObj = Instantiate(treeblocksList.blockList["Pre Order Traversal"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
                if (inventoryHit.collider.gameObject.name == "In Order Traversal")
                {
                    //Create a new insert function
                    currentObj = Instantiate(treeblocksList.blockList["In Order Traversal"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
                if (inventoryHit.collider.gameObject.name == "Post Order Traversal")
                {
                    //Create a new insert function
                    currentObj = Instantiate(treeblocksList.blockList["Post Order Traversal"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
            }
            else
            {
                //Sending a raycast to data objects
                RaycastHit2D dataHit = Physics2D.Raycast(mousePos, Vector3.zero, Mathf.Infinity, dataLayer);

                if (dataHit.collider != null)
                {
                    currentObj = dataHit.collider.gameObject;

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (currentObj != null)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                //Move the selected game object
                currentObj.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //For inventory objects in workspace
            if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<TreeBlock>().inWorkspace == true && currentObj.name == "Tree Node(Clone)")
            {
                currentObj.GetComponent<TreeBlock>().snapped = false;
                for (int i = 0; i < levelManager.isSnapBlock.Count; i++)
                {
                    //When current object is in trigger of a snap block in the tree
                    if (levelManager.isSnapBlock[i] == true && levelManager.isNodeSnapped[i] == false)
                    {
                        currentObj.transform.position = levelManager.snapPoints[i].transform.position;      //snap
                        levelManager.isSnapBlock[i] = false;
                        levelManager.isNodeSnapped[i] = true;
                        currentObj.GetComponent<TreeBlock>().snapped = true;
                        currentObj.GetComponent<TreeBlock>().snappedLevel = levelManager.snapPoints[i].transform.parent.parent.name;
                        levelManager.snapPoints[i].transform.parent.Find("Shade").gameObject.SetActive(false);      //Hide the shade after the node is snapped
                        ChangeBlockLayer(currentObj.transform, "Workspace");
                        TrackLinePoints(currentObj);

                        //Add data snap point to the list
                        GameObject SnapPoint = currentObj.transform.Find("Snap Point").gameObject;
                        levelManager.dataSnapPoints.Add(SnapPoint);

                        //Display the head text
                        if (currentObj.transform.position == levelManager.snapPoints[0].transform.position)
                        {
                            currentObj.transform.Find("Head Text").gameObject.SetActive(true);
                        }
                        currentObj = null;
                    }
                }


                if (currentObj != null && currentObj.GetComponent<TreeBlock>().snapped == false)
                {
                    //If object is not snapped destroy
                    Debug.Log("snapped false");
                    Destroy(currentObj);

                }
            }

            else if (currentObj != null && currentObj.GetComponent<TreeBlock>().inWorkspace == true)
            {
                Debug.Log("in workspace");
                if (currentObj.name == "Insert Function(Clone)")
                {
                    Debug.Log("Insert function dropped");
                    for (int i = 0; i < levelManager.isFunctionBlock.Count; i++)
                    {
                        //When current object is in trigger of a function snap block
                        if (levelManager.isFunctionBlock[i] == true && levelManager.isFunctionSnapped[i] == false)
                        {
                            currentObj.transform.position = levelManager.functionSnapPoints[i].transform.position;      //snap

                            machinePrefab.SetActive(true);

                            levelManager.isFunctionBlock[i] = false;
                            levelManager.isFunctionSnapped[i] = true;
                            ChangeBlockLayer(currentObj.transform, "Workspace");
                            currentObj = null;
                        }
                    }
                }

                if (currentObj.name == "Pre Order Traversal(Clone)" || currentObj.name == "In Order Traversal(Clone)" || currentObj.name == "Post Order Traversal(Clone)")
                {
                    Debug.Log("Traversal dropped");
                    for (int i = 0; i < levelManager.isFunctionBlock.Count; i++)
                    {
                        //When current object is in trigger of a function snap block
                        if (levelManager.isFunctionBlock[i] == true && levelManager.isFunctionSnapped[i] == false)
                        {
                            currentObj.transform.position = levelManager.functionSnapPoints[i].transform.position;      //snap

                            arrayPrefab.SetActive(true);

                            levelManager.isFunctionBlock[i] = false;
                            levelManager.isFunctionSnapped[i] = true;
                            ChangeBlockLayer(currentObj.transform, "Workspace");
                            currentObj = null;
                        }
                    }
                }

            }

            

            //For data objects
            if (currentObj != null && currentObj.CompareTag("Data"))
            {

                float _snapRadius = currentObj.GetComponent<DataBlock>().snapRadius;

                currentObj.GetComponent<DataBlock>().snapped = false;

                for (int i = 0; i < levelManager.dataSnapPoints.Count; i++)
                {
                    //Check whether it is within the snap radius and data object is not snapped already
                    if ((Mathf.Abs(currentObj.transform.position.x - levelManager.dataSnapPoints[i].transform.position.x) <= _snapRadius &&
                    Mathf.Abs(currentObj.transform.position.y - levelManager.dataSnapPoints[i].transform.position.y) <= _snapRadius) &&
                    levelManager.dataSnapPoints[i].transform.childCount == 0)
                    {
                        currentObj.transform.position = new Vector3(levelManager.dataSnapPoints[i].transform.position.x, levelManager.dataSnapPoints[i].transform.position.y, 0f);      //snap
                        currentObj.GetComponent<DataBlock>().snapped = true;

                        ChangeBlockLayer(currentObj.transform, "Data");

                        //Make the data element a child of the snapped point
                        currentObj.transform.SetParent(levelManager.dataSnapPoints[i].transform);

                        currentObj.transform.localScale = new Vector3(1f, 1f, 0f);

                        break;
                    }
                }

                //If data is not snapped
                if (currentObj.GetComponent<DataBlock>().snapped == false)
                {
                    //Return data object to where it was
                    Vector3 currentResetPos = currentObj.GetComponent<DataBlock>().resetPosition;
                    currentObj.transform.position = new Vector3(currentResetPos.x, currentResetPos.y, currentResetPos.z);
                    currentObj.transform.SetParent(dataParentObj);
                    currentObj.transform.localScale = currentObj.GetComponent<DataBlock>().originalScale;

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

}
