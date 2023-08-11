using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayTutorial2 : MonoBehaviour
{
    [SerializeField]
    private GameObject programIntroduction;

    [SerializeField]
    private GameObject controlFlowIntroduction;

    private void Start()
    {
        programIntroduction.SetActive(true);
        controlFlowIntroduction.SetActive(false);
    }
}
