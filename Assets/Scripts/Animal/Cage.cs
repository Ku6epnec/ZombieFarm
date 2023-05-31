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
    //count
    [SerializeField] AnimalFollow animal;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] private Transform cageModel;
    [SerializeField] private ParticleSystem disappearVFX;

    private float destroyTimeout = 2f;
    private PlayerView playerView;

    public float _maxHealth = 10;
    public float _health = 10;

    private float _damage = 1;

    private void Awake()
    {
        progressBar.ProcessCompleted += Free;
        progressBar.InitSlider(_maxHealth);
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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            progressBar.gameObject.SetActive(false);
            //progressBar.ResetProgress();

            OnDestroyProcess(false);
        }
    }

    private void Free()
    {
        OnDestroyProcess(false);

        cageModel.gameObject.SetActive(false);
        
        //set animal to follow
        animal.StartFollowing();
        
        disappearVFX.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(false);

        StartCoroutine(DestroyTimer(destroyTimeout));
    }

    private IEnumerator DestroyTimer(float destroyTimeout)
    {
        yield return new WaitForSeconds(destroyTimeout);

        //delete the cage
        Destroy(gameObject);
    }
}
