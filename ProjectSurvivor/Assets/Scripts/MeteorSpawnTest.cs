using UnityEngine;

public class MeteorSpawnTest : MonoBehaviour
{
    [SerializeField] 
    private GameObject meteorPrefab;
    [SerializeField] 
    private Vector3 targetPosition;
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private float meteorSpawnInterval = 8f;

    private Vector3 direction;
    private GameObject meteor;
    private float timer;

    private void Update()
    {
        if (timer <= 0f)
        {
            SpawnMeteor();
            timer = meteorSpawnInterval;
        }

        if (transform.position.y <= 0f)
        {
            meteor.SetActive(false);
        }

       meteor.transform.position += direction * speed * Time.deltaTime;
    }

    private void SpawnMeteor()
    {
        Vector3 spawnPos = transform.position + Vector3.up * 20f;

        Vector3 target = transform.position + new Vector3(Random.Range(targetPosition.x, -targetPosition.x),
            0f, Random.Range(targetPosition.z, -targetPosition.z));

        meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);

        target.y = 0f;

        direction = (target - spawnPos).normalized;
    }
}
