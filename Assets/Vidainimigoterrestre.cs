using UnityEngine;

public class Vidainimigoterrestre : MonoBehaviour
{
    public int vida;
    public int vidaMaxima = 10;

    private Animator animacao;
    private InimigoTerrestre scriptMovimento; 

    void Start()
    {
        vida = vidaMaxima;
    
        animacao = GetComponentInChildren<Animator>();
    
        scriptMovimento = GetComponent<InimigoTerrestre>();
    }

    public void Receberdano(int monte)
    {
        vida -= monte;

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
          
            TocarAnimacaoDano();
        }
    }

    void TocarAnimacaoDano()
    {
        if (animacao == null) return;

        if (scriptMovimento != null && scriptMovimento.PontoB != null)
        {
          
            if (transform.position.x < scriptMovimento.PontoB.position.x)
            {
                animacao.Play("ITDamagedireita");
            }
            else
            {
                animacao.Play("ITDamageesquerda");
            }
        }
    }
}