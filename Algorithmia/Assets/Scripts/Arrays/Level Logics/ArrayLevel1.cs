using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayLevel1 : MonoBehaviour
{
    [SerializeField]
    private ArrayLevelManager levelManager;

    [SerializeField]
    private GameObject finalInstruction;

    [SerializeField]
    private float animationDuration;

    [SerializeField]
    private GameObject redrawLine;

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i=0; i <levelManager.lines.Count; i++)
            {
                StartCoroutine(RedrawLine(i));
            }
        }*/
    }

    private IEnumerator RedrawLine(int index)
    {
        float startTime = Time.time;

        GameObject newLine = Instantiate(redrawLine, new Vector3(0, 0, 0), Quaternion.identity);
        newLine.GetComponent<LineRenderer>().positionCount = 2;

        Vector3 startPosition = levelManager.lines[index].GetComponent<LineRenderer>().GetPosition(0);
        Vector3 endPosition = levelManager.lines[index].GetComponent<LineRenderer>().GetPosition(1);

        newLine.GetComponent<LineRenderer>().SetPosition(0, startPosition);

        Vector3 pos = startPosition;
        while (pos != endPosition)
        {
            float t = (Time.time - startTime) / animationDuration;
            pos = Vector3.Lerp(startPosition, endPosition, t);
            newLine.GetComponent<LineRenderer>().SetPosition(1, pos);
            yield return null;
        }
    }

    public void LevelRunEffect()
    {
        for (int i = 0; i < levelManager.lines.Count; i++)
        {
            StartCoroutine(RedrawLine(i));
        }
    }

    public void OptimalAnswer()
    {
        finalInstruction.SetActive(false);

        //Expected items count for the optimal victory condition
        int dataCount = 4;
        int blockCount = 1;
        int lineCount = 1;

        //Check whether every snap points in array has a data value
        int snapPoints = levelManager.correctForms.Count;
        int elementCount = 0;



        for (int i=0; i < snapPoints; i++)
        {
            if (levelManager.correctForms[i].transform.childCount != 0)
            {
                elementCount += 1;
            }
        }

        if (elementCount == 4)
        {
            Debug.Log("Pass");
        } else
        {
            Debug.Log("Fail");
        }
    }
}
