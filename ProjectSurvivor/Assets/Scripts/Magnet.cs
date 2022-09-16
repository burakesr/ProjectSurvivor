using UnityEngine;
using DG.Tweening;

public class Magnet : MonoBehaviour
{
    [SerializeField]
    private float magnetRadius = 5f;
    [SerializeField]
    private float areaCheckInterval = 0.25f;
    [SerializeField]
    private float pickupTravelTime = 0.25f;
    [SerializeField]
    private LayerMask targetLayer;

    private float timer;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            CheckForPickupableItems();
            timer = areaCheckInterval;
        }
    }

    private void CheckForPickupableItems()
    {
        Collider[] pickups = Physics.OverlapSphere(transform.position, magnetRadius, targetLayer);

        for (int i = 0; i < pickups.Length; i++)
        {
            pickups[i].transform.DOMove(transform.position, pickupTravelTime, false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}