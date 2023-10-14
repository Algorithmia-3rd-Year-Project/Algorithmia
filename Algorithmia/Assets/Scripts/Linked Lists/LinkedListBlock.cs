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

    public GameObject pseudoElement;

    private void Start()
    {
        inWorkspace = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Workspace"))
        {
            inWorkspace = true;
            Debug.Log("In");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Workspace"))
        {
            inWorkspace = false;
            Debug.Log("out");
        }
    }


}
