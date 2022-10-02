using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialObject : MonoBehaviour
{
    [Range(0f, 100f)] public float spawnChance;

    private Health _health;

    private void Awake() {
        _health = GetComponent<Health>();
    }
}
