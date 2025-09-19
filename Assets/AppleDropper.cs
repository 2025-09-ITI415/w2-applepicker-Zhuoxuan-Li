using UnityEngine;
using System.Collections;

public class AppleDropper : MonoBehaviour
{
    [Header("AppleSpawn")]
    public GameObject applePrefab;           
    public Transform[] spawnPoints;         

    [Header("DropInterval")]
    public float intervalMin = 0.8f;         
    public float intervalMax = 1.6f;         
    public bool randomizePointEachTime = true;

    [Header("Speed")]
    public bool giveInitialDownSpeed = false;
    public float initialDownSpeed = 2f;      

    void OnValidate()
    {
        if (intervalMax < intervalMin) intervalMax = intervalMin;
    }

    IEnumerator Start()
    {
        if (applePrefab == null)
        {
            Debug.LogError("[AppleDropper] ���� Inspector ��ָ�� applePrefab��");
            yield break;
        }
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("[AppleDropper] ������ָ��һ�� spawnPoint��");
            yield break;
        }

        while (true)
        {
            SpawnOne();
            float wait = Random.Range(intervalMin, intervalMax);
            yield return new WaitForSeconds(wait);
        }
    }

    void SpawnOne()
    {
        Transform p = randomizePointEachTime
            ? spawnPoints[Random.Range(0, spawnPoints.Length)]
            : spawnPoints[0];

        
        GameObject apple = Instantiate(applePrefab, p.position, Quaternion.identity);

        
        if (giveInitialDownSpeed)
        {
            
            var rb3 = apple.GetComponent<Rigidbody>();
            if (rb3) rb3.linearVelocity = Vector3.down * initialDownSpeed;

            
            var rb2 = apple.GetComponent<Rigidbody2D>();
            if (rb2) rb2.linearVelocity = Vector2.down * initialDownSpeed;
        }
    }

    
    void OnDrawGizmosSelected()
    {
        if (spawnPoints == null) return;
        Gizmos.color = Color.yellow;
        foreach (var t in spawnPoints)
        {
            if (t == null) continue;
            Gizmos.DrawSphere(t.position, 0.05f);
        }
    }
}
