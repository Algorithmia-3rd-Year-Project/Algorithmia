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
        //Changed to Box Colliders instead of box collider 2D after adding 3D models
        if (!simulation.anyMenuOpened)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        } else
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
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
