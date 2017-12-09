using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBoundary : MonoBehaviour {
    private Color gizmosColor = Color.red;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
