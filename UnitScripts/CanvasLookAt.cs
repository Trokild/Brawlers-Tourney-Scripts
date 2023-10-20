using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CanvasLookAt : MonoBehaviour {

    Camera m_Camera;
    Animator anim;
    [SerializeField] private Image[] Effectors;
    private List<Affliction> Affliction_Canvas = new List<Affliction>();
    [SerializeField] private Image effect;

    void Start()
    {
        m_Camera = Camera.main;
        anim = GetComponent<Animator>();
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
    }
    void Update()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
    }

    public void NewEffect(Buff_Affliction aff)
    {
        //Debug.Log("NewEffect");
        effect.sprite = aff.AfflitionSprite;
        effect.gameObject.SetActive(true);
        anim.SetTrigger("Effect");
        Affliction_Canvas.Add(aff);
        int idx = (Affliction_Canvas.Count - 1);
        Effectors[idx].sprite = aff.AfflitionSprite;
        GameObject uiA = Effectors[idx].gameObject;
        StartCoroutine(NewAffliction(aff, uiA, aff.Durration));
    }

    public void AddEffectPassiv(Affliction aff)
    {
        if(Affliction_Canvas.Contains(aff))
        {
            Debug.LogError("AddEffectPassiv already on");
            return;
        }
        //Debug.Log("NewEffectPassive");
        Affliction_Canvas.Add(aff);
        int idx = (Affliction_Canvas.Count - 1);
        Effectors[idx].sprite = aff.AfflitionSprite;
        GameObject uiA = Effectors[idx].gameObject;
        uiA.SetActive(true);
    }

    public void RemoveEffectPassiv(Affliction aff)
    {
        //Debug.Log("RemoveEffectPassiv");
        int idx = Affliction_Canvas.IndexOf(aff);
        Affliction_Canvas.Remove(aff);
        Effectors[idx].sprite = aff.AfflitionSprite;
        GameObject uiA = Effectors[idx].gameObject;
        uiA.SetActive(false);
    }

    private IEnumerator NewAffliction(Affliction a, GameObject go, float tim)
    {
        yield return new WaitForSeconds(1f);

        go.gameObject.SetActive(true);
        
        StartCoroutine(RemoveAffliction(a ,go, tim));
    }

    private IEnumerator RemoveAffliction(Affliction aff ,GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        Affliction_Canvas.Remove(aff);
        go.SetActive(false);

        for (int i = 0; i < Effectors.Length; i++)
        {
            Effectors[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < Affliction_Canvas.Count; i++)
        {
            Effectors[i].gameObject.SetActive(true);
            Effectors[i].sprite = Affliction_Canvas[i].AfflitionSprite;
        }
    }
}
