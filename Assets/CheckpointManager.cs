using UnityEngine;
// Put this on a persistent GameObject in your scene (e.g. "GameManagers")
public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }
    [Header("References (drag these in)")]
    public Jogadormanager jogadorManager;
    public GameObject Jogador;
    public GameObject Jogador2;
    private Vector3 checkpointPosition;
    private bool hasCheckpoint = false;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Called by Checkpoint.cs when a player crosses one
    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        hasCheckpoint = true;
        Debug.Log("Checkpoint guardado em: " + position);
    }
    // Called by Vidajogador1/2 when the active character dies
    public void Respawn()
    {
        Vector3 pos = hasCheckpoint ? checkpointPosition : Jogador.transform.position;
        // Reset both characters' health, regardless of which one died
        var vida1 = Jogador.GetComponent<Vidajogador1>();
        var vida2 = Jogador2.GetComponent<Vidajogador2>();
        if (vida1 != null) vida1.ResetVida();
        if (vida2 != null) vida2.ResetVida();
        // Re-enable physics on whichever GameObjects got frozen on death
        ReativarFisica(Jogador);
        ReativarFisica(Jogador2);
        // Snap whichever character is currently active to the checkpoint;
        // keep the inactive one parked at the same spot too, so a swap
        // right after respawn doesn't teleport from some old position
        Jogador.transform.position = pos;
        Jogador2.transform.position = pos;
        // Make sure player 1 is the one active after respawn
        // (skip this call if you'd rather keep whichever was active before death)
        jogadorManager.SendMessage("DefinirJogador1");
        // Snap the camera straight to the new position, otherwise it
        // slowly drifts back via the dead-zone follow logic
        SnapCamera(pos);
        Debug.Log("Jogador respawnado em: " + pos);
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
        camPos.y = pos.y + 2.5f; // matches the +2.5f y-offset used in ControlaJogador/2's FixedUpdate
        cam.transform.position = camPos;
    }
}