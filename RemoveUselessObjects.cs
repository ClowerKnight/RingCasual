using UnityEngine;

public class RemoveUselessObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Remover"))
        {
            Destroy(this.gameObject);
        }
    }
}
