using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayLevel3 : MonoBehaviour
{
    [SerializeField]
    private GameObject startingTransition;

    private void Start()
    {
        startingTransition.SetActive(true);
        StartCoroutine(DisableStartTransition());
    }

    private IEnumerator DisableStartTransition()
    {
        yield return new WaitForSeconds(1f);
        startingTransition.SetActive(false);
    }
}
