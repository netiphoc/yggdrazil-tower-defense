using System.Collections;
using TowerDefenseGame.GameEntity;
using TowerDefenseGame.Map;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.Visual
{
    public class VisualAreaExplode : MonoBehaviour
    {
        [SerializeField] private TowerB towerB;
        [SerializeField] private Transform[] explodeFx;
        [SerializeField] private float explodeDuration = 1f;
        private WaitForSeconds _waitForSeconds;

        private void Awake()
        {
            towerB.DebugAssert();
            HideExplode();
            UnParentExplode();
            _waitForSeconds = new WaitForSeconds(explodeDuration);
        }

        private void OnEnable()
        {
            towerB.onExplodeArea.AddListener(OnExplodeArea);
        }

        private void OnDisable()
        {
            towerB.onExplodeArea.RemoveListener(OnExplodeArea);
        }

        private void OnExplodeArea(Block[] blocks)
        {
            for (var i = 0; i < blocks.Length; i++)
            {
                var fx = explodeFx[i];
                var block = blocks[i];
                if (!block) continue;
                var explodePos = new Vector3(block.X, 1f, block.Y);
                fx.position = explodePos;
                fx.gameObject.SetActive(true);
                StartCoroutine(HideFx(fx));
            }
        }

        private IEnumerator HideFx(Transform fx)
        {
            yield return _waitForSeconds;
            fx.gameObject.SetActive(false);
        }

        private void HideExplode()
        {
            foreach (var fx in explodeFx)
                fx.gameObject.SetActive(false);
        }

        private void UnParentExplode()
        {
            foreach (var fx in explodeFx)
                fx.parent = null;
        }
    }
}