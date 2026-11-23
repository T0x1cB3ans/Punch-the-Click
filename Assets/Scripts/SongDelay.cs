using System.Collections;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

public class SongDelay : MonoBehaviour
{
    public SimpleMusicPlayer musicPlayer;
    public Koreography koreography;
    public float delaySeconds = 3f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delaySeconds);
        musicPlayer.LoadSong(koreography, 0, true);
    }
}