using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace AR
{
    public class blockMove : MonoBehaviour
    {
        private float timeMoveDownAutomatic;
        private float actualTimeDownAutomatic;

        private float timeMoveSide;
        private float actualTimeSide;
        private float timeMoveDown;
        private float actualTimeDown;

        private Vector2 gridSize;
        private board boardScript;

        private CONTROL_TYPE controlType;

        private delegate void controlMoveSide(ref int h);
        private delegate void controlMoveDown(ref int v);

        controlMoveSide side;
        controlMoveDown down;

        // Use this for initialization
        void Start()
        {
            actualTimeDownAutomatic = 0;
            actualTimeDown = 0;
            actualTimeSide = 0;
            boardScript = transform.parent.GetComponent<board>();
            gridSize = boardScript.getSizeBoard();

            controlType = boardScript.getControlType();

            switch (controlType)
            {
                case CONTROL_TYPE.buttons:
                    side = moveButtonsSide;
                    down = moveButtonsDown;
                    break;

                case CONTROL_TYPE.joystick:
                    side = moveJoysticSide;
                    down = moveJoysticDown;
                    break;

                case CONTROL_TYPE.JandB:
                    side = moveJandBSide; ;
                    down = moveButtonsDown;
                    break;

                default:
                    side = moveButtonsSide;
                    down = moveButtonsDown;
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            int v = 0;
            int h = 0;

            actualTimeDownAutomatic += Time.deltaTime;
            actualTimeSide += Time.deltaTime;
            actualTimeDown += Time.deltaTime;

            if (actualTimeDownAutomatic > timeMoveDownAutomatic)
            {
                if (checkNextPlace(1))
                {
                    actualTimeDownAutomatic = 0;
                    moveDown();
                }
                else
                {
                    endMove();
                    return;
                }
            }

            if (actualTimeSide > timeMoveSide)
            {
                side(ref h);
            }

            if (actualTimeDown > timeMoveDown)
            {
                down(ref v);
            }

            if (checkNextPlace(-v, h))
            {
                if (v != 0)
                {
                    actualTimeDown = 0;
                }

                if (h != 0)
                {
                    actualTimeSide = 0;
                }

                if (h != 0 || v != 0)
                {
                    moveBlock(h, v);
                }
            }
        }

        public void moveSpeed(speedBlocks speed)
        {
            timeMoveDownAutomatic = speed.moveDownAutomatic;
            timeMoveDown = speed.moveDown;
            timeMoveSide = speed.moveSide;
        }

        public void setComponent(bool status)
        {
            gameObject.GetComponent<blockMove>().enabled = status;
            gameObject.GetComponent<rotateBlock>().enabled = status;
        }

        private bool checkNextPlace(int yy , int xx = 0)
        {
            foreach (Transform child in transform)
            {
                int x = Mathf.Abs((int)(child.localPosition.x + transform.localPosition.x)) + xx;
                int y = Mathf.Abs((int)(child.localPosition.y + transform.localPosition.y)) + yy;

                if (x >= gridSize.x || y >= gridSize.y || x < 0)
                {
                    return false;
                }

                if (!boardScript.getStateOfField(y, x))
                {
                    return false;
                }
            }
            return true;
        }

        private void setChildToBoard()
        {
            foreach (Transform child in transform)
            {
                int x = Mathf.Abs((int)(child.localPosition.x + transform.localPosition.x));
                int y = Mathf.Abs((int)(child.localPosition.y + transform.localPosition.y));
                boardScript.setBlockToBoard(y, x, child.gameObject);
            }
        }

        private void moveBlock(int Horizontal , int Vertical = 0)
        {
            Vector3 oldPosition = transform.localPosition;
            if (Vertical < 0)
                oldPosition.y += Vertical;
            oldPosition.x += Horizontal;
            transform.localPosition = oldPosition;
        }

        private void moveDown()
        {
            Vector3 oldPosition = transform.localPosition;
            oldPosition.y -= 1;
            transform.localPosition = oldPosition;
        }

        private void endMove()
        {
            setComponent(false);
            setChildToBoard();
            if(!boardScript.isGameOver())
                boardScript.spawn();
        }

        private void moveButtonsSide(ref int h)
        {
            //sterowanie klockiem
            if (CrossPlatformInputManager.GetButton("Left")) { h = -1; }
            else if (CrossPlatformInputManager.GetButton("Right")) { h = 1; }
        }

        private void moveJoysticSide(ref int h)
        {
            //sterowanie klockiem
            h = Mathf.RoundToInt(CrossPlatformInputManager.GetAxisRaw("Horizontal"));
        }

        private void moveJandBSide(ref int h)
        {
            //sterowanie klockiem
            h = Mathf.RoundToInt(CrossPlatformInputManager.GetAxisRaw("Horizontal"));
        }

        private void moveButtonsDown(ref int v)
        {
            //sterowanie klockiem
            if (CrossPlatformInputManager.GetButton("Down")) { v = -1; }
        }

        private void moveJoysticDown(ref int v)
        {
            //sterowanie klockiem
            v = Mathf.RoundToInt(CrossPlatformInputManager.GetAxisRaw("Vertical"));
        }
    }
}
