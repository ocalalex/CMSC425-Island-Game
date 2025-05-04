using UnityEngine;
using UnityEngine.Audio;

public class MasterVolumeControl : MonoBehaviour
{
    public AudioMixer masterMixer;
    void Start()
    {
        if(masterMixer == null)
        {
            Debug.LogError("Master Mixer is not assigned in the inspector.");
            return;
        }
    }

    public void updateMasterVolume(float vol){
        float volumeDb = (vol > 0.001f) ? Mathf.Log10(vol) * 20f : -80f;

        // Set the exposed parameter on the mixer
        masterMixer.SetFloat("MasterVolume", volumeDb);
    }
}
