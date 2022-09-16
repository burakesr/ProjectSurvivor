using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[DisallowMultipleComponent]
public class SoundEffectManager : SingletonMonoBehaviour<SoundEffectManager>
{
    [Header("SOUND")]
    public AudioMixerGroup soundMasterMixerGroup;
    public int soundsVolume = 8;

    private void Start()
    {
        SetSoundsVolume(soundsVolume);
    }

    public void PlaySoundEffect(SoundEffectSO soundEffect, Vector3 position, Quaternion rotation,  bool playOneShot)
    {
        SoundEffect sound = PoolManager.Instance.SpawnFromPool(soundEffect.soundPrefab, position, rotation).GetComponent<SoundEffect>();

        sound.SetSound(soundEffect);
        if (playOneShot)
        {
            sound.PlayOneShot(soundEffect.soundEffectClip);
        }
        else
        {
            sound.PlaySound();
        }
        StartCoroutine(DisableSound(sound, soundEffect.soundEffectClip.length));
    }

    private IEnumerator DisableSound(SoundEffect sound, float length)
    {
        yield return new WaitForSeconds(length);
        sound.gameObject.SetActive(false);
    }

    private void SetSoundsVolume(int soundsVolume)
    {
        float muteDecibels = -80f;

        if (soundsVolume == 0)
        {
            soundMasterMixerGroup.audioMixer.SetFloat("soundsVolume", muteDecibels);
        }
        else
        {
            soundMasterMixerGroup.audioMixer.SetFloat("soundsVolume", HelperUtilities.LinearToDecibels(soundsVolume));
        }
    }
}
