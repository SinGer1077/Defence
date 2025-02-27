using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private GameObject _currentProjectile;
    private GameObject _defaultProjectile;

    [SerializeField]
    private Transform[] _muzzleTransforms;

    [SerializeField]
    private float _delayBetweenShots;

    [SerializeField]
    private Transform _parentProjectile;

    [SerializeField]
    private AudioSource _source;

    [SerializeField]
    private ParticleSystem _shotImpact;

    private int _currentMuzzle = -1;

    private float _timer;

    private bool _readyToShot;

    private List<RuneInfo> _currentRuneList = new List<RuneInfo>();

    private void Start()
    {
        _defaultProjectile = _currentProjectile;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Shot();
        }
    }

    private void FixedUpdate()
    {
        if (_readyToShot == false)
        {
            if (_timer < _delayBetweenShots)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _readyToShot = true;
                _timer = 0;
            }
        }
    }

    private void Shot()
    {
        if (_readyToShot == true)
        {
            _source.Play();
            UpdateMuzzleNumber();
            GameObject projectile = Instantiate(_currentProjectile, _muzzleTransforms[_currentMuzzle].position, transform.rotation, _parentProjectile);
            projectile.GetComponent<BaseProjectile>().SetFireComponent(this);
            Instantiate(_shotImpact, _muzzleTransforms[_currentMuzzle].position, Quaternion.identity);
            var anim = _muzzleTransforms[_currentMuzzle].parent.GetComponent<Animation>();
            if (!anim.isPlaying)
                anim.Play();
            _readyToShot = false;
        }
    }

    private void UpdateMuzzleNumber()
    {
        if (_currentMuzzle + 1 >= _muzzleTransforms.Length)
        {
            _currentMuzzle = -1;
        }
        _currentMuzzle += 1;
    }

    public void ChangeProjectile(GameObject newPrefab, float time)
    {
        //_currentProjectile = newPrefab;
        //StartCoroutine(ToDefaultProjectileCoroutine(time));
    }

    public void SetRune(RuneInfo rune)
    {
        //_currentProjectile = rune.Projectile;
        //if (!_currentRuneList.Contains(rune))
        //{
        //    _currentRuneList.Add(rune) //Õ¿ƒŒ ƒŒ¡¿¬Àﬂ“‹ Õ≈ –”Õ »Õ‘Œ, ŒÕ ∆≈ ƒ≈—“–Œ»“—ﬂ
        //}
        //StartCoroutine(ToDefaultProjectileCoroutine(rune.WorkTime));
    }

    private IEnumerator ToDefaultProjectileCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        _currentProjectile = _defaultProjectile;
    }
}
