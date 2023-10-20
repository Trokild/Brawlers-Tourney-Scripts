using System.Collections;
using UnityEngine;

public class ArrowProjectile : Projectile {

    public GameObject[] Arrow;
    [SerializeField] private ParticleSystem[] ps;


    public override void FireProjectile(GameObject tar, float time, float height)
    {
        StartCoroutine(ShootArrow(tar.transform, time, height));
    }

    public override void FireProjectileStraight(GameObject tar, float time)
    {
        StartCoroutine(ShootArrowStraight(tar.transform, time));
    }

    private IEnumerator ShootArrow(Transform end, float secs, float hght)
    {
        yield return new WaitForSeconds(0.2f);
        transform.parent = null;
        Arrow[apLvl].SetActive(true);
        ps[apLvl].enableEmission = true;

        Vector3 startPos = transform.position;
        Vector3 heightV = new Vector3(0, 1.2f, 0);
        float elatim = 0;

        while(elatim < secs)
        {
            currentMovementDirection = transform.position;
            Vector3 dir = currentMovementDirection - previousPosition;
            if (currentMovementDirection != previousPosition)
            {
                previousPosition = currentMovementDirection;
            }
            transform.rotation = Quaternion.LookRotation(dir);

            transform.position = MathParabola.Parabola(startPos, (end.position + heightV), hght, (elatim / secs));
            elatim += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end.position;
        ps[apLvl].enableEmission = false;
        ResPos();
    }

    private IEnumerator ShootArrowStraight(Transform end, float secs)
    {
        yield return new WaitForSeconds(0.2f);
        transform.parent = null;
        Arrow[apLvl].SetActive(true);
        ps[apLvl].enableEmission = true;

        Vector3 startPos = transform.position;
        float elatim = 0;

        Vector3 heightV = new Vector3(0, 1.2f, 0);

        while (elatim < secs)
        {
            transform.position = Vector3.Lerp(startPos, (end.position + heightV), (elatim / secs));
            //parabola
            elatim += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end.position;
        ps[apLvl].enableEmission = false;
        ResPos();
    }

    protected override void ResPos()
    {
        transform.parent = father.transform;
        Arrow[apLvl].SetActive(false);
        transform.localPosition = savPos;
    }
}
