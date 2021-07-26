using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    [SerializeField] List<Transform> _objects = new List<Transform>();
    Mesh _mesh;
    MeshFilter _meshFilter;
    Vector3[] _vertex;
    List<int> _triangles;

    public void Setup()
    {
        _mesh = new Mesh();
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;
    }
    
    public void CreateMesh(bool p_useHandCreated) 
    {
        ClearMesh();
        if (p_useHandCreated) 
        {
            HandCreatedMesh();
        }
        else
        {
            ObjectCreatedMesh();
        }
        _mesh.vertices = _vertex;
        _mesh.triangles = _triangles.ToArray();
    }

    public void ClearMesh()
    {
        _mesh.Clear();
    }

    void HandCreatedMesh()
    {
        _vertex = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 0, 0)
        };

        _triangles = new List<int>()
        {
            0, 1, 2, // Clockwise triangle.
            // 2, 1, 0 // Counter clockwise triangle.
        };
    }

    void ObjectCreatedMesh() 
    {
        _vertex = _objects.ConvertAll(__object => __object.position).ToArray();
        
        _triangles = new List<int>();
        for (int i = 0; i < _objects.Count - 2; i++)
        {
            // Direction triangle.
            _triangles.Add(i);
            _triangles.Add(i+1);
            _triangles.Add(i+2);
            
            // Counter direction triangle.
            // _triangles.Add(i+2);
            // _triangles.Add(i+1);
            // _triangles.Add(i);
        }
    }
}
