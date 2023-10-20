using UnityEngine;

public class Apperance_DualWeild : Apperance_Orc
{
    [SerializeField] private SkinnedMeshRenderer _MeshUnitHeadCharge;
    // SHIELDS == OFFHAND
    void OtherWepOff()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i].activeSelf)
            {
                weapons[i].SetActive(false);
            }
        }
        for (int i = 0; i < shields.Length; i++)
        {
            if (shields[i].activeSelf)
            {
                shields[i].SetActive(false);
            }
        }
    }

    public override void WeaponInt(int w)
    {
        if(weapons.Length != shields.Length)
        {
            Debug.LogError("Not sync wep amd shield lenght");
            return;
        }
        if(isShow)
        {
            OtherWepOff();
        }

        if (w < weapons.Length && w >= 0)
        {
            int mainH = 0;
            int offH = 0;
            switch(w)
            {
                case 0: //Hatcet Dagger rand
                    mainH = Random.Range(0, 2);
                    offH = Random.Range(0, 2);
                    break;
                case 1: //Mace Club rand
                    mainH = Random.Range(2, 4);
                    offH = Random.Range(2, 4);
                    break;
                case 2: //Sword Ace Rand
                    mainH = Random.Range(4, 6);
                    offH = Random.Range(4, 6);
                    break;

                default:
                    mainH = w;
                    offH = w;
                    break;
            }

            weapons[mainH].SetActive(true);
            shields[offH].SetActive(true);

            if (hif != null)
            {
                hif.GetMeshFromGO(weapons[mainH]);
                hif.GetMeshFromGO(shields[offH]);
            }

        }
        else
        {
            Debug.LogError("Invalid weapon int: " + w);
        }
    }

    public void ArmorDualInt(int a)
    {
        if (a < 3)
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

                default:
                    break;
            }
        }
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
        ArmsC();
        NoShoulders();
    }

    protected override void Armor_Costume3()
    {
        BodyArmorA();
        LegsA();
        ArmsA();
        NoShoulders();
    }

    public void ChargerHead(int hc)
    {
        if(hc == 0)
        {
            NoHelm();
        }
        else
        {
            Hooded();
        }

        switch (hc)
        {
            case 1:
                _MeshUnitHeadCharge.material = colorMat[2];
                break;
            case 2:
                _MeshUnitHeadCharge.material = colorMat[6];
                break;
            case 3:
                _MeshUnitHeadCharge.material = colorMat[5];
                break;
            default:
                _MeshUnitHeadCharge.material = colorMat[0];
                break;
        }
    }

    public void DualArmorWep(int arm, int wep)
    {
        ArmorDualInt(arm);
        NoHelm();
        if (wep < weapons.Length)
        {
            WeaponInt(wep);
        }
        CreatOutline();
    }

    public void DualArmorWepCharge(int arm, int wep, int hc)
    {
        ArmorDualInt(arm);   //turns head on NoHelm
        ChargerHead(hc); //then gives it the ponytail one

        if (wep < weapons.Length)
        {
            WeaponInt(wep);
        }
        CreatOutline();
    }
}
