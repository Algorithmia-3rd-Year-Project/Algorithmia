using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayBlockManager : MonoBehaviour
{

    [SerializeField]
    private List<string> blockNames;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject arrayPrefab;

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


    private void Start()
    {
        workspaceLayer = LayerMask.NameToLayer("Workspace");
        arrayblocksList = GetComponent<ArrayBlockList>();

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
                    
                } else if (hit.collider.name == "Array Print Function")
                {
                    currentObj = Instantiate(arrayblocksList.blockList["Array Print Function"], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
                }

                startPosX = mousePos.x - currentObj.transform.position.x;
                startPosY = mousePos.y - currentObj.transform.position.y;
            } else
            {
                RaycastHit2D dataHit = Physics2D.Raycast(mousePos, Vector3.zero, 20f, dataLayer);

                if (dataHit.collider != null)
                {
                    currentObj = dataHit.collider.gameObject;

                    startPosX = mousePos.x - currentObj.transform.position.x;
                    startPosY = mousePos.y - currentObj.transform.position.y;
                } else
                {
                    //RaycastHit2D workspaceHit = Physics2D.Raycast(mousePos, Vector3.zero, 20f, anotherLayer);

                    RaycastHit2D[] allhits = Physics2D.RaycastAll(mousePos, Vector3.zero, Mathf.Infinity, anotherLayer);


                    if (allhits.Length > 1)
                    {
                        foreach(RaycastHit2D singleHit in allhits)
                        {
                            if (singleHit.collider.CompareTag("Data"))
                            {
                                currentObj = singleHit.collider.gameObject;
                            }
                        }
                    } else if (allhits.Length == 1)
                    {
                        currentObj = allhits[0].collider.gameObject;
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

                

                currentObj.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<ArrayBlock>().inWorkspace == true)
            {

                if (currentObj.layer != workspaceLayer)
                {
                    ChangeBlockLayer(currentObj.transform, "Workspace");
                    TrackSnapPoints(currentObj);
                }

                
                currentObj = null;
            } else if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<ArrayBlock>().inWorkspace == false)
            {

                DestroyBlocks(currentObj);

            } else if (currentObj != null && currentObj.CompareTag("Data"))
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

                        break;
                    }
                }

                if (currentObj.GetComponent<DataBlock>().snapped == false)
                {
                    Vector3 currentResetPos = currentObj.GetComponent<DataBlock>().resetPosition;
                    currentObj.transform.position = new Vector3(currentResetPos.x, currentResetPos.y, currentResetPos.z);
                    currentObj.transform.SetParent(dataParentObj);
                    ChangeBlockLayer(currentObj.transform, "Data");
                }
                

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

    //To change the layer of each objects when they are being dragged from one camera view to another
    private void ChangeBlockLayer(Transform currentObj, string layerName)
    {
        if (currentObj != null)
        {
            currentObj.gameObject.layer = LayerMask.NameToLayer(layerName);
        }

        for (int i=0; i < currentObj.childCount; i++)
        {
            Transform childTransform = currentObj.GetChild(i);
            ChangeBlockLayer(childTransform, layerName);
        }
    }

    //Have to delete both the block and its snap points that was assigned to the level manager
    private void DestroyBlocks(GameObject currentObj)
    {
        Transform snapPointsListObj = currentObj.transform.Find("Snap Points");


        if (snapPointsListObj != null)
        {
            Transform[] snapPointsList = snapPointsListObj.GetComponentsInChildren<Transform>(includeInactive: false);
            int deletedCount = 0;

            for (int i=0; i < snapPointsList.Length; i++)
            {
                for (int j=0; j < levelManager.correctForms.Count; j++)
                {
                    if (snapPointsList[i].gameObject == levelManager.correctForms[j])
                    {
                        levelManager.correctForms.RemoveAt(j);
                        deletedCount += 1;
                    }
                }
            }
            if (deletedCount == 4)
            {
                Destroy(currentObj);
            }
        }
    }
   

}
