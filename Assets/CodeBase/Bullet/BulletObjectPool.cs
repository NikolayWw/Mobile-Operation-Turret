using CodeBase.Logic.Pool;
using CodeBase.Services.StaticData;
using System.Collections;
using UnityEngine;

namespace CodeBase.Bullet
{
    public class BulletObjectPool : MonoBehaviour, IObjectPool
    {
        private WaitForSeconds _wait;

        public void Construct(IStaticDataService dataService)
        {
            _wait = new WaitForSeconds(dataService.BulletStaticData.LifeTime);
        }

        public bool IsReady() =>
            gameObject.activeInHierarchy == false;

        public void Enable()
        {
            gameObject.SetActive(true);
            StartCoroutine(DisableTimer());
        }

        public void Disable()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }

        private IEnumerator DisableTimer()
        {
            yield return _wait;
            Disable();
        }
    }
}