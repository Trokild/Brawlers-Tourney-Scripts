using UnityEngine;

[CreateAssetMenu(fileName = "New List Upg Arch", menuName = "AI Upgrades/List/Spearmen")]
public class AI_ListUpgSpear : ScriptableObject
{
    [SerializeField] private AI_UpgradeSpear[] SpearUpgs_Aggressive1;
    [SerializeField] private AI_UpgradeSpear[] SpearUpgs_Aggressive2;
    [SerializeField] private AI_UpgradeSpear[] SpearUpgs_Aggressive3;
    [Space]
    [SerializeField] private AI_UpgradeSpear[] SpearUpgs_Defensive1;
    [SerializeField] private AI_UpgradeSpear[] SpearUpgs_Defensive2;
    [SerializeField] private AI_UpgradeSpear[] SpearUpgs_Defensive3;
    [Space]
    [SerializeField] private AI_UpgradeSpear[] SpearUpgs_Neutral1;
    [SerializeField] private AI_UpgradeSpear[] SpearUpgs_Neutral2;
    [SerializeField] private AI_UpgradeSpear[] SpearUpgs_Neutral3;

    public AI_UpgradeSpear[] GetAggresiveList(int a)
    {
        switch (a)
        {
            case 1:
                return SpearUpgs_Aggressive1;

            case 2:
                return SpearUpgs_Aggressive2;

            case 3:
                return SpearUpgs_Aggressive3;

            default:
                return null;
        }
    }

    public AI_UpgradeSpear[] GetDefensiveList(int a)
    {
        switch (a)
        {
            case 1:
                return SpearUpgs_Defensive1;

            case 2:
                return SpearUpgs_Defensive2;

            case 3:
                return SpearUpgs_Defensive3;

            default:
                return null;
        }
    }

    public AI_UpgradeSpear[] GetNeturalList(int a)
    {
        switch (a)
        {
            case 1:
                return SpearUpgs_Neutral1;

            case 2:
                return SpearUpgs_Neutral2;

            case 3:
                return SpearUpgs_Neutral3;

            default:
                return null;
        }
    }
}
