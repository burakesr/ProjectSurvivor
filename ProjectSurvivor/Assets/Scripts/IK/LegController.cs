using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private IKFootSolver[] ikLegs;

    private float maxTipWait = 0.7f;

    private bool readySwitchOrder = false;
    private bool stepOrder = true;
    private float bodyHeightBase = 1.3f;

    private Vector3 bodyPos;
    private Vector3 bodyUp;
    private Vector3 bodyForward;
    private Vector3 bodyRight;
    private Quaternion bodyRotation;

    private float PosAdjustRatio = 0.1f;
    private float RotAdjustRatio = 0.2f;

    private void Start()
    {
        // Start coroutine to adjust body transform
        StartCoroutine(AdjustBodyTransform());
    }

    private void Update()
    {
        if (ikLegs.Length < 2) return;

        // If tip is not in current order but it's too far from target position, Switch the order
        for (int i = 0; i < ikLegs.Length; i++)
        {
            if (ikLegs[i].TipDistance > maxTipWait)
            {
                stepOrder = i % 2 == 0;
                break;
            }
        }

        // Ordering steps
        foreach (IKFootSolver ikLeg in ikLegs)
        {
            ikLeg.Moveable = stepOrder;
            stepOrder = !stepOrder;
        }

        int index = stepOrder ? 0 : 1;

        // If the opposite foot step completes, switch the order to make a new step
        if (readySwitchOrder && !ikLegs[index].Animating)
        {
            stepOrder = !stepOrder;
            readySwitchOrder = false;
        }

        if (!readySwitchOrder && ikLegs[index].Animating)
        {
            readySwitchOrder = true;
        }
    }

    private IEnumerator AdjustBodyTransform()
    {
        while (true)
        {
            Vector3 tipCenter = Vector3.zero;
            bodyUp = Vector3.zero;

            // Collect leg information to calculate body transform
            foreach (IKFootSolver ikLeg in ikLegs)
            {
                tipCenter += ikLeg.TipPos;
                bodyUp += ikLeg.TipUpDir + ikLeg.RaycastTipNormal;
            }

            RaycastHit hit;
            if (Physics.Raycast(bodyTransform.position, bodyTransform.up * -1, out hit, 10.0f))
            {
                bodyUp += hit.normal;
            }

            tipCenter /= ikLegs.Length;
            bodyUp.Normalize();

            // Interpolate postition from old to new
            bodyPos = tipCenter + bodyUp * bodyHeightBase;
            bodyTransform.position = Vector3.Lerp(bodyTransform.position, bodyPos, PosAdjustRatio);

            // Calculate new body axis
            bodyRight = Vector3.Cross(bodyUp, bodyTransform.forward);
            bodyForward = Vector3.Cross(bodyRight, bodyUp);

            // Interpolate rotation from old to new
            bodyRotation = Quaternion.LookRotation(bodyForward, bodyUp);
            bodyTransform.rotation = Quaternion.Slerp(bodyTransform.rotation, bodyRotation, RotAdjustRatio);

            yield return new WaitForFixedUpdate();
        }
    }
}
