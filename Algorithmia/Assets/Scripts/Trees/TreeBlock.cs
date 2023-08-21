using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBlock : MonoBehaviour
{
    public bool inWorkspace;

    public string blockName;

    public bool snapped;

    [SerializeField]
    private TreeLevelManager levelManager;


    private void Start()
    {
        levelManager = FindObjectOfType<TreeLevelManager>();
        inWorkspace = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null) 
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
            if (collision.gameObject.transform.parent.name == "Level 3 Left 1")
            {
                inWorkspace = true;
                Debug.Log("In");
                levelManager.isSnapBlock[7] = true;
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Right 1")
            {
                inWorkspace = true;
                Debug.Log("In");
                levelManager.isSnapBlock[8] = true;
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Left 2")
            {
                inWorkspace = true;
                Debug.Log("In");
                levelManager.isSnapBlock[9] = true;
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Right 2")
            {
                inWorkspace = true;
                Debug.Log("In");
                levelManager.isSnapBlock[10] = true;
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Left 3")
            {
                inWorkspace = true;
                Debug.Log("In");
                levelManager.isSnapBlock[11] = true;
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Right 3")
            {
                inWorkspace = true;
                Debug.Log("In");
                levelManager.isSnapBlock[12] = true;
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Left 4")
            {
                inWorkspace = true;
                Debug.Log("In");
                levelManager.isSnapBlock[13] = true;
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Right 4")
            {
                inWorkspace = true;
                Debug.Log("In");
                levelManager.isSnapBlock[14] = true;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent.name == "Level 0")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[0] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 1 Left")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[1] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 1 Right")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[2] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Left 1")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[3] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Right 1")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[4] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Left 2")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[5] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Right 2")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[6] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Left 1")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[7] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Right 1")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[8] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Left 2")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[9] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Right 2")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[10] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Left 3")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[11] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Right 3")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[12] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Left 4")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[13] = false;
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Right 4")
        {
            Debug.Log("out");
            levelManager.isSnapBlock[14] = false;
        }
    }
}
