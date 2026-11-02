using UnityEngine;
using SonicBloom.Koreo;

public class Note : MonoBehaviour
{
    public KoreographyEvent evt;
    public Lane lane;

    void Update()
    {
        int now = lane.Now;

        float t = 1f - (evt.StartSample - now) / (float)lane.LeadSamples;
        transform.position = Vector3.LerpUnclamped(lane.spawnPoint.position, lane.hitPoint.position, t);

        if (t > 1.3f) {lane.Miss(this);}
    }
}
