using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDrop : MonoBehaviour
{
    public string data;

    public List<GameObject> correctForms;

    [SerializeField]
    private bool moving;

    private float startPosX;

    private float startPosY;

    private Vector3 resetPosition;

    [SerializeField]
    private float snapRadius = 1f;

    [SerializeField]
    private GameObject levelManager;

    private void Start()
    {
        resetPosition = this.transform.position;
    }

    private void Update()
    {
        if (moving)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.position.z);

        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.position.x;
            startPosY = mousePos.y - this.transform.position.y;

            moving = true;

        }
    }

    private void OnMouseUp()
    {
        moving = false;
        bool snapped = false;

        for (int i = 0; i < correctForms.Count; i++)
        {
            if ((Mathf.Abs(this.transform.position.x - correctForms[i].transform.position.x) <= snapRadius &&
            Mathf.Abs(this.transform.position.y - correctForms[i].transform.position.y) <= snapRadius) &&
            correctForms[i].transform.childCount == 0)
            {
                this.transform.position = new Vector3(correctForms[i].transform.position.x, correctForms[i].transform.position.y, correctForms[i].transform.position.z);
                snapped = true;

                //make the data element a child of the snapped point
                transform.SetParent(correctForms[i].transform);

                break;
            }
        }

        if (!snapped)
        {
            this.transform.position = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
            transform.SetParent(null);
        }


    }

}
