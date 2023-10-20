using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour {

    public Unit un;
    public GameObject GoingToHit;
    public GameObject GoingToHit2;
    public GameObject GoingToHit3;
    public GameObject Insidething;
    public float speed;
    public float acceleration;
    public Unit_Appearance cav;

	void Start ()
    {
        Invoke("Thisfunc", 2f);
        //cav.ArmorWeaponShield(4, 4, 2);
        //cav.ChangeHorseMat(2);
        
    }
	
	// Update is called once per frame
	void Thisfunc () {
        un.GetPushed(transform, 3.5f);
        //StartCoroutine(Shove(GoingToHit.transform, speed, acceleration));
    }

    void HitThis()
    {
        if(speed > 0)
        {
            GoingToHit.transform.position = Vector3.MoveTowards(GoingToHit.transform.position, transform.position, -1 * speed * Time.deltaTime);
            speed -= acceleration * Time.deltaTime;
        }

    }

    void HitThis2()
    {

            GoingToHit2.transform.position = Vector3.MoveTowards(GoingToHit2.transform.position, transform.position, -1 * speed);

    }

    public IEnumerator SuperCharge(Transform targt, float secs)
    {
            float elatim = 0;
            Vector3 Dir = (transform.position - targt.position).normalized;
            Vector3 startp = targt.position;

            while (elatim < secs)
            {
                targt.position = Vector3.Lerp(startp, (targt.position - (Dir * 2.5f)), (elatim / secs));
                elatim += Time.deltaTime;
                yield return new WaitForEndOfFrame(); //Needs to be sec, endofframe causes issues when you have diffrent fps
            }        
        
    }

    public IEnumerator Shove(Transform targt, float speed, float aks)
    {
        StartCoroutine(HitUp(Insidething.transform, 1));

        float maksv = speed;
        float startTime = Time.time;
        while (speed > 0)
        {
            GoingToHit.transform.position = Vector3.MoveTowards(GoingToHit.transform.position, targt.position, -1 * speed * Time.deltaTime);
            aks += 0.1f;
            speed -= aks * Time.deltaTime;

            yield return new WaitForEndOfFrame(); //Needs to be sec, endofframe causes issues when you have diffrent fps
        }
        yield break;
    }

    public IEnumerator HitUp(Transform targt, float tim)
    {
        Vector3 sunrise = new Vector3(0, 0, 0);
        Vector3 sunset = new Vector3(0, 10, 0);
        Vector3 center = (sunrise + sunset) * 0.5F;

        float elatim = 0;

        while (elatim < tim)
        {

            Vector3 riseRelCenter = sunrise - center;
            Vector3 setRelCenter = sunset - center;

            Vector3 newVek = Vector3.Slerp(riseRelCenter, setRelCenter, (elatim / tim));
            float newVekY = newVek.y;
            elatim += Time.deltaTime;
            targt.localPosition = new Vector3(0, newVekY, 0);
            targt.localPosition += center;

            yield return new WaitForEndOfFrame(); //Needs to be sec, endofframe causes issues when you have diffrent fps
        }

        yield return new WaitForSeconds(0.1f);
        elatim = 0;
        
        while (elatim < (tim + tim))
        {

            Vector3 riseRelCenter = sunrise - center;
            Vector3 setRelCenter = sunset - center;

            Vector3 newVek = Vector3.Slerp(setRelCenter, riseRelCenter,(elatim / (tim + tim)));
            float newVekY = newVek.y;
            tim -= 0.01f;
            elatim += Time.deltaTime;
            targt.localPosition = new Vector3(0, newVekY, 0);
            targt.localPosition += center;

            yield return new WaitForEndOfFrame(); //Needs to be sec, endofframe causes issues when you have diffrent fps
        }

        yield break;
    }

}
