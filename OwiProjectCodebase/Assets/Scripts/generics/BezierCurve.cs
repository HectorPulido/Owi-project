using UnityEngine;
using System.Collections.Generic;


[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    public List<GameObject> controlPoints = new List<GameObject>();
    public Material material;
    public float scrollSpeed = 1;

    public int numberOfPoints = 20;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
    }


    void Update()
    {
        if (controlPoints.Count == 0)
        {
            return;
        }

        if (null == lineRenderer || controlPoints == null || controlPoints.Count < 3)
        {
            return; // not enough points specified
        }

        if (numberOfPoints < 2)
        {
            numberOfPoints = 2;
        }

        material.SetTextureOffset("_MainTex", new Vector2(Time.time * scrollSpeed, 0));
        lineRenderer.positionCount = numberOfPoints * (controlPoints.Count - 2);

        Vector3 p0, p1, p2;
        for (int j = 0; j < controlPoints.Count - 2; j++)
        {
            // check control points
            if (controlPoints[j] == null || controlPoints[j + 1] == null
            || controlPoints[j + 2] == null)
            {
                return;
            }
            // determine control points of segment
            p0 = 0.5f * (controlPoints[j].transform.position + controlPoints[j + 1].transform.position);
            p1 = controlPoints[j + 1].transform.position;
            p2 = 0.5f * (controlPoints[j + 1].transform.position + controlPoints[j + 2].transform.position);

            // set points of quadratic Bezier curve
            Vector3 position;
            float t;
            float pointStep = 1.0f / numberOfPoints;
            if (j == controlPoints.Count - 3)
            {
                pointStep = 1.0f / (numberOfPoints - 1.0f);
                // last point of last segment should reach p2
            }
            for (int i = 0; i < numberOfPoints; i++)
            {
                t = i * pointStep;
                position = (1.0f - t) * (1.0f - t) * p0
                + 2.0f * (1.0f - t) * t * p1 + t * t * p2;
                lineRenderer.SetPosition(i + j * numberOfPoints, position);
            }
        }
    }
}