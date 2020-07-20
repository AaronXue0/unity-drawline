using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour
{
    public GameObject target;
    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPos;

	private void Start() {
		CreateLineOnTarget();
	}

    private void Update()
    {
		UpdateLine(target.transform.position);
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (Vector2.Distance(tempFingerPos, fingerPos[fingerPos.Count - 1]) > .1f)
            {
                UpdateLine(tempFingerPos);
            }
        }
    }

    void CreateLineOnTarget()
    {
        currentLine = Instantiate(linePrefab, target.transform);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        fingerPos.Clear();
        fingerPos.Add(target.transform.position);
        fingerPos.Add(target.transform.position);
        lineRenderer.SetPosition(0, fingerPos[0]);
        lineRenderer.SetPosition(1, fingerPos[1]);
        edgeCollider.points = fingerPos.ToArray();
    }

    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        fingerPos.Clear();
        fingerPos.Add(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
        fingerPos.Add(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
        lineRenderer.SetPosition(0, fingerPos[0]);
        lineRenderer.SetPosition(1, fingerPos[1]);
        edgeCollider.points = fingerPos.ToArray();
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPos.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPos.ToArray();
    }
}