using System;
using System.Collections;
using UnityEngine;
using ZombieFarm.Interfaces;
using ZombieFarm.Views.Player;

public class Cage : ReceivedDamageObject, IRemovableObject, IHealth
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

    [SerializeField] private float _maxHealth = 10;
    [SerializeField] private float _health = 10;

    private float _damage = 1;

    private float maxTimer = 2.0f;
    private float receivedDamageTimer;

    [SerializeField] private ZombieFarm.Views.Player.InteractiveArea interactiveArea;

    public override void Interaction(float damage)
    {
        ReceivedDamage(damage);
    }

    private void Awake()
    {
        interactiveArea = FindObjectOfType<InteractiveArea>();
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
        interactiveArea.InteractiveObject = null;
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

    public void ReceivedDamage(float damage)
    {
        if (receivedDamageTimer <= 0)
        {
            _health -= damage;
            progressBar.StartProgress(_health);
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
