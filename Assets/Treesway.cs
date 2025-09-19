using UnityEngine;

public class Treesway : MonoBehaviour
{
    [Header("平移参数")]
    public float distance = 2f;    
    public float speed = 0.5f;       
    public bool centerAtStart = true; 

    Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
    }

    void Update()
    {
        
        float t = Mathf.PingPong(Time.time * speed, 1f);     
        float xOffset = Mathf.Lerp(-distance, distance, t);  

        if (centerAtStart)
            transform.position = new Vector3(startPos.x + xOffset, startPos.y, startPos.z);
        else
            transform.position = new Vector3(startPos.x + Mathf.Abs(xOffset), startPos.y, startPos.z);
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 p = Application.isPlaying ? startPos : transform.position;
        Gizmos.DrawLine(p + Vector3.left * distance, p + Vector3.right * distance);
        Gizmos.DrawSphere(p + Vector3.left * distance, 0.05f);
        Gizmos.DrawSphere(p + Vector3.right * distance, 0.05f);
    }
}
