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
    private ArrayLevelManager levelManager;

    private void Start()
    {
        instruction1.SetActive(true);
        instruction2.SetActive(false);
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
        }
    }

}
