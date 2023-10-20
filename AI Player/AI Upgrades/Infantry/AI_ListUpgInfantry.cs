using UnityEngine;

[CreateAssetMenu(fileName = "New List Upg Inf", menuName = "AI Upgrades/List/Infantry")]
public class AI_ListUpgInfantry : ScriptableObject
{
    [SerializeField] private AI_UpgradeInf[] InfUpgs_Aggressive1; //sword
    [SerializeField] private AI_UpgradeInf[] InfUpgs_Aggressive2; //axe
    [SerializeField] private AI_UpgradeInf[] InfUpgs_Aggressive3; //mace
    [Space]
    [SerializeField] private AI_UpgradeInf[] InfUpgs_Defensive1; 
    [SerializeField] private AI_UpgradeInf[] InfUpgs_Defensive2;
    [SerializeField] private AI_UpgradeInf[] InfUpgs_Defensive3;
    [Space]
    [SerializeField] private AI_UpgradeInf[] InfUpgs_Neutral1;
    [SerializeField] private AI_UpgradeInf[] InfUpgs_Neutral2;
    [SerializeField] private AI_UpgradeInf[] InfUpgs_Neutral3;

    public AI_UpgradeInf[] GetAggresiveList(int a)
    {
        switch(a)
        {
            case 1:
                return InfUpgs_Aggressive1;

            case 2:
                return InfUpgs_Aggressive2;

            case 3:
                return InfUpgs_Aggressive3;

            default:
                return null;
        }
    }

    public AI_UpgradeInf[] GetDefensiveList(int a)
    {
        switch (a)
        {
            case 1:
                return InfUpgs_Defensive1;

            case 2:
                return InfUpgs_Defensive2;

            case 3:
                return InfUpgs_Defensive3;

            default:
                return null;
        }
    }

    public AI_UpgradeInf[] GetNeturalList(int a)
    {
        switch (a)
        {
            case 1:
                return InfUpgs_Neutral1;

            case 2:
                return InfUpgs_Neutral2;

            case 3:
                return InfUpgs_Neutral3;

            default:
                return null;
        }
    }
}
