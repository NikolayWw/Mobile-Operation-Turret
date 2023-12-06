using CodeBase.Logic;
using CodeBase.Services.GameObserver;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using DG.Tweening;
using System;
using UnityEngine;

namespace CodeBase.Player.Car
{
    public class CarHealth : MonoBehaviour, IApplyDamage
    {
        [SerializeField] private Transform _bodyTransform;

        public Action OnValueChange;
        public float CurrentHealth { get; private set; }

        private IUIFactory _uiFactory;
        private IGameObserverService _observerService;
        private bool _isLose;
        private Vector3 _startScale;
        private Tweener _tweener;

        public void Construct(IStaticDataService dataService, IUIFactory uiFactory, IGameObserverService observerService)
        {
            CurrentHealth = dataService.CarStaticData.CarConfig.StartHealth;
            _uiFactory = uiFactory;
            _observerService = observerService;
            _startScale = _bodyTransform.localScale;
        }

        public void ApplyDamage(float value)
        {
            if (_isLose)
                return;

            CurrentHealth -= value;
            OnValueChange?.Invoke();
            _tweener.Kill();
            _tweener = _bodyTransform.DOShakeScale(0.2f, 0.03f, 6).SetEase(Ease.Linear)
                .OnComplete(() => _bodyTransform.DOScale(_startScale, 0.3f));

            if (CurrentHealth <= 0)
            {
                _isLose = true;
                _observerService.SendPlayerLose();
                _uiFactory.CreateLoseWindow();
            }
        }
    }
}