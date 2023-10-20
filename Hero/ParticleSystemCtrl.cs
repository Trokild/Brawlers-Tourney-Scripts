using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemCtrl : MonoBehaviour
{
    public List<ParticleSystem> particleSytems = new List<ParticleSystem>();

    public void StopParticles()
    {
        foreach(ParticleSystem ps in particleSytems)
        {
            ps.Stop();
        }
    }
}
