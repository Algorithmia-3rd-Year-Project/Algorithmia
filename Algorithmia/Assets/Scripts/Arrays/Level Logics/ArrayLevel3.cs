using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayLevel3 : MonoBehaviour
{
    [SerializeField]
    private GameObject startingTransition;


    [SerializeField]
    private float animationDuration;

    [SerializeField]
    private GameObject redrawLine;

    [SerializeField]
    private ArrayLevelManager levelManager;

    [SerializeField]
    private GameObject victoryMenu;

    private void Start()
    {
        startingTransition.SetActive(true);
        StartCoroutine(DisableStartTransition());
    }

    private IEnumerator DisableStartTransition()
    {
        yield return new WaitForSeconds(1f);
        startingTransition.SetActive(false);
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

        StartCoroutine(VictoryMenuLoading());
    }

    private IEnumerator VictoryMenuLoading()
    {
        yield return new WaitForSeconds(0.6f);
        victoryMenu.SetActive(true);
    }

}
