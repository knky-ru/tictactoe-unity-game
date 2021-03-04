using CryoDI;
using UnityEngine;

namespace UI
{
    public class ButtonStart : CryoBehaviour
    {
        [Dependency]
        private EventEmitter Events { get; set; }

        public void OnClick()
        {
            Events.OnGameStart_Event();
        }
    
    }
}
