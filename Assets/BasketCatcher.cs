using UnityEngine;

public class BasketCatcher3D : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            Debug.Log("Caught an apple!");
        }
    }
}
