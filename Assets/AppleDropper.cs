using UnityEngine;
using System.Collections;

public class AppleDropper : MonoBehaviour
{
    [Header("ƻ�������")]
    public GameObject applePrefab;           // ����� Apple.prefab
    public Transform[] spawnPoints;          // ��ƻ���㣨����һ����

    [Header("�������")]
    public float intervalMin = 0.8f;         // ��̼��
    public float intervalMax = 1.6f;         // ����
    public bool randomizePointEachTime = true;

    [Header("���ٶ�(��ѡ)")]
    public bool giveInitialDownSpeed = false;
    public float initialDownSpeed = 2f;      // ��һ�����³��ٶ�����������

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

        // ����ƻ����ע�ⲻҪ�� parent������ƻ���Ͳ���������ƶ�
        GameObject apple = Instantiate(applePrefab, p.position, Quaternion.identity);

        // ��ѡ����һ�����³��ٶȣ�2D/3D ͨ�ԣ�
        if (giveInitialDownSpeed)
        {
            // 3D ����
            var rb3 = apple.GetComponent<Rigidbody>();
            if (rb3) rb3.linearVelocity = Vector3.down * initialDownSpeed;

            // 2D ����
            var rb2 = apple.GetComponent<Rigidbody2D>();
            if (rb2) rb2.linearVelocity = Vector2.down * initialDownSpeed;
        }
    }

    // ��ѡ���ڱ༭���л�������
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
