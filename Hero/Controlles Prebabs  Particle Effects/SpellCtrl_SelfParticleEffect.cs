using UnityEngine;

public class SpellCtrl_SelfParticleEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particleSystems;
    private Transform follow;

    private void Update()
    {
        if(follow != null)
        {
            transform.position = follow.position;
        }
    }

    public void StartParticleSystems(float cooldown, Transform hero)
    {
        follow = hero;
        if (_particleSystems.Length > 0)
        {
            for (int i = 0; i < _particleSystems.Length; i++)
            {
                _particleSystems[i].Play();
            }
        }
        Invoke("StopParticleSystems", cooldown);
    }

    void StopParticleSystems()
    {
        if (_particleSystems.Length > 0)
        {
            for (int i = 0; i < _particleSystems.Length; i++)
            {
                _particleSystems[i].Stop();
            }
        }
        Destroy(this.gameObject, 5f);
    }
}
