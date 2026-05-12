using UnityEngine;
using UnityEngine.InputSystem;




public class ControlaJogador2 : MonoBehaviour
{
    private Rigidbody2D RB;
    int salto;
    bool naParede;
    Vector2 Normalparede;
    private GameObject Cam;
    private Animator animacao;

    public int direcao = 1;
    public bool aAtacar = false;

    public Collectiblemanager cm;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        salto = 2;
        Cam = Camera.main.gameObject;
        RB.freezeRotation = true;
        animacao = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        // CÂMARA (Restaurado)
        float DifCam = Cam.transform.position.x - transform.position.x;

        if (RB.linearVelocity.x > 0.5f && DifCam < 3)
        {
            Cam.transform.Translate(12 * Time.fixedDeltaTime, 0, 0);
            if (DifCam > 2.7f)
            {
                Vector3 C = Cam.transform.position;
                C.x = transform.position.x + 3;
                Cam.transform.position = C;
            }
        }

        if (RB.linearVelocity.x < -0.5f && DifCam > -3)
        {
            Cam.transform.Translate(-12 * Time.fixedDeltaTime, 0, 0);
            if (DifCam < -2.7f)
            {
                Vector3 C = Cam.transform.position;
                C.x = transform.position.x - 3;
                Cam.transform.position = C;
            }
        }

        Vector3 CamTemp = Cam.transform.position;
        CamTemp.y = transform.position.y + 2.5f;
        Cam.transform.position = CamTemp;
    }

    void Update()
    {
        if (aAtacar) return;

        // Deslizar na parede (Original)
        if (naParede && RB.linearVelocity.y < 0)
        {
            RB.linearVelocity = new Vector2(RB.linearVelocity.x, -2f);
        }

        // SALTO (W)
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            if (naParede)
            {
                RB.linearVelocity = Vector2.zero;
                RB.AddForce(new Vector2(Normalparede.x * 800, 900f));
                naParede = false;
            }
            else if (salto > 0)
            {
                RB.linearVelocity = new Vector2(RB.linearVelocity.x, 0);
                RB.AddForce(new Vector2(0, 900f));
                salto--;
            }
        }

        // DESCIDA RÁPIDA (S) - Tinha faltado na última versão!
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            RB.AddForce(new Vector2(0, -900f));
        }

        // MOVIMENTO (A e D)
        if (Keyboard.current.aKey.IsPressed())
        {
            RB.AddForce(new Vector2(-8000 * Time.deltaTime, 0));
            direcao = -1;
        }

        if (Keyboard.current.dKey.IsPressed())
        {
            RB.AddForce(new Vector2(8000 * Time.deltaTime, 0));
            direcao = 1;
        }

        // LIMITADORES DE VELOCIDADE
        if (RB.linearVelocity.x > 4) RB.linearVelocity = new Vector2(4, RB.linearVelocity.y);
        if (RB.linearVelocity.x < -4) RB.linearVelocity = new Vector2(-4, RB.linearVelocity.y);

        // ANIMAÇÕES E SOM GLOBAL
        float vel = Mathf.Abs(RB.linearVelocity.x);

        if (salto < 2)
        {
            AudioManager.Instance.TocarPassos(false);
            if (direcao == 1) PlayAnim("abelhasaltadirei");
            else PlayAnim("abelhasaltaesq");
        }
        else if (vel > 0.1f)
        {
            if (direcao == 1) PlayAnim("abelhacaminhadirei");
            else PlayAnim("Abelhacaminhaesq");
            AudioManager.Instance.TocarPassos(true);
        }
        else
        {
            AudioManager.Instance.TocarPassos(false);
            if (direcao == 1) PlayAnim("abelhaidledireita");
            else PlayAnim("Abelhaidle");
        }
    }

    void PlayAnim(string nome)
    {
        if (!animacao.GetCurrentAnimatorStateInfo(0).IsName(nome)) animacao.Play(nome);
    }

    // COLISÕES
    void OnCollisionEnter2D(Collision2D col)
    {
        salto = 2;
        Normalparede = col.contacts[0].normal;
        if (Mathf.Abs(Normalparede.x) > 0.5f) naParede = true;
    }

    void OnCollisionExit2D(Collision2D collision) { naParede = false; }

    // COLETA (Tag Collectibles)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectibles"))
        {
            if (cm != null)
            {
                cm.collectiblecount++;
                Destroy(collision.gameObject);
            }
        }
    }
}