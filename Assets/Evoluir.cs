using UnityEngine;
public class Evoluir : MonoBehaviour
{
    public int cura = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vidajogador1 jogador1 = collision.GetComponent<Vidajogador1>();
        if (jogador1 != null)
        {
            jogador1.Curar(cura);
            Destroy(gameObject);
            return;
        }

        Vidajogador2 jogador2 = collision.GetComponent<Vidajogador2>();
        if (jogador2 != null)
        {
            jogador2.Curar(cura);
            Destroy(gameObject);
        }
    }
}

