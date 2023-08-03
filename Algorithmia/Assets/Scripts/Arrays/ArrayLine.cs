using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class ArrayLine : MonoBehaviour
{
    public GameObject startPos;

    public GameObject endPos;

    public bool lineDrawn;

    public PolygonCollider2D lineCollider;

    public List<Vector2> colliderPoints = new List<Vector2>();

    public Vector3[] linePositions;

    public float lineWidth;

    [SerializeField]
    private LineRenderer lineRenderer;

    public string startPointName;
    public string endPointName;

    private List<Vector2> resetPathsForCollider = new List<Vector2>()
    {
        new Vector2(0f, 0f),
        new Vector2(0f, 0f),
        new Vector2(0f, 0f),
        new Vector2(0f, 0f)
    };

    private void Start()
    {
        lineDrawn = false;
        lineCollider = GetComponent<PolygonCollider2D>();
        lineCollider.SetPath(0, resetPathsForCollider);
        startPointName = "";
        endPointName = "";
    }

    private void Update()
    {

        if (lineDrawn && startPointName == "" && endPointName == "")
        {
            startPointName = startPos.transform.parent.parent.GetComponent<ArrayBlock>().blockName;

            if (endPos.name == "PC")
            {
                endPointName = "Computer";
            } else
            {
                endPointName = endPos.transform.parent.parent.GetComponent<ArrayBlock>().blockName;
            }
            
        } else if (lineDrawn == false)
        {
            startPointName = "";
            endPointName = "";
        }

        if (lineDrawn)
        {
            lineRenderer.SetPosition(0, new Vector3(startPos.transform.position.x, startPos.transform.position.y, 0f));
            lineRenderer.SetPosition(1, new Vector3(endPos.transform.position.x, endPos.transform.position.y, 0f));

            colliderPoints = CalculateColliderPoints(linePositions, lineWidth);
            lineCollider.SetPath(0, colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));

            linePositions = GetPositions();
            lineWidth = GetWidth();
        }
        
    }

    private List<Vector2> CalculateColliderPoints(Vector3[] positions, float width)
    {

        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        List<Vector2> colliderPositions = new List<Vector2>
        {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };

        return colliderPositions;
    }

    private Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        return positions;
    }

    private float GetWidth()
    {
        return lineRenderer.startWidth;
    }
}
