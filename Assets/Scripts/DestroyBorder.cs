using UnityEngine;

public class DestroyBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Apple" ||
            collision.gameObject.tag == "Knife" ||
            collision.gameObject.tag == "KnifeInWood" ||
            collision.gameObject.tag == "Wood" ||
            collision.gameObject.tag == "PartWood" ||
            collision.gameObject.tag == "PartApple")
            Destroy(collision.gameObject);
    }
}
