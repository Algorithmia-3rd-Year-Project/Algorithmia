using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBlock : MonoBehaviour
{
    public bool inWorkspace;

    public string blockName;

    public string snappedLevel;

    public bool snapped;

    [SerializeField]
    private TreeLevelManager levelManager;


    private void Start()
    {
        levelManager = FindAnyObjectByType<TreeLevelManager>();
        inWorkspace = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null) 
        {
            if (collision.gameObject.transform.parent.name == "Level 0" && levelManager.isNodeSnapped[0] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[0] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 1 Left" && levelManager.isNodeSnapped[1] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[1] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 1 Right" && levelManager.isNodeSnapped[2] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[2] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 2 Left 1" && levelManager.isNodeSnapped[3] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[3] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 2 Right 1" && levelManager.isNodeSnapped[4] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[4] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 2 Left 2" && levelManager.isNodeSnapped[5] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[5] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 2 Right 2" && levelManager.isNodeSnapped[6] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[6] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Left 1" && levelManager.isNodeSnapped[7] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[7] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Right 1" && levelManager.isNodeSnapped[8] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[8] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Left 2" && levelManager.isNodeSnapped[9] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[9] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Right 2" && levelManager.isNodeSnapped[10] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[10] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Left 3" && levelManager.isNodeSnapped[11] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[11] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Right 3" && levelManager.isNodeSnapped[12] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[12] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
            if (collision.gameObject.transform.parent.name == "Level 3 Left 4" && levelManager.isNodeSnapped[13] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[13] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            } 
            if (collision.gameObject.transform.parent.name == "Level 3 Right 4" && levelManager.isNodeSnapped[14] == false)
            {
                inWorkspace = true;
                levelManager.isSnapBlock[14] = true;
                collision.gameObject.transform.Find("Shade").gameObject.SetActive(true);
            }
        }
        
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent.name == "Level 0")
        {
            levelManager.isSnapBlock[0] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 1 Left")
        {
            levelManager.isSnapBlock[1] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 1 Right")
        {
            levelManager.isSnapBlock[2] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Left 1")
        {
            levelManager.isSnapBlock[3] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Right 1")
        {
            levelManager.isSnapBlock[4] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Left 2")
        {
            levelManager.isSnapBlock[5] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 2 Right 2")
        {
            levelManager.isSnapBlock[6] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Left 1")
        {
            levelManager.isSnapBlock[7] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Right 1")
        {
            levelManager.isSnapBlock[8] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Left 2")
        {
            levelManager.isSnapBlock[9] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Right 2")
        {
            levelManager.isSnapBlock[10] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Left 3")
        {
            levelManager.isSnapBlock[11] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Right 3")
        {
            levelManager.isSnapBlock[12] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Left 4")
        {
            levelManager.isSnapBlock[13] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
        if (collision.gameObject.transform.parent.name == "Level 3 Right 4")
        {
            levelManager.isSnapBlock[14] = false;
            collision.gameObject.transform.Find("Shade").gameObject.SetActive(false);
        }
    }
}
