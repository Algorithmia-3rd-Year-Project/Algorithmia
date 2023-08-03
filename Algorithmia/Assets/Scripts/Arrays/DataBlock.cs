using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBlock : MonoBehaviour
{

    public Vector3 resetPosition;

    public float snapRadius;

    public bool snapped;

    public string dataValue;

    public Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
        resetPosition = transform.position;
        snapped = false;
    }

}
