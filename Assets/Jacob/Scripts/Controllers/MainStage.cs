using System;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
    public class MainStage : MonoBehaviour
    {
        public Transform player;
        private Cam _cam;
        
        private void Awake()
        {
            _cam = Camera.main!.GetComponent<Cam>();
        }

        private void OnEnable()
        {
            _cam.followedObject = player;
        }
    }
}
