using UnityEngine;
using SonicBloom.Koreo;

public class Note : MonoBehaviour
{
    public KoreographyEvent evt;
    public Lane lane;

    [Header("Glow")]
    public Color startColor = new Color(0.3f, 0.3f, 0.5f); //dim color
    public Color glowColor = new Color(0.6f,1f,1f); //bright color
    public float glowStart = 0.6f; //glow % down lane
    public float growAmount = 0.2f; //how much bigger it gets

    [Header("Miss")]
    public Color missedColor = new Color(0.35f, 0.35f, 0.35f);
    public float fadeSpeed = 2f;

    SpriteRenderer sr;
    Vector3 baseScale;
    bool missed;
    float fade = 1f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale;
    }

    void Update()
    {
        int now = lane.Now;

        float t = 1f - (evt.StartSample - now) / (float)lane.LeadSamples;
        transform.position = Vector3.LerpUnclamped(lane.spawnPoint.position, lane.hitPoint.position, t);

        if (missed)
        {
            // turn gray
            fade -= Time.deltaTime * fadeSpeed;
            Color c = missedColor;
            c.a = fade;
            sr.color = c;
            transform.localScale = baseScale;
            
            if(fade <= 0f) Destroy(gameObject);
            return;
        }

        //glow brighter
        float glow = Mathf.InverseLerp(glowStart, 1f, t);
        sr.color = Color.Lerp(startColor, glowColor, glow);
        transform.localScale = baseScale * (1f + growAmount * glow);

        if (t > 1.15f) lane.Miss(this);
    }

    public void MarkMissed() => missed = true;
}
