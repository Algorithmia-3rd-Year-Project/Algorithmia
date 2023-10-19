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

    public List<GameObject> lineDrawnPoints;

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
        lineDrawnPoints = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, workspaceLayer);
            RaycastHit2D[] allhits = Physics2D.RaycastAll(mousePos, Vector3.zero, Mathf.Infinity, workspaceLayer);

                foreach (RaycastHit2D singleHit in allhits)
                {
                    if (singleHit.collider.CompareTag("LineStart"))
                    {
                        if (singleHit.collider.gameObject.name == "Line Start 1")
                        {
                            startPoint = singleHit.collider.gameObject;

                            GameObject functionObj = singleHit.collider.gameObject.transform.parent.parent.gameObject;
                            currentLine = functionObj.transform.Find("Line 1").gameObject;
                            currentLine.GetComponent<LineRenderer>().positionCount = 2;
                            currentLine.GetComponent<TreeLine>().startPos = singleHit.collider.gameObject;
                        }

                        if (singleHit.collider.gameObject.name == "Line Start 2")
                        {
                            startPoint = singleHit.collider.gameObject;

                            GameObject functionObj = singleHit.collider.gameObject.transform.parent.parent.gameObject;
                            currentLine = functionObj.transform.Find("Line 2").gameObject;
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

        if (Input.GetMouseButtonUp(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(currentLine.GetComponent<TreeLine>().startPos.name);

            if (currentLine != null)
            {
                //FOR LEVEL 0
                if  (currentLine.GetComponent<TreeLine>().startPos.transform.parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 0") //Check if the name of the start position is Level 0
                {
                    if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 1")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 1 Left")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }

                    else if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 2")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 1 Right")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }
                }

                //FOR LEVEL 1 LEFT
                else if (currentLine.GetComponent<TreeLine>().startPos.transform.parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 1 Left") //Check if the name of the start position is Level 0
                {
                    if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 1")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 2 Left 1")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }

                    else if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 2")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 2 Right 1")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }
                }

                //FOR LEVEL 1 RIGHT 
                else if (currentLine.GetComponent<TreeLine>().startPos.transform.parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 1 Right") //Check if the name of the start position is Level 0
                {
                    if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 1")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 2 Left 2")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }

                    else if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 2")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 2 Right 2")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }
                }

                //FOR LEVEL 2 LEFT 1
                else if (currentLine.GetComponent<TreeLine>().startPos.transform.parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 2 Left 1") //Check if the name of the start position is Level 0
                {
                    if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 1")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 3 Left 1")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }

                    else if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 2")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 3 Right 1")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }
                }

                //FOR LEVEL 2 RIGHT 1 
                else if (currentLine.GetComponent<TreeLine>().startPos.transform.parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 2 Right 1") //Check if the name of the start position is Level 0
                {
                    if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 1")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 3 Left 2")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }

                    else if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 2")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 3 Right 2")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }
                }

                //FOR LEVEL 2 LEFT 2
                else if (currentLine.GetComponent<TreeLine>().startPos.transform.parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 2 Left 2") //Check if the name of the start position is Level 0
                {
                    if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 1")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 3 Left 3")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }

                    else if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 2")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 3 Right 3")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }
                }

                //FOR LEVEL 2 RIGHT 2
                else if (currentLine.GetComponent<TreeLine>().startPos.transform.parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 2 Right 2") //Check if the name of the start position is Level 0
                {
                    if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 1")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 3 Left 4")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }

                    else if (currentLine.GetComponent<TreeLine>().startPos.name == "Line Start 2")
                    {
                        for (int i = 0; i < levelManager.lineEndPoints.Count; i++)
                        {
                            if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 2f
                                && levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel == "Level 3 Right 4")
                            {
                                Debug.Log(levelManager.lineEndPoints[i].parent.parent.GetComponent<TreeBlock>().snappedLevel);
                                afterLineDrawn(currentLine, i);
                                lineDrawnPoints.Add(levelManager.lineEndPoints[i].gameObject);
                                currentLine = null;
                                break;
                            }
                        }
                    }
                }

                if (currentLine != null && currentLine.GetComponent<TreeLine>().lineDrawn == false)
                {
                    currentLine.GetComponent<LineRenderer>().positionCount = 0;
                    currentLine.GetComponent<TreeLine>().startPos = null;
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
            for (int i = 0; i < levelManager.lines.Count; i++)
            {
                if (levelManager.lines[i] == currentLine)
                {
                    levelManager.lines.RemoveAt(i);
                }
            }

            currentLine.GetComponent<LineRenderer>().positionCount = 0;
            currentLine.GetComponent<TreeLine>().lineDrawn = false;
            currentLine.GetComponent<TreeLine>().colliderPoints.Clear();
            currentLine.GetComponent<TreeLine>().lineCollider.SetPath(0, resetPathsForCollider);
            currentLine.GetComponent<TreeLine>().startPos = null;
            currentLine.GetComponent<TreeLine>().endPos = null;
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
