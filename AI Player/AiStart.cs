public class AiStart : PlayerStart
{
    public int AiBehav;

    public enum StartAiType { Netrual, Aggressor, Defensive }
    public StartAiType currentAi;
    public int st_Ai;

    public void NeturalAi()
    {
        currentAi = StartAiType.Netrual;
        st_Ai = 0;
    }

    public void AggresiveAi()
    {
        currentAi = StartAiType.Aggressor;
        st_Ai = 1;
    }

    public void DefensiveAi()
    {
        currentAi = StartAiType.Defensive;
        st_Ai = 2;
    }
}
