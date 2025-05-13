using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleBoundaryDrawer : MonoBehaviour
{
    public Transform circleObject; // The object representing the circle must be added in the inspector
    public int segmentCount = 100;
    public float yHeight = 0.01f; // Height added so it is displayed above the ground
    private LineRenderer lr;
    public Material lineMaterial;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        lr.positionCount = segmentCount + 1;
        //lr.material = new Material(Shader.Find("Unlit/Color"));
        //lr.material.color = Color.red;
        lr.material = lineMaterial;

        lr.widthMultiplier = 0.2f;
        DrawCircleBoundary();
    }

    void DrawCircleBoundary()
    {
        if (circleObject == null)
            return;

        Vector3 center = circleObject.position;
        float radius = circleObject.localScale.x * 0.5f; // Using X scale as the diameter

        for (int i = 0; i <= segmentCount; i++)
        {
            float angle = i * Mathf.PI * 2 / segmentCount;
            float x = Mathf.Cos(angle) * radius + center.x;
            float z = Mathf.Sin(angle) * radius + center.z;
            lr.SetPosition(i, new Vector3(x, yHeight, z));
        }
    }
}
