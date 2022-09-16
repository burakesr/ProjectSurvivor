using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastMove : MonoBehaviour
{
    [SerializeField]
    private Transform raycastPoint;
    [SerializeField]
    private Color debugColor;

    void Update()
    {
        if (Physics.Raycast(raycastPoint.position, -Vector3.up, out RaycastHit hit, 100f))
        {
            transform.position = hit.point;
            transform.rotation = Quaternion.FromToRotation(transform.forward, hit.normal);

            Debug.DrawLine(transform.position, hit.point, debugColor);
        }
    }
}
