using UnityEngine;

public class ProjectileTrajectoryMove : MonoBehaviour
{
    public float speed = 1f;
    public float time = 1f;
    public float gravity = 18f;
    
    private Vector3 velocity;

    private void Update()
    {
        transform.position += velocity * Time.deltaTime * speed;
        velocity.y -= gravity * Time.deltaTime * speed;
    }


    public void CalculateVelocity(Vector3 target, Vector3 origin)
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
