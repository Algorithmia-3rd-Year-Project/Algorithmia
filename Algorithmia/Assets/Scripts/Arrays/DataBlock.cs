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

    [SerializeField]
    private bool setTriggers;

    [SerializeField]
    private GameObject guideMark;

    public GameObject pseudoElement;

    private void Awake()
    {
        originalScale = transform.localScale;
        resetPosition = transform.position;
        snapped = false;
    }

    private void Update()
    {
        if (guideMark != null && setTriggers)
        {
            if (this.transform.position != resetPosition)
            {
                guideMark.SetActive(false);
            } else
            {
                guideMark.SetActive(true);
            }
        }
    }

}
