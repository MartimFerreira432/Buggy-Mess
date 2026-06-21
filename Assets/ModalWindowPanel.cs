using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject popupPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (popupPanel == null)
        {
            var found = GameObject.Find("Info_liblinha");
            if (found != null)
            {
                popupPanel = found;
                Debug.Log("UIManager: popupPanel auto-assigned to Info_liblinha");
            }
            else
            {
                Debug.Log("UIManager: popupPanel is null; assign in Inspector or name a GameObject 'Info_liblinha'.");
            }
        }
    }

    public void Show()
    {
        Debug.Log("UIManager: Show called");
        gameObject.SetActive(true);
    }

    public void ShowPopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
            Debug.Log("UIManager: ShowPopup activated popupPanel");
        }
        else Debug.Log("UIManager: ShowPopup called but popupPanel is null");
    }

    public void HidePopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
            Debug.Log("UIManager: HidePopup deactivated popupPanel");
        }
        else Debug.Log("UIManager: HidePopup called but popupPanel is null");
    }
}

public class ModalWindowPanel : MonoBehaviour
{
    private Action onBackAction;

    public void Back()
    {
        onBackAction?.Invoke();
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        Debug.Log("ModalWindowPanel: Show called");
        gameObject.SetActive(true);
    }

    public void SetOnBack(Action callback)
    {
        onBackAction = callback;
    }
}