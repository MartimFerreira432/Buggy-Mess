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

    void OnEnable()
    {
        if (RB == null) RB = GetComponent<Rigidbody2D>();
        if (RB != null)
        {
            RB.linearVelocity = Vector2.zero;
        }
        direcao = 1;
        if (animacao != null) animacao.speed = 1f;
    }

    void FixedUpdate()
    {
        if (Cam == null) Cam = Camera.main.gameObject;
        if (Cam == null) return;

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

        if (naParede && RB.linearVelocity.y < 0)
        {
            RB.linearVelocity = new Vector2(RB.linearVelocity.x, -2f);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            if (naParede)
            {
                RB.linearVelocity = Vector2.zero;
                RB.AddForce(new Vector2(Normalparede.x * 800, 900f));
                naParede = false;

                if (Sonsemcomum.Instance != null) Sonsemcomum.Instance.TocarPulo();
            }
            else if (salto > 0)
            {
                RB.linearVelocity = new Vector2(RB.linearVelocity.x, 0);
                RB.AddForce(new Vector2(0, 900f));
                salto--;

                if (Sonsemcomum.Instance != null) Sonsemcomum.Instance.TocarPulo();
            }
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            RB.AddForce(new Vector2(0, -900f));
        }

        bool carregandoEsquerda = Keyboard.current.aKey.IsPressed();
        bool carregandoDireita = Keyboard.current.dKey.IsPressed();

        if (carregandoEsquerda)
        {
            RB.AddForce(new Vector2(-8000 * Time.deltaTime, 0));
            direcao = -1;
        }
        else if (carregandoDireita)
        {
            RB.AddForce(new Vector2(8000 * Time.deltaTime, 0));
            direcao = 1;
        }
        else
        {
            RB.linearVelocity = new Vector2(0, RB.linearVelocity.y);
        }

        if (RB.linearVelocity.x > 4) RB.linearVelocity = new Vector2(4, RB.linearVelocity.y);
        if (RB.linearVelocity.x < -4) RB.linearVelocity = new Vector2(-4, RB.linearVelocity.y);

        float vel = Mathf.Abs(RB.linearVelocity.x);

        if (direcao == 1) transform.localScale = new Vector3(1, 1, 1);
        else if (direcao == -1) transform.localScale = new Vector3(-1, 1, 1);

        if (salto < 2)
        {
            if (Sonsemcomum.Instance != null) Sonsemcomum.Instance.TocarPassos(false);
            PlayAnim("abelhasaltadirei");

            var info = animacao.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("abelhasaltadirei") && info.normalizedTime >= 1f)
            {
                animacao.speed = 0f;
            }
        }
        else if (vel > 0.1f)
        {
            animacao.speed = 1f;
            PlayAnim("abelhacaminhadirei");
            if (Sonsemcomum.Instance != null) Sonsemcomum.Instance.TocarPassos(true);
        }
        else
        {
            animacao.speed = 1f;
            if (Sonsemcomum.Instance != null) Sonsemcomum.Instance.TocarPassos(false);
            PlayAnim("abelhaidledireita");
        }
    }

    void PlayAnim(string nome)
    {
        if (animacao != null && !animacao.GetCurrentAnimatorStateInfo(0).IsName(nome)) animacao.Play(nome);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts.Length > 0)
        {
            Normalparede = col.contacts[0].normal;

            if (Normalparede.y > 0.5f)
            {
                salto = 2;
                if (animacao != null) animacao.speed = 1f;
            }

            if (Mathf.Abs(Normalparede.x) > 0.5f) naParede = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) { naParede = false; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectibles"))
        {
            if (cm != null)
            {
             
                if (Sonsemcomum.Instance != null)
                {
                    Sonsemcomum.Instance.TocarComer();
                }

                cm.collectiblecount++;
                Destroy(collision.gameObject);
            }
        }
    }
}