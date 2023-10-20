using TMPro;
using UnityEngine;

public class Hero_StatsUI : MonoBehaviour
{
    //Where do you initilize this? Hero_UI
    [SerializeField] private TextMeshProUGUI damagTxt;
    [SerializeField] private TextMeshProUGUI attkSTxt;
    [SerializeField] private TextMeshProUGUI armPTxt;
    [SerializeField] private TextMeshProUGUI ArmrTxt;
    [SerializeField] private TextMeshProUGUI HPTxt;
    [SerializeField] private TextMeshProUGUI manaTxt;
    [Space]
    [SerializeField]
    private Color Col_normal;
    [SerializeField]
    private Color Col_buff;

    //private Hero_StatsUI heroStatUI;

    //private void Update()
    //{
    //    if(heroStatUI != null)
    //    {

    //    }
    //}

    public void SetStat_Attack(Vector3Int ero) // (Dmg, Attkspd, AP)
    {
        damagTxt.SetText(ero.x.ToString());
        attkSTxt.SetText(ero.y.ToString());
        armPTxt.SetText(ero.z.ToString());
    }

    public void SetStat_Defense(Vector2Int def) // (Armor, Hp)
    {
        ArmrTxt.SetText(def.x.ToString());
        HPTxt.SetText(def.y.ToString());
    }

    public void SetStat_Magic(int mag) //Mana
    {
        manaTxt.SetText(mag.ToString());
    }

    #region Green Txt
    public void GreenText_Damage(int t)
    {
        damagTxt.SetText(t.ToString());
        damagTxt.color = Col_buff;
    }

    public void GreenText_AttackSpd(int t)
    {
        attkSTxt.SetText(t.ToString());
        attkSTxt.color = Col_buff;
    }

    public void GreenText_AP(int t)
    {
        armPTxt.SetText(t.ToString());
        armPTxt.color = Col_buff;
    }

    public void GreenText_Armor(int t)
    {
        ArmrTxt.SetText(t.ToString());
        ArmrTxt.color = Col_buff;
    }

    public void GreenText_HealthRegen(int t)
    {
        HPTxt.SetText(t.ToString());
        HPTxt.color = Col_buff;
    }

    public void GreenText_ManaRegen(int t)
    {
        manaTxt.SetText(t.ToString());
        manaTxt.color = Col_buff;
    }
    #endregion

    #region Normal Txt
    public void NormalText_Damage(int t)
    {
        damagTxt.SetText(t.ToString());
        damagTxt.color = Col_normal;
    }

    public void NormalText_AttackSpd(int t)
    {
        attkSTxt.SetText(t.ToString());
        attkSTxt.color = Col_normal;
    }

    public void NormalText_AP(int t)
    {
        armPTxt.SetText(t.ToString());
        armPTxt.color = Col_normal;
    }

    public void NormalText_Armor(int t)
    {
        ArmrTxt.SetText(t.ToString());
        ArmrTxt.color = Col_normal;
    }

    public void NormalText_HealthRegen(int t)
    {
        HPTxt.SetText(t.ToString());
        HPTxt.color = Col_normal;
    }

    public void NormalText_ManaRegen(int t)
    {
        manaTxt.SetText(t.ToString());
        manaTxt.color = Col_normal;
    }
    #endregion
    //Update Stat, both lvl up [x], Buffs[x] and Items[]
}
