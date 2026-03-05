using UnityEngine;

public class InfiniteParallaxLayer2D : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;

    [Header("Parallax")]
    [Range(0f, 1f)]
    public float parallaxFactor = 0.5f; // 0 = stuck to camera, 1 = moves with world

    [Header("Looping")]
    public bool loop = true;

    private float tileWidth; // world units
    private Vector3 lastCamPos;

    void Awake()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Start()
    {
        lastCamPos = cameraTransform.position;

        // Get width from first child sprite renderer bounds
        if (transform.childCount == 0)
        {
            Debug.LogError($"{name}: No child tiles found. Add 3 tiled sprite children.");
            enabled = false;
            return;
        }

        var sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError($"{name}: First child has no SpriteRenderer.");
            enabled = false;
            return;
        }

        tileWidth = sr.bounds.size.x;
        if (tileWidth <= 0.0001f)
        {
            Debug.LogError($"{name}: Tile width is invalid.");
            enabled = false;
        }
    }

    void LateUpdate()
    {
        // 1) Parallax move based on camera delta
        Vector3 camDelta = cameraTransform.position - lastCamPos;
        transform.position += new Vector3(camDelta.x * parallaxFactor, 0f, 0f);
        lastCamPos = cameraTransform.position;

        if (!loop) return;

        // 2) Recycle tiles when camera passes them
        // We assume children are laid out left->right and we keep them in that order.
        float camX = cameraTransform.position.x;
        Transform leftMost = transform.GetChild(0);
        Transform rightMost = transform.GetChild(transform.childCount - 1);

        // Move left-most tile to the right when it's far behind the camera
        // Threshold: when camera is beyond leftMost by more than one tile width
        while (camX - leftMost.position.x > tileWidth)
        {
            leftMost.position = new Vector3(rightMost.position.x + tileWidth, leftMost.position.y, leftMost.position.z);
            leftMost.SetAsLastSibling(); // updates order
            leftMost = transform.GetChild(0);
            rightMost = transform.GetChild(transform.childCount - 1);
        }

        // Optional: If you also allow moving left (backtracking), recycle the other way
        while (rightMost.position.x - camX > tileWidth)
        {
            rightMost.position = new Vector3(leftMost.position.x - tileWidth, rightMost.position.y, rightMost.position.z);
            rightMost.SetAsFirstSibling();
            leftMost = transform.GetChild(0);
            rightMost = transform.GetChild(transform.childCount - 1);
        }
    }
}