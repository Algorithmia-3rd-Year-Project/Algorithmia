using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayTutorial1 : MonoBehaviour
{

    [SerializeField]
    private GameObject instruction1;

    [SerializeField]
    private GameObject instruction2;

    [SerializeField]
    private GameObject instruction3;

    [SerializeField]
    private GameObject instruction4;

    [SerializeField]
    private GameObject instruction5;

    [SerializeField]
    private ArrayLevelManager levelManager;

    private bool instruction1Executed;
    private bool instruction2Executed;
    private bool instruction3Executed;
    private bool instruction4Executed;
    private bool instruction5Executed;

    private void Start()
    {
        instruction1.SetActive(true);
        instruction2.SetActive(false);
        instruction3.SetActive(false);
        instruction4.SetActive(false);
    }

    private void Update()
    {
        if (!instruction2Executed && levelManager.blockCount != 0)
        {
            instruction1.SetActive(false);
            instruction2.SetActive(true);
            instruction2Executed = true;
        }

        if (!instruction3Executed && levelManager.correctForms.Count > 0 && levelManager.correctForms[0].transform.childCount != 0)
        {
            instruction2.SetActive(false);
            instruction3.SetActive(true);
            instruction3Executed = true;
        }

        if (instruction3.activeSelf)
        {
            int objectsCount = 0;
            for (int i=0; i < levelManager.correctForms.Count; i++)
            {
                if (levelManager.correctForms[i].transform.childCount > 0) 
                {
                    objectsCount += 1;
                }
            }

            if (objectsCount == 4)
            {
                instruction3.SetActive(false);
                instruction4.SetActive(true);
            }
        }

        if (!instruction5Executed && levelManager.lines.Count > 0)
        {
            instruction4.SetActive(false);
            instruction5.SetActive(true);
            instruction5Executed = true;
        }
    }

}
