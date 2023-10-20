using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click : ClickParent {

    private bool canSelect = false;

    private float delay = 0.5f;
    bool one_click = false;
    bool timer_running;
    float timerDbClk;
    private GameObject clickGO;

    public Texture2D cursorNormal;
    public Texture2D cursorAttack;
    public Texture2D cursorMoveAttack;
    public Texture2D cursorFriend;

    private enum CursorState { Normal, Target, Aoe, MoveTarget} //Move target is use for dash type spells
    private CursorState currentState;

    public enum Formation { NoFormation, KeepFormation, BoxFormation }
    public Formation curForm;
    private int col;
    [SerializeField] private float sideDist;
    [SerializeField] private float radDist;
    private float offset;
    private Vector3 total2;

    private Health hoverH;
    private bool isHover;
    private bool isFriendlySpell;
    private bool isCastSelfSpell;

    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;
    public Vector2 MidSpot;

    private Vector3 mousePos1;
    private Vector3 mousePos2;
    private Vector3 total;

    [SerializeField]
    private WaypointCtrl wayPoint;

    [SerializeField]
    private LayerMask clickAbleLayer;
    [SerializeField]
    private LayerMask GroundLayerMask;
//    [HideInInspector]

    public UserInterface_Units uiu;

    private Camera cam;
    private New_Camera nw_cam;
    private MiniMap miniMapRef;
    private Hero_UI hroUI;
    public GameObject aoeLight_go;
    private AoeLight alight;
    private int btnA = 0;

    private GraphicRaycaster _graphicRaycaster;
    private EventSystem _eventSystem;
    PointerEventData _pointerEvent;
     
    //MACHINE LEARNING AI TO TAKE OEVR THE WORLD
    public Transform attackPoint;
    public List<Unit> selectableAiUints;

    //Audio
    private AudioSource audi;
    public AudioClip oneUnit;
    public AudioClip multipleUnits;
    public AudioClip moveMany;
    public AudioClip moveOne;
    public AudioClip attack;
    public AudioClip attackMove;

    private bool hasStarted = false;
    void Start()
    {
        EnemyObjects = new List<GameObject>();
        if(isLocalHost)
        {
            SetNormal();
            aoeLight_go = GameObject.FindGameObjectWithTag("AoeLight");
            alight = aoeLight_go.GetComponent<AoeLight>();
            currentState = CursorState.Normal;
        }
    }

    public void StartLocalHost() //Starts on TheStartingTest.StartGame()
    {
        if (isLocalHost)
        {
            miniMapRef = GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>();
            miniMapRef.clickRef = this;
            miniMapRef.StartMiniMap(playerClick);
            _eventSystem = EventSystem.current;
            _graphicRaycaster = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GraphicRaycaster>();
            cam = Camera.main;
            hroUI = GameObject.FindGameObjectWithTag("HeroUI").GetComponent<Hero_UI>();
            nw_cam = cam.GetComponent<New_Camera>();
            audi = GetComponent<AudioSource>();
            hasStarted = true;
        }
    }

    void Update ()
    {
        if(!isLocalHost || !hasStarted) // || !hasStarted
        {
            return;
        }

        switch(currentState)
        {
            case CursorState.Normal:
                #region Normal
                RaycastHit rH;
                Ray ry = cam.ScreenPointToRay(Input.mousePosition);

                if (Input.GetKey(KeyCode.LeftAlt) && MainSystem.sys_Selected.Count > 0)
                {
                    SetMoveAttack();
                }
                else if (Physics.Raycast(ry, out rH, Mathf.Infinity, clickAbleLayer))
                {
                    Health enemy = rH.collider.GetComponent<Health>();
                    if (enemy != null)
                    {
                        if (enemy.healthTeam != playerClick.teamPlayer && MainSystem.sys_Selected.Count > 0)
                        {
                            SetAttack();
                            if (!isHover)
                            {
                                if (enemy.currentHealthType == Health.HealthType.Building)
                                {
                                    if (!enemy.isDead)
                                    {
                                        hoverH = enemy;
                                        enemy.HealthBarHover();
                                        hoverH.ol.OutlineColor = Color.red;
                                        isHover = true;
                                    }
                                }
                                else
                                {
                                    if (!enemy.isDead)
                                    {
                                        hoverH = enemy;
                                        enemy.HealthBarHover();
                                        Unit oul = enemy.GetComponent<Unit>();
                                        oul.ClickMe();
                                        isHover = true;
                                    }
                                }
                            }

                        }
                        else if (enemy.idHealth == playerClick.idPlayer)
                        {
                            SetFriend();
                            if (enemy.currentHealthType == Health.HealthType.Building && !isHover)
                            {
                                if (!enemy.isDead)
                                {
                                    hoverH = enemy;
                                    enemy.HealthBarHover();
                                    hoverH.ol.OutlineColor = Color.white;
                                    isHover = true;
                                }
                            }
                        }
                        else
                        {
                            SetNormal();
                        }

                        if (hoverH == null)
                        {
                            // outline on new hover
                            hoverH = enemy;
                            enemy.HealthBarHover();
                            isHover = true;
                        }
                        else if (hoverH != enemy) //hoverH is old, enemy is new
                        {
                            if (enemy.currentHealthType != Health.HealthType.Building)
                            {
                                if (enemy.healthTeam != playerClick.teamPlayer)
                                {
                                    Unit un = hoverH.GetComponent<Unit>(); // make un.offlights on health script?
                                    if (un != null)
                                    {
                                        if (!un.currentlySelected || un.unitTeam != playerClick.teamPlayer)
                                        {
                                            un.OffLights();
                                        }
                                    }
                                    if (MainSystem.sys_Selected.Count > 0)
                                    {
                                        enemy.GetComponent<Unit>().ClickMe();
                                    }
                                }
                                else
                                {
                                    Unit un = hoverH.GetComponent<Unit>();
                                    if (un != null && un.unitTeam != playerClick.teamPlayer)
                                    {
                                        un.OffLights();
                                    }
                                }
                            }
                            else
                            {
                                if(enemy.healthTeam == playerClick.teamPlayer)
                                {
                                    enemy.ol.OutlineColor = Color.white;
                                }
                                else if(MainSystem.sys_Selected.Count > 0)
                                {
                                    enemy.ol.OutlineColor = Color.red;
                                }

                                if(hoverH.healthTeam == playerClick.teamPlayer)
                                {
                                    Unit un = hoverH.GetComponent<Unit>();
                                    if (un != null)
                                    {
                                        if (!un.currentlySelected || un.unitTeam != playerClick.teamPlayer)
                                        {
                                            un.OffLights();
                                        }
                                    }
                                }
                                else
                                {
                                    Unit un = hoverH.GetComponent<Unit>();
                                    if (un != null && un.unitTeam != playerClick.teamPlayer)
                                    {
                                        un.OffLights();
                                    }
                                }
                            }

                            if (hoverH.currentHealthType == Health.HealthType.Building)
                            {
                                hoverH.ol.OutlineColor = Color.black;
                            }

                            hoverH.HealthBarHoverOff();
                            hoverH = enemy;
                            enemy.HealthBarHover();
                            isHover = true;
                        }

                    }
                    else
                    {
                        SetNormal();
                    }

                }
                else
                {
                    SetNormal();
                    if (isHover)
                    {
                        if (hoverH.currentHealthType == Health.HealthType.Building)
                        {
                            Unit_Spawner uso = hoverH.GetComponent<Unit_Spawner>();
                            if(uso != null)
                            {
                                if (!uso.isUiOpen && !uso.isDestroyed)
                                {
                                    hoverH.ol.OutlineColor = Color.black;
                                    hoverH.HealthBarHoverOff();
                                    hoverH = null;
                                    isHover = false;
                                }
                            }
                            else
                            {
                                hoverH.ol.OutlineColor = Color.black;
                                hoverH.HealthBarHoverOff();
                                hoverH = null;
                                isHover = false;
                            }

                        }
                        else
                        {
                            hoverH.HealthBarHoverOff();

                            if (hoverH.healthTeam != playerClick.teamPlayer)
                            {
                                Unit un = hoverH.GetComponent<Unit>();
                                un.OffLights();
                            }

                            hoverH = null;
                            isHover = false;
                        }
                    }
                }

                if (!_eventSystem.IsPointerOverGameObject())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        canSelect = true;
                        mousePos1 = cam.ScreenToViewportPoint(Input.mousePosition);

                        RaycastHit rayHit;
                        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, clickAbleLayer))
                        {
                            Unit unit = rayHit.collider.GetComponent<Unit>();
                            if (unit != null && unit.idUnit == playerClick.idPlayer)
                            {
                                if (!unit.health.isDead)
                                {
                                    ClickSelectedUnit(unit);
                                }
                                return;
                            }

                            Unit_Spawner baseSpawn = rayHit.collider.GetComponent<Unit_Spawner>();
                            if (baseSpawn != null && baseSpawn.buildingId == playerClick.idPlayer)
                            {
                                if (!baseSpawn.isDestroyed)
                                {
                                    ClickSelectedBase(baseSpawn);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            ClearSelection();
                        }
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        RaycastHit rayHit;
                        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, clickAbleLayer) && MainSystem.sys_Selected.Count > 0)
                        {
                            Health enemy = rayHit.collider.GetComponent<Health>();
                            if (enemy.healthTeam != playerClick.teamPlayer)
                            {
                                audi.PlayOneShot(attack);
                                foreach (Unit Selected in MainSystem.sys_Selected)
                                {
                                    Selected.GetComponent<Unit>().AttackOrder(enemy.gameObject);
                                }
                            }
                        }
                        else if (Physics.Raycast(ray, out rayHit, GroundLayerMask))
                        {
                            if (MainSystem.sys_Selected.Count > 0)
                            {
                                if (Input.GetKey(KeyCode.LeftAlt))
                                {
                                    ClickMoveOrder(rayHit.point, true);
                                }
                                else
                                {
                                    ClickMoveOrder(rayHit.point, false);
                                }
                            }
                        }
                    }

                    if (one_click)
                    {
                        if ((Time.time - timerDbClk) > delay)
                        {
                            one_click = false;
                            clickGO = null;
                        }
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _pointerEvent = new PointerEventData(_eventSystem);
                        _pointerEvent.position = Input.mousePosition;

                        List<RaycastResult> results = new List<RaycastResult>();
                        _graphicRaycaster.Raycast(_pointerEvent, results);

                        foreach (RaycastResult raysult in results)
                        {
                            if (raysult.gameObject.name == "UnitType")
                            {
                                Image img = raysult.gameObject.GetComponent<Image>();
                                GameObject uiUnit = uiu.CheckImageInList(img);
                                Unit unitU = uiUnit.GetComponent<Unit>();
                                if(unitU != null)
                                {
                                    ClickUnitUI(unitU);
                                }
                            }
                        }
                    }

                    if (one_click)
                    {
                        if ((Time.time - timerDbClk) > delay)
                        {
                            one_click = false;
                            clickGO = null;
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    mousePos2 = cam.ScreenToViewportPoint(Input.mousePosition);
                    if (mousePos1 != mousePos2 && canSelect == true)
                    {
                        SelectUnits();
                        canSelect = false;
                    }
                }

                break;
            #endregion 
            case CursorState.Target:
                #region Target
                if (isFriendlySpell)
                {
                    SetFriend();
                }
                else
                {
                    SetAttack();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (!_eventSystem.IsPointerOverGameObject())
                    {
                        mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

                        RaycastHit rayHit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, clickAbleLayer))
                        {
                            Health HealthScript = rayHit.collider.GetComponent<Health>();
                            Spell_Target splTrg = hroUI.Hero_Magic.gameObject.GetComponent<Spell_Target>();

                            float disToTar = Vector3.Distance(HealthScript.transform.position, splTrg.transform.position);
                            if(disToTar < splTrg.sRange)
                            {
                                if (splTrg == null)
                                {
                                    Debug.LogError("Cant find Spell_Target");
                                    SwitchStateNormal();
                                }

                                if (isFriendlySpell)
                                {
                                    if (HealthScript.healthTeam == playerClick.teamPlayer)
                                    {
                                        if(splTrg.gameObject != HealthScript.gameObject)
                                        {
                                            splTrg.TargetSpell(rayHit.collider.gameObject);
                                            hroUI.AbilityButtons[btnA].StartCooldown();
                                            SwitchStateNormal();
                                        }
                                        else if (isCastSelfSpell)
                                        {
                                            splTrg.TargetSpell(rayHit.collider.gameObject);
                                            hroUI.AbilityButtons[btnA].StartCooldown();
                                            SwitchStateNormal();
                                        }
                                    }
                                    else
                                    {
                                        Debug.LogError("Can't do spell on EnemyUnit");
                                    }
                                }
                                else
                                {
                                    if (HealthScript.healthTeam != playerClick.teamPlayer)
                                    {
                                        splTrg.TargetSpell(rayHit.collider.gameObject);
                                        hroUI.AbilityButtons[btnA].StartCooldown();
                                        SwitchStateNormal();
                                    }
                                    else
                                    {
                                        Debug.LogError("Can't do spell on FriendlyUnit");
                                    }
                                }
                            }
                            else
                            {
                                Debug.LogError("The distance between: " + HealthScript.gameObject + " and " + splTrg.gameObject + " is " + disToTar + ", and thats way too far fam.");
                            }
                        }
                        else
                        {
                            Debug.LogError("Ivalid Target");
                        }
                    }
                    else
                    {
                        _pointerEvent = new PointerEventData(_eventSystem);
                        _pointerEvent.position = Input.mousePosition;

                        List<RaycastResult> results = new List<RaycastResult>();
                        _graphicRaycaster.Raycast(_pointerEvent, results);

                        Spell_Target splTrg = hroUI.Hero_Magic.ActiveSpells[btnA] as Spell_Target;

                        foreach (RaycastResult raysult in results)
                        {
                            if (raysult.gameObject.name == "UnitType")
                            {
                                Image img = raysult.gameObject.GetComponent<Image>();
                                GameObject uiUnit = uiu.CheckImageInList(img);
                                Health HealthScript = uiUnit.GetComponent<Health>();

                                float disToTar = Vector3.Distance(HealthScript.transform.position, splTrg.transform.position);
                                if (disToTar < splTrg.sRange)
                                {
                                    if (splTrg == null)
                                    {
                                        Debug.LogError("Cant find Spell_Target");
                                        SwitchStateNormal();
                                    }

                                    if (isFriendlySpell)
                                    {
                                        if (HealthScript.healthTeam == playerClick.teamPlayer)
                                        {
                                            if(splTrg.gameObject != HealthScript.gameObject)
                                            {
                                                splTrg.TargetSpell(uiUnit);
                                                hroUI.AbilityButtons[btnA].StartCooldown();
                                                SwitchStateNormal();
                                            }
                                            else if(isCastSelfSpell)
                                            {
                                                splTrg.TargetSpell(uiUnit);
                                                hroUI.AbilityButtons[btnA].StartCooldown();
                                                SwitchStateNormal();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    SwitchStateNormal();
                }
                break;
            #endregion
            case CursorState.Aoe:
                #region Aoe
                if (isFriendlySpell)
                {
                    SetFriend();
                }
                else
                {
                    SetAttack();
                }

                Ray rayA = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] rayHits = Physics.RaycastAll(rayA.origin, rayA.direction, Mathf.Infinity);
                for (int i = 0; i < rayHits.Length; i++)
                {
                    RaycastHit hit = rayHits[i];
                    if(hit.collider.gameObject.tag == "Ground")
                    {
                        aoeLight_go.transform.position = hit.point;

                        if (Input.GetMouseButtonDown(1))
                        {
                            Debug.Log("GetMouseButtonDown CastAoeSpellDmg");
                            Spell_Aoe aoesp = hroUI.Hero_Magic.ActiveSpells[btnA] as Spell_Aoe;
                            if(aoesp == null)
                            {
                                Debug.LogError("GetMouseButtonDown CastAoeSpellDmg null");
                            }
                            else if (alight.dist <= aoesp.sRange)
                            {
                                aoesp.CastAoeSpell(hit.point);
                                hroUI.AbilityButtons[btnA].StartCooldown();
                                alight.TurnAoeLight_Off();
                                SwitchStateNormal();
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    alight.TurnAoeLight_Off();
                    SwitchStateNormal();
                }
                #endregion
                break;
            case CursorState.MoveTarget:
                #region Move Target
                SetAttack();

                Ray rayB = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] rayHita = Physics.RaycastAll(rayB.origin, rayB.direction, Mathf.Infinity);
                for (int i = 0; i < rayHita.Length; i++)
                {
                    RaycastHit hit = rayHita[i];
                    if (hit.collider.gameObject.tag == "Ground")
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            Debug.Log("GetMouseButtonDown CastSpellDash");
                            Spell_Dash dsh = hroUI.Hero_Magic.ActiveSpells[btnA] as Spell_Dash;
                            if (dsh == null)
                            {
                                Debug.LogError("GetMouseButtonDown Spell_Dash null");
                            }
                            else if (dsh.CanDash(hit.point))
                            {
                                dsh.DashSpell(hit.point);
                                hroUI.AbilityButtons[btnA].StartCooldown();
                                SwitchStateNormal();
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    SwitchStateNormal();
                }
                #endregion
                break;
        }
    }

    #region Functions
    public void SwitchStateTarget(bool isFriendly, bool isCastSelf, int btn)
    {
        currentState = CursorState.Target;
        isFriendlySpell = isFriendly;
        isCastSelfSpell = isCastSelf;
        btnA = btn;
        //ClearSelection();
    }

    public void SwitchStateAoe(bool isFriendly, int btn)
    {
        currentState = CursorState.Aoe;
        isFriendlySpell = isFriendly;
        if(alight == null)
        {
            Debug.LogError("alight null ", gameObject);
        }
        else
        {
            alight.TurnAoeLight_On();
        }

        btnA = btn;
    }

    public void SwitchStateMoveTarget(int btn)
    {
        currentState = CursorState.MoveTarget;
        btnA = btn;    
    }

    public void SwitchStateNormal()
    {
        currentState = CursorState.Normal;
    }

    void ClickSelectedUnit(Unit us)
    {
        if(Input.GetKey("left ctrl") && MainSystem.sys_Selected.Count < 32)
        {
            if (!us.currentlySelected)
            {
                ClickUnit(us);
                audi.PlayOneShot(oneUnit);
            }
            else
            {
                UnClickUnit(us);
            }
        }
        else
        {
            if (!one_click)
            {
                clickGO = null;
                one_click = true;
                clickGO = us.gameObject;
                timerDbClk = Time.time;

                ClearSelection();
                audi.PlayOneShot(oneUnit);
                ClickUnit(us);
            }
            else
            {
                if(clickGO == us.gameObject)
                {
                    SelectAllUnits(us);                   
                }
                else
                {
                    ClearSelection();
                    audi.PlayOneShot(oneUnit);
                    ClickUnit(us);
                }
            }
        }
    }

    void ClickUnitUI(Unit uu)
    {
        if (!one_click)
        {
            clickGO = null;
            one_click = true;
            clickGO = uu.gameObject;
            timerDbClk = Time.time;
        }
        else
        {
            if (clickGO == uu.gameObject)
            {
                ClearSelection();
                audi.PlayOneShot(oneUnit);
                ClickUnit(uu);
            }
        }
    }

    void ClickUnit(Unit ck)
    {
        MainSystem.sys_Selected.Add(ck);
        MainSystem.SortSelected();
        uiu.AddUnitToUI(ck.gameObject);
        ck.ClickMe();
    }

    void UnClickUnit(Unit ck)
    {
        MainSystem.sys_Selected.Remove(ck);
        MainSystem.SortSelected();
        uiu.RemoveUnitToUI(ck.gameObject);
        ck.ClickMe();
    }

    void SelectAllUnits(Unit u)
    {
        ClearSelection();
        audi.PlayOneShot(multipleUnits);
        foreach (Unit unt in MainSystem.sys_SelectableUnits)
        {
            Vector3 screenPoint = cam.WorldToViewportPoint(unt.transform.position);
            if(screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
            {
                if (unt.currentUnitType == u.currentUnitType)
                {
                    MainSystem.sys_Selected.Add(unt);
                    MainSystem.SortSelected();
                    uiu.AddUnitToUI(unt.gameObject);
                    unt.ClickMe();
                }
            }
        }
    }

    void ClickSelectedBase(Unit_Spawner bs)
    {
        bs.TurnOnUi();
    }

    void ClearSelection()
    {
        if(MainSystem.sys_Selected.Count > 0)
        {
            foreach(Unit un in MainSystem.sys_Selected)
            {
                if(un != null)
                {
                    un.DeSelect();
                }
            }
            MainSystem.sort_Selected.Clear();
            MainSystem.sys_Selected.Clear();
            uiu.ClearUnitToUI();
        }
    }

    void SelectUnits()
    {
        List<Unit> remUnits = new List<Unit>();

        if(!Input.GetKey("left ctrl"))
        {
            ClearSelection();
        }
        
        Rect selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        foreach (Unit selectUnit in MainSystem.sys_SelectableUnits)
        {
            if(selectUnit.idUnit == playerClick.idPlayer)
            {
                if (selectUnit != null && MainSystem.sys_Selected.Count < 32)
                {
                    if (selectRect.Contains(Camera.main.WorldToViewportPoint(selectUnit.transform.position), true))
                    {
                        MainSystem.sys_Selected.Add(selectUnit);
                        //uiu.AddUnitToUI(selectUnit.gameObject);             
                        selectUnit.ClickMe();
                    }
                }
                else
                {
                    remUnits.Add(selectUnit);
                }
            }
        }

        if(MainSystem.sys_Selected.Count > 0)
        {
            MainSystem.SortSelected();
            uiu.UpdateUnitUi();

            if(MainSystem.sys_Selected.Count > 1)
            {
                audi.PlayOneShot(multipleUnits);
            }
            else
            {
                audi.PlayOneShot(oneUnit);
            }
        }


        if (remUnits.Count > 0)
        {
            foreach(Unit rem in remUnits)
            {
                uiu.RemoveUnitToUI(rem.gameObject);
                MainSystem.sys_Selected.Remove(rem);
            }
            remUnits.Clear();
        }
    }

    public void ClickMoveOrder(Vector3 pos, bool attack)
    {
        if (MainSystem.sys_SelectableUnits.Count > 0)
        {
            foreach (Unit Selected in MainSystem.sys_Selected)
            {
                if (Selected != null)
                {
                    total += Selected.transform.position;
                }
            }
            Vector3 center = total / MainSystem.sys_Selected.Count;
            switch (curForm)
            {
                case Formation.KeepFormation:
                    foreach (Unit SelectedUnit in MainSystem.sys_Selected)
                    {
                        if (SelectedUnit != null)
                        {
                            Vector3 startPos = SelectedUnit.transform.position - center;
                            if(attack)
                            {
                                SelectedUnit.AttackMoveOrder(pos + startPos);
                                SelectedUnit.atkOrdPos = (pos + startPos);
                            }
                            else
                            {
                                SelectedUnit.MoveOrder(pos + startPos);
                                SelectedUnit.movOrdPos = (pos + startPos);
                            }

                            total = Vector3.zero;
                        }
                    }
                    break;

                case Formation.BoxFormation:
                    //Vector3 dir2 = (pos - center).normalized;
                    //Vector3 dir = (center - pos).normalized;

                    if (MainSystem.sys_Selected.Count < 12)
                    {
                        col = 4;
                    }
                    if (MainSystem.sys_Selected.Count >= 12)
                    {
                        col = 5;
                    }
                    if (MainSystem.sys_Selected.Count > 24)
                    {
                        col = 7;
                    }
                    if (MainSystem.sys_Selected.Count > 36)
                    {
                        col = 9;
                    }

                    offset = ((sideDist * col) / 2) * -1;
                    bool first = false;
                    int curCol = 0;

                    for (int i = 0; i < MainSystem.sys_Selected.Count; i++)
                    {
                        if (first)
                        {
                            if (curCol >= col)
                            {
                                total2 += new Vector3(0, 0, radDist);
                                total2.x = 0;
                                curCol = 0;
                            }
                            else
                            {
                                curCol += 1;
                                total2 += new Vector3(sideDist, 0, 0);
                            }
                        }
                        else
                        {
                            first = true;
                        }

                        Vector3 startPos = new Vector3(total2.x + offset, 0, total2.z);

                        if (MainSystem.sys_Selected[i] != null)
                        {
                            if(attack)
                            {
                                MainSystem.sys_Selected[i].AttackMoveOrder(pos + startPos);
                                MainSystem.sys_Selected[i].atkOrdPos = (pos + startPos);
                            }
                            else
                            {
                                MainSystem.sys_Selected[i].MoveOrder(pos + startPos);
                                MainSystem.sys_Selected[i].movOrdPos = (pos + startPos);
                            }
                        }
                    }
                    total2 = Vector3.zero;
                    break;
            }

            if (!attack)
            {
                wayPoint.WayPoint(pos, true);
                if (MainSystem.sys_Selected.Count > 1)
                {
                    audi.PlayOneShot(moveMany);
                }
                else if (MainSystem.sys_Selected.Count == 1)
                {
                    audi.PlayOneShot(moveOne);
                }
            }
            else
            {
                audi.PlayOneShot(attackMove);
                wayPoint.WayPoint(pos, false);
            }
        }
    }

    public void SetNormal()
    {
        Cursor.SetCursor(cursorNormal, hotSpot, cursorMode);
    }

    public void SetFriend()
    {
        Cursor.SetCursor(cursorFriend, hotSpot, cursorMode);
    }

    public void SetAttack()
    {
        Cursor.SetCursor(cursorAttack, hotSpot, cursorMode);
    }

    public void SetMoveAttack()
    {
        Cursor.SetCursor(cursorMoveAttack, hotSpot, cursorMode);
    }
    #endregion
}
