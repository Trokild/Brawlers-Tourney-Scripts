using TMPro;
using UnityEngine;

public class TowerHealth : BuildingHealth
{
    [SerializeField]private TowerUnit towerUnit;

    protected override void Start()
    {
        base.Start();
        towerUnit = GetComponent<TowerUnit>();
    }

    public override void TeamId(int team, int id, int color)
    {
        mapd.gameObject.SetActive(true);
        mapd.SetColorDot(color);
        //towerUnit.enabled = true;
        towerUnit.SetUpTower(color);

        healthTeam = team;
        idHealth = id;
        Cur_Health = max_Health;
        Invoke("AddBuilding", 0.5f);
    }

    public override void TakeDamage(int amount, int armPerc, int side, int murderer)
    {
        if (isDead == false)
        {
            int arm = armor.GetValue() - Mathf.RoundToInt(armor.GetValue() * (armPerc / 100f));
            int dmg = amount - arm;

            dmg = Mathf.Clamp(dmg, 1, int.MaxValue);
            Cur_Health -= dmg;

            Healthbar();

            if (Cur_Health <= 0)
            {
                Cur_Health = 0;
                DestroyBuilding();
                if (!rewardGiven)
                {
                    foreach (Player p in MainSystem.PlayerList)
                    {
                        if (p.idPlayer == murderer) // && p.isLocal
                        {
                            //Visuall reward
                            p.gold += goldReward;
                            if (p.isLocal)
                            {
                                PopUpTxt.SetActive(true);
                                PopUpTxt.GetComponent<TextMeshProUGUI>().SetText("+" + goldReward.ToString());
                            }

                            rewardGiven = true;
                        }
                    }
                }
            }

            if (Cur_Health < ((float)max_Health * 0.6) && !isHalfDestroyed)
            {
                for (int i = 0; i < ps_Smoke.Length; i++)
                {
                    ps_Smoke[i].enableEmission = true;
                }
                isHalfDestroyed = true;
            }

            if (Cur_Health < ((float)max_Health * 0.3) && !isQuaterDestroyed)
            {
                fireAudio.Play();

                for (int i = 0; i < ps_Fire.Length; i++)
                {
                    ps_Fire[i].SetActive(true);
                }
                isQuaterDestroyed = true;
            }
        }
    }

    protected override void DestroyBuilding()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("DestroyBuilding TowerHealth");
            MainSystem.RemoveBuildingList(this);

            mapd.DotDead();
            GetComponent<TowerUnit>().DeathTower();

            Destroy(ol);
            fireAudio.minDistance = 20f;
            fireAudio.maxDistance = 60f;
            StartCoroutine(Shake(0.1f, 40));
        }
    }
}
