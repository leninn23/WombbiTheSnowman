using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootstep : MonoBehaviour
{
    public void PlaySound()
    {
        AudioManager.PlaySound(SoundType.PASOS);
    }
}
