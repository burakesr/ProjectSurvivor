using UnityEngine;

public class ProjectileBullet : ProjectileBase
{
    [SerializeField]
    private bool disableOnContact = false;
    [SerializeField]
    private float lifeTime = 3f;
    [SerializeField]
    private float spawnHeight = 0.3f;

    private float timer;

    private void OnEnable()
    {
        timer = lifeTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            DisableProjectile();
        }
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageAnEnemyOneTime(other);

        if (disableOnContact) 
        {
            DisableProjectile();
        }
    }

    public override void Fire(Vector3 start, Vector3 end)
    {
        p_moveDirection = (end - start).normalized;
        //transform.LookAt(new Vector3(p_moveDirection.x, transform.position.y, p_moveDirection.z));
        
        Quaternion lookRotation = Quaternion.LookRotation(p_moveDirection);
        transform.rotation = lookRotation;

        transform.position += Vector3.up * spawnHeight;

        // lookRotation = Quaternion.Euler(0, lookRotation.y, lookRotation.z);
        // transform.Rotate(lookRotation.eulerAngles, Space.World);
    }
}
