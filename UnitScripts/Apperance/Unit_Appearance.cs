using System.Collections;
using FoW;
using UnityEngine;

public class Unit_Appearance : MonoBehaviour {

    //private GameObject thisGO;
    //public Unit un;
    public Material[] colorMat;
    //public Material[] colorDot;
    //public MeshRenderer theDot;
    public GameObject[] weapons;
    public GameObject[] shields;
    public GameObject[] bodyArmor;
    public GameObject[] legs;
    public GameObject[] arms;
    public GameObject[] shoulders;
    public GameObject[] helmet;
    public SkinnedMeshRenderer[] _MeshUnitBody;
    public MeshRenderer[] _MeshUnitOther;
    [SerializeField] protected HideInFog hif;
    public bool isShow = false;

    public virtual void Start()
    {
        //ChangeUnitColor(3);
        //Invoke("Armor_2", 6);
        //Invoke("Armor_3", 9);
        //Invoke("Armor_4", 13);
        //Invoke("Spear", 16);
        //Invoke("SmallSword", 19);
        //Invoke("BigSword", 23);
        //Debug.Log("Unit_Appearance");
    }
    
    public void HeroOutline()
    {
        Outline outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.green;
        outline.OutlineWidth = 1f;
        Hero un = gameObject.GetComponentInParent<Hero>();
        un.selectionOutline = outline;
        un.selectionOutline.enabled = false;

        StartCoroutine(OutlineSpawn());
    }

    public void ChangeUnitColor(int cc)
    {
        for (int i = 0; i < _MeshUnitBody.Length; i++)
        {
            _MeshUnitBody[i].material = colorMat[cc];
        }
        //if (!isShow)
        //{
        //    theDot.material = colorDot[cc];
        //}
        //if (hif != null)
        //{
        //    hif.GetTheMesh(theDot);
        //}
    }

    public void ChangeUnitColorNoDot(int cc) //no minimap dot
    {
        for (int i = 0; i < _MeshUnitBody.Length; i++)
        {
            _MeshUnitBody[i].material = colorMat[cc];
        }
    }

    public void ChangeOtherColor(int cc) //with weapon meshes
    {
        for (int i = 0; i < _MeshUnitOther.Length; i++)
        {
            _MeshUnitOther[i].material = colorMat[cc];
        }

        for (int i = 0; i < _MeshUnitBody.Length; i++)
        {
            _MeshUnitBody[i].material = colorMat[cc];
        }

        //theDot.material = colorDot[cc];
        //if (hif != null)
        //{
        //    hif.GetTheMesh(theDot);
        //}
    }

    public void ArmorWeapon(int a, int w)
    {
        ArmorInt(a);

        if (w < weapons.Length)
        {
            WeaponInt(w);
        }

        //Outline outline = gameObject.AddComponent<Outline>();
        //outline.OutlineMode = Outline.Mode.OutlineAll;
        //outline.OutlineColor = Color.green;
        //outline.OutlineWidth = 1f;
        //Unit un = gameObject.GetComponentInParent<Unit>();
        //un.selectionOutline = outline;
        //un.selectionOutline.enabled = false;
        CreatOutline();
        StartCoroutine(OutlineSpawn());
    }

    public void ArmorWeaponShield(int a, int w, int s)
    {
        ArmorInt(a);

        if (w < weapons.Length)
        {
            WeaponInt(w);
        }

        if (s < shields.Length)
        {
            ShieldInt(s);
        }

        //Outline outline = gameObject.AddComponent<Outline>();
        //outline.OutlineMode = Outline.Mode.OutlineAll;
        //outline.OutlineColor = Color.green;
        //outline.OutlineWidth = 1f;
        //Unit un = gameObject.GetComponentInParent<Unit>();
        //un.selectionOutline = outline;
        //outline.enabled = false;
        CreatOutline();
        StartCoroutine(OutlineSpawn());
    }

    public void ArmorShield(int a, int s, bool ol)
    {
        ArmorInt(a);
        ShieldInt(s);
        if (ol)
        {
            CreatOutline();
        }
    }

    public void Bigger(float size, int wep)
    {
        Vector3 ols = transform.localScale;
        transform.localScale = ols * (1 + (size / 100));

        Vector3 wes = weapons[wep].transform.localScale;
        weapons[wep].transform.localScale = wes * (1 - (size / 100));
    }

    public void BiggerShield(float size, int wep, int shi)
    {
        Vector3 ols = transform.localScale;
        transform.localScale = ols * (1 + (size / 100));

        Vector3 wes = weapons[wep].transform.localScale;
        weapons[wep].transform.localScale = wes * (1 - (size / 100));

        Vector3 sha = shields[shi].transform.localScale;
        shields[shi].transform.localScale = sha * (1 - (size / 100));
    }

    #region Armor

    public virtual void ArmorInt(int a)
    {
        if(a < 5)
        {
            switch (a)
            {
                case 0:
                    NoArmor();
                    break;

                case 1:
                    Armor_1();
                    break;

                case 2:
                    Armor_2();
                    break;

                case 3:
                    Armor_3();
                    break;

                case 4:
                    Armor_4();
                    break;

                default:
                    break;
            }
        }
        else
        {
            Armor_4();
        }
    }

    protected void NoArmor()
    {
        BodyArmorA();
        LegsA();
        ArmsA();
        NoShoulders();
        NoHelm();
    }

    protected void Armor_1()
    {
        BodyArmorB();
        LegsB();
        ArmsB();
        NoShoulders();
        NoHelm();
    }

    protected void Armor_2()
    {
        BodyArmorC();
        LegsB();
        ArmsD();
        NoShoulders();
        Hooded();
    }

    protected void Armor_3()
    {
        BodyArmorC();
        LegsC();
        ArmsC();
        SmallHelm();
        ShoulderB();
    }

    protected void Armor_4()
    {
        BodyArmorD();
        LegsC();
        ArmsC();
        FullHelm();
        ShoulderA();
    }
    #endregion

    #region Weapons

    public virtual void WeaponInt(int w)
    {
        if (w < weapons.Length && w >= 0)
        {
            weapons[w].SetActive(true);
            if (hif != null)
            {
                hif.GetMeshFromGO(weapons[w]);
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                if (i != w)
                {
                    weapons[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("Invalid weapon int: " + w);
        }
    }

    public void UnarmWep()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i != 0)
            {
                weapons[i].SetActive(false);
            }
        }
    }

    #endregion

    #region Shields

    public void NoShield()
    {
        for (int i = 0; i < shields.Length; i++)
        {
            shields[i].SetActive(false);
        }
    }

    public void UnarmShid()
    {
        for (int i = 0; i < shields.Length; i++)
        {
            if (i != 0)
            {
                shields[i].SetActive(false);
            }
        }
    }

    public void ShieldInt(int sh)
    {
        if (sh < shields.Length && sh >= 0)
        {
            shields[sh].SetActive(true);
            if (hif != null)
            {
                hif.GetMeshFromGO(shields[sh]);
            }
            for (int i = 0; i < shields.Length; i++)
            {
                if (i != sh)
                {
                    shields[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("Invalid Shield int");
        }
    }

    void RoundShield()
    {
        shields[0].SetActive(true);
        if (hif != null)
        {
            hif.GetMeshFromGO(shields[0]);
        }
        for (int i = 0; i < shields.Length; i++)
        {
            if (i != 0)
            {
                shields[i].SetActive(false);
            }
        }
    }

    void SquareShield()
    {
        shields[1].SetActive(true);
        if (hif != null)
        {
            hif.GetMeshFromGO(shields[1]);
        }
        for (int i = 0; i < shields.Length; i++)
        {
            if (i != 1)
            {
                shields[i].SetActive(false);
            }
        }
    }

    void MetalShield()
    {
        shields[2].SetActive(true);
        if (hif != null)
        {
            hif.GetMeshFromGO(shields[2]);
        }
        for (int i = 0; i < shields.Length; i++)
        {
            if (i != 2)
            {
                shields[i].SetActive(false);
            }
        }
    }

    void ThickShield()
    {
        shields[3].SetActive(true);
        if (hif != null)
        {
            hif.GetMeshFromGO(shields[3]);
        }
        for (int i = 0; i < shields.Length; i++)
        {
            if (i != 3)
            {
                shields[i].SetActive(false);
            }
        }
    }
    #endregion

    #region Body Armor

    protected void BodyArmorA()
    {
        if(!bodyArmor[0].activeSelf)
        {
            bodyArmor[0].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(bodyArmor[0]);
        }
        for (int i = 0; i < bodyArmor.Length; i++)
        {
            if (i != 0)
            {
                bodyArmor[i].SetActive(false);
            }
        }
    }

    protected void BodyArmorB()
    {
        if (!bodyArmor[1].activeSelf)
        {
            bodyArmor[1].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(bodyArmor[1]);
        }
        for (int i = 0; i < bodyArmor.Length; i++)
        {
            if (i != 1)
            {
                bodyArmor[i].SetActive(false);
            }
        }
    }

    protected void BodyArmorC()
    {
        if (!bodyArmor[2].activeSelf)
        {
            bodyArmor[2].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(bodyArmor[2]);
        }
        for (int i = 0; i < bodyArmor.Length; i++)
        {
            if (i != 2)
            {
                bodyArmor[i].SetActive(false);
            }
        }
    }

    protected void BodyArmorD()
    {
        if (!bodyArmor[3].activeSelf)
        {
            bodyArmor[3].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(bodyArmor[3]);
        }
        for (int i = 0; i < bodyArmor.Length; i++)
        {
            if (i != 3)
            {
                bodyArmor[i].SetActive(false);
            }
        }
    }

    #endregion

    #region Legs

    protected void LegsA()
    {
        if (!legs[0].activeSelf)
        {
            legs[0].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(legs[0]);
        }
        for (int i = 0; i < legs.Length; i++)
        {
            if (i != 0)
            {
                legs[i].SetActive(false);
            }
        }
    }

    protected void LegsB()
    {
        if (!legs[1].activeSelf)
        {
            legs[1].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(legs[1]);
        }
        for (int i = 0; i < legs.Length; i++)
        {
            if (i != 1)
            {
                legs[i].SetActive(false);
            }
        }
    }

    protected void LegsC()
    {
        if (!legs[2].activeSelf)
        {
            legs[2].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(legs[2]);
        }
        for (int i = 0; i < legs.Length; i++)
        {
            if (i != 2)
            {
                legs[i].SetActive(false);
            }
        }
    }

    #endregion

    #region Shoulder

    public void NoShoulders()
    {
        for (int i = 0; i < shoulders.Length; i++)
        {
            shoulders[i].SetActive(false);
        }
    }

    public void ShoulderA()
    {
        if (!shoulders[0].activeSelf)
        {
            shoulders[0].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(shoulders[0]);
        }
        for (int i = 0; i < shoulders.Length; i++)
        {
            if (i != 0)
            {
                shoulders[i].SetActive(false);
            }
        }
    }

    public void ShoulderB()
    {
        if (!shoulders[1].activeSelf)
        {
            shoulders[1].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(shoulders[1]);
        }
        for (int i = 0; i < shoulders.Length; i++)
        {
            if (i != 1)
            {
                shoulders[i].SetActive(false);
            }
        }
    }
    #endregion

    #region Helmet

    protected void NoHelm()
    {
        if (!helmet[0].activeSelf)
        {
            helmet[0].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(helmet[0]);
        }
        for (int i = 0; i < helmet.Length; i++)
        {
            if (i != 0)
            {
                helmet[i].SetActive(false);
            }
        }
    }

    protected void Hooded()
    {
        if (!helmet[1].activeSelf)
        {
            helmet[1].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(helmet[1]);
        }
        for (int i = 0; i < helmet.Length; i++)
        {
            if (i != 1)
            {
                helmet[i].SetActive(false);
            }
        }
    }

    protected void SmallHelm()
    {
        if (!helmet[2].activeSelf)
        {
            helmet[2].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(helmet[2]);
        }
        for (int i = 0; i < helmet.Length; i++)
        {
            if (i != 2)
            {
                helmet[i].SetActive(false);
            }
        }
    }

    protected void FullHelm()
    {
        if (!helmet[3].activeSelf)
        {
            helmet[3].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(helmet[3]);
        }
        for (int i = 0; i < helmet.Length; i++)
        {
            if (i != 3)
            {
                helmet[i].SetActive(false);
            }
        }
    }

    #endregion

    #region Arms
    protected void ArmsA()
    {
        if (!arms[0].activeSelf)
        {
            arms[0].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(arms[0]);
        }
        for (int i = 0; i < arms.Length; i++)
        {
            if (i != 0)
            {
                arms[i].SetActive(false);
            }
        }
    }

    protected void ArmsB()
    {
        if (!arms[1].activeSelf)
        {
            arms[1].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(arms[1]);
        }
        for (int i = 0; i < arms.Length; i++)
        {
            if (i != 1)
            {
                arms[i].SetActive(false);
            }
        }
    }

    protected void ArmsC()
    {
        if (!arms[2].activeSelf)
        {
            arms[2].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(arms[2]);
        }
        for (int i = 0; i < arms.Length; i++)
        {
            if (i != 2)
            {
                arms[i].SetActive(false);
            }
        }
    }

    void ArmsD()
    {
        if (!arms[3].activeSelf)
        {
            arms[3].SetActive(true);
        }
        if (hif != null)
        {
            hif.GetMeshFromGO(arms[3]);
        }
        for (int i = 0; i < arms.Length; i++)
        {
            if (i != 3)
            {
                arms[i].SetActive(false);
            }
        }
    }
    #endregion

    protected void CreatOutline()
    {
        Outline outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.green;
        outline.OutlineWidth = 1f;

        Unit un = gameObject.GetComponentInParent<Unit>();
        if(un != null)
        {
            un.selectionOutline = outline;
            un.selectionOutline.enabled = false;
            StartCoroutine(OutlineSpawn());
        }
    }

    protected IEnumerator OutlineSpawn()
    {
        Unit un = gameObject.GetComponentInParent<Unit>();
        un.selectionOutline.enabled = true;

        yield return new WaitForSeconds(0.1f);

        if (!un.currentlySelected)
        {
            un.selectionOutline.enabled = false;
        }
    }
}
