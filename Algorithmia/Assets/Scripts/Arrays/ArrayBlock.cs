using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayBlock : MonoBehaviour
{
    public bool inWorkspace;

    public string blockName;

    public string dataType;

    public string pseudoCode;

    public GameObject pseudoElement;

    public int dataElementCount;

    [Header("Print Function Settings")]
    public string startPoint;
    public string endPoint;

    [Header("Reverse Function Settings")]
    public string start;
    public string end;

    public string dataStructure;

    private void Start()
    {
        inWorkspace = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Workspace"))
        {
            inWorkspace = true;
        }

        if (collision.gameObject.CompareTag("OutWorkspace"))
        {
            inWorkspace = false;
        }
    }
}
