using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_StormLaser : Spell
{
    public AudioClip BeamSound;

    private bool cSelf;
    private bool isF;
    private float sRng;
    private int amount;
    private int nrStorm;
    private float stormSpeed;

    public GameObject Cast_PartiSys;
    public GameObject Hit_PartiSys;
    public GameObject Bem;

    private LayerMask clickAbleLayer;
    private Health myHeath;

    protected override void Start()
    {
        base.Start();
        clickAbleLayer = LayerMask.GetMask("ClickAble");
        myHeath = GetComponent<Health>();
    }

    public void SetAbility(int amo, int manc, bool cs, bool fri)
    {
        amount = amo;
        manaCost = manc;
        cSelf = cs;
        isF = fri;
    }

    public void SetValuesOther(float rng, int Strm, float spd)
    {
        sRng = rng;
        nrStorm = Strm;
        stormSpeed = spd;
    }

    public override void UseSpell(int s)
    {
        if (magicHero.Cur_Mana >= manaCost)
        {
            StartCoroutine(Storm());
            magicHero.uiHero.AbilityButtons[s].StartCooldown();
            magicHero.UseMana(manaCost);
        }
    }

    public void CastStormLaserAuto()
    {
        if (magicHero.Cur_Mana >= manaCost)
        {
            StartCoroutine(Storm());
            magicHero.UseMana(manaCost);
        }
    }

    private IEnumerator Storm()
    {
        hro.Stagnate();
        List<Health> enem = new List<Health>();
        hro.anim.SetTrigger("CastC");
        hro.anim.SetBool("IsCasting", true);
        magicHero.auio.PlayOneShot(SpellSound);
        GameObject go_psys = Instantiate(Cast_PartiSys, magicHero.castStaffPoint.position, Quaternion.identity);
        ParticleSystemCtrl psys = go_psys.GetComponent<ParticleSystemCtrl>();
        go_psys.transform.parent = magicHero.castStaffPoint;
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < nrStorm; i++)
        {
            yield return new WaitForSeconds(stormSpeed);

            Collider[] hitColiders = Physics.OverlapSphere(transform.position, sRng, clickAbleLayer);
            int x = 0;
            if (hitColiders.Length > 0)
            {
                while (x < hitColiders.Length)
                {
                    GameObject go = hitColiders[x].gameObject;
                    Health hth = go.GetComponent<Health>();
                    if (!isF && hth.currentHealthType != Health.HealthType.Building)
                    {
                        if (hth != null && hth.healthTeam != hro.unitTeam)
                        {
                            enem.Add(hth);
                        }
                    }
                    x++;
                }
            }

            if (enem.Count > 0)
            {
                int rand = Random.Range(0, enem.Count);
                if (enem[rand] != null)
                {
                    yield return new WaitForSeconds(stormSpeed);
                    magicHero.auio_Alt.PlayOneShot(BeamSound);
                    magicHero.auio_Alt.pitch = Random.Range(0.92f, 1.05f);
                    enem[rand].TakeDamage(amount, 100, 0, myHeath.idHealth);

                    Vector3 sp = new Vector3(enem[rand].transform.position.x, (enem[rand].transform.position.y + 2f), enem[rand].transform.position.z);
                    GameObject hsys = Instantiate(Hit_PartiSys, sp, Quaternion.identity);
                    hsys.transform.parent = enem[rand].transform;

                    GameObject lb = Instantiate(Bem, magicHero.castStaffPoint.position, Quaternion.identity);

                    LineRenderer line = lb.GetComponentInChildren<LineRenderer>();
                    line.SetPosition(0, magicHero.castStaffPoint.position);
                    line.SetPosition(1, sp);

                    lb.transform.parent = magicHero.castStaffPoint;


                    Destroy(hsys, 5f);
                    Destroy(lb, 5f);
                }
            }
            enem.Clear();
        }
        psys.StopParticles();
        hro.IdleState();
        hro.anim.SetTrigger("EndCast");
        hro.anim.SetBool("IsCasting", false);
    }
}
