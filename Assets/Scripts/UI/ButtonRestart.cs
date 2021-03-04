using CryoDI;
using UnityEngine;

namespace UI
{
    public class ButtonRestart : CryoBehaviour
    {
        [Dependency]
        private EventEmitter Events { get; set; }

        public void OnClick()
        {
            Events.OnGameRestart_Event();
        }
    
    }
}
