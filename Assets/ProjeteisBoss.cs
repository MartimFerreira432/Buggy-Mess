using UnityEngine;

public class ProjeteisBoss : MonoBehaviour
{
    public float tempoDeVida = 5f;

    private Vector2 direcao;
    private float velocidade;
    private int dano;

    public void Inicializar(Vector2 direcaoMovimento, float velocidadeProjetil, int danoProjetil)
    {
        direcao = direcaoMovimento;
        velocidade = velocidadeProjetil;
        dano = danoProjetil;

        Destroy(gameObject, tempoDeVida);

        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg + 180f;
        transform.rotation = Quaternion.Euler(0f, 0f, angulo);
    }

    void Update()
    {
        transform.position += (Vector3)(direcao * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vidajogador1 vida = collision.GetComponent<Vidajogador1>();
            if (vida != null)
                vida.Receberdano(dano);

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player2"))
        {
            Vidajogador2 vida = collision.GetComponent<Vidajogador2>();
            if (vida != null)
                vida.Receberdano(dano);

            Destroy(gameObject);
        }
    }
}