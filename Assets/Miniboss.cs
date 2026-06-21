using UnityEngine;



public class Miniboss : MonoBehaviour
{
    public Transform Jogador1;
    public Transform Jogador2;
    public float vel = 2f;
    public float alcanceDeteccao = 5f;
    public float distanciaAtaque = 6f;


    public GameObject prefabProjetil;
    public float velocidadeProjetil = 6f;
    public float cadenciaTiroMin = 1f;
    public float cadenciaTiroMax = 3f;
    public int danoProjetil = 1;

    private Transform alvoAtual;
    private Animator animacaovoa;
    private bool aPerseguir = false;
    private float proximoTiro;

    void Start()
    {
        animacaovoa = GetComponentInChildren<Animator>();
        DefinirProximoTiro();
    }

    void Update()
    {
        EscolherAlvo();

        if (alvoAtual == null) return;

        float distanciaAlvo = Vector2.Distance(transform.position, alvoAtual.position);
        aPerseguir = distanciaAlvo <= alcanceDeteccao;

        if (aPerseguir)
            Perseguir();
    }

    void EscolherAlvo()
    {
        bool j1Valido = Jogador1 != null && Jogador1.gameObject.activeInHierarchy;
        bool j2Valido = Jogador2 != null && Jogador2.gameObject.activeInHierarchy;

        if (!j1Valido && !j2Valido)
        {
            alvoAtual = null;
            return;
        }
        if (!j1Valido)
        {
            alvoAtual = Jogador2;
            return;
        }
        if (!j2Valido)
        {
            alvoAtual = Jogador1;
            return;
        }

        float dist1 = Vector2.Distance(transform.position, Jogador1.position);
        float dist2 = Vector2.Distance(transform.position, Jogador2.position);
        alvoAtual = (dist1 < dist2) ? Jogador1 : Jogador2;
    }

    void Perseguir()
    {
        float distancia = Vector2.Distance(transform.position, alvoAtual.position);
        Virar(alvoAtual.position.x - transform.position.x);

        if (distancia > distanciaAtaque)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                alvoAtual.position,
                vel * Time.deltaTime
            );
            animacaovoa.Play("Minibossvoar");
        }
        else
        {
            Atacar();
        }
    }

    void Atacar()
    {
        if (Time.time >= proximoTiro)
        {
            animacaovoa.Play("MiniBossataque");
            Disparar();
            DefinirProximoTiro();
        }
    }

    void Virar(float direcaoX)
    {
        Vector3 escala = animacaovoa.transform.localScale;
        if (direcaoX > 0)
            escala.x = -Mathf.Abs(escala.x);
        else if (direcaoX < 0)
            escala.x = Mathf.Abs(escala.x);
        animacaovoa.transform.localScale = escala;
    }

    void DefinirProximoTiro()
    {
        proximoTiro = Time.time + Random.Range(cadenciaTiroMin, cadenciaTiroMax);
    }

    void Disparar()
    {
        if (prefabProjetil == null || alvoAtual == null) return;

        GameObject obj = Instantiate(prefabProjetil, transform.position, Quaternion.identity);
        ProjeteisBoss projetil = obj.GetComponent<ProjeteisBoss>();

        if (projetil != null)
        {
            Vector2 direcao = (alvoAtual.position - transform.position).normalized;
            projetil.Inicializar(direcao, velocidadeProjetil, danoProjetil);
        }
    }
}