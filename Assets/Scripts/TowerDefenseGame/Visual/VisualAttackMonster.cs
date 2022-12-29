using TowerDefenseGame.GameEntity;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.Visual
{
    public class VisualAttackMonster : MonoBehaviour
    {
        [SerializeField] private AbstractTower tower;
        [SerializeField] private Transform laserTransform;
        [SerializeField] private float laserDuration;

        private float _targetLaserLength;
        private Vector3 _startLaserLength;

        private void Awake()
        {
            tower.DebugAssert();
            laserTransform.DebugAssert();
            laserTransform.gameObject.SetActive(false);

            var scale = laserTransform.localScale;
            scale.z = 0.01f;
            _startLaserLength = scale;
        }

        private void OnEnable()
        {
            tower.onAttackEnemy.AddListener(OnAttackMonster);
        }

        private void OnDisable()
        {
            tower.onAttackEnemy.RemoveListener(OnAttackMonster);
        }

        private void OnAttackMonster(Monster monster)
        {
            var lineLenght = Vector3.Distance(monster.transform.position, transform.position);
            DoLaser(lineLenght);
            this.Log($"Attacking: {monster.gameObject.name}");
        }

        private void Update()
        {
            var localScale = laserTransform.localScale;
            var targetScale = localScale;
            targetScale.z = _targetLaserLength;

            // Distance = Time x speed
            // Speed = Distance / Time 
            var speed = _targetLaserLength / laserDuration;
            speed *= 1.2f;

            targetScale = Vector3.Slerp(localScale, targetScale, Time.deltaTime * speed);
            localScale = targetScale;
            laserTransform.localScale = localScale;
        }

        private void DoLaser(float distance)
        {
            _targetLaserLength = distance;
            laserTransform.gameObject.SetActive(true);
            laserTransform.localScale = _startLaserLength;
            CancelInvoke(nameof(HideLaser));
            Invoke(nameof(HideLaser), laserDuration);
        }

        private void HideLaser()
        {
            laserTransform.gameObject.SetActive(false);
        }
    }
}