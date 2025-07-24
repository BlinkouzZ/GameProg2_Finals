using UnityEngine;
using System.Collections;
namespace Utility {
    public class Timer : MonoBehaviour
    {
        float delay;
        System.Action action;

        // Will never call this frame, always the next frame at the earliest
        public static Timer Create(float delay, System.Action action)
        {
            Timer cad = new GameObject("Timer").AddComponent<Timer>();
            cad.delay = delay;
            cad.action = action;
            return cad;
        }

        float age;

        void Update()
        {
            if (age > delay)
            {
                action();
                Destroy(gameObject);
            }
        }
        void LateUpdate()
        {
            age += Time.deltaTime;
        }
    }
}