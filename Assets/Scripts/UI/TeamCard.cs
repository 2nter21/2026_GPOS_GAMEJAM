using UnityEngine;
using UnityEngine.EventSystems;

public class TeamCard : MonoBehaviour
{
    [Header("Stat")]
    public float ProgressTime = 1.0f;
    private float Progress = 0f; // 0 ~ 1
    private bool Activate = false;

    [Header("Progressbar")]
    public GameObject Progressbar;
    private float StartTime;

    // Update is called once per frame
    void Update()
    {
        if (Activate)
        {
            Progress = (StartTime % ProgressTime) / ProgressTime;
        }
        else
        {
            Progress = 0;
        }


    }

    public void StartProgress()
    {
        StartTime = Time.time;
        Activate = true;
    }

    public void StopProgress()
    {
        Activate = false;
        Progress = 0f;
    }
}
