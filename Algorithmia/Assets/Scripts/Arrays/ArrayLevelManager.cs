using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayLevelManager : MonoBehaviour
{

    public List<GameObject> correctForms;

    public Dictionary<string, string> correctWires = new Dictionary<string, string>();

    private void Start()
    {
        int correctPosCount = correctForms.Count;


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
