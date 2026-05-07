using UnityEngine;

public class InimigoVoador : MonoBehaviour
{
    public Transform PontoC;
    public Transform PontoD;

    public Transform Jogador1;
    public Transform Jogador2;

    public float vel = 2f;
    public float alcanceDeteccao = 4f;
    public float distanciaAtaque = 1.5f;

    private Transform Bichovoar;
    private Transform alvoAtual;

    private Animator animacaovoa;

    private bool aPerseguir = false;
    private bool emAtaque = false;

    void Start()
    {
        animacaovoa = GetComponentInChildren<Animator>();

        PontoC.parent = null;
        PontoD.parent = null;

        Bichovoar = PontoD;
    }

    void Update()
    {
        if (Jogador1 == null && Jogador2 == null) return;

        EscolherAlvo();

        float distanciaAlvo = Vector2.Distance(transform.position, alvoAtual.position);

        aPerseguir = distanciaAlvo <= alcanceDeteccao;

        if (aPerseguir)
            Perseguir();
        else
            Voar();
    }

    void EscolherAlvo()
    {
        if (Jogador1 == null && Jogador2 == null) return;

        if (Jogador1 == null)
        {
            alvoAtual = Jogador2;
            return;
        }

        if (Jogador2 == null)
        {
            alvoAtual = Jogador1;
            return;
        }

        float dist1 = Vector2.Distance(transform.position, Jogador1.position);
        float dist2 = Vector2.Distance(transform.position, Jogador2.position);

        alvoAtual = (dist1 < dist2) ? Jogador1 : Jogador2;
    }

    void Voar()
    {
        emAtaque = false;

        transform.position = Vector2.MoveTowards(
            transform.position,
            Bichovoar.position,
            vel * Time.deltaTime
        );

        float distancia = Vector2.Distance(transform.position, Bichovoar.position);

        if (alvoAtual == null) return;

        if (Bichovoar == PontoD)
            animacaovoa.Play("Inimigovoadoresdirei");
        else
            animacaovoa.Play("Inimigovoadoresq");

        if (distancia < 0.05f)
            Bichovoar = (Bichovoar == PontoC) ? PontoD : PontoC;
    }

    void Perseguir()
    {
        if (emAtaque) return;

        float distancia = Vector2.Distance(transform.position, alvoAtual.position);

        if (distancia > distanciaAtaque)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                alvoAtual.position,
                vel * Time.deltaTime
            );
        }
        else
        {
            Atacar();
            return;
        }

        if (alvoAtual.position.x > transform.position.x)
            animacaovoa.Play("Inimigovoadoresdirei");
        else
            animacaovoa.Play("Inimigovoadoresq");
    }

    void Atacar()
    {
        if (emAtaque) return;

        emAtaque = true;

        if (alvoAtual.position.x > transform.position.x)
            animacaovoa.Play("IVatacadireita");
        else
            animacaovoa.Play("Ivatacaresq");

      
    }
}