using System.Collections;
using UnityEngine;

public class BuildingHealth : Health
{
    public bool isTower = false;
    //private Unit_Spawner us;
    [SerializeField] protected ParticleSystem[] ps_Smoke;
    [SerializeField] protected ParticleSystem[] ps_BigSmoke;
    [SerializeField] protected GameObject[] ps_Fire;
    [SerializeField] protected GameObject rubble;
    protected ParticleSystem rubbleSav;
    [SerializeField] protected Transform rubbleSpawn;
    protected bool isHalfDestroyed = false;
    protected bool isQuaterDestroyed = false;
    public bool isWarning ;
    protected bool mainBuilding = false;

    public BuildingDot mapd;
    [SerializeField] protected AudioSource fireAudio;
    [SerializeField] protected AudioSource effectAudio;
    [SerializeField] protected AudioClip[] effectSounds;

    protected virtual void Warning()
    {
    }

    protected virtual void DestroyBuilding()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("DestroyBuilding BuildingHealth");
            MainSystem.RemoveBuildingList(this);
            mapd.DotDead();
            if(!isTower)
            {
                //us.DestroyedBuilding();
            }
            else
            {
                GetComponent<TowerUnit>().DeathTower();
            }
            Destroy(ol);
            fireAudio.minDistance = 20f;
            fireAudio.maxDistance = 60f;
            StartCoroutine(Shake(0.1f, 40));
        }
    }

    //public override void TeamId(int team, int id, int color)
    //{
    //    us = GetComponent<Unit_Spawner>();
    //    if(us == null)
    //    {
    //        Debug.LogError("Can't find Unit_Spawner.cs", gameObject);
    //    }

    //    us.buildingTeam = team;
    //    us.buildingId = id;
    //    us.SetColor(color);
    //    mapd.gameObject.SetActive(true);
    //    mapd.SetColorDot(color);

    //    healthTeam = team;
    //    idHealth = id;
    //    Cur_Health = max_Health;
    //    Invoke("AddBuilding", 0.5f);
    //}

    public void AddBuilding()
    {
        MainSystem.AddBuildingList(this);
    }

    protected IEnumerator Shake(float amount, float durration)
    {
        float elatim = 0;
        Vector3 oldPos = transform.position;

        for (int i = 0; i < ps_BigSmoke.Length; i++)
        {
            ps_BigSmoke[i].enableEmission = true;
        }

        GameObject rub = (GameObject)Instantiate(rubble, rubbleSpawn.position, rubbleSpawn.rotation);
        rubbleSav = rub.GetComponent<ParticleSystem>();

        while (elatim < durration)
        {
            Vector2 ShakePos = Random.insideUnitCircle * (amount * 0.2f);
            transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y, transform.position.z + ShakePos.y);
            //transform.position -= new Vector3(0, amount, 0);
            elatim += 0.5f;
            yield return new WaitForSeconds(0.05f);
        }
        effectAudio.PlayOneShot(effectSounds[0]);
        StartCoroutine(ShakeDown(amount, durration));
    }

    private IEnumerator ShakeDown(float amount, float durration)
    {
        float elatim = 0;
        Vector3 oldPos = transform.position;

        while (elatim < durration)
        {
            Vector2 ShakePos = Random.insideUnitCircle * amount;
            transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y, transform.position.z + ShakePos.y);
            transform.position -= new Vector3(0, amount, 0);
            elatim += 0.5f;
            yield return new WaitForSeconds(0.02f);
        }

        effectAudio.PlayOneShot(effectSounds[1]);
        rubbleSav.enableEmission = true;
        rubbleSav.Play();
        fireAudio.Stop();

        for (int i = 0; i < ps_Smoke.Length; i++)
        {
            ps_Smoke[i].enableEmission = false;
        }

        for (int i = 0; i < ps_Fire.Length; i++)
        {
            ps_Fire[i].SetActive(false);
        }

        for (int i = 0; i < ps_BigSmoke.Length; i++)
        {
            ps_BigSmoke[i].enableEmission = false;
        }
    }
}
