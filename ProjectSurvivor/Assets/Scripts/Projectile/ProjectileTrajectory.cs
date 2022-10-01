using UnityEngine;

public class ProjectileTrajectory : ProjectileBase
{
    [SerializeField]
    private float rotateSpeed = 270f;
    [SerializeField]
    private float time = 1f;
    [SerializeField]
    private float gravity = 9.8f;
    [SerializeField]
    private bool rotate;
    [SerializeField]
    private bool lookVelocityDirection;

    public Vector3 Velocity 
    {   
        get 
        { 
            return velocity; 
        } 
    }

    private Vector3 velocity;
    
    private void Update()
    {
        transform.position += velocity * Time.deltaTime * speed;
        velocity.y -= gravity * Time.deltaTime * speed;

        if (transform.position.y < 0)
        {
            gameObject.SetActive(false);
        }

        if (rotate)
        {
            transform.RotateAround(transform.position, transform.right, rotateSpeed * Time.deltaTime);
        }

        if (lookVelocityDirection)
        {
            Quaternion lookRotation = Quaternion.LookRotation(velocity, transform.position);
            Quaternion lerpRotation = Quaternion.Lerp(transform.rotation, lookRotation, 5f * Time.deltaTime);

            transform.rotation = lerpRotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(hitDetectTransform.position, hitDetectRadius, hitLayer);
        foreach (Collider collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                p_damage = Damage(p_damage, ref isHitCritical);
                DamagePopup(collider);
                damageable.TakeDamage(p_damage, isHitCritical, false);
                return;
            }
        }
    }

    public override void Fire(Vector3 origin, Vector3 target)
    {
        //define the distance x and y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.Normalize();
        distanceXZ.y = 0;

        //creating a float that represents our distance 
        float distY = distance.y;
        float distXZ = distance.magnitude;

        //calculating initial x velocity
        //Vx = x / t
        float Vxz = distXZ / time;

        ////calculating initial y velocity
        //Vy0 = y/t + 1/2 * g * t
        float Vy = distY / time + 0.5f * Mathf.Abs(gravity) * time;

        velocity = distanceXZ * Vxz;
        velocity.y = Vy;
    }
}
