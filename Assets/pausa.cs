using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.SceneManagement; 
using UnityEngine.InputSystem;

public class pausa : MonoBehaviour
{
    public GameObject container;

    private void Start()
    {
        if (container == null)
            container = GameObject.Find("pausa_container");
            container.SetActive(false);
            Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (container == null) return;
            
            if (container.activeSelf)
            {
                container.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                container.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    public void ContinuarButton()
    {
        container.SetActive(false);
        Time.timeScale = 1;
    }
    public void ColecionaveisButton()
    {
        SceneManager.LoadScene("Info_borb"); 
    }

    public void SairButton()
    {
        SceneManager.LoadScene("Menu entrada");
    }
}
