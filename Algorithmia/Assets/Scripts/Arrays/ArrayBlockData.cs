using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Array Blocks", menuName = "Block Lists/Array Blocks List")]
public class ArrayBlockData : ScriptableObject
{

    [SerializeField]
    List<string> keys = new List<string>();

    [SerializeField]
    List<GameObject> values = new List<GameObject>();

    public List<string> Keys { get => keys; set => keys = value; }
    public List<GameObject> Values { get => values; set => values = value; }
}
