using CryoDI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DifficultySelect : CryoBehaviour
    {
        [Dependency]
        private EventEmitter Events { get; set; }

        public void OnChange() {
            Dropdown dd = GetComponent<Dropdown>();
            Events.OnDifficultyChange_Event(dd.value);
        } 
        
    }
}
