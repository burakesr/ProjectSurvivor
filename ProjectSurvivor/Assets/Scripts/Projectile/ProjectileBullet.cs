using UnityEngine;

public class ProjectileBullet : ProjectileBase
{
    [SerializeField] 
    private bool disableOnContact = false;
    [SerializeField]
    private float lifeTime = 3f;

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
        transform.position += moveDirection * speed * Time.deltaTime;
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
        moveDirection = (end - start).normalized;
        transform.rotation = Quaternion.LookRotation(moveDirection);
    }
}
