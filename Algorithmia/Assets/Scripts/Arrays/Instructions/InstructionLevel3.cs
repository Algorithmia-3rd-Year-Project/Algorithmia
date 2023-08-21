using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionLevel3 : MonoBehaviour
{

    [SerializeField]
    private GameObject programPanel;

    private void Start()
    {
        programPanel.SetActive(true);
    }

}
