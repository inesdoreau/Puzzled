using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class PuzzlePieceAnimation : MonoBehaviour
{
    [SerializeField] private Amplitude amplitudePosition;
    [SerializeField] private float positionCycleDuration = 2;

    [SerializeField] private Amplitude amplitudeRotation;
    [SerializeField] private float rotationCycleDuration = 2;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMove(new Vector3(amplitudePosition.X, amplitudePosition.Y ,amplitudePosition.Z), positionCycleDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        transform.DOLocalRotate(new Vector3(amplitudeRotation.X, amplitudeRotation.Y, amplitudeRotation.Z), rotationCycleDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //    positionX = Mathf.PingPong(Time.time * AmplitudePosition.X, PositionSpeed);
    //    postionY = Mathf.PingPong(Time.time * AmplitudePosition.Y, PositionSpeed);
    //    positionZ = Mathf.PingPong(Time.time * AmplitudePosition.Z, PositionSpeed);

    //    transform.Rotate(AmplitudeRotation.X * Time.deltaTime, AmplitudeRotation.Y * Time.deltaTime, AmplitudeRotation.Z * Time.deltaTime); 
    //    transform.localPosition = new Vector3(positionX, postionY, positionZ);
    //}
}

[Serializable]
public class Move
{
    public bool X, Y, Z;
}


[Serializable]
public class Amplitude
{
    public float X, Y, Z;
}
