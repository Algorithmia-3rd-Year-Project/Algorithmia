using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLineManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask workspaceLayer;

    [SerializeField]
    GameObject currentLine;

    GameObject startPoint;

    Vector3 mousePos;

    [SerializeField]
    private TreeLevelManager levelManager;

    private List<Vector2> resetPathsForCollider = new List<Vector2>()
    {
        new Vector2(0f, 0f),
        new Vector2(0f, 0f),
        new Vector2(0f, 0f),
        new Vector2(0f, 0f)
    };

    private void Start()
    {
        currentLine = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            //Sending a raycast to inventory objects
            RaycastHit2D lineStartHit = Physics2D.Raycast(mousePos, Vector3.zero, Mathf.Infinity, workspaceLayer);


            if (lineStartHit.collider.CompareTag("LineStart"))
            {
                Debug.Log("Line start");
                startPoint = lineStartHit.collider.gameObject;


                GameObject functionObj = lineStartHit.collider.gameObject.transform.parent.parent.gameObject;
                currentLine = functionObj.transform.Find("Line").gameObject;
                currentLine.GetComponent<LineRenderer>().positionCount = 2;
                currentLine.GetComponent<TreeLine>().startPos = lineStartHit.collider.gameObject;

            }
        }
    }

        
}
