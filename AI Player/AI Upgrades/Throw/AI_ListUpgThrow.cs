using UnityEngine;

[CreateAssetMenu(fileName = "New List Upg Throw", menuName = "AI Upgrades/List/Throw Orc")]
public class AI_ListUpgThrow : ScriptableObject
{
    [SerializeField] private Ai_UpgradeThrow[] Thow_Aggressive1;
    [SerializeField] private Ai_UpgradeThrow[] Thow_Aggressive2;
    [SerializeField] private Ai_UpgradeThrow[] Thow_Aggressive3;
    [Space]
    [SerializeField] private Ai_UpgradeThrow[] Thow_Defensive1;
    [SerializeField] private Ai_UpgradeThrow[] Thow_Defensive2;
    [SerializeField] private Ai_UpgradeThrow[] Thow_Defensive3;
    [Space]
    [SerializeField] private Ai_UpgradeThrow[] Thow_Neutral1;
    [SerializeField] private Ai_UpgradeThrow[] Thow_Neutral2;
    [SerializeField] private Ai_UpgradeThrow[] Thow_Neutral3;

    public Ai_UpgradeThrow[] GetAggresiveList(int a)
    {
        switch (a)
        {
            case 1:
                return Thow_Aggressive1;

            case 2:
                return Thow_Aggressive2;

            case 3:
                return Thow_Aggressive3;

            default:
                return null;
        }
    }

    public Ai_UpgradeThrow[] GetDefensiveList(int a)
    {
        switch (a)
        {
            case 1:
                return Thow_Defensive1;

            case 2:
                return Thow_Defensive2;

            case 3:
                return Thow_Defensive3;

            default:
                return null;
        }
    }

    public Ai_UpgradeThrow[] GetNeturalList(int a)
    {
        switch (a)
        {
            case 1:
                return Thow_Neutral1;

            case 2:
                return Thow_Neutral2;

            case 3:
                return Thow_Neutral3;

            default:
                return null;
        }
    }
}
