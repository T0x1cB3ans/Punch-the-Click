using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using SonicBloom.Koreo;

public class Lane : MonoBehaviour
{
    public Koreography koreography;
    public string eventID = "Lane0";
    public Key key = Key.A;
    public Key altKey = Key.LeftArrow;
    public Transform spawnPoint, hitPoint;
    public Note notePrefab;
    public float leadTimeSeconds = 5f;
    public float hitWindowMs = 100f;
    public AudioSource music;
    public int Now => music.timeSamples;

    public int LeadSamples => (int)(leadTimeSeconds * koreography.SampleRate);

    List<KoreographyEvent> events;
    int nextIndex;
    readonly Queue<Note> active = new();

    void Start() => events = koreography.GetTrackByID(eventID).GetAllEvents();

    void Update()
    {
        int now = Now;
        Debug.Log("Song time: " + now);

        //spawn notes
        while (nextIndex < events.Count && events[nextIndex].StartSample - now <= LeadSamples)
        {
            var n = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
            n.evt = events[nextIndex++]; n.lane = this;
            active.Enqueue(n);
        }

        // judge hits
        bool pressed = Keyboard.current[key].wasPressedThisFrame
            || Keyboard.current[altKey].wasPressedThisFrame;

        if (pressed && active.Count > 0)
        {
            var n = active.Peek();
            float offMs = (now - n.evt.StartSample) * 1000f / koreography.SampleRate;
            if (Mathf.Abs(offMs) <= hitWindowMs)
            {
                Debug.Log($"Hit! {offMs:F0} ms");
                active.Dequeue(); Destroy(n.gameObject);
            }
        }
    }

    public void Miss(Note n)
    {
        if (active.Count > 0 && active.Peek() == n) active.Dequeue();
        Debug.Log("Miss"); Destroy(n.gameObject);
    }
}
