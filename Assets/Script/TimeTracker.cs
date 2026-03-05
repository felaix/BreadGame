using System.Collections;
using TMPro;
using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    public static TimeTracker Instance;
    public TMP_Text timeTMP;
    private int timeLeft = 10;

    private void Awake()
    {
        Instance = this;
    }

    public void StartTrack()
    {
        StartCoroutine(TrackTime());
    }

    private IEnumerator TrackTime()
    {

        while (timeLeft != 0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            timeTMP.text = timeLeft.ToString();
        }

        timeLeft = 10;
        timeTMP.text = timeLeft.ToString();

        yield return null;
    }
}
