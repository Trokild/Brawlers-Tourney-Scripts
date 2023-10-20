public class Apperanc_Throw : Apperance_Orc
{
    public void NormalHelm(int h)
    {
        switch (h)
        {
            case 0:
                NoHelm();
                break;

            case 1:
                NoHelm();
                break;

            case 2:
                Hooded();
                break;

            case 3:
                Hooded();
                break;

            default:
                NoHelm();
                break;
        }
    }

    public void GooglesHelm()
    {
        SmallHelm();
    }

    protected override void Armor_Costume1()
    {
        BodyArmorB();
        LegsB();
        ArmsB();
        NoShoulders();
    }

    protected override void Armor_Costume2()
    {
        BodyArmorC();
        LegsA();
        ArmsA();
        NoShoulders();
    }

    protected override void Armor_Costume3()
    {
        BodyArmorD();
        LegsC();
        ArmsC();
        NoShoulders();
    }

    protected override void Armor_Costume4()
    {
        BodyArmorE();
        LegsC();
        ArmsC();
        ShoulderB();
    }
}
