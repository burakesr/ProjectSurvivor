using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDrawer : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private Color gizmoColor;


    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
