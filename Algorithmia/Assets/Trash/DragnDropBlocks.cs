using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDropBlocks : MonoBehaviour
{

    [SerializeField]
    private GameObject arrayBlockPrefab;

    public bool moving;

    private float startPosX;

    private float startPosY;

    private GameObject currentDraggableObject;

    [SerializeField]
    private bool inWorkspace;

    public bool hasSet;

    private void Start()
    {
        inWorkspace = false;
        hasSet = false;
        currentDraggableObject = null;
    }

    private void Update()
    {
        if (currentDraggableObject != null && currentDraggableObject.GetComponent<DragnDropBlocks>().moving && !currentDraggableObject.GetComponent<DragnDropBlocks>().hasSet)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            currentDraggableObject.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, currentDraggableObject.transform.position.z);

        }
    }

    private void OnMouseDown()
    {


        currentDraggableObject = Instantiate(arrayBlockPrefab, transform.position, transform.rotation);
        currentDraggableObject.name = "New Array Block";


        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        startPosX = mousePos.x - this.transform.position.x;
        startPosY = mousePos.y - this.transform.position.y;

        currentDraggableObject.GetComponent<DragnDropBlocks>().moving = true;



    }

    private void OnMouseUp()
    {
        if (inWorkspace)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            currentDraggableObject.transform.position = new Vector3(mousePos.x, mousePos.y, currentDraggableObject.transform.position.z);

            currentDraggableObject.GetComponent<DragnDropBlocks>().hasSet = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Inventory"))
        {
            inWorkspace = true;
            Debug.Log("In");
        }
    }

}
