using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Runtime.Promises;
using UnityTools.UnityRuntime.Timers;

namespace ZombieFarm.InteractableObjects
{ 
    public class Seedbed : OnTriggerInteractionBase
    {
        [Serializable]
        public struct GrowStep
        {
            public float time;
            public GameObject gameObject;
        }

        [SerializeField] private List<GrowStep> growSteps;

        private IPromise growingQueue;

        protected override void Awake()
        {
            base.Awake();

            HideAllPlantsSteps();
        }

        protected override void FinishProcess()
        {
            base.FinishProcess();

            if (growingQueue == null)
            {
                StartGrow();
            }
        }

        private void StartGrow()
        {
            growingQueue = Deferred.Resolved();

            foreach (GrowStep growStep in growSteps)
            {
                growingQueue = growingQueue.Then(() => SetGrowStep(growStep));
            }
        }

        private IPromise SetGrowStep(GrowStep growStep)
        {
            HideAllPlantsSteps();

            growStep.gameObject.SetActive(true);

            return Timer.Instance.WaitUnscaled(growStep.time);
        }

        private void HideAllPlantsSteps()
        {
            foreach (GrowStep growStep in growSteps)
            {
                growStep.gameObject.SetActive(false);
            }
        }
    }
}
