using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedListBlock : MonoBehaviour
{
    //create a list
    //save all the snap point related to the object
    //linkedlist blockmanager-> tracklinepoint -->create a function (access all the snap points and change the set active attribut==true)
    public bool inWorkspace;

    public string blockName;

    public string pseudoCode;

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
