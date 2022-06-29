using UnityEngine;
using TMPro;

namespace Goo.Visuals.FloatingTexts
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [Header("Config")]
        [SerializeField] private float _lifeTime = 1.5f;

        private float timer;

        public void SetText(string message, Vector3 position, Quaternion rotation)
        {
            _text.text = message;
            SetText(position, rotation);
        }

        private void SetText(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            gameObject.SetActive(true);
        }

        protected virtual void OnEnable()
        {
            timer = 0;
        }

        protected virtual void Update()
        {
            if (timer < _lifeTime)
            {
                timer += Time.deltaTime;
                transform.position += Vector3.up * Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}