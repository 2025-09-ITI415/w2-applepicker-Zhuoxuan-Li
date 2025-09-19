using UnityEngine;

public class BasketController : MonoBehaviour
{
    public float moveSpeed = 5f;  // 控制速度，可在 Inspector 调整

    void Update()
    {
        // 获取输入：A/D 控制左右，W/S 控制上下
        float moveX = Input.GetAxisRaw("Horizontal"); // A=-1, D=1
        float moveY = Input.GetAxisRaw("Vertical");   // S=-1, W=1

        // 组合成一个移动向量
        Vector3 move = new Vector3(moveX, moveY, 0f).normalized;

        // 移动篮子
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
