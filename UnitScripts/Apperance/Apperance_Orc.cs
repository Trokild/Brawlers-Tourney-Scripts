
public class Apperance_Orc : Unit_Appearance
{
    public override void ArmorInt(int a)
    {
        if (a < 4)
        {
            switch (a)
            {
                case 0:
                    Armor_Costume1();
                    break;

                case 1:
                    Armor_Costume2();
                    break;

                case 2:
                    Armor_Costume3();
                    break;

                case 3:
                    Armor_Costume4();
                    break;

                default:
                    Armor_Costume1();
                    break;
            }
        }
        else
        {
            Armor_Costume1();
        }
    }

    protected virtual void Armor_Costume1()
    {
        BodyArmorB();
        LegsB();
        ArmsB();
        NoShoulders();
        NoHelm();
    }

    protected virtual void Armor_Costume2()
    {
        BodyArmorC();
        LegsA();
        ArmsA();
        NoShoulders();
        NoHelm();
    }

    protected virtual void Armor_Costume3()
    {
        BodyArmorD();
        LegsC();
        ArmsC();
        NoShoulders();
        Hooded();
    }

    protected virtual void Armor_Costume4()
    {
        BodyArmorE();
        LegsC();
        ArmsC();
        ShoulderB();
        Hooded();
    }

    protected void BodyArmorE()
    {
        if (!bodyArmor[4].activeSelf)
        {
            bodyArmor[4].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(bodyArmor[4]);
        }
        for (int i = 0; i < bodyArmor.Length; i++)
        {
            if (i != 4)
            {
                bodyArmor[i].SetActive(false);
            }
        }
    }

}
