using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLDataBlock : MonoBehaviour
{
    public Vector3 resetPosition;

    public float snapRadius;

    public bool snapped;

    public string dataValue;

    public Vector3 originalScale;

    public string blockName;

    public string pseudoCode;

    public GameObject pseudoElement;

    [SerializeField]
    private bool setTriggers;

    [SerializeField]
    private GameObject guideMark;

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
            }
            else
            {
                guideMark.SetActive(true);
            }
        }
    }
}
