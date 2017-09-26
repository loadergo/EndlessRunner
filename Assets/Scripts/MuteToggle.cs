using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour
{
    
    private Toggle _muteToggle;



    void Start ()
    {
        _muteToggle = GetComponent<Toggle>();
		CheckMuteValue();
	}

    public void SetMuteValue()
    {
        if (_muteToggle.isOn)
        {
            SoundManager.Instance.MainAudioMixer.SetFloat("Master", -80f);
        }
        else
        {
            SoundManager.Instance.MainAudioMixer.ClearFloat("Master");
        }
        PPrefs.Mute = _muteToggle.isOn;
    }

    private void CheckMuteValue()
    {
        var value = PPrefs.Mute;
        _muteToggle.isOn = value;
        SetMuteValue();
    }
}
