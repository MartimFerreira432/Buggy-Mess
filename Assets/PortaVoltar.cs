using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PortaVoltar : MonoBehaviour
{
    private bool jogadorEstaNaPorta = false;
    private bool bossDerrotado = false;

    private void OnEnable()
    {

        Vidaminiboss.OnBossMorreu += LiberarPorta;
    }

    private void OnDisable()
    {
     
        Vidaminiboss.OnBossMorreu -= LiberarPorta;
    }

    private void LiberarPorta()
    {
        bossDerrotado = true;
        Debug.Log("O Boss morreu! A porta para voltar está aberta.");
    }

    void Update()
    {
       
        if (jogadorEstaNaPorta && bossDerrotado && Keyboard.current.wKey.wasPressedThisFrame)
        {
      
            SceneManager.LoadScene("Messy bug saves");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            jogadorEstaNaPorta = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            jogadorEstaNaPorta = false;
        }
    }
}