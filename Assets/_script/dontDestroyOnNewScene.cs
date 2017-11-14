using UnityEngine;

namespace AR
{
    public class dontDestroyOnNewScene : MonoBehaviour
    {
        public static dontDestroyOnNewScene Instance;

        void Awake()
        {
            //jesli juz istnieje instancja tego obiektu to przy powrocie do sceny (w ktorej obiekt jest dodany)
            //nowy obiekt jest odrazu kasowany a "stary" ktory powrucil z innej sceny dalej zyje
            if(Instance)
                  DestroyImmediate(gameObject);
            else
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
        }
    }
}
