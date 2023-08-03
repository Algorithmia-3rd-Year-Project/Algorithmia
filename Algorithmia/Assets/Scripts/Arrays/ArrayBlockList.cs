using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayBlockList : MonoBehaviour, ISerializationCallbackReceiver
{

    [SerializeField]
    private ArrayBlockData blockData;

    [SerializeField]
    private List<string> keys = new List<string>();

    [SerializeField]
    private List<GameObject> values = new List<GameObject>();

    public Dictionary<string, GameObject> blockList = new Dictionary<string, GameObject>();

    private void Start()
    {
        //PrintDictionary();
    }

    public void OnBeforeSerialize()
    {

        keys.Clear();
        values.Clear();
        for (int i=0; i < Mathf.Min(blockData.Keys.Count, blockData.Values.Count); i++)
        {
            keys.Add(blockData.Keys[i]);
            values.Add(blockData.Values[i]);
        }

    }

    public void OnAfterDeserialize()
    {
        blockList = new Dictionary<string, GameObject>();

        for (int i = 0; i != Mathf.Min(keys.Count, values.Count); i++)
            blockList.Add(keys[i], values[i]);
    }

    /*
    public void PrintDictionary()
    {
        foreach (var pair in blockList)
        {
            Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
        }

        Debug.Log(blockList.Count);
    }*/
}
