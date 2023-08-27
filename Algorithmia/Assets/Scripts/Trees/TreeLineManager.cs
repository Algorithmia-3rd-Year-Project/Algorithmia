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

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, workspaceLayer);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("LineStart"))
                {
                    if (hit.collider.gameObject.name == "Line Start 1")
                    {
                        startPoint = hit.collider.gameObject;

                        GameObject functionObj = hit.collider.gameObject.transform.parent.parent.gameObject;
                        currentLine = functionObj.transform.Find("Line 1").gameObject;
                        currentLine.GetComponent<LineRenderer>().positionCount = 2;
                        currentLine.GetComponent<TreeLine>().startPos = hit.collider.gameObject;
                    }

                    if (hit.collider.gameObject.name == "Line Start 2")
                    {
                        startPoint = hit.collider.gameObject;

                        GameObject functionObj = hit.collider.gameObject.transform.parent.parent.gameObject;
                        currentLine = functionObj.transform.Find("Line 2").gameObject;
                        currentLine.GetComponent<LineRenderer>().positionCount = 2;
                        currentLine.GetComponent<TreeLine>().startPos = hit.collider.gameObject;
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

        if (Input.GetMouseButtonUp(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(currentLine.GetComponent<TreeLine>().startPos.name);

            if (currentLine != null)
            {
                if  (currentLine.GetComponent<TreeLine>().startPos.transform.parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 0") //Check if the name of the start position is Level 0
                {
                    if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 1")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 1 Left") //or level 1 Right
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                currentLine = null;
                                break;
                            }
                        }
                    }

                    if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 2")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 1 Right") //or level 1 Right
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                currentLine = null;
                                break;
                            }
                        }
                    }

                }   //Repeat this thing for all levels



                if (currentLine != null && currentLine.GetComponent<TreeLine>().lineDrawn == false)
                {
                    currentLine.GetComponent<LineRenderer>().positionCount = 0;
                    currentLine.GetComponent<TreeLine>().startPos = null;
                    currentLine = null;
                }

            }
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

    private void afterLineDrawn(GameObject currentLine, int i)
    {
        currentLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(levelManager.lineEndPoints[i].position.x, levelManager.lineEndPoints[i].position.y, 0f));
        currentLine.GetComponent<TreeLine>().endPos = levelManager.lineEndPoints[i].gameObject;
        currentLine.GetComponent<TreeLine>().lineDrawn = true;
        currentLine.GetComponent<TreeLine>().linePositions = GetLinePositions(currentLine);
        currentLine.GetComponent<TreeLine>().lineWidth = GetWidth(currentLine);

        levelManager.lines.Add(currentLine);

    }


}
