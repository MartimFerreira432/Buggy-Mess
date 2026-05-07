using UnityEngine;

public class Danoinimigovoador : MonoBehaviour
{
    public Vidajogador1 vidajogador1;
    public Vidajogador2 vidajogador2;

    public float intervaloDano = 0.5f;

    private bool jogador1EmContacto = false;
    private bool jogador2EmContacto = false;

    private float proximoDano1;
    private float proximoDano2;

    void Update()
    {
        if (jogador1EmContacto && vidajogador1 != null && Time.time >= proximoDano1)
        {
            vidajogador1.Receberdano(1);
            proximoDano1 = Time.time + intervaloDano;
        }

        if (jogador2EmContacto && vidajogador2 != null && Time.time >= proximoDano2)
        {
            vidajogador2.Receberdano(1);
            proximoDano2 = Time.time + intervaloDano;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            vidajogador1 = collision.gameObject.GetComponent<Vidajogador1>();
            jogador1EmContacto = true;
            proximoDano1 = Time.time;
        }

        if (collision.gameObject.CompareTag("Player2"))
        {
            vidajogador2 = collision.gameObject.GetComponent<Vidajogador2>();
            jogador2EmContacto = true;
            proximoDano2 = Time.time;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jogador1EmContacto = false;
        }

        if (collision.gameObject.CompareTag("Player2"))
        {
            jogador2EmContacto = false;
        }
    }
}