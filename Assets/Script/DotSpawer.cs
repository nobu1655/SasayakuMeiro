using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotSpawer : MonoBehaviour
{
    public GameObject dotPrefab;
    public int dotCount = 100;          
    public Vector3 areaSize = new Vector3(90, 5, 90); 
    public LayerMask groundLayer;      

    void Start()
    {
        SpawnDots();
    }

    void SpawnDots()
    {
        for (int i = 0; i < dotCount; i++)
        {
            
            Vector3 randomPos = transform.position +
                                new Vector3(
                                    Random.Range(-areaSize.x / 2, areaSize.x / 2),
                                    areaSize.y,
                                    Random.Range(-areaSize.z / 2, areaSize.z / 2)
                                );

            RaycastHit hit;

         
            if (Physics.Raycast(randomPos, Vector3.down, out hit, areaSize.y * 2, groundLayer))
            {
                Instantiate(dotPrefab, hit.point + Vector3.up * 0.1f, Quaternion.identity);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}

