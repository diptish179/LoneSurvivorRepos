using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int poolSize = 10;
    public float projectileLifetime = 5f; // Lifetime of projectiles

    private List<GameObject> pooledProjectiles = new List<GameObject>();

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            pooledProjectiles.Add(projectile);
        }
    }

    public GameObject GetPooledProjectile()
    {
        for (int i = 0; i < pooledProjectiles.Count; i++)
        {
            if (!pooledProjectiles[i].activeInHierarchy)
            {
                StartCoroutine(DeactivateAfterTime(pooledProjectiles[i])); // deactivate after a certain time
                return pooledProjectiles[i];
            }
        }

        // If no inactive object is found, instantiate a new one
        GameObject newProjectile = Instantiate(projectilePrefab);
        newProjectile.SetActive(false);
        pooledProjectiles.Add(newProjectile);
        StartCoroutine(DeactivateAfterTime(newProjectile)); 

        return newProjectile;
    }

    IEnumerator DeactivateAfterTime(GameObject projectile)
    {
        yield return new WaitForSeconds(projectileLifetime);
        projectile.SetActive(false);
    }
}
