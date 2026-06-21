using UnityEngine;

public class flowerpop : MonoBehaviour
{
    [Tooltip("Assign the Info_liblinha Canvas GameObject here")]
    public GameObject infoCanvas;

    void Start()
    {
        if (infoCanvas != null)
        {
            infoCanvas.SetActive(false);
            Debug.Log("flowerpop: infoCanvas assigned in Inspector.");
        }
        else
        {
            var found = GameObject.Find("Info_liblinha");
            if (found != null)
            {
                infoCanvas = found;
                infoCanvas.SetActive(false);
                Debug.Log("flowerpop: found Info_liblinha by name at Start.");
            }
            else
            {
                Debug.Log("flowerpop: infoCanvas not assigned and Info_liblinha not found in scene.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("flowerpop: Player entered trigger - showing Info_liblinha");
            if (infoCanvas != null)
            {
                var modal = infoCanvas.GetComponent<ModalWindowPanel>();
                if (modal != null) modal.Show();
                else
                {
                    var ui = infoCanvas.GetComponent<UIManager>();
                    if (ui != null) ui.ShowPopup();
                    else infoCanvas.SetActive(true);
                }
            }
            else if (UIManager.Instance != null)
            {
                UIManager.Instance.ShowPopup();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("flowerpop: Player exited trigger - hiding Info_liblinha");
            if (infoCanvas != null)
            {
                var modal = infoCanvas.GetComponent<ModalWindowPanel>();
                if (modal != null) modal.Close();
                else
                {
                    var ui = infoCanvas.GetComponent<UIManager>();
                    if (ui != null) ui.HidePopup();
                    else infoCanvas.SetActive(false);
                }
            }
            else if (UIManager.Instance != null)
            {
                UIManager.Instance.HidePopup();
            }
        }
    }

    // 2D physics support
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("flowerpop (2D): Player entered trigger - showing Info_liblinha");
            if (infoCanvas != null)
            {
                var modal = infoCanvas.GetComponent<ModalWindowPanel>();
                if (modal != null) modal.Show();
                else
                {
                    var ui = infoCanvas.GetComponent<UIManager>();
                    if (ui != null) ui.ShowPopup();
                    else infoCanvas.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("flowerpop (2D): Player exited trigger - hiding Info_liblinha");
            if (infoCanvas != null)
            {
                var modal = infoCanvas.GetComponent<ModalWindowPanel>();
                if (modal != null) modal.Close();
                else
                {
                    var ui = infoCanvas.GetComponent<UIManager>();
                    if (ui != null) ui.HidePopup();
                    else infoCanvas.SetActive(false);
                }
            }
        }
    }
}
