using UnityEngine;

public class florPop : MonoBehaviour
{
    [Tooltip("Assign the Info_liblinha Canvas GameObject here")]
    public GameObject infoCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("florPop: Player entered trigger - showing Info_liblinha");
            if (infoCanvas != null) infoCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("florPop: Player exited trigger - hiding Info_liblinha");
            if (infoCanvas != null) infoCanvas.SetActive(false);
        }
    }

 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("florPop (2D): Player entered trigger - showing Info_liblinha");
            if (infoCanvas != null) infoCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("florPop (2D): Player exited trigger - hiding Info_liblinha");
            if (infoCanvas != null) infoCanvas.SetActive(false);
        }
    }
}
