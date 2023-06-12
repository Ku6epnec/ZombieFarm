using System;
using System.Collections;
using UnityEngine;
using ZombieFarm.Interfaces;
using ZombieFarm.Views.Player;

public class Cage : MonoBehaviour, IRemovableObject, IHealth
{
    public event Action<bool> OnDestroyProcess = (inProgress) => { };
    public float Health => _health;
    public float MaxHealth => _maxHealth;

    [SerializeField] AnimalFollow animal;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] private Transform cageModel;
    [SerializeField] private ParticleSystem disappearVFX;

    private float destroyTimeout = 2f;
    private PlayerView playerView;

    public float _maxHealth = 10;
    public float _health = 10;

    private float _damage = 1;

    private float Maxtimer = 2.0f;
    private float recievedDamageTimer;

    private void Awake()
    {
        progressBar.ProcessCompleted += Free;
        progressBar.InitSlider(_maxHealth);
    }

    private void Update()
    {
        recievedDamageTimer -= Time.deltaTime;
    }

    private void OnDestroy()
    {
        progressBar.ProcessCompleted -= Free;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerView>().RegisterRemovableObject(this);
            OnDestroyProcess(true);

            progressBar.gameObject.SetActive(true);
            _health -= _damage;
            progressBar.StartProgress(_health);
        }
    }

    public void RecievedDamage(float damage)
    {
        if (recievedDamageTimer <= 0)
        {
            _health -= damage;
            progressBar.StartProgress(_health);
            recievedDamageTimer = Maxtimer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            progressBar.gameObject.SetActive(false);
            OnDestroyProcess(false);
        }
    }

    private void Free()
    {
        OnDestroyProcess(false);

        cageModel.gameObject.SetActive(false);
        
        animal.StartFollowing();
        
        disappearVFX.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(false);

        StartCoroutine(DestroyTimer(destroyTimeout));
    }

    private IEnumerator DestroyTimer(float destroyTimeout)
    {
        yield return new WaitForSeconds(destroyTimeout);

        Destroy(gameObject);
    }
}
