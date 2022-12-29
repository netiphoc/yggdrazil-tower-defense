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

        private void Awake()
        {
            tower.DebugAssert();
            laserTransform.DebugAssert();
            laserTransform.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            tower.onAttackMonster.AddListener(OnAttackMonster);
        }

        private void OnDisable()
        {
            tower.onAttackMonster.RemoveListener(OnAttackMonster);
        }

        private void OnAttackMonster(Monster monster)
        {
            var lineLenght = Vector3.Distance(monster.transform.position, transform.position);
            DoLaser(lineLenght);
            this.Log($"Attacking: {monster.gameObject.name}");
        }

        private void DoLaser(float distance)
        {
            laserTransform.gameObject.SetActive(true);
            var oldScale = laserTransform.localScale;
            oldScale.z = distance;
            laserTransform.localScale = oldScale;
            CancelInvoke(nameof(HideLaser));
            Invoke(nameof(HideLaser), laserDuration);
        }

        private void HideLaser()
        {
            laserTransform.gameObject.SetActive(false);
        }
    }
}