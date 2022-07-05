using System.Collections.Generic;
using UnityEngine;
public class TestGenerate : MonoBehaviour
{
    public int size = 10;
    public Vector2 regionSize = Vector2.one;
    public float displayRadius = 0.1f;
    List<Vector2> points;
    int numSamplesBeforeRejection = 50;
    public float radius;
    
    private void OnValidate()
    {
        radius =Mathf.Sqrt(regionSize.x * regionSize.y / size * Mathf.PI);
        points = Utility.GeneratePoints(size, regionSize,numSamplesBeforeRejection);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(regionSize/2,regionSize);
        if (points == null) return;
        for (int i = 0; i < size; i++)
        {
            Gizmos.DrawSphere(points[i],displayRadius);
        }
    }
}
