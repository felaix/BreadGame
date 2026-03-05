using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public string playerTag = "Player";
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothSpeed = 5f;

    private Transform target;

    void Start()
    {
        FindPlayer();
    }

    void LateUpdate()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            target = player.transform;
        }
    }
}