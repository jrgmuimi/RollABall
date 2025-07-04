using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPickup : MonoBehaviour
{

    public GameObject pickup;
    public GameObject wall;
    public GameObject player;
    public GameObject enemy;

    public int spawnCountPickup = 10;
    public int spawnCountWall = 2;
    public LayerMask overlapMask;

    public float xMin = -8f, xMax = 8f;
    public float zMin = -8f, zMax = 8f;
    
    private float spacingRadius = 1f; // How far apart collectibles must be
    private float spawnY = 0.5f;

    Vector3 ranVec3()
    {
        
        return new Vector3(
                Random.Range(xMin, xMax),
                spawnY,
                Random.Range(zMin, zMax)
            );
    }

    void randomlyPlace(string gameObject)
    {
        int attempts = 0;
        int placed = 0;
        int maxAttempts = spawnCountPickup * 100;

        int limit = 0;
        if (gameObject.Equals("pickup")) { limit = spawnCountPickup; spacingRadius = 1.0f; }
        else if (gameObject.Equals("wall")) { limit = spawnCountWall; spacingRadius = 4.0f;
            xMin += 3; xMax -= 3; zMin += 3; zMax -= 3; // a stupid hack but i don't why they're clipping
        }

        while (placed < limit && attempts < maxAttempts)
        {
            Vector3 randomPosPrefab = ranVec3();

            if (!Physics.CheckSphere(randomPosPrefab, spacingRadius, overlapMask))
            {
                Quaternion rotation = Quaternion.Euler(45, 45, 45);
                GameObject target = pickup;
                if (gameObject.Equals("pickup")) {}
                else if (gameObject.Equals("wall"))
                {
                    target = wall;
                    if (Random.value < 0.5f)
                    { rotation = Quaternion.identity; }
                    else
                    { rotation = Quaternion.Euler(0, 90, 0); }
                }
                Instantiate(target, randomPosPrefab, rotation);
                placed++;
            }

            attempts++;
        }

        if (gameObject.Equals("wall"))
        {
            xMin -= 3; xMax += 3; zMin -= 3; zMax += 3; // a stupid hack but i don't why they're clipping
        }

        if (placed < limit)
        {
            Debug.LogWarning($"Only placed {placed} out of {limit} collectibles (space may be too small)");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        Vector3 playerNewPos = ranVec3();
        player.transform.position = playerNewPos;

        Vector3 enemyNewPos = ranVec3();
        while(Vector3.Distance(enemyNewPos, playerNewPos) < 5.0f)
        {
            enemyNewPos = ranVec3(); // is this dangerous? idk...
        }
        enemy.GetComponentInChildren<NavMeshAgent>().Warp(enemyNewPos);


        randomlyPlace("wall");
        randomlyPlace("pickup");
    }
}
