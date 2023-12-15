using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic
{
    public class LoadCurtain
    {
        private const string CurtainPath = "LoadinCurtain";

        private CanvasGroup _loadCurtain;

        private readonly CanvasGroup _curtainPrefab;
        private readonly ICoroutineRunner _coroutine;
        private bool _curtainShow;

        public LoadCurtain(ICoroutineRunner coroutine)
        {
            _coroutine = coroutine;
            _curtainPrefab = Resources.Load<GameObject>(CurtainPath).GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            if (_curtainShow == false)
            {
                _loadCurtain = Object.Instantiate(_curtainPrefab);
                _curtainShow = true;
            }
        }

        public void Hide()
        {
            _coroutine.StartCoroutine(HideCurtain());
        }

        private IEnumerator HideCurtain()
        {
            do
            {
                _loadCurtain.alpha -= Time.deltaTime * 2f;
                yield return null;
            } while (_loadCurtain.alpha > 0.0f);

            Object.Destroy(_loadCurtain.gameObject);
            _curtainShow = false;
        }
    }
}