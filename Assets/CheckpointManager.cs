using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    private Vector3 checkpointPosition;
    private string checkpointSceneName;
    private bool hasCheckpoint = false;
    private bool respawnPending = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        checkpointSceneName = SceneManager.GetActiveScene().name;
        hasCheckpoint = true;
        Debug.Log("Checkpoint guardado em: " + position + " na cena " + checkpointSceneName);
    }

    public void Respawn()
    {
        if (!hasCheckpoint)
        {
            Debug.LogWarning("Sem checkpoint guardado ainda — não é possível respawnar.");
            return;
        }

        respawnPending = true;
        SceneManager.LoadScene(checkpointSceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!respawnPending) return;
        respawnPending = false;

        GameObject jogador = GameObject.Find("Jogador");
        GameObject jogador2 = GameObject.Find("Jogador2");
        Jogadormanager jogadorManager = FindAnyObjectByType<Jogadormanager>();

        if (jogador == null || jogador2 == null || jogadorManager == null)
        {
            Debug.LogError("Respawn: não encontrei Jogador/Jogador2/Jogadormanager na cena " + scene.name);
            return;
        }

        var vida1 = jogador.GetComponent<Vidajogador1>();
        var vida2 = jogador2.GetComponent<Vidajogador2>();
        if (vida1 != null) vida1.ResetVida();
        if (vida2 != null) vida2.ResetVida();

        ReativarFisica(jogador);
        ReativarFisica(jogador2);

        jogador.transform.position = checkpointPosition;
        jogador2.transform.position = checkpointPosition;

        var controlaJogador1 = jogador.GetComponent<ControlaJogador>();
        var controlaJogador2 = jogador2.GetComponent<ControlaJogador2>();
        if (controlaJogador1 != null) controlaJogador1.enabled = true;
        if (controlaJogador2 != null) controlaJogador2.enabled = false;

        jogador.SetActive(true);
        jogador2.SetActive(false);

        jogadorManager.jogadorativo = true;
        PlayerPrefs.SetInt("JogadorAtivoSalvo", 1);
        PlayerPrefs.Save();

        SnapCamera(checkpointPosition);

        // Toca o som de respawn centralizado
        if (Sonsemcomum.Instance != null)
        {
            Sonsemcomum.Instance.TocarRespawn();
        }

        Debug.Log("Jogador respawnado em: " + checkpointPosition + " na cena " + scene.name);
    }

    private void ReativarFisica(GameObject jogador)
    {
        if (jogador.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void SnapCamera(Vector3 pos)
    {
        Camera cam = Camera.main;
        if (cam == null) return;
        Vector3 camPos = cam.transform.position;
        camPos.x = pos.x;
        camPos.y = pos.y + 2.5f;
        cam.transform.position = camPos;
    }
}