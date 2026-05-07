using UnityEngine;

public class InimigoTerrestre : MonoBehaviour
{
    public Transform PontoA;
    public Transform PontoB;

    public Transform jogador;
    public Transform jogador2;

    public float speed = 2f;
    public float alcanceAtaque = 2.5f;

    private Transform alvoAtual;
    private Animator animacao;

    private bool aAtacar = false;

    void Start()
    {
        animacao = GetComponentInChildren<Animator>();

        PontoA.parent = null;
        PontoB.parent = null;

        alvoAtual = PontoB;
    }

    void Update()
    {
        Transform alvo = GetAlvoMaisProximo();

        if (alvo == null)
        {
            Caminhar();
            return;
        }

        float distancia = Vector2.Distance(transform.position, alvo.position);

        if (distancia <= alcanceAtaque)
        {
            Atacar(alvo);
        }
        else
        {
            Caminhar();
        }
    }

    Transform GetAlvoMaisProximo()
    {
        float dist1 = jogador != null ? Vector2.Distance(transform.position, jogador.position) : Mathf.Infinity;
        float dist2 = jogador2 != null ? Vector2.Distance(transform.position, jogador2.position) : Mathf.Infinity;

        if (dist1 < dist2)
            return jogador;
        else
            return jogador2;
    }

    void Atacar(Transform alvo)
    {
        if (aAtacar) return;

        aAtacar = true;

        if (alvo.position.x > transform.position.x)
        {
            animacao.Play("Itattackdireita");
        }
        else
        {
            animacao.Play("Itattackesq");
        }
    }

    void Caminhar()
    {
        aAtacar = false;

        transform.position = Vector2.MoveTowards(
            transform.position,
            alvoAtual.position,
            speed * Time.deltaTime
        );

        float distancia = Vector2.Distance(transform.position, alvoAtual.position);

        if (distancia < 0.2f)
        {
            alvoAtual = (alvoAtual == PontoA) ? PontoB : PontoA;
        }

        if (alvoAtual == PontoB)
        {
            animacao.Play("Inimigoterrestredirei");
        }
        else
        {
            animacao.Play("InimigoTerrestreesquerda");
        }
    }
}