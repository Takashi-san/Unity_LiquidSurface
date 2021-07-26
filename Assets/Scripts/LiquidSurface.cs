using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LiquidSurface : MonoBehaviour
{
    [SerializeField] Vector2 _size = Vector2.one;

    [Header("Surface")]
    [SerializeField] float _surfaceNodeDensity = 10;
    [SerializeField] float _surfaceSpring = 1;
    [SerializeField] float _surfaceDamping = 1;
    [SerializeField] float _surfaceSpread = 1;
    [SerializeField] float _testVelocity = 1;

    LineRenderer _lineRenderer;
    Vector3[] _surfaceNodesPosition;
    float[] _surfaceNodesVelocity;
    float[] _surfaceNodesAcceleration;

    void Awake() 
    {
        Setup();
        CreateSurface();
    }

    void FixedUpdate() 
    {
        UpdateSurface();
    }
    
    void Setup()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void CreateSurface() 
    {
        _surfaceNodesPosition = new Vector3[Mathf.RoundToInt(_size.x * _surfaceNodeDensity) + 1];
        _surfaceNodesVelocity = new float[_surfaceNodesPosition.Length];
        _surfaceNodesAcceleration = new float[_surfaceNodesPosition.Length];
        
        float __nodeWidthStep = _size.x / (_surfaceNodesPosition.Length - 1);
        Vector3 __startPosition = transform.position - Vector3.right * 0.5f * _size.x;
        for (int i = 0; i < _surfaceNodesPosition.Length; i++) 
        {
            _surfaceNodesPosition[i] = __startPosition + Vector3.right * __nodeWidthStep * i;
            _surfaceNodesVelocity[i] = 0;
            _surfaceNodesAcceleration[i] = 0;
        }
        
        _lineRenderer.positionCount = _surfaceNodesPosition.Length;
        _lineRenderer.SetPositions(_surfaceNodesPosition);
    }

    void UpdateSurface() 
    {
        float[] __forceSpring = new float[_surfaceNodesPosition.Length];
        float[] __forceDamping = new float[_surfaceNodesPosition.Length];
        float[] __leftDeltaForce = new float[_surfaceNodesPosition.Length];
        float[] __rightDeltaForce = new float[_surfaceNodesPosition.Length];
        for (int i = 0; i < _surfaceNodesPosition.Length; i++)
        {
            // Spring and damper force.
            __forceSpring[i] = - _surfaceSpring * (_surfaceNodesPosition[i].y - transform.position.y);
            __forceDamping[i] = - _surfaceDamping * _surfaceNodesVelocity[i];
            
            // Wave propagation force.
            if (i > 0) 
            {
                __leftDeltaForce[i] = _surfaceSpread * (_surfaceNodesPosition[i].y - _surfaceNodesPosition[i - 1].y);
            }
            if (i < _surfaceNodesPosition.Length - 1) 
            {
                __rightDeltaForce[i] = _surfaceSpread * (_surfaceNodesPosition[i].y - _surfaceNodesPosition[i + 1].y);
            }
        }

        for (int i = 0; i < _surfaceNodesPosition.Length; i++)
        {
            float __force = __forceSpring[i] + __forceDamping[i];
            if (i > 0) 
            {
                __force += __rightDeltaForce[i - 1];
            }
            if (i < _surfaceNodesPosition.Length - 1) 
            {
                __force += __leftDeltaForce[i + 1];
            }

            _surfaceNodesAcceleration[i] = __force; // Considering a mass of 1.
            _surfaceNodesPosition[i].y += _surfaceNodesVelocity[i] * Time.fixedDeltaTime;
            _surfaceNodesVelocity[i] += _surfaceNodesAcceleration[i] * Time.fixedDeltaTime;
        }
        _lineRenderer.SetPositions(_surfaceNodesPosition);
    }

    public void Test() 
    {
        _surfaceNodesVelocity[_surfaceNodesVelocity.Length / 2] = -_testVelocity;
    }
}
