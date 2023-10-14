using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionLevel4 : MonoBehaviour
{
    [SerializeField]
    private GameObject statPanel;

    private void Start()
    {
        statPanel.SetActive(true);
    }
}
