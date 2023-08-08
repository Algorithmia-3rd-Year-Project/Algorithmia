using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linkedlistblockmanager : MonoBehaviour
{
    [SerializeField]
    private List<string> blockNames;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject linkedListPrefab;

    GameObject currentObj;

    private float startPosX;

    private float startPosY;

    private int workspaceLayer;

    private ArrayBlockList linkedListblocksList;


    // Start is called before the first frame update
    void Start()
    {
        workspaceLayer = LayerMask.NameToLayer("Workspace");
        linkedListblocksList = GetComponent<ArrayBlockList>();
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
            if (currentObj != null && currentObj.GetComponent<LinkedListBlock>().inWorkspace == true)
            {
                currentObj.layer = workspaceLayer;
                currentObj = null;
            }
            else
            {
                Destroy(currentObj);
            }

        }
    }
}
