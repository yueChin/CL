using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapEffectUI : MonoBehaviour, IPointerDownHandler
{
    AudioSource mAudioSource;
    // Use this for initialization
    void Start () {
        mAudioSource = this.GetComponent<AudioSource>();
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        mAudioSource.Play();
    }
}
