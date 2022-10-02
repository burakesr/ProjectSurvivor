using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] materialSpawnPoints;
    [SerializeField]
    private MaterialObject[] materialObjects;

    private void Start() {
        SpawnMaterialObjects();
    }

    private void SpawnMaterialObjects()
    {
        for (int i = 0; i < materialSpawnPoints.Length; i++)
        {    
            MaterialObject selectedMaterial = null;

            foreach (var material in materialObjects)
            {
                float random = UnityEngine.Random.Range(0f, 100f);
                if (random < material.spawnChance){
                    selectedMaterial = material;
                }
            }

            if (selectedMaterial == null) 
                selectedMaterial = materialObjects[0];

            Instantiate(selectedMaterial.gameObject, materialSpawnPoints[i]);
        }
    }
}
