using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBehaviour : MonoBehaviour
{
    private Vector3 _destination;

    private Vector3 _startPos;

    private Vector3 _firstBezierPoint;

    private Vector3 _secondBezierPoint;

    private float _timeToSwim = 20f;

    private float _timer;

    private GameController _game;

    private void Start()
    {
        _startPos = transform.position;
        Transform port = GameObject.Find("Port").transform;
        int randomPort = Random.Range(0, 3);
        Transform targetPort = port.GetChild(0).GetChild(randomPort);
        _destination = targetPort.position;

        float randomCoef = Random.Range(0.6f, 0.8f);
        _firstBezierPoint = (_destination + transform.position) * randomCoef;
        int znak = (Random.Range(0, 2) * 2 - 1);
        _firstBezierPoint.x += znak * Random.Range(30f, 50f);
        _firstBezierPoint.y = _destination.y;

        randomCoef = Random.Range(0.2f, 0.4f);
        _secondBezierPoint = (_destination + transform.position) * randomCoef;
        _secondBezierPoint.x += znak * -1 * Random.Range(30f, 50f);
        _secondBezierPoint.y = _destination.y;

        _game = FindObjectOfType<GameController>();
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        transform.position = GetBezierPoint(_timer / _timeToSwim);
        transform.rotation = Quaternion.LookRotation(GetRotation(_timer / _timeToSwim));

        if (_timer >= _timeToSwim)
        {
            _game.EndGame(false);
        }
    }

    private Vector3 GetBezierPoint(float time)
    {
        Vector3 q12 = Vector3.Lerp(_startPos, _firstBezierPoint, time);
        Vector3 q23 = Vector3.Lerp(_firstBezierPoint, _secondBezierPoint, time);
        Vector3 q34 = Vector3.Lerp(_secondBezierPoint, _destination, time);

        Vector3 q123 = Vector3.Lerp(q12, q23, time);
        Vector3 q234 = Vector3.Lerp(q23, q34, time);

        Vector3 q1234 = Vector3.Lerp(q123, q234, time);
        return q1234;
    }

    private Vector3 GetRotation(float time)
    {
        float t = Mathf.Clamp01(time);
        float oneMinus = 1f - time;
        return
            3f * oneMinus * oneMinus * (_firstBezierPoint - transform.position) +
            6f * oneMinus * t * (_secondBezierPoint - _firstBezierPoint) +
            3f * t * t * (_destination - _secondBezierPoint);
    }

}
