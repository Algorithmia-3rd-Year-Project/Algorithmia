using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 dragOrigin;

    [SerializeField]
    private SpriteRenderer editorBackground;

    private float editorMinX, editorMaxX, editorMinY, editorMaxY;


    public float minZoom = 2f;
    public float maxZoom = 5f;
    public float zoomSpeed = 2f;

    private void Awake()
    {
        editorMinX = editorBackground.transform.position.x - editorBackground.bounds.size.x / 2f;
        editorMaxX = editorBackground.transform.position.x + editorBackground.bounds.size.x / 2f;

        editorMinY = editorBackground.transform.position.y - editorBackground.bounds.size.y / 2f;
        editorMaxY = editorBackground.transform.position.y + editorBackground.bounds.size.y / 2f;
    }

    private void Update()
    {
        PanCamera();

        ZoomCamera();
    }

    private void PanCamera()
    {

        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }

    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = editorMinX + camWidth;
        float maxX = editorMaxX - camWidth;
        float minY = editorMinY + camHeight;
        float maxY = editorMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }

    private void ZoomCamera()
    {
        float newZoom = cam.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

        cam.orthographicSize = newZoom;
    }
}
