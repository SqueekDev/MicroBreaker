using System.Collections;
using System.Collections.Generic;
using Base;
using Boosters;
using Data;
using UnityEngine;

namespace Platform
{
    public class LaserShooter : MonoBehaviour
    {
        private const float DelayTime = 0.5f;

        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private ObjectPooler _pooler;
        [SerializeField] private List<ShootPoint> _shootPoints;

        private float _timer;
        private Coroutine _shootingCoroutine;
        private WaitForSeconds _delay = new WaitForSeconds(DelayTime);

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private LaserProjectile TryGetProjectile()
        {
            if (_pooler.TryGetObject(out LaserProjectile projectile))
            {
                return projectile;
            }
            else
            {
                return null;
            }
        }

        private void EnableLaser()
        {
            PlayerUtilities.CheckCoroutine(_shootingCoroutine, this);
            _shootingCoroutine = StartCoroutine(EnablingProjectiles());
        }

        private void ResetBoosters()
        {
            PlayerUtilities.CheckCoroutine(_shootingCoroutine, this);
            _timer = 0;
        }

        private IEnumerator EnablingProjectiles()
        {
            _timer = PlayerUtilities.BaseBoosterDurationTime;

            while (_timer > 0)
            {
                foreach (var shootPoint in _shootPoints)
                {
                    LaserProjectile projectile = TryGetProjectile();

                    if (projectile != null)
                    {
                        projectile.transform.position = shootPoint.transform.position;
                        projectile.transform.rotation = Quaternion.identity;
                        projectile.gameObject.SetActive(true);
                    }

                    _timer -= DelayTime;
                    yield return _delay;
                }
            }
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            switch (type)
            {
                case BoostersEnum.LaserEnabled:
                    EnableLaser();
                    break;
                case BoostersEnum.Reseted:
                    ResetBoosters();
                    break;
            }
        }
    }
}