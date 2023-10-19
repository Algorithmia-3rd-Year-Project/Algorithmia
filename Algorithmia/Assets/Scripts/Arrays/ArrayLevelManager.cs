using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayLevelManager : MonoBehaviour
{
    public List<GameObject> correctForms;

    public List<Transform> lineEndPoints;

    public List<GameObject> lines;

    public List<GameObject> blocks;

    public Dictionary<string, string> correctWires = new Dictionary<string, string>();

    public int blockCount;

    public List<GameObject> additionalSnapPositions;

    private void Start()
    {
        int correctPosCount = correctForms.Count;
        blockCount = 0;

    }

    private void Update()
    {
       
    }

}
