using TMPro;
using UnityEngine;

public class SpawnerHealth : BuildingHealth
{
    private Unit_Spawner us;

    public override void TeamId(int team, int id, int color)
    {
        us = GetComponent<Unit_Spawner>();
        if (us != null)
        {
            us.buildingTeam = team;
            us.buildingId = id;
            us.SetColor(color);
            if (id == MainSystem.LocalPlayerMainSystem.idPlayer)
            {
                us.SetGameConsol();
            }
        }
        else
        {
            Debug.LogError("Can't find Unit_Spawner.cs", gameObject);
        }
        if(mapd != null)
        {
            mapd.gameObject.SetActive(true);
            mapd.SetColorDot(color);
        }

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

            if (!underAttack && !isTower)
            {
                if (us != null) // DOnt need to check if null after Health script clean up, is should always have a ref
                {
                    if (!us.playerRef.isLocal)
                    {
                        us.playerRef.gameObject.GetComponent<AI_Controller>().DefendBase();
                        Warning();
                    }
                }
            }
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
                            else
                            {
                                p.GetComponent<AI_UpgraderCtrl>().NewBaseAI();
                            }
                            us.playerRef.PlayerOut(murderer);
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
            Debug.Log("DestroyBuilding BuildingHealth");
            MainSystem.RemoveBuildingList(this);
            us.DestroyedBuilding();

            Destroy(ol);
            fireAudio.minDistance = 20f;
            fireAudio.maxDistance = 60f;
            StartCoroutine(Shake(0.1f, 40));
        }
    }

    protected override void Warning()
    {
        if (!isWarning)
        {
            if (us.playerRef.isLocal)
            {
                mapd.MapWarningDot();
                isWarning = true;
            }
            else
            {
                us.playerRef.gameObject.GetComponent<AI_Controller>().DefendBase();
            }
        }
    }
}
