using System;
using UnityEngine;
using static CodeBase.Data.GameConstants;

namespace CodeBase.FinishPoint
{
    public class FinishFindCar : MonoBehaviour
    {
        public Action OnCarEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(CarTag))
                OnCarEnter?.Invoke();
        }
    }
}