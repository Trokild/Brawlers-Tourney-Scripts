using UnityEngine;

[CreateAssetMenu(fileName = "New List Upg Cavalry", menuName = "AI Upgrades/List/Cavalry")]
public class AI_ListUpgCavalry : ScriptableObject
{
    [SerializeField] private AI_UpgradeCav[] CavUpgs_Aggressive1; //sword
    [SerializeField] private AI_UpgradeCav[] CavUpgs_Aggressive2; //axe
    [SerializeField] private AI_UpgradeCav[] CavUpgs_Aggressive3; //mace
    [Space]
    [SerializeField] private AI_UpgradeCav[] CavUpgs_Defensive1;
    [SerializeField] private AI_UpgradeCav[] CavUpgs_Defensive2;
    [SerializeField] private AI_UpgradeCav[] CavUpgs_Defensive3;
    [Space]
    [SerializeField] private AI_UpgradeCav[] CavUpgs_Neutral1;
    [SerializeField] private AI_UpgradeCav[] CavUpgs_Neutral2;
    [SerializeField] private AI_UpgradeCav[] CavUpgs_Neutral3;

    public AI_UpgradeCav[] GetAggresiveList(int a)
    {
        switch (a)
        {
            case 1:
                return CavUpgs_Aggressive1;

            case 2:
                return CavUpgs_Aggressive2;

            case 3:
                return CavUpgs_Aggressive3;

            default:
                return null;
        }
    }

    public AI_UpgradeCav[] GetDefensiveList(int a)
    {
        switch (a)
        {
            case 1:
                return CavUpgs_Defensive1;

            case 2:
                return CavUpgs_Defensive2;

            case 3:
                return CavUpgs_Defensive3;

            default:
                return null;
        }
    }

    public AI_UpgradeCav[] GetNeturalList(int a)
    {
        switch (a)
        {
            case 1:
                return CavUpgs_Neutral1;

            case 2:
                return CavUpgs_Neutral2;

            case 3:
                return CavUpgs_Neutral3;

            default:
                return null;
        }
    }
}
