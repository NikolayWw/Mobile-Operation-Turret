using CodeBase.Services.GameObserver;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Camera
{
    public class CameraAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _animatePoint;

        private IGameObserverService _observerService;
        private Transform _cameraTransform;

        public void Construct(UnityEngine.Camera mainCamera, IGameObserverService observerService)
        {
            _cameraTransform = mainCamera.transform;
            _observerService = observerService;

            _observerService.OnGameStart += PlayStartAnimation;
        }

        private void Start()
        {
            InitCameraAndCameraPoint();
        }

        private void OnDestroy()
        {
            _observerService.OnGameStart -= PlayStartAnimation;
        }

        private void PlayStartAnimation()
        {
            _animatePoint.DORotate(new Vector3(40, 0, 0), 0.6f).SetEase(Ease.Linear);
            _animatePoint.DOLocalMove(Vector3.zero, 0.6f).SetEase(Ease.Linear);
        }

        private void InitCameraAndCameraPoint()
        {
            _animatePoint.SetPositionAndRotation(_cameraTransform.position, _cameraTransform.rotation);

            _cameraTransform.SetParent(_animatePoint);
            _cameraTransform.localRotation = Quaternion.identity;
            _cameraTransform.localPosition = Vector3.zero;
        }
    }
}