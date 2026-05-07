using UnityEngine;
using System.Collections.Generic;
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
            }
            else if (salto > 0)
            {
                RB.linearVelocity = new Vector2(RB.linearVelocity.x, 0);
                RB.AddForce(new Vector2(0, 900f));
                salto--;
            }
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            RB.AddForce(new Vector2(0, -900f));
        }

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

        if (RB.linearVelocity.x > 4)
            RB.linearVelocity = new Vector2(4, RB.linearVelocity.y);

        if (RB.linearVelocity.x < -4)
            RB.linearVelocity = new Vector2(-4, RB.linearVelocity.y);

        float vel = Mathf.Abs(RB.linearVelocity.x);

        if (salto < 2)
        {
            if (direcao == 1)
                PlayAnim("abelhasaltadirei");
            else
                PlayAnim("abelhasaltaesq");
        }
        else if (vel > 0.1f)
        {
            if (direcao == 1)
                PlayAnim("abelhacaminhadirei");
            else
                PlayAnim("Abelhacaminhaesq");
        }
        else
        {
            if (direcao == 1)
                PlayAnim("abelhaidledireita");
            else
                PlayAnim("Abelhaidle");
        }
    }

    void PlayAnim(string nome)
    {
        if (!animacao.GetCurrentAnimatorStateInfo(0).IsName(nome))
        {
            animacao.Play(nome);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        salto = 2;
        Normalparede = col.contacts[0].normal;

        if (Mathf.Abs(Normalparede.x) > 0.5f)
            naParede = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        naParede = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectibles"))
        {
            cm.collectiblecount++;
            Destroy(collision.gameObject);
        }
    }
}