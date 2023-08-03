using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNodeLevelManager : MonoBehaviour
{
    public List<GameObject> correctForms;

    public List<Transform> lineEndPoints;

    public List<GameObject> lines;

    public Dictionary<string, string> correctWires = new Dictionary<string, string>();

    public int blockCount;

    private void Start()
    {
        int correctPosCount = correctForms.Count;
        blockCount = 0;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Run();
        }
    }

    public void Run()
    {
        if (correctForms[0].transform.childCount > 0 && correctForms[0].transform.GetChild(0).GetComponent<DragnDrop>().data == "a")
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Failure");
        }
    }
}
