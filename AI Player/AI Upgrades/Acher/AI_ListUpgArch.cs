using UnityEngine;

[CreateAssetMenu(fileName = "New List Upg Arch", menuName = "AI Upgrades/List/Archer")]
public class AI_ListUpgArch : ScriptableObject
{
    [SerializeField] private AI_UpgradeArch[] ArchUpgs_Aggressive1; 
    [SerializeField] private AI_UpgradeArch[] ArchUpgs_Aggressive2; 
    [SerializeField] private AI_UpgradeArch[] ArchUpgs_Aggressive3; 
    [Space]
    [SerializeField] private AI_UpgradeArch[] ArchUpgs_Defensive1;
    [SerializeField] private AI_UpgradeArch[] ArchUpgs_Defensive2;
    [SerializeField] private AI_UpgradeArch[] ArchUpgs_Defensive3;
    [Space]
    [SerializeField] private AI_UpgradeArch[] ArchUpgs_Neutral1;
    [SerializeField] private AI_UpgradeArch[] ArchUpgs_Neutral2;
    [SerializeField] private AI_UpgradeArch[] ArchUpgs_Neutral3;

    public AI_UpgradeArch[] GetAggresiveList(int a)
    {
        switch (a)
        {
            case 1:
                return ArchUpgs_Aggressive1;

            case 2:
                return ArchUpgs_Aggressive2;

            case 3:
                return ArchUpgs_Aggressive3;

            default:
                return null;
        }
    }

    public AI_UpgradeArch[] GetDefensiveList(int a)
    {
        switch (a)
        {
            case 1:
                return ArchUpgs_Defensive1;

            case 2:
                return ArchUpgs_Defensive2;

            case 3:
                return ArchUpgs_Defensive3;

            default:
                return null;
        }
    }

    public AI_UpgradeArch[] GetNeturalList(int a)
    {
        switch (a)
        {
            case 1:
                return ArchUpgs_Neutral1;

            case 2:
                return ArchUpgs_Neutral2;

            case 3:
                return ArchUpgs_Neutral3;

            default:
                return null;
        }
    }
}
