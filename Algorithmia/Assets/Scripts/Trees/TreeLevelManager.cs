using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLevelManager : MonoBehaviour
{

    public List<GameObject> snapPoints;

    public List<GameObject> dataSnapPoints;

    public List<bool> isSnapBlock = new List<bool>()
    {
        false, false, false, false, false, false, false, false, false, false, false, false, false, false, false
    };

    public List<bool> isNodeSnapped = new List<bool>()
    {
        false, false, false, false, false, false, false, false, false, false, false, false, false, false, false
    };



}
