using UnityEngine;
using System.Collections.Generic;

public class SpellCtrl_AoeParticleEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem psMain;
    [SerializeField] private ParticleSystem psCrater;
    public List<ParticleCollisionEvent> collisonEvent;
    [SerializeField]
    private LayerMask GroundLayerMask;

    Vector2Int dmg;
    int cTeam;
    int cId;


    void Start()
    {
        if(psMain == null)
        {
            psMain = GetComponent<ParticleSystem>();
        }

        collisonEvent = new List<ParticleCollisionEvent>();
    }

    public void SetSpellData(Vector2Int dp, Vector2Int t_id)
    {
        dmg = dp;
        cTeam = t_id.x;
        cId = t_id.y;
    }

    public void SetParticleEffectData(int amt, float durr, float aoe)
    {
        var main = psMain.main;
        var shape = psMain.shape;
        main.maxParticles = amt;
        main.duration = durr;
        shape.radius = (aoe / 8);
        psMain.Play();
    }

    public void StopEmission()
    {
        var emission = psMain.emission;
        emission.enabled = false;
    }

    void OnParticleCollision(GameObject other)
    {
        Health hl = other.GetComponent<Health>();
        if (hl != null)
        {
            if (hl.healthTeam != cTeam)
            {
                hl.TakeDamage(dmg.x, dmg.y, 0, cId);
            }
        }
        else if (IsGroundHit(GroundLayerMask, other.layer))
        {
            ParticlePhysicsExtensions.GetCollisionEvents(psMain, other, collisonEvent);
            for (int i = 0; i < collisonEvent.Count; i++)
            {
                EmitAtLocation(collisonEvent[i]);
            }
        }
    }

    void EmitAtLocation(ParticleCollisionEvent pce)
    {
        psCrater.transform.position = pce.intersection;
        psCrater.Emit(1);
    }

    bool IsGroundHit(LayerMask mask, int lyr)
    {
        return mask == (mask | (1 << lyr));
    }
}
