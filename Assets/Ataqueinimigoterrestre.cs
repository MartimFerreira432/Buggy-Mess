using UnityEngine;

public class InimigoAtaqueJogador : MonoBehaviour
{
    public Transform jogador;
    public float alcanceAtaque = 2f;

    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (jogador == null) return;

        VirarParaJogador();
        TentarAtacar();
    }

    void VirarParaJogador()
    {
        if (jogador.position.x > transform.position.x)
        {
            anim.Play("Inimigoterrestredirei");
        }
        else
        {
            anim.Play("InimigoTerrestreesquerda");
        }
    }

    void TentarAtacar()
    {
        float distancia = Vector2.Distance(transform.position, jogador.position);

        if (distancia <= alcanceAtaque)
        {
            if (jogador.position.x > transform.position.x)
            {
                anim.Play("Itattackdireita");
            }
            else
            {
                anim.Play("Itattackesq");
            }
        }
    }
}