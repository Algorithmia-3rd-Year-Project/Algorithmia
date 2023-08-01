using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayLevel1 : MonoBehaviour
{
    [SerializeField]
    private ArrayLevelManager levelManager;

    public void OptimalAnswer()
    {
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
