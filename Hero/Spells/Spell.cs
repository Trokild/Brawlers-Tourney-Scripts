using UnityEngine;

public class Spell : MonoBehaviour
{
    public int manaCost;
    public AudioClip SpellSound;
    protected Hero hro;
    protected HeroMagic magicHero;
    protected Vector2Int team_Id;

    protected virtual void Start()
    {
        magicHero = gameObject.GetComponent<HeroMagic>();
        hro = gameObject.GetComponent<Hero>();
        SetSpellTeamId(hro.unitTeam, hro.idUnit);
    }

    void SetSpellTeamId(int team, int id)
    {
        Vector2Int idt = new Vector2Int(team, id);
        team_Id = idt;
    }

    public virtual void CastSpellAuto()
    {
        Debug.Log("CastSpellAuto");
    }

    public virtual void UseSpell(int s)
    {
        Debug.Log("CastSpell");
    }
}
