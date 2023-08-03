using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayBlock : MonoBehaviour
{
    public bool inWorkspace;

    public string blockName;

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
