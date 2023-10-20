using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BuildBuilding : MonoBehaviour
{
    public enum BuilderRace { Werdoom, Urka}
    public BuilderRace RaceBuilder;

    [SerializeField] private AudioClip ClickBuilding;
    [SerializeField] private AudioClip BuyBuilding;
    public int[] BasePrizes;
    [SerializeField] private GameObject[] Prizes;
    [SerializeField] private TextMeshProUGUI[] PrizesTxt;
    [SerializeField] private GameObject ChooseBld_Werdoom;
    [SerializeField] private GameObject ChooseBld_Urka;
    [SerializeField] private GameObject ChooseBldBtn;
    [Space]
    [SerializeField] private GameObject[] things;
    [SerializeField] private ParticleSystem smokeDonut;
    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image progressBar;
    [SerializeField] private Spawner_Manager sm;
    [SerializeField] private bool isRight;
    [SerializeField] private float timeBuild;
    [SerializeField] private bool isAi;
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void SetAi()
    {
        isAi = true;
        ChooseBldBtn.SetActive(false);
    }

    public void Prize_Show(int p)
    {
        Prizes[p].SetActive(true);
    }

    public void Prize_Hide(int p)
    {
        Prizes[p].SetActive(false);
    }

    public void SetLocal(int rInt)
    {
        isAi = false;
        ChooseBldBtn.SetActive(true);
        switch(rInt)
        {
            case 0:
                RaceBuilder = BuilderRace.Werdoom;
                break;
            case 1:
                RaceBuilder = BuilderRace.Urka;
                break;
            default:
                RaceBuilder = BuilderRace.Werdoom;
                break;
        }
    }

    public void BuildBtn_Show()
    {
        switch (RaceBuilder)
        {
            case BuilderRace.Werdoom:
                ChooseBld_Werdoom.SetActive(true);
                break;

            case BuilderRace.Urka:
                ChooseBld_Urka.SetActive(true);
                break;
        }

        ChooseBldBtn.SetActive(false);
        audio.PlayOneShot(ClickBuilding);

        for (int i = 0; i < PrizesTxt.Length; i++)
        {
            PrizesTxt[i].SetText(BasePrizes[i].ToString());
        }
    }

    public void BuildBtn_Hide()
    {
        switch (RaceBuilder)
        {
            case BuilderRace.Werdoom:
                ChooseBld_Werdoom.SetActive(false);
                break;

            case BuilderRace.Urka:
                ChooseBld_Urka.SetActive(false);
                break;
        }
        ChooseBldBtn.SetActive(true);
    }

    public void StartBuilding(int Type)
    {
        if(isRight)
        {
            if (!sm.rightBaseBuildt)
            {
                if (!isAi)
                {
                    if(sm.smPlayerRef.gold >= BasePrizes[Type])
                    {
                        sm.smPlayerRef.gold -= BasePrizes[Type];
                        BasePrizes[Type] = 0;
                        audio.PlayOneShot(BuyBuilding);
                    }
                    else
                    {
                        return;
                    }
                }
                switch (RaceBuilder)
                {
                    case BuilderRace.Werdoom:
                        ChooseBld_Werdoom.SetActive(false);
                        break;

                    case BuilderRace.Urka:
                        ChooseBld_Urka.SetActive(false);
                        break;
                }
                ChooseBldBtn.SetActive(false);

                smokeDonut.enableEmission = true;
                hammer.SetActive(true);
                canvas.SetActive(true);
                StartCoroutine(Building(timeBuild, Type));
                StartCoroutine(ProgressBar(timeBuild));
            }
        }
        else
        {
            if (!sm.leftBaseBuildt)
            {
                if (!isAi)
                {
                    if (sm.smPlayerRef.gold >= BasePrizes[Type])
                    {
                        sm.smPlayerRef.gold -= BasePrizes[Type];
                        BasePrizes[Type] = 0;
                        audio.PlayOneShot(BuyBuilding);
                    }
                    else
                    {
                        return;
                    }
                }
                switch (RaceBuilder)
                {
                    case BuilderRace.Werdoom:
                        ChooseBld_Werdoom.SetActive(false);
                        break;

                    case BuilderRace.Urka:
                        ChooseBld_Urka.SetActive(false);
                        break;
                }

                ChooseBldBtn.SetActive(false);

                smokeDonut.enableEmission = true;
                hammer.SetActive(true);
                canvas.SetActive(true);
                StartCoroutine(Building(timeBuild, Type));
                StartCoroutine(ProgressBar(timeBuild));
            }
        }
    }

    private IEnumerator Building(float sec, int ty)
    {
        float tim = (sec - 3f) / things.Length;
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < things.Length; i++)
        {
            things[i].SetActive(true);
            yield return new WaitForSeconds(tim);
        }
        gameObject.SetActive(false);
        sm.SetUpBase(ty, isRight);
    }

    private IEnumerator ProgressBar(float secs)
    {
        float elatim = 0;

        while (elatim < secs)
        {
            elatim += Time.deltaTime;
            progressBar.fillAmount = elatim / secs;
            yield return new WaitForEndOfFrame();
        }
    }
}
