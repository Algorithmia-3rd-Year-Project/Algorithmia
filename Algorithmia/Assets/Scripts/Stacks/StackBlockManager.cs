using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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


    private void Start()
    {
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
                Debug.Log("Ray sent");

                if (hit.collider.name == "Empty Stack Block")
                {
                    Debug.Log("Clicked on stack");
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
            if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<StackBlock>().inWorkspace == true)
            {

                if (currentObj.layer != workspaceLayer)
                {
                    ChangeBlockLayer(currentObj.transform, "Workspace");
                    levelManager.blockCount += 1;

                }

                currentObj = null;

            }
            else if (currentObj != null && currentObj.CompareTag("Inventory") && currentObj.GetComponent<StackBlock>().inWorkspace == false)
            {
                DestroyBlocks(currentObj);

            }
                currentObj = null;

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
        Transform linePointsObj = currentObj.transform.Find("Line Points");

        if (linePointsObj != null)
        {
            //idunno
        }


        levelManager.blockCount -= 1;
        Destroy(currentObj);
    }

}


