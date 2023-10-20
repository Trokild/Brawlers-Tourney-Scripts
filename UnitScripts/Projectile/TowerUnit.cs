using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
    TowerHealth towerHealth;
    private enum ToweState { Idle, Attack, Destroyed }
    private ToweState currentstate;

    public bool deadTower { get; private set; }
    private float cooldownAtk;
    public Stat attackSpeed;
    public Stat damage;
    public Stat ap;
    private float atkSpd;

    [SerializeField] private Transform Turrent;
    [SerializeField] private ArrowProjectile Arrow;

    [SerializeField] private float Range;
    [SerializeField] private float RotationSpeed;
    private Transform target = null;
    private GameObject nearestEnemy = null;
    private float shortestDist = Mathf.Infinity;
    [SerializeField] private LayerMask clickAbleLayer;

    [SerializeField] private float secLook;
    private float secLeft;
    [Space]
    [SerializeField] private MeshRenderer MeshBuilding;
    [SerializeField] private Material[] colorBuilding;
    [Space]
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip shootArrow;
    [SerializeField] private AudioClip loadArrow;

    private void Start()
    {
        towerHealth = GetComponent<TowerHealth>();
        atkSpd = 100f / attackSpeed.GetValue();
        cooldownAtk = atkSpd;
    }

    public void SetUpTower(int col)
    {
        Invoke("TempTow", 1f);
        //Turrent.gameObject.SetActive(true);
        SetBuildingColor(col);
    }

    void TempTow()
    {
        towerHealth.AddBuilding();
        towerHealth.FullHealth();
        deadTower = false;
    }

    public void DeathTower()
    {
        currentstate = ToweState.Destroyed;
        deadTower = true;
    }

    void SetBuildingColor(int colorInt)
    {
        MeshBuilding.material = colorBuilding[colorInt];
    }

    void Update()
    {
        switch (currentstate)
        {
            case ToweState.Idle:

                secLeft -= Time.deltaTime;
                if (secLeft <= 0)
                {
                    FindNearestEnemie();
                    secLeft = secLook;
                }

                if (nearestEnemy != null && shortestDist <= Range)
                {
                    currentstate = ToweState.Attack;
                }

                break;

            case ToweState.Attack:

                if (cooldownAtk > 0)
                {
                    cooldownAtk -= Time.deltaTime;
                }
                else if (cooldownAtk < 0)
                {
                    cooldownAtk = 0f;
                }

                if (target != null)
                {
                    Aim();
                }
                else
                {
                    FindNearestEnemie();

                    if (nearestEnemy != null && shortestDist < Range)
                    {
                        target = nearestEnemy.transform;
                    }
                    else
                    {
                        currentstate = ToweState.Idle;
                    }
                }
                break;
        }
    }

    void Aim()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

        Vector3 dir = target.position - Turrent.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(Turrent.rotation, lookRotation, Time.deltaTime * RotationSpeed).eulerAngles;
        Turrent.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

        if (distanceToEnemy < Range)
        {
            Shoot();
        }
        else
        {
            if (!deadTower)
            {
                currentstate = ToweState.Idle;
            }
            else
            {
                return;
            }

        }
    }

    void Shoot()
    {
        if (cooldownAtk <= 0)
        {
            atkSpd = 100f / attackSpeed.GetValue();
            cooldownAtk = atkSpd;
            //Debug.Log("Bang");

            float dist = Vector3.Distance(this.transform.position, target.transform.position);

            Health enem = target.GetComponent<Health>();
            if(enem.isDead)
            {
                target = null;
                currentstate = ToweState.Idle;
                return;
            }

            if (dist > 15)
            {
                StartCoroutine(AtkDelay(0.4f, 2, enem));
                Arrow.FireProjectileStraight(target.gameObject, 0.4f);
                //Debug.Log("LONGSHOT");
            }
            else if (dist <= 15 && dist > 8)
            {
                StartCoroutine(AtkDelay(0.25f, 2, enem));
                Arrow.FireProjectileStraight(target.gameObject, 0.25f);
                //Debug.Log("MIDSHOT");
            }
            else
            {
                StartCoroutine(AtkDelay(0.1f, 2, enem));
                Arrow.FireProjectileStraight(target.gameObject, 0.1f);
                //Debug.Log("ShotSHOT");
            }
            _audio.PlayOneShot(shootArrow);
        }
    }

    protected IEnumerator AtkDelay(float delayT, int anl, Health trg)
    {
        yield return new WaitForSeconds(delayT);
        if (trg != null && !trg.isDead)
        {
            trg.TakeDamage(damage.GetBaseValue(), ap.GetValue(), anl, towerHealth.idHealth);
        }
        else
        {
            target = null;
            if (!deadTower)
            {
                currentstate = ToweState.Idle;
            }
            else
            {
                yield break;
            }
        }
        yield return new WaitForSeconds(0.5f);
        _audio.PlayOneShot(loadArrow);
    }

    void FindNearestEnemie()
    {
        if (target != null)
        {
            if (target.GetComponent<Health>().isDead)
            {
                target = null;
            }
        }

        nearestEnemy = null;
        shortestDist = Mathf.Infinity;

        Collider[] hitColi = Physics.OverlapSphere(transform.position, Range, clickAbleLayer);
        if (hitColi.Length > 0)
        {
            int i = 0;
            while (i < hitColi.Length)
            {
                Health ene = hitColi[i].GetComponent<Health>();
                if (ene != null && !ene.isDead)
                {
                    if (ene.healthTeam != towerHealth.healthTeam)
                    {
                        float distance = Vector3.Distance(transform.position, ene.transform.position);
                        if (distance < shortestDist)
                        {
                            shortestDist = distance;
                            nearestEnemy = ene.gameObject;
                        }
                    }
                }
                i++;
            }
        }
        else
        {
            nearestEnemy = null; ;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
