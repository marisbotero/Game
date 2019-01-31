using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ladron")
        {
            Enemy2 temp = collision.GetComponent<Enemy2>();
            temp.ReceiveDamage(2);
            Destroy(gameObject);
        }
    }
}
