using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLevelManager : MonoBehaviour
{

   
    //Variable for data 
    public List<GameObject> dataSnapPoints;

    //Variables for tree nodes
    public List<GameObject> snapPoints;

    public List<bool> isSnapBlock = new List<bool>()
    {
        false, false, false, false, false, false, false, false, false, false, false, false, false, false, false
    };

    public List<bool> isNodeSnapped = new List<bool>()
    {
        false, false, false, false, false, false, false, false, false, false, false, false, false, false, false
    };

    //Variables for functions
    public List<GameObject> functionSnapPoints;

    public List<bool> isFunctionBlock = new List<bool>()
    {
        false, false, false, false
    };

    public List<bool> isFunctionSnapped = new List<bool>()
    {
        false, false, false, false
    };

    //Variables for lines
    public List<Transform> lineEndPoints;

    public List<GameObject> lines;



}
