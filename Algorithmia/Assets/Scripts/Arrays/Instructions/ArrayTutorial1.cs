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

    private void Start()
    {
        instruction1.SetActive(true);
        instruction2.SetActive(false);
        instruction3.SetActive(false);
        instruction4.SetActive(false);
    }

    private void Update()
    {
        if (levelManager.blockCount != 0)
        {
            instruction1.SetActive(false);
            instruction2.SetActive(true);
        }

        if (levelManager.correctForms.Count > 0 && levelManager.correctForms[0].transform.childCount != 0)
        {
            instruction2.SetActive(false);
            instruction3.SetActive(true);
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

        if (levelManager.lines.Count > 0)
        {
            instruction4.SetActive(false);
            instruction5.SetActive(true);
        }
    }

}
