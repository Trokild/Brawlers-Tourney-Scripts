using UnityEngine;

[CreateAssetMenu(fileName = "New List Upg Dual", menuName = "AI Upgrades/List/Dual Orc")]
public class AI_ListUpgDual : ScriptableObject
{
    [SerializeField] private Ai_UpgradeDual[] DualUpgs_Aggressive1;
    [SerializeField] private Ai_UpgradeDual[] DualUpgs_Aggressive2;
    [SerializeField] private Ai_UpgradeDual[] DualUpgs_Aggressive3;
    [Space]
    [SerializeField] private Ai_UpgradeDual[] DualUpgs_Defensive1;
    [SerializeField] private Ai_UpgradeDual[] DualUpgs_Defensive2;
    [SerializeField] private Ai_UpgradeDual[] DualUpgs_Defensive3;
    [Space]
    [SerializeField] private Ai_UpgradeDual[] DualUpgs_Neutral1;
    [SerializeField] private Ai_UpgradeDual[] DualUpgs_Neutral2;
    [SerializeField] private Ai_UpgradeDual[] DualUpgs_Neutral3;

    public Ai_UpgradeDual[] GetAggresiveList(int a)
    {
        switch (a)
        {
            case 1:
                return DualUpgs_Aggressive1;

            case 2:
                return DualUpgs_Aggressive2;

            case 3:
                return DualUpgs_Aggressive3;

            default:
                return null;
        }
    }

    public Ai_UpgradeDual[] GetDefensiveList(int a)
    {
        switch (a)
        {
            case 1:
                return DualUpgs_Defensive1;

            case 2:
                return DualUpgs_Defensive2;

            case 3:
                return DualUpgs_Defensive3;

            default:
                return null;
        }
    }

    public Ai_UpgradeDual[] GetNeturalList(int a)
    {
        switch (a)
        {
            case 1:
                return DualUpgs_Neutral1;

            case 2:
                return DualUpgs_Neutral2;

            case 3:
                return DualUpgs_Neutral3;

            default:
                return null;
        }
    }
}
