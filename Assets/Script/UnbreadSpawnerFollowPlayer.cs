using UnityEngine;

public class UnbreadSpawnerFollowPlayer : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private Transform player;
    private float lastPlayerX;

    private void Start()
    {
        var p = GameObject.FindGameObjectWithTag(playerTag);
        if (p != null)
        {
            player = p.transform;
            lastPlayerX = player.position.x;
        }
    }

    private void LateUpdate()
    {
        if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag(playerTag);
            if (p == null) return;
            player = p.transform;
            lastPlayerX = player.position.x;
        }

        float dx = player.position.x - lastPlayerX;

        // only move when player moves right
        if (dx > 0f)
        {
            transform.position += new Vector3(dx, 0f, 0f);
        }

        lastPlayerX = player.position.x;
    }
}