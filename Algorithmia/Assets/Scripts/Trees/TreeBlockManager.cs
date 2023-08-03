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

    }

}
