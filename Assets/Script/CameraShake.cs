using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake Instance { get; private set; }

    [SerializeField] private float duration = .12f;
    [SerializeField] private float strength = .25f;
    [SerializeField] private int vibrato = 20;
    [SerializeField] private bool useUnscaledTime = false;


    private Vector3 baseLocalPos;
    private void Awake()
    {
        Instance = this;
        baseLocalPos = transform.localPosition;
    }

    public void Shake()
    {
        StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        baseLocalPos = transform.localPosition;

        float t = 0f;
        float step = 1f;

        while (t < duration)
        {
            float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            // Fade out over time (nice “punch” feel)
            float fade = 1f - Mathf.Clamp01(t / duration);

            // Random inside circle for 2D shake
            Vector2 rand = Random.insideUnitCircle * (strength * fade);
            transform.localPosition = baseLocalPos + new Vector3(rand.x, rand.y, 0f);

            // Hold the offset a tiny bit so it doesn’t look like high-frequency noise
            float hold = 0f;
            while (hold < step && t < duration)
            {
                float hdt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                hold += hdt;
                t += hdt;
                yield return null;
            }

        }

        transform.localPosition = baseLocalPos;
        yield return null;
    }
}
