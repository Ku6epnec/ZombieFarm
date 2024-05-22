using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZombieFarm.Config.Links;
using ZombieFarm.Config.LinkTargets;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.UI
{
    public class SettingsWindow : MonoBehaviour, IWindow
    {
        [Header("Main Settings elements")]
        [SerializeField] private Button vibrationButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button soundButton;

        [Header("Vibration Settings elements")]
        [SerializeField] private bool vibration;
        [SerializeField] private GameObject vibrationOn;
        [SerializeField] private GameObject vibrationOff;

        [Header("Music Settings elements")]
        [SerializeField] private bool music;
        [SerializeField] private GameObject musicOn;
        [SerializeField] private GameObject musicOff;

        [Header("Sound Settings elements")]
        [SerializeField] private bool sound;
        [SerializeField] private GameObject soundOn;
        [SerializeField] private GameObject soundOff;

        [Header("Socials elements")]
        [SerializeField] private Button telegramButton;
        [SerializeField] private Button vkButton;
        [SerializeField] private Button discordButton;
        [SerializeField] private Button tiktokButton;

        [Header("Final elements")]
        [SerializeField] private Button googlePlayButton;
        [SerializeField] private Button authorsButton;
        [SerializeField] private Button resetButton;

        private void Start()
        {
            gameObject.SetActive(false);
            vibration = true;
            music = true;
            sound = true;
        }

        public void VibrationChange()
        {
            if (!vibration)
            {
                vibrationOn.SetActive(true);
                vibrationOff.SetActive(false);
            }
            else
            {
                vibrationOff.SetActive(true);
                vibrationOn.SetActive(false);
            }
            vibration = !vibration;
        }

        public void MusicChange()
        {
            if (!music)
            {
                musicOn.SetActive(true);
                musicOff.SetActive(false);
            }
            else
            {
                musicOff.SetActive(true);
                musicOn.SetActive(false);
            }
            music = !music;
        }

        public void SoundChange()
        {
            if (!sound)
            {
                soundOn.SetActive(true);
                soundOff.SetActive(false);
            }
            else
            {
                soundOff.SetActive(true);
                soundOn.SetActive(false);
            }
            sound = !sound;
        }

        public void Telegram()
        {
            Application.OpenURL("http://unity3d.com/");
        }


        public void VK()
        {
            Application.OpenURL("http://unity3d.com/");
        }

        public void Discord()
        {
            Application.OpenURL("http://unity3d.com/");
        }

        public void Tiktok()
        {
            Application.OpenURL("http://unity3d.com/");
        }

        public void GooglePlay()
        {
            Application.OpenURL("http://unity3d.com/");
        }

        public void Authors()
        {

        }

        public void Reset()
        {

        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
