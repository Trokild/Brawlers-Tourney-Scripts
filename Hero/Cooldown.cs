public class Cooldown
{
    public bool isReady;
    public float Durration;

    public Cooldown (bool isR, float durr)
    {
        isReady = isR;
        Durration = durr;
    }

    public void ActivateCD(float durr)
    {
        isReady = false;
        Durration = durr;
    }

    public void SetReadyCD()
    {
        isReady = true;
        Durration = 0;
    }
}
