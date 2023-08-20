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

            RaycastHit2D inventoryHit = Physics2D.Raycast(mousePos, Vector3.zero, Mathf.Infinity, blockLayer);

            if (inventoryHit.collider != null)
            {
                if (inventoryHit.collider.gameObject.name == "Tree Node")
                {
                    currentObj = Instantiate(treeblocksList.blockList["Tree Node"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                }
            }
            else
            {
                RaycastHit2D dataHit = Physics2D.Raycast(mousePos, Vector3.zero, Mathf.Infinity, dataLayer);

                if (dataHit.collider != null)
                {
                    Debug.Log("data");
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


                currentObj.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<TreeBlock>().inWorkspace == true)
            {
                for(int i = 0; i<levelManager.isSnapBlock.Count; i++)
                {
                    if (levelManager.isSnapBlock[i] == true && levelManager.isNodeSnapped[i] == false)
                    {
                        currentObj.transform.position = levelManager.snapPoints[i].transform.position;
                        levelManager.isSnapBlock[i] = false;
                        levelManager.isNodeSnapped[i] = true;
                        currentObj.GetComponent<TreeBlock>().snapped = true;

                        GameObject SnapPoint = currentObj.transform.Find("Snap Point").gameObject;
                        levelManager.dataSnapPoints.Add(SnapPoint);
                       
                    }
                }
                if (currentObj.GetComponent<TreeBlock>().snapped == false)
                {
                    Destroy(currentObj);
                }

                currentObj = null;
            }

            else if (currentObj != null && currentObj.CompareTag("Data"))
            {

                float _snapRadius = currentObj.GetComponent<DataBlock>().snapRadius;

                currentObj.GetComponent<DataBlock>().snapped = false;

                for (int i = 0; i < levelManager.dataSnapPoints.Count; i++)
                {
                    if ((Mathf.Abs(currentObj.transform.position.x - levelManager.dataSnapPoints[i].transform.position.x) <= _snapRadius &&
                    Mathf.Abs(currentObj.transform.position.y - levelManager.dataSnapPoints[i].transform.position.y) <= _snapRadius) &&
                    levelManager.dataSnapPoints[i].transform.childCount == 0)
                    {
                        currentObj.transform.position = new Vector3(levelManager.dataSnapPoints[i].transform.position.x, levelManager.dataSnapPoints[i].transform.position.y, 0f);
                        currentObj.GetComponent<DataBlock>().snapped = true;

                        //ChangeBlockLayer(currentObj.transform, "Workspace");

                        //make the data element a child of the snapped point
                        currentObj.transform.SetParent(levelManager.dataSnapPoints[i].transform);

                        currentObj.transform.localScale = new Vector3(1f, 1f, 0f);

                        break;
                    }
                }

                if (currentObj.GetComponent<DataBlock>().snapped == false)
                {
                    Vector3 currentResetPos = currentObj.GetComponent<DataBlock>().resetPosition;
                    currentObj.transform.position = new Vector3(currentResetPos.x, currentResetPos.y, currentResetPos.z);
                    currentObj.transform.SetParent(dataParentObj);
                    //ChangeBlockLayer(currentObj.transform, "Data");
                    currentObj.transform.localScale = currentObj.GetComponent<DataBlock>().originalScale;

                    currentObj.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    Transform dataText = currentObj.transform.Find("a-data");
                    dataText.GetComponent<SpriteRenderer>().sortingOrder = 4;


                }

                currentObj = null;
            }
        }

    }

}
