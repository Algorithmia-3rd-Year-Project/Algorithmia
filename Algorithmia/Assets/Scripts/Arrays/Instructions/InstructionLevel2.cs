using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionLevel2 : MonoBehaviour
{
    [SerializeField]
    private GameObject dataTypePanel;

    [SerializeField]
    private GameObject instructionOverlay;

    private void Start()
    {
        instructionOverlay.SetActive(true);
        dataTypePanel.SetActive(true);
    }
}
