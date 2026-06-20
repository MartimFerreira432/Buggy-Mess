using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Portaboss : MonoBehaviour
{
    private bool jogadorEstaNaPorta = false;

    void Update()
    {
      
        if (jogadorEstaNaPorta && Keyboard.current.wKey.wasPressedThisFrame)
        {
           
            SceneManager.LoadScene("Boss");
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