using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererConnectObjects : MonoBehaviour
{
    [SerializeField] List<Transform> _objects = new List<Transform>();
    LineRenderer _lineRenderer;
    
    void Setup() 
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _objects.Count;
    }

    void Awake() 
    {
        Setup();
    }

    void Update() 
    {
        _lineRenderer.SetPositions(_objects.ConvertAll(__object => __object.position).ToArray());
    }
}
