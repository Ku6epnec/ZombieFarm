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
        [SerializeField] private GameObject musicObject;
        [SerializeField] private AudioSource[] musicSource;
        [SerializeField] private Sprite mutedMusicImage;
        [SerializeField] private Sprite unmutedMusicImage;

        [Header("Sound Settings elements")]
        [SerializeField] private bool sound;
        [SerializeField] private GameObject soundObject;
        [SerializeField] private AudioSource[] soundSource;
        [SerializeField] private Sprite mutedSoundImage;
        [SerializeField] private Sprite unmutedSoundImage;

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
            if (music)
            {
                musicObject.GetComponent<Image>().sprite = mutedMusicImage;
                foreach (var source in musicSource)
                {
                    source.mute = true;
                }
            }
            else
            {
                musicObject.GetComponent<Image>().sprite = unmutedMusicImage;
                foreach (var source in musicSource)
                {
                    source.mute = false;
                }
            }

            music = !music;
        }

            public void SoundChange()
        {
            if (sound)
            {
                soundObject.GetComponent<Image>().sprite = mutedSoundImage;
                foreach (var source in soundSource)
                {
                    source.mute = true;
                }
            }
            else
            {
                soundObject.GetComponent<Image>().sprite = unmutedSoundImage;
                foreach (var source in soundSource)
                {
                    source.mute = false;
                }
            }

            sound = !sound;
        }

        public void Telegram()
        {
            Application.OpenURL("https://t.me/farm_zombie");
        }


        public void VK()
        {
            Application.OpenURL("https://vk.com/burning_hearts_games");
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
