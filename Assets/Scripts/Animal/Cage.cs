using System;
using System.Collections;
using UnityEngine;
using ZombieFarm.Interfaces;
using ZombieFarm.Views.Player;

public class Cage : ReceivedDamageObject, IRemovableObject, IHealth
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;

    public event Action<bool> OnDestroyProcess = (inProgress) => { };
    internal override event Action CleanInteractiveObject = () => { };

    [SerializeField] AnimalFollow animal;
    [SerializeField] ProgressBar progressBar;

    [SerializeField] private Transform cageModel;
    [SerializeField] private ParticleSystem disappearVFX;

    [SerializeField] private float _maxHealth = 10;
    [SerializeField] private float _health = 10;

    private float destroyTimeout = 2f;
    private float maxTimer = 2.0f;
    private float receivedDamageTimer;

    public override void Interaction(float damage)
    {
        ReceivedDamage(damage);
    }

    private void Awake()
    {
        progressBar.ProcessCompleted += Free;
        progressBar.InitSlider(_maxHealth);
    }

    private void Update()
    {
        receivedDamageTimer -= Time.deltaTime;
    }

    private void OnDestroy()
    {
        progressBar.ProcessCompleted -= Free;
        CleanInteractiveObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            progressBar.gameObject.SetActive(true);
            OnDestroyProcess(true);
        }
    }

    public void ReceivedDamage(float damage)
    {
        if (receivedDamageTimer <= 0)
        {
            _health -= damage;
            progressBar.RefreshProgress(_health);
            receivedDamageTimer = maxTimer;
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
