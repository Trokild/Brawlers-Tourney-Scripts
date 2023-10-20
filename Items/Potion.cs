using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Item/Potion")]
public class Potion : Consumables
{
    public enum PotionType { Health, Mana, Stamina }
    public PotionType ThisPotionIs;
    public int Amount;
    public int Doses;
    public float DrinkSpeed;
    public GameObject ParticleEffect;
    public float ParticleCD;

    public override void UseItem(GameObject User)
    {
        Animator anem = User.GetComponentInChildren<Animator>();
        switch (ThisPotionIs)
        {
            case PotionType.Health:
                HeroHealth _heroHealth = User.GetComponent<HeroHealth>();
                _heroHealth.HealTick(Doses, Amount, DrinkSpeed);
                break;
            case PotionType.Mana:
                HeroMagic _heroMagic = User.GetComponent<HeroMagic>();
                _heroMagic.ManaTick(Doses, Amount, DrinkSpeed);
                break;
            case PotionType.Stamina:
                Unit _heroUnit = User.GetComponent<Unit>();
                _heroUnit.StaminaTick(Doses, Amount, DrinkSpeed);
                break;
        }
        if(ParticleEffect != null)
        {
            GameObject go_psys = Instantiate(ParticleEffect, User.transform.position, Quaternion.identity);
            SpellCtrl_SelfParticleEffect sc_spe = go_psys.GetComponent<SpellCtrl_SelfParticleEffect>();
            if (sc_spe != null)
            {
                sc_spe.StartParticleSystems(ParticleCD, User.transform);
            }
            else
            {
                Debug.LogError(go_psys + "Does not contain SpellCtrl_SelfParticleEffect");
                Destroy(go_psys);
            }
        }
        anem.SetTrigger("Drink");
    }
}
