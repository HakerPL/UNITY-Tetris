using UnityEngine;
using UnityEngine.SceneManagement;

namespace AR
{
    public enum CONTROL_TYPE { joystick = 1 , buttons , JandB };

    public class mainMenu : MonoBehaviour
    {
        public GameObject menuCanvas;
        public GameObject optionsCanvas;
        public GameObject creditCanvas;

        private activeButtons active;
        private optionsGame options;

        CONTROL_TYPE controlChoose;
        SOUND_TYPE musicChoose;
        SOUND_TYPE effectsChoose;

        void Awake()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        // Use this for initialization
        void Start()
        {
            active = GetComponent<activeButtons>();
            options = GameObject.FindGameObjectWithTag("Options").GetComponent<optionsGame>();

            SetControl((int)options.getControl());
            SetMusic((int)options.getMusicState());
            SetEffects((int)options.getEffectsState());
        }

        public void SetControl(int newControl)
        {
            controlChoose = (CONTROL_TYPE)newControl;
            active.setCheckButton(controlChoose);
        }

        public void SetMusic(int newMusic)
        {
            if (newMusic != 0)
            {
                musicChoose = (SOUND_TYPE)newMusic;
            }
            else
            {
                musicChoose = (musicChoose == SOUND_TYPE.ON) ? SOUND_TYPE.OFF : SOUND_TYPE.ON;
            }
            active.setMusicButton(musicChoose);
        }

        public void SetEffects(int newEffects)
        {
            if (newEffects != 0)
            { 
                effectsChoose = (SOUND_TYPE)newEffects;
            }
            else
            {
                effectsChoose = (effectsChoose == SOUND_TYPE.ON) ? SOUND_TYPE.OFF : SOUND_TYPE.ON;
            }
            active.setEffectsButton(effectsChoose);
        }

        public void SaveOptions(GameObject canvas)
        {
            //zapisujemy dane z opcji
            options.setControl(controlChoose);
            options.setMusicState(musicChoose);
            options.setEffectsState(effectsChoose);

            canvas.SetActive(false);
            menuCanvas.SetActive(true);
        }

        public void StartGame()
        {
            SceneManager.LoadScene("game");
        }

        public void Options()
        {
            //ustawiamy menu tak jak sa wybrane opcje
            SetControl((int)options.getControl());
            SetMusic((int)options.getMusicState());
            SetEffects((int)options.getEffectsState());

            menuCanvas.SetActive(false);
            optionsCanvas.SetActive(true);
        }

        public void Credit()
        {
            menuCanvas.SetActive(false);
            creditCanvas.SetActive(true);
        }

        public void ExitGame()
        {
            #if UNITY_EDITOR
                 UnityEditor.EditorApplication.isPlaying = false;
            #else
                 Application.Quit();
            #endif
        }

        public void ReturnToMain(GameObject canvas)
        {
            canvas.SetActive(false);
            menuCanvas.SetActive(true);
        }
    }
}
