using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackLevelManager : MonoBehaviour
{
    public List<GameObject> snapPoints;

    public List<Transform> lineEndPoints;

    public List<GameObject> lines;

    public List<GameObject> blocks;


    public int blockCount;


    private void Start()
    {
        int correctPosCount = snapPoints.Count;
        blockCount = 0;
    }

    private void Update()
    {

    }

}
