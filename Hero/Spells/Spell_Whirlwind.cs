using System.Collections;
using UnityEngine;

public class Spell_Whirlwind : Spell
{
    public int spns;
    public float durrSpin;
    public Vector2Int dmgAp;
    public GameObject colliderPrefab;
    private GameObject curr_Colider;
    private HeroHealth heroHlt;
    public Buff_Affliction triggrBuff;
    public bool hasBuffTrigger;

    //Make the coliderPrefab an 'object pool', turn on/off instead off Instantiate/Destroy

    protected override void Start()
    {
        base.Start();
        heroHlt = hro.health as HeroHealth;
    }

    public override void UseSpell(int s)
    {
        if (magicHero.Cur_Mana >= manaCost)
        {
            Whildwind();
            magicHero.uiHero.AbilityButtons[s].StartCooldown();
            magicHero.UseMana(manaCost);
        }
    }

    public void Whildwind()
    {
        GameObject colP = Instantiate(colliderPrefab, transform.position, Quaternion.identity);
        colP.transform.parent = this.transform;
        colP.transform.rotation = new Quaternion(0, 0, 0, 0);
        curr_Colider = colP;
        SpellCtrl_SwipeCol sc_sc = colP.GetComponent<SpellCtrl_SwipeCol>();
        sc_sc.SetUpSwipeCol(team_Id, dmgAp);

        if (hasBuffTrigger && triggrBuff != null)
        {
            for (int i = 0; i < heroHlt.buffs.Count; i++)
            {
                if (heroHlt.buffs[i] == triggrBuff)
                {
                    Debug.Log("Bonus Whirlwind");
                    break;
                }
            }
        }

        StartCoroutine(Spin(durrSpin, spns));
    }

    IEnumerator Spin(float tim, int spi)
    {
        hro.Stagnate();
        hro.anim.SetTrigger("Spin");
        hro.anim.SetBool("IsSpinning", true);
        float startRotation = transform.eulerAngles.y;
        float rot = (360.0f * spi);
        float endRotation = startRotation - rot;
        float t = 0.0f;
        yield return new WaitForSeconds(0.2f);
        magicHero.auio.PlayOneShot(SpellSound);
        while (t < tim)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / tim) % rot;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }

        if(curr_Colider != null)
        {
            Destroy(curr_Colider);
            curr_Colider = null;
        }
        hro.IdleState();
        hro.anim.SetTrigger("EndSpin");
        hro.anim.SetBool("IsSpinning", false);
    }

    public override void CastSpellAuto()
    {
        if(magicHero.Cur_Mana >= manaCost)
        {
            LayerMask clickAbleLayer = LayerMask.GetMask("ClickAble");
            Collider[] hitColiders = Physics.OverlapSphere(transform.position, 4, clickAbleLayer);
            Health thisH = this.GetComponent<Health>();
            if (hitColiders.Length > 0)
            {
                for (int i = 0; i < hitColiders.Length; i++)
                {
                    GameObject go = hitColiders[i].gameObject;
                    Health hth = go.GetComponent<Health>();
                    if (hth != null)
                    {
                        if (hth.healthTeam != hro.unitTeam && !hth.isDead)
                        {
                            if (hth.Cur_Health < hth.max_Health)
                            {
                                Whildwind();
                                magicHero.UseMana(manaCost);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
