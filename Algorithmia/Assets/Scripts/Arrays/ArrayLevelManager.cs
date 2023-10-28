using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrayLevelManager : MonoBehaviour
{
    public List<GameObject> correctForms;

    public List<Transform> lineEndPoints;

    public List<GameObject> lines;

    public List<GameObject> blocks;

    public Dictionary<string, string> correctWires = new Dictionary<string, string>();

    public int blockCount;

    public List<GameObject> additionalSnapPositions;

    private int startingBlockCount;

    [SerializeField] private Transform dataParentObj;
    [SerializeField] private ArrayBlockManager blockManager;
    
    [SerializeField] private TMP_Text blockCountText;
    
    private void Start()
    {
        int correctPosCount = correctForms.Count;
        startingBlockCount = blockCount;

    }

    private void Update()
    {
        blockCountText.text = blockCount.ToString();
    }

    public void ResetLevel()
    {
        correctForms.Clear();
        
        //Remove all line end points except PC
        for (int i = 0; i < lineEndPoints.Count; i++)
        {
            if (lineEndPoints[i].name != "PC")
            {
                lineEndPoints.Remove(lineEndPoints[i]);
            }
        }
        
        lines.Clear();
        
        //Check through blocks list to remove the assigned pseudo codes to them
        for (int i = 0; i < blocks.Count; i++)
        {
            Debug.Log(blocks.Count);
            GameObject pseudoCodeObj = blocks[i].GetComponent<ArrayBlock>().pseudoElement;
            Destroy(pseudoCodeObj);
            
            //Reset data blocks assigned to blocks
            Transform snapPointParent = blocks[i].transform.Find("Snap Points");
            for (int j = 0; j < snapPointParent.childCount; j++)
            {
                Transform singleSnapPoint = snapPointParent.GetChild(j);
                if (singleSnapPoint.childCount != 0)
                {
                    GameObject dataObject = singleSnapPoint.GetChild(0).gameObject;
                    Vector3 currentResetPos = dataObject.GetComponent<DataBlock>().resetPosition;
                    dataObject.transform.position = new Vector3(currentResetPos.x, currentResetPos.y, currentResetPos.z);
                    dataObject.GetComponent<DataBlock>().snapped = false;
                    Destroy(dataObject.GetComponent<DataBlock>().pseudoElement);
                    dataObject.transform.SetParent(dataParentObj);
                    blockManager.ChangeBlockLayer(dataObject.transform, "Data");
                    dataObject.transform.localScale = dataObject.GetComponent<DataBlock>().originalScale;
                }
                
            }
            
            Destroy(blocks[i]);
            //blocks.Remove(blocks[i]);
            
            
        }
        
        blocks.Clear();
        blockCount = startingBlockCount;
    }

}
