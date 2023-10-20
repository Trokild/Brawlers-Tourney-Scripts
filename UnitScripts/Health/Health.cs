using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public int healthTeam;
    public int idHealth;

    public int goldReward;
    public int xpReward;
    protected bool rewardGiven = false;

    public Stat armor;

    public float sizeRange;
    public bool isDead;
    public bool isRipeDead;

    public int max_Health;
    public int Cur_Health { get; protected set; }


    public enum HealthType { Infantry, Cavalry, Spearmen, Hero, Building }
    public HealthType currentHealthType;

    [SerializeField]
    private Color fullColor;
    [SerializeField]
    private Color midColor;
    [SerializeField]
    private Color lowColor;

    [SerializeField] private Image health_bar;
    [SerializeField] private GameObject healthBarGO;
    [SerializeField] protected CanvasLookAt canvas;
    [SerializeField] protected GameObject PopUpTxt;
    private float val;
    private float duration = 3f;
    public bool underAttack { get; protected set; }

    [HideInInspector]
    public ClickParent clickRef;
    public Outline ol;
    protected AudioSource audioS;

    protected virtual void Start()
    {
        audioS = GetComponent<AudioSource>();

        if (ol == null)
        {
            ol = GetComponent<Outline>();
        }
    }

    public void SetBaseHealth(int maxH, StatArmor ar)
    {
        max_Health = maxH;
        Cur_Health = max_Health;
        armor.SetBaseValue(ar.armor);
    }



    public virtual void TeamId(int team, int id, int color)
    {
        healthTeam = team;
        idHealth = id;
    }

    public virtual void TakeDamage(int amount, int armPerc, int side, int murderer)
    {
        if (isDead == false)
        {
            int dmg;
            int arm = armor.GetValue() - Mathf.RoundToInt(armor.GetValue() * (armPerc / 100f));


            if (side == 2)
            {
                dmg = (amount + (amount * side)) - arm;

            }
            else if (side == 1)
            {
                dmg = amount - arm;
            }
            else
            {

                dmg = amount - arm;
            }

            dmg = Mathf.Clamp(dmg, 1, int.MaxValue);
            Cur_Health -= dmg;

            Healthbar();

            if (Cur_Health <= 0)
            {
                Cur_Health = 0;
                Death();

                if (!rewardGiven)
                {
                    foreach (Player p in MainSystem.PlayerList)
                    {
                        if (p.idPlayer == murderer) // && p.isLocal
                        {
                            p.gold += goldReward;
                            p.PlayerXp(xpReward, transform.position);

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
        }
    }

    public void Heal(int amount, bool healthbar)
    {
        if (amount > 0)
        {
            Cur_Health += amount;
            if (Cur_Health > max_Health)
            {
                Cur_Health = max_Health;
            }

            if (healthbar)
            {
                Healthbar();
            }
        }
    }

    public void HealTick(int ticks, int healA, float rate)
    {
        StartCoroutine(TickHealFucntion(ticks, healA, rate));
    }

    IEnumerator TickHealFucntion(int t, int h, float r)
    {
        for (int i = 0; i < t; i++)
        {
            if (!isDead)
            {
                Heal(h, true);
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(r);
        }
    }

    public void FullHealth()
    {
        Cur_Health = max_Health;
        if (isDead)
        {
            isDead = false;
        }
    }

    protected virtual void Death()
    {
    }

    protected void Healthbar()
    {
        if (!underAttack)
        {
            healthBarGO.SetActive(true);
            duration = 3f;
            StartCoroutine(HealthBarOff());
        }
        else
        {
            duration = 3f;
        }

        val = (Cur_Health * 1f) / max_Health;
        health_bar.fillAmount = val;

        if (val >= 0.5f)
        {
            float CorrectedValG = ((val - 0.5f)) / (1 - 0.5f);
            health_bar.GetComponent<Image>().color = Color.Lerp(midColor, fullColor, CorrectedValG);
        }
        else if (val < 0.8f && val >= 0.1)
        {
            float CorrectedValY = ((val - 0.1f)) / (0.5f - 0.1f);
            health_bar.GetComponent<Image>().color = Color.Lerp(lowColor, midColor, CorrectedValY);
        }
        else if (val < 0.1f)
        {
            health_bar.GetComponent<Image>().color = lowColor;
        }

    }

    IEnumerator HealthBarOff()
    {
        underAttack = true;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        healthBarGO.SetActive(false);
        underAttack = false;
    }

    public void HealthBarHover()
    {
        if (!underAttack)
        {
            healthBarGO.SetActive(true);
            duration = 3f;
            underAttack = true;
        }
    }

    public void HealthBarHoverOff()
    {
        if (underAttack)
        {
            healthBarGO.SetActive(false);
            duration = 0f;
            underAttack = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sizeRange);
    }
}
