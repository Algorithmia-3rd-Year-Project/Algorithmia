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
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero, Mathf.Infinity, workspaceLayer);

            if (hits.Length > 0)
            {
                foreach (RaycastHit2D singleHit in hits)
                {
                    if (singleHit.collider.CompareTag("LineStart"))
                    {
                        startPoint = singleHit.collider.gameObject;
                        Debug.Log(startPoint);

                        GameObject functionObj = singleHit.collider.gameObject.transform.parent.parent.gameObject;
                        currentLine = functionObj.transform.Find("Line").gameObject;
                        currentLine.GetComponent<LineRenderer>().positionCount = 2;
                        currentLine.GetComponent<TreeLine>().startPos = singleHit.collider.gameObject;

                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {

            if (currentLine != null)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentLine.GetComponent<LineRenderer>().SetPosition(0, new Vector3(currentLine.GetComponent<TreeLine>().startPos.transform.position.x, currentLine.GetComponent<TreeLine>().startPos.transform.position.y, 0f));
                currentLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
            }
        }
    }

        
}
