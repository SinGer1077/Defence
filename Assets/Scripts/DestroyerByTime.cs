using UnityEngine;

public class DestroyerByTime : MonoBehaviour
{
    [SerializeField]
    private float _timeToDestroy;

    private float _timer;

    private void Start()
    {
        _timer = _timeToDestroy;
    }

    private void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;
        if (_timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
