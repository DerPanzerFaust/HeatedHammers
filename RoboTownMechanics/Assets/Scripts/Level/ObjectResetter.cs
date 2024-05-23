using System.Collections;
using UnityEngine;

namespace Objects.resetter
{
    public class ObjectResetter : MonoBehaviour
    {
        //--------------------Private--------------------//
        private Vector3 _originalPosition;
        private Vector3 _originalScale;

        private Quaternion _originalRotation;

        private Rigidbody _rigidBody;

        //--------------------Functions--------------------//
        private void Awake()
        {
            _originalPosition = transform.position;
            _originalRotation = transform.rotation;
            _originalScale = transform.localScale;
        }

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Resets the object back to their original postion saved on awake
        /// </summary>
        public IEnumerator ResetObject()
        {
            _rigidBody.isKinematic = true;

            yield return null;

            gameObject.transform.position = _originalPosition;
            gameObject.transform.rotation = _originalRotation;
            gameObject.transform.localScale = _originalScale;
            _rigidBody.isKinematic = false;
        }
    }
}