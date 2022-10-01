using System.Collections.Generic;
using UnityEngine;

public class DropItemOnDestroy : MonoBehaviour
{
    [SerializeField] private List<PickupableItems> items = new List<PickupableItems>();
    [SerializeField] private Transform[] dropItemTransforms;

    private List<PickupableItems> selectedItems = new List<PickupableItems>();

    public void DropItem()
    {
        foreach (var item in items)
        {
            float rng = Random.Range(0f, 100f);
            if (item.chanceToDrop >= rng)
            {
                selectedItems.Add(item);
            }
        }

        if (selectedItems.Count > 0)
        {
            for (int i = 0; i < selectedItems.Count; i++)
            {
                Vector3 pos = transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                pos.y = selectedItems[i].spawnYVector;
                GameObject droppedItem = PoolManager.Instance.SpawnFromPool(selectedItems[i].prefab, pos, Quaternion.identity);
                selectedItems.Remove(selectedItems[i]);
            }
        }
    }
}

[System.Serializable]
public struct PickupableItems
{
    public GameObject prefab;
    [Range(0f, 100f)]
    public float chanceToDrop;
    public float spawnYVector;
}