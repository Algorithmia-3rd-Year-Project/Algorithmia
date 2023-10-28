using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackLineManager : MonoBehaviour
{
     [SerializeField]
    private LayerMask workspaceLayer;

    [SerializeField]
    GameObject currentLine;

    GameObject startPoint;

    Vector3 mousePos;

    [SerializeField]
    private StackLevelManager levelManager;

    [SerializeField]
    private StackBlockManager blockManager;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero, Mathf.Infinity, workspaceLayer);

            if (hits.Length > 0) 
            {
                Debug.Log(hits[0].collider.gameObject.name);
                foreach (RaycastHit2D singleHit in hits)
                {
                    if (singleHit.collider.CompareTag("LineStart"))
                    {
                        startPoint = singleHit.collider.gameObject.transform.parent.parent.gameObject;


                        GameObject functionObj = singleHit.collider.gameObject.transform.parent.parent.gameObject;
                        currentLine = functionObj.transform.Find("Line").gameObject;
                        currentLine.GetComponent<LineRenderer>().positionCount = 2;
                        currentLine.GetComponent<StackLine>().startPos = singleHit.collider.gameObject;

                        //orderIndex = functionObj.GetComponent<StackBlock>().pseudoElement.transform.GetSiblingIndex();

                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {

            if (currentLine != null)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentLine.GetComponent<LineRenderer>().SetPosition(0, new Vector3(currentLine.GetComponent<StackLine>().startPos.transform.position.x, currentLine.GetComponent<StackLine>().startPos.transform.position.y, 0f));
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
                    if (Mathf.Abs(mousePos.x - levelManager.lineEndPoints[i].position.x) <= 0.2f && Mathf.Abs(mousePos.y - levelManager.lineEndPoints[i].position.y) <= 0.2f)
                    {
                        currentLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(levelManager.lineEndPoints[i].position.x, levelManager.lineEndPoints[i].position.y, 0f));
                        currentLine.GetComponent<StackLine>().endPos = levelManager.lineEndPoints[i].gameObject;
                        currentLine.GetComponent<StackLine>().lineDrawn = true;
                        currentLine.GetComponent<StackLine>().linePositions = GetLinePositions(currentLine);
                        currentLine.GetComponent<StackLine>().lineWidth = GetWidth(currentLine);

                        levelManager.lines.Add(currentLine);

                        GameObject nextObject = levelManager.lineEndPoints[i].gameObject.transform.parent.parent.gameObject;
                        if (nextObject.name != "Machine")
                        {
                            if (startPoint.GetComponent<StackBlock>().dataElementCount == 0)
                            {
                                //nextObject.GetComponent<StackBlock>().pseudoElement.transform.SetSiblingIndex(orderIndex + 1);
                            }
                            else if (startPoint.GetComponent<StackBlock>().dataElementCount != 0)
                            {

                                int dataCount = startPoint.GetComponent<StackBlock>().dataElementCount;

                                //To change the data elements order in the vertical layout group
                                int encounteredChildren = 0;

                                for (int j = 0; j < 4; j++)
                                {
                                    if (startPoint.transform.Find("Snap Points").transform.Find(j.ToString()).transform.childCount != 0)
                                    {
                                        GameObject child = startPoint.transform.Find("Snap Points").transform.Find(j.ToString()).transform.Find("Data Block").gameObject;
                                        encounteredChildren += 1;
                                        //child.GetComponent<DataBlock>().pseudoElement.transform.SetSiblingIndex(orderIndex + encounteredChildren);
                                    }

                                    if (encounteredChildren == dataCount)
                                    {
                                        break;
                                    }
                                }

                               // nextObject.GetComponent<StackBlock>().pseudoElement.transform.SetSiblingIndex(orderIndex + dataCount + 1);
                            }


                            //pass the connected data structure name along the line
                            /*if (nextObject.GetComponent<ArrayBlock>().blockName == "Array Insertion")
                            {
                                if (levelManager.lineEndPoints[i].gameObject.name == "Parameter Array")
                                {
                                    nextObject.GetComponent<ArrayBlock>().newDataStructure = "<color=#CF9FFF>" + startPoint.GetComponent<ArrayBlock>().dataStructure + "</color>";
                                }
                                else if (levelManager.lineEndPoints[i].gameObject.name == "Line End")
                                {
                                    nextObject.GetComponent<ArrayBlock>().dataStructure = "<color=#89CFF0>" + startPoint.GetComponent<ArrayBlock>().dataStructure + "</color>";
                                }


                            }
                            else
                            {
                                if (startPoint.GetComponent<ArrayBlock>().blockName == "Array Insertion")
                                {
                                    nextObject.GetComponent<ArrayBlock>().dataStructure = startPoint.GetComponent<ArrayBlock>().newDataStructure;
                                }
                                else
                                {
                                    nextObject.GetComponent<ArrayBlock>().dataStructure = "<color=#89CFF0>" + startPoint.GetComponent<ArrayBlock>().dataStructure + "</color>";
                                }

                            }*/
                        }

                        currentLine = null;
                        break;
                    }
                }

                if (currentLine != null && currentLine.GetComponent<StackLine>().lineDrawn == false)
                {
                    currentLine.GetComponent<LineRenderer>().positionCount = 0;
                    currentLine.GetComponent<StackLine>().startPos = null;
                    currentLine = null;
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
            currentLine.GetComponent<StackLine>().lineDrawn = false;
            currentLine.GetComponent<StackLine>().colliderPoints.Clear();
            currentLine.GetComponent<StackLine>().lineCollider.SetPath(0, resetPathsForCollider);
            currentLine.GetComponent<StackLine>().startPos = null;
            currentLine.GetComponent<StackLine>().endPos = null;
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
