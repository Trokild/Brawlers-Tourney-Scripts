using UnityEngine;

public class SpellCtrl_AoePrefab : MonoBehaviour
{
    [SerializeField] private SpellCtrl_AoeParticleEffect scpe;

    public void InitializeRain(Vector2Int dmgAp, Vector2Int Team_id, float durr, float aoe, int amount)
    {
        scpe.SetParticleEffectData(amount, durr, aoe);
        scpe.SetSpellData(dmgAp, Team_id);
        Invoke("DeleteThis", durr); 
    }

    void DeleteThis()
    {
        scpe.StopEmission();
        Destroy(gameObject, 5f);
    }
}
