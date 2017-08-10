using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mspaint : MonoBehaviour
{
    private Color paintColor = Color.red;
    private float paintSize = 0.1f;
    private LineRenderer currentLine;
    public Material material;
    private List<Vector3> positions = new List<Vector3>();
    private bool isMouseDown = false;
    private float lineDistance = 0.1f;

    private void Update()

    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            InitLineRenderer();
            AddPosition();
            lineDistance += 0.1f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            positions.Clear();
        }

        if (isMouseDown)
        {
            AddPosition();
        }
    }

    private void InitLineRenderer()
    {
        GameObject go = new GameObject();
        go.transform.SetParent(transform);
        currentLine = go.AddComponent<LineRenderer>();

        currentLine.material = material;

        currentLine.startWidth = paintSize;
        currentLine.endWidth = paintSize;

        currentLine.startColor = paintColor;
        currentLine.endColor = paintColor;

        currentLine.numCapVertices = 5;
        currentLine.numCornerVertices = 5;
    }

    private void AddPosition()
    {
        Vector3 pos = GetMousePoint();
        pos.z -= lineDistance;
        if (positions.Count == 0 || Vector3.Distance(positions[positions.Count - 1], pos) > 0.1f)
        {
            positions.Add(pos);
            currentLine.positionCount = positions.Count;
            currentLine.SetPositions(positions.ToArray());
        }
    }

    private Vector3 GetMousePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            return hit.point;
        return Vector3.zero;
    }

    #region ColorToggleEvent

    public void OnRedColorChanged(bool isOn)
    {
        if (isOn)
        {
            paintColor = Color.red;
        }
    }

    public void OnGreenColorChanged(bool isOn)
    {
        if (isOn)
        {
            paintColor = Color.green;
        }
    }

    public void OnBlueColorChanged(bool isOn)
    {
        if (isOn)
        {
            paintColor = Color.blue;
        }
    }

    #endregion ColorToggleEvent

    #region SizeToggleEvent

    public void OnPoint1Changed(bool isOn)
    {
        if (isOn)
        {
            paintSize = 0.1f;
        }
    }

    public void OnPoint2Changed(bool isOn)
    {
        if (isOn)
        {
            paintSize = 0.2f;
        }
    }

    public void OnPoint4Changed(bool isOn)
    {
        if (isOn)
        {
            paintSize = 0.4f;
        }
    }

    #endregion SizeToggleEvent
}