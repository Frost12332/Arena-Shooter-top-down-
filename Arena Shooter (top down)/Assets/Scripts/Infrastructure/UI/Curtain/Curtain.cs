using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Curtain
{
    public class Curtain : MonoBehaviour, ICurtain
    {
        [SerializeField] private CanvasGroup _curtainUI;

        [SerializeField] private const float _timeForStep = 0.01f;
        [SerializeField] private const float _alphaStep = 0.05f;

        public void Show()
        {
            gameObject.SetActive(true);
            _curtainUI.alpha = 1;
        }

        public void Hide()
        {
            StartCoroutine(DelayHide());
        }

        private IEnumerator DelayHide()
        {
            while (_curtainUI.alpha > 0)
            {
                _curtainUI.alpha -= _alphaStep;
                yield return new WaitForSeconds(_timeForStep);
            }

            gameObject.SetActive(false);
        }
    }
}