using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AR
{
    public class board : MonoBehaviour
    {
        public GameObject controlsCanvas;
        public Image gameOverImg;
        public Vector2 sizeBoard;
        public getControler ControlType;

        private GameObject[,] Board;
        private bool[] lineHaveBlock;

        private AudioSource deleteLineSound;

        private bool gameOver;

        private float timeEndGame;
        private float actualTime;
        private bool endGame;

        void Awake()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        // Use this for initialization
        void Start()
        {
            timeEndGame = 3f;
            actualTime = 0;
            endGame = false;

            deleteLineSound = GetComponent<AudioSource>();
            gameOver = false;
            lineHaveBlock = new bool[(int)sizeBoard.y];
            Board = new GameObject[(int)sizeBoard.y, (int)sizeBoard.x];
            for (int y = 0; y < (int)sizeBoard.y; y++)
            {
                lineHaveBlock[y] = false;
                for (int x = 0; x < (int)sizeBoard.x; x++)
                    Board[y, x] = null;
            }
        }

        void Update()
        {
            if(gameOver)
            {
                if (!endGame)
                {
                    ControlType.gameObject.SetActive(false);
                    gameOverImg.color = new Color(255, 255, 255, 255);
                    endGame = true;
                }

                actualTime += Time.deltaTime;

                if (actualTime > timeEndGame)
                {
                    /*
                    wirtualnie udajemy ze przycisk zostal puszczony
                    zepobiega to samodzielnemu poruszaniu sie klocka przy powrocie do gry
                    w przypadku kiedy przed powrotem do menu byl wcisniety przycisk
                    */
                    CrossPlatformInputManager.SetButtonUp("Left");
                    CrossPlatformInputManager.SetButtonUp("Right");
                    CrossPlatformInputManager.SetButtonUp("Down");
                    CrossPlatformInputManager.SetButtonUp("Rotate");

                    SceneManager.LoadScene("menu");
                }
            }
        }

        public bool isGameOver()
        {
            return gameOver;
        }

        public CONTROL_TYPE getControlType()
        {
            return ControlType.getCurrentControl();
        }

        public bool getStateOfField(int y , int x)
        {
            if (y >= sizeBoard.y || x >= sizeBoard.x || x < 0 || y < 0)
            {
                return false;
            }

            return !Board[y, x];
        }

        public void spawn()
        {
            checkBoard();
            gameObject.SendMessage("spawnNextBlock");
        }

        public Vector2 getSizeBoard()
        {
            return sizeBoard;
        }

        public void setBlockToBoard(int y , int x , GameObject block)
        {
            Board[y, x] = block;
            lineHaveBlock[y] = true;
            if(lineHaveBlock[1])
            {
                gameOver = true;
            }
        }

        public void checkBoard()
        {
            int lineDelete = 0;
            for(int y = 0; y < sizeBoard.y; y++)
            {
                int blockRow = 0;
                for(int x = 0; x < sizeBoard.x; x++)
                {
                    if (Board[y,x] == null)
                    {
                        break;
                    }
                    blockRow++;
                }

                if(blockRow == sizeBoard.x)
                {
                    lineHaveBlock[y] = false;
                    deleteLine(y);
                    getDownLine();

                    lineDelete++;
                }
            }

            if (lineDelete > 0)
            {
                cleanObject();
                gameObject.SendMessageUpwards("updateScore", lineDelete);
                deleteLineSound.Play();
            }
        }

        private void getDownLine()
        {
            bool againCheck = false;
            while(true)
            {
                for(int y = 0; y < sizeBoard.y; y++)
                {
                    if(((y + 1) < sizeBoard.y) && lineHaveBlock[y])
                    {
                        if(lineHaveBlock[y] && !lineHaveBlock[y + 1])
                        {
                            againCheck = true;
                        }

                        if(lineHaveBlock[y] != lineHaveBlock[y + 1])
                        {
                            for (int x = 0; x < sizeBoard.x; x++)
                            {
                                if (Board[y, x] == null)
                                    continue;
                                Vector3 oldPosition = Board[y, x].transform.localPosition;
                                oldPosition.y -= 1;
                                Board[y, x].transform.localPosition = oldPosition;
                                Board[y + 1, x] = Board[y, x];
                                Board[y , x] = null;
                            }

                            lineHaveBlock[y] = false;
                            lineHaveBlock[y + 1] = true;
                        }
                    }
                }

                if(!againCheck)
                {
                    break;
                }

                againCheck = false;
            }
        }

        private void deleteLine(int y)
        {
            for(int x = 0; x < sizeBoard.x; x++)
            {
                Destroy(Board[y, x]);
            }
        }

        private void cleanObject()
        {
            foreach (Transform child in transform)
            {
                bool deleteEmptyChild = true;
                if (child.name == "Spawn")
                {
                    continue;
                }

                foreach (Transform childInChild in child.transform)
                {
                    deleteEmptyChild = false;
                }

                if (deleteEmptyChild)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
