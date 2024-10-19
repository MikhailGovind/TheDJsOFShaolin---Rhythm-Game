using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.ComponentModel;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private void Awake() => instance = this;

    private void onShake(float duration, float strenght)
    {
        transform.DOShakePosition(duration, strenght);
        transform.DOShakeRotation(duration, strenght);
    }

    public static void Shake(float duration, float strength) => instance.onShake(duration, strength);
}
