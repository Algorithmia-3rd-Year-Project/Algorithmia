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
    private ArrayBlockManager blockManager;

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

                            //pass the connected data structure name along the line
                            nextObject.GetComponent<ArrayBlock>().dataStructure = startPoint.GetComponent<ArrayBlock>().dataStructure;

                            //update the pseudo code
                            string dataStructure = nextObject.GetComponent<ArrayBlock>().dataStructure;

                            if (dataStructure != "")
                            {

                                if (nextObject.GetComponent<ArrayBlock>().blockName == "Array Print")
                                {

                                    nextObject.GetComponent<ArrayBlock>().pseudoCode = "for index=<color=yellow>s</color> to <color=yellow>e</color>%	      print <color=#89CFF0>" + dataStructure + "</color>[index]%end for";

                                    GameObject codeObject = nextObject.GetComponent<ArrayBlock>().pseudoElement;

                                    string pseudoText = nextObject.GetComponent<ArrayBlock>().pseudoCode;
                                    string[] pseudoSubstrings = pseudoText.Split('%');


                                    StartCoroutine(blockManager.TypingMultipleCode(pseudoSubstrings, codeObject));

                                } else if (nextObject.GetComponent<ArrayBlock>().blockName == "Array Reverse")
                                {

                                    nextObject.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>start</color> = <color=#F88379>0</color>%<color=yellow>end</color> = <color=#F88379>0</color>%while <color=yellow>start</color> < <color=yellow>end</color>%      <color=green>Number</color> temp = <color=#89CFF0>" + dataStructure + "</color>[<color=yellow>start</color>]%      <color=#89CFF0>" + dataStructure + "</color>[<color=yellow>start</color>] = <color=#89CFF0>" + dataStructure + "</color>[<color=yellow>end</color>]%      <color=#89CFF0>" + dataStructure + "</color>[<color=yellow>end</color>] = temp%      <color=yellow>start</color> = <color=yellow>start</color> + 1%      <color=yellow>end</color> = <color=yellow>end</color> - 1%end while";

                                    GameObject codeObject = nextObject.GetComponent<ArrayBlock>().pseudoElement;

                                    string pseudoText = nextObject.GetComponent<ArrayBlock>().pseudoCode;
                                    string[] pseudoSubstrings = pseudoText.Split('%');


                                    StartCoroutine(blockManager.TypingMultipleCode(pseudoSubstrings, codeObject));
                                }



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

                    GameObject lineEndBlock = lineHit.collider.gameObject.GetComponent<ArrayLine>().endPos;
                    GameObject block = lineEndBlock.transform.parent.parent.gameObject;

                    if (block.GetComponent<ArrayBlock>().dataStructure != "")
                    {
                        block.GetComponent<ArrayBlock>().dataStructure = "";

                        if (block.GetComponent<ArrayBlock>().blockName == "Array Print")
                        {

                            block.GetComponent<ArrayBlock>().pseudoCode = "for index=<color=yellow>s</color> to <color=yellow>e</color>%	      print Array[index]%end for";
                            GameObject codeObject = block.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = block.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');

                            StartCoroutine(blockManager.TypingMultipleCode(pseudoSubstrings, codeObject));
                        }

                        if (block.GetComponent<ArrayBlock>().blockName == "Array Reverse")
                        {

                            block.GetComponent<ArrayBlock>().pseudoCode = "<color=yellow>start</color> = <color=#F88379>0</color>%<color=yellow>end</color> = <color=#F88379>0</color>%while <color=yellow>start</color> < <color=yellow>end</color>%      <color=green>Number</color> temp = Array[<color=yellow>start</color>]%      Array[<color=yellow>start</color>] = Array[<color=yellow>end</color>]%      Array[<color=yellow>end</color>] = temp%      <color=yellow>start</color> = <color=yellow>start</color> + 1%      <color=yellow>end</color> = <color=yellow>end</color> - 1%end while";
                            GameObject codeObject = block.GetComponent<ArrayBlock>().pseudoElement;

                            string pseudoText = block.GetComponent<ArrayBlock>().pseudoCode;
                            string[] pseudoSubstrings = pseudoText.Split('%');

                            StartCoroutine(blockManager.TypingMultipleCode(pseudoSubstrings, codeObject));
                        }
                    }

             
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
