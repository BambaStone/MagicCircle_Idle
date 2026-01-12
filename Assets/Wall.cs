using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MagicCircle"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.transform.position = new Vector2(0,-1);
        }
    }
}
