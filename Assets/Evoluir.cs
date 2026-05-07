using UnityEngine;

public class FolhaCura : MonoBehaviour
{
    public int cura = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        Vidajogador1 jogador1 = collision.GetComponent<Vidajogador1>();
        if (jogador1 != null)
        {
            Curar(jogador1);
            return;
        }

     
        Vidajogador2 jogador2 = collision.GetComponent<Vidajogador2>();
        if (jogador2 != null)
        {
            Curar(jogador2);
        }
    }

    void Curar(Vidajogador1 jogador)
    {
        jogador.vida += cura;

        if (jogador.vida > jogador.vidaMaxima)
            jogador.vida = jogador.vidaMaxima;

        Destroy(gameObject);
    }

    void Curar(Vidajogador2 jogador)
    {
        jogador.vida += cura;

        if (jogador.vida > jogador.vidaMaxima)
            jogador.vida = jogador.vidaMaxima;

        Destroy(gameObject);
    }
}
