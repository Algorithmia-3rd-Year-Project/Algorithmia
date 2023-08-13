using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBlock : MonoBehaviour
{
    public bool inWorkspace;

    public string blockName;

    [SerializeField]
    private TreeLevelManager levelManager;


    private void Start()
    {
        levelManager = FindObjectOfType<TreeLevelManager>();
        inWorkspace = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent.name == "Level 0")
        {
            inWorkspace = true;
            Debug.Log("In");
            levelManager.isSnapBlock[0] = true;
        }
        if (collision.gameObject.transform.parent.name == "Level 1 Left")
        {
            inWorkspace = true;
            Debug.Log("In");
            levelManager.isSnapBlock[1] = true;
        }
        if (collision.gameObject.transform.parent.name == "Level 1 Right")
        {
            inWorkspace = true;
            Debug.Log("In");
            levelManager.isSnapBlock[2] = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent.name == "Level 0")
        {
            inWorkspace = false;
            Debug.Log("out");
            levelManager.isSnapBlock[0] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 1 Left")
        {
            inWorkspace = false;
            Debug.Log("out");
            levelManager.isSnapBlock[1] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 1 Right")
        {
            inWorkspace = false;
            Debug.Log("out");
            levelManager.isSnapBlock[2] = false;
        }
    }
}
