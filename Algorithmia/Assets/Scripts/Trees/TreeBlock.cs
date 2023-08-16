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
        if (collision.gameObject.transform.parent.name == "Level 2 Left 1")
        {
            inWorkspace = true;
            Debug.Log("In");
            levelManager.isSnapBlock[3] = true;
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Right 1")
        {
            inWorkspace = true;
            Debug.Log("In");
            levelManager.isSnapBlock[4] = true;
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Left 2")
        {
            inWorkspace = true;
            Debug.Log("In");
            levelManager.isSnapBlock[5] = true;
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Right 2")
        {
            inWorkspace = true;
            Debug.Log("In");
            levelManager.isSnapBlock[6] = true;
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
        if (collision.gameObject.transform.parent.name == "Level 2 Left 1")
        {
            inWorkspace = false;
            Debug.Log("out");
            levelManager.isSnapBlock[3] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Right 1")
        {
            inWorkspace = false;
            Debug.Log("out");
            levelManager.isSnapBlock[4] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Left 2")
        {
            inWorkspace = false;
            Debug.Log("out");
            levelManager.isSnapBlock[5] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Right 2")
        {
            inWorkspace = false;
            Debug.Log("out");
            levelManager.isSnapBlock[6] = false;
        }
    }
}
