using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayTutorial3 : MonoBehaviour
{
    [SerializeField]
    private GameObject statsIntroduction;

    private void Start()
    {
        statsIntroduction.SetActive(true);
    }
}
