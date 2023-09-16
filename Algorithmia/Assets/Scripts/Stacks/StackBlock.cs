using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBlock : MonoBehaviour
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
        }
    }
}
