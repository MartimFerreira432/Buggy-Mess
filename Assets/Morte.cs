using UnityEngine;

public class Morte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TentarMatar(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TentarMatar(collision.collider);
    }

    private void TentarMatar(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vidajogador1 vida = collision.GetComponent<Vidajogador1>();
            if (vida != null)
                vida.MorteInstantanea();
        }
        else if (collision.CompareTag("Player2"))
        {
            Vidajogador2 vida = collision.GetComponent<Vidajogador2>();
            if (vida != null)
                vida.MorteInstantanea();
        }
    }
}