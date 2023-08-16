using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{

    [SerializeField]
    private SimManager simulation;

    [SerializeField]
    private GameObject UIMenu;

    private void Update()
    {
        if (!simulation.anyMenuOpened)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        } else
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnMouseEnter()
    {
        simulation.ChangeMouseCursor(true);
    }

    private void OnMouseExit()
    {
        simulation.ChangeMouseCursor(false);
    }

    private void OnMouseDown()
    {
        simulation.anyMenuOpened = true;
        UIMenu.SetActive(true);
    }

}
