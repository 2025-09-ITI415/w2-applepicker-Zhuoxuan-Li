using UnityEngine;

public class Treesway : MonoBehaviour
{
    [Header("平移参数")]
    public float distance = 2f;    // 左右各一半，总来回 2*distance
    public float speed = 0.5f;       // 往返速度（越大越快）
    public bool centerAtStart = true; // 是否以初始位置为中心左右移动

    Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 0~1~0 的往返节奏
        float t = Mathf.PingPong(Time.time * speed, 1f);     // [0,1]
        float xOffset = Mathf.Lerp(-distance, distance, t);  // [-d, d]

        if (centerAtStart)
            transform.position = new Vector3(startPos.x + xOffset, startPos.y, startPos.z);
        else
            transform.position = new Vector3(startPos.x + Mathf.Abs(xOffset), startPos.y, startPos.z);
    }

    // 选：在编辑器里画出运动范围，方便对齐
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 p = Application.isPlaying ? startPos : transform.position;
        Gizmos.DrawLine(p + Vector3.left * distance, p + Vector3.right * distance);
        Gizmos.DrawSphere(p + Vector3.left * distance, 0.05f);
        Gizmos.DrawSphere(p + Vector3.right * distance, 0.05f);
    }
}
