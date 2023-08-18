using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayLineManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask workspaceLayer;

    [SerializeField]
    GameObject currentLine;

    GameObject startPoint;

    Vector3 mousePos;

    [SerializeField]
    private ArrayLevelManager levelManager;

    [SerializeField]
    private int orderIndex;

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
                        startPoint = singleHit.collider.gameObject.transform.parent.parent.gameObject;
                        

                        GameObject functionObj = singleHit.collider.gameObject.transform.parent.parent.gameObject;
                        currentLine = functionObj.transform.Find("Line").gameObject;
                        currentLine.GetComponent<LineRenderer>().positionCount = 2;
                        currentLine.GetComponent<ArrayLine>().startPos = singleHit.collider.gameObject;

                        orderIndex = functionObj.GetComponent<ArrayBlock>().pseudoElement.transform.GetSiblingIndex();

                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {

            if (currentLine != null)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentLine.GetComponent<LineRenderer>().SetPosition(0, new Vector3(currentLine.GetComponent<ArrayLine>().startPos.transform.position.x, currentLine.GetComponent<ArrayLine>().startPos.transform.position.y, 0f));
                currentLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (currentLine != null)
            {
                for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                {
                    if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f)
                    {
                        currentLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(levelManager.lineEndPoints[i].position.x, levelManager.lineEndPoints[i].position.y, 0f));
                        currentLine.GetComponent<ArrayLine>().endPos = levelManager.lineEndPoints[i].gameObject;
                        currentLine.GetComponent<ArrayLine>().lineDrawn = true;
                        currentLine.GetComponent<ArrayLine>().linePositions = GetLinePositions(currentLine);
                        currentLine.GetComponent<ArrayLine>().lineWidth = GetWidth(currentLine);

                        levelManager.lines.Add(currentLine);

                        GameObject nextObject = levelManager.lineEndPoints[i].gameObject.transform.parent.parent.gameObject;
                        if (nextObject.name != "Computer")
                        {
                            if (startPoint.GetComponent<ArrayBlock>().dataElementCount == 0)
                            {
                                nextObject.GetComponent<ArrayBlock>().pseudoElement.transform.SetSiblingIndex(orderIndex + 1);
                            }
                            else if (startPoint.GetComponent<ArrayBlock>().dataElementCount != 0)
                            {
                                int dataCount = startPoint.GetComponent<ArrayBlock>().dataElementCount;
                                nextObject.GetComponent<ArrayBlock>().pseudoElement.transform.SetSiblingIndex(orderIndex + dataCount + 1);
                            }
                        }

                        currentLine = null;
                        break;
                    } 
                }

                if (currentLine != null && currentLine.GetComponent<ArrayLine>().lineDrawn == false)
                {
                    currentLine.GetComponent<LineRenderer>().positionCount = 0;
                    currentLine.GetComponent<ArrayLine>().startPos = null;
                    currentLine = null;
                }

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D lineHit = Physics2D.Raycast(mousePos, Vector3.zero, Mathf.Infinity, workspaceLayer);

            if (lineHit.collider != null)
            {
                if (lineHit.collider.CompareTag("Line"))
                {
                    Debug.Log("Line Hit");
                    EraseLine(lineHit.collider.gameObject);
                }
            }
        }
    }

    private void EraseLine(GameObject currentLine) 
    {
        if (currentLine != null)
        {
            for (int i=0; i < levelManager.lines.Count; i++)
            {
                if (levelManager.lines[i] == currentLine)
                {
                    levelManager.lines.RemoveAt(i);
                }
            }

            currentLine.GetComponent<LineRenderer>().positionCount = 0;
            currentLine.GetComponent<ArrayLine>().lineDrawn = false;
            currentLine.GetComponent<ArrayLine>().colliderPoints.Clear();
            currentLine.GetComponent<ArrayLine>().lineCollider.SetPath(0, resetPathsForCollider);
            currentLine.GetComponent<ArrayLine>().startPos = null;
            currentLine.GetComponent<ArrayLine>().endPos = null;
        }
    }

    private Vector3[] GetLinePositions(GameObject currentLine)
    {
        Vector3[] positions = new Vector3[currentLine.GetComponent<LineRenderer>().positionCount];
        currentLine.GetComponent<LineRenderer>().GetPositions(positions);
        return positions;
    }

    private float GetWidth(GameObject currentLine)
    {
        return currentLine.GetComponent<LineRenderer>().startWidth;
    }

}
