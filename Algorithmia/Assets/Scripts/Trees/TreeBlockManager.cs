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

            if (currentObj != null && currentObj.GetComponent<TreeBlock>().inWorkspace == true)
            {
                for(int i = 0; i<levelManager.isSnapBlock.Count; i++)
                {
                    if (levelManager.isSnapBlock[i] == true && levelManager.isNodeSnapped[i] == false)
                    {
                        currentObj.transform.position = levelManager.snapPoints[i].transform.position;
                        levelManager.isSnapBlock[i] = false;
                        levelManager.isNodeSnapped[i] = true;
                       
                    }
                    else
                    {
                       //bh
                    }
                }

                currentObj = null;
            }
        }

    }

}
