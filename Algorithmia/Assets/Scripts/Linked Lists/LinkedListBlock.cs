using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedListBlock : MonoBehaviour
{
    public bool inWorkspace;

    public string blockName;

    public string pseudoCode;

    public string alternativePseudoCode;

    public string variableName;

    public bool addedBlock;

    public GameObject pseudoElement;

    public bool snapped;

    public string currentNodeName;

    public string previouseNodeName;

    public GameObject triggerNode;

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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Workspace"))
        {
            inWorkspace = false;
        }
    }


}
