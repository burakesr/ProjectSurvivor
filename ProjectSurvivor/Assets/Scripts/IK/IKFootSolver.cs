using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField]
    private Transform bodyTransform;
    [SerializeField]
    private Transform rayOrigin;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private GameObject ikTarget;
    [Header("VARIABLES")]
    [SerializeField]
    private float ikOffset = 1.0f;
    [SerializeField]
    private float tipMoveDist = 1.4f;
    [SerializeField]
    private float tipAnimationTime = 0.15f;
    [SerializeField]
    private float tipMaxHeight = 0.2f;
    [SerializeField]
    private float maxRayDist = 7f;
    [SerializeField]
    private AnimationCurve speedCurve;
    [SerializeField]
    private AnimationCurve heightCurve;

    private float m_tipAnimationFrameTime = 1f / 60;

    public Vector3 TipPos { get; private set; }
    public Vector3 RaycastTipPos { get; private set; }
    public Vector3 RaycastTipNormal { get; private set; }
    public Vector3 TipUpDir { get; private set; }

    public float TipDistance { get; private set; }
    public bool Animating { get; private set; }
    public bool Moveable { get; set;}


    private void Start()
    {
        rayOrigin.parent = bodyTransform;
        TipPos = ikTarget.transform.position;

        UpdateIKTargetTransform();
    }


    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin.position, bodyTransform.up.normalized * -1, out hit, maxRayDist))
        {
            RaycastTipPos = hit.point;
            RaycastTipNormal = hit.normal;
            //Debug.DrawLine(rayOrigin.position, hit.point, Color.magenta);
        }

        TipDistance = (RaycastTipPos - TipPos).magnitude;

        //Debug.Log(TipDistance > tipMoveDist);
        //if (TipDistance > tipMoveDist)
        //{
        //    ikTarget.transform.position = RaycastTipPos + bodyTransform.up.normalized * ikOffset;
        //}


        if (!Animating && (TipDistance > tipMoveDist))
        {
            StartCoroutine(AnimateLeg());
        }
    }

    private void UpdateIKTargetTransform()
    {
        ikTarget.transform.position = TipPos + bodyTransform.up.normalized * ikOffset;
        ikTarget.transform.rotation = Quaternion.LookRotation(TipPos - ikTarget.transform.position); //* Quaternion.Euler(0, 0, 0);
    }


    //IEnumerator MoveLeg()
    //{
    //    float timer = 0.0f;
    //    float animTime;
    //    Vector3 startTipPos = TipPos;
    //}

    private IEnumerator AnimateLeg()
    {
        Animating = true;

        float timer = 0.0f;
        float animTime;

        Vector3 startTipPos = TipPos;
        Vector3 tipDir = RaycastTipPos - TipPos;
        float tipPassOver = tipMoveDist / 2.0f;

        tipDir += tipDir.normalized * tipPassOver;

        Vector3 right = Vector3.Cross(bodyTransform.up, tipDir.normalized).normalized;
        TipUpDir = Vector3.Cross(tipDir.normalized, right);

        while (timer < tipAnimationTime + m_tipAnimationFrameTime)
        {
            animTime = speedCurve.Evaluate(timer / tipAnimationTime);

            float tipAcceleration = Mathf.Max((RaycastTipPos - startTipPos).magnitude / tipDir.magnitude, 1.0f);

            TipPos = startTipPos + tipDir * tipAcceleration * animTime;
            TipPos += TipUpDir * heightCurve.Evaluate(animTime) * tipMaxHeight;

            UpdateIKTargetTransform();

            timer += m_tipAnimationFrameTime;

            yield return new WaitForSeconds(m_tipAnimationFrameTime);
        }

        Animating = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(RaycastTipPos, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(TipPos, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(TipPos, RaycastTipPos);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(ikTarget.transform.position, 0.1f);
    }
}
