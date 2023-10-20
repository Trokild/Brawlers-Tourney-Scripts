using System.Collections;
using UnityEngine;

public class RockProjectile : Projectile
{
    public GameObject[] Rock;
    [SerializeField] private ParticleSystem[] ps;
    private int rkInt;

    public override void FireProjectile(GameObject tar, float time, float height)
    {
        StartCoroutine(ThrowRock(tar.transform, time, height));
    }

    private IEnumerator ThrowRock(Transform end, float secs, float hght)
    {
        yield return new WaitForSeconds(0.2f);
        transform.parent = null;
        rkInt = apLvl;
        Rock[apLvl].SetActive(true);
        ps[apLvl].enableEmission = true;

        Vector3 startPos = transform.position;
        Vector3 heightV = new Vector3(0, 1.2f, 0);
        float elatim = 0;

        while (elatim < secs)
        {
            currentMovementDirection = transform.position;
            Vector3 dir = currentMovementDirection - previousPosition;
            if (currentMovementDirection != previousPosition)
            {
                previousPosition = currentMovementDirection;
            }
            transform.Rotate(0, 0, 5);

            transform.position = MathParabola.Parabola(startPos, (end.position + heightV), hght, (elatim / secs));
            elatim += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end.position;
        ps[rkInt].enableEmission = false;
        ResPos();
    }

    protected override void ResPos()
    {
        transform.parent = father.transform;
        Rock[rkInt].SetActive(false);
        transform.localPosition = savPos;
    }
}
