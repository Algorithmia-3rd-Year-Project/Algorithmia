using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedListLevelManager : MonoBehaviour
{
    public List<GameObject> correctForms;

    public List<Transform> lineEndPoints;

    public List<GameObject> lines;

    public Dictionary<string, string> correctWires = new Dictionary<string, string>();

    public int blockCount;

    public List<GameObject> additionalSnapPositions;

    public List<Transform> functionSnapPoints;

    private void Start()
    {
        int correctPosCount = correctForms.Count;
        blockCount = 0;

    }

    private void Update()
    {

    }
}
