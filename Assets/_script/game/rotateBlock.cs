using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace AR
{
    [System.Serializable]
    public struct dataRotate
    {
        public GameObject[] block;
        public int[] x;
        public int[] y;
    }

    public class rotateBlock : MonoBehaviour
    {
        public dataRotate[] rotateposition;

        private int actualState;

        public float speedRotation;
        private float actualTimeRotation;

        private board boardScript;

        // Use this for initialization
        void Start()
        {
            actualTimeRotation = 0;
            boardScript = transform.parent.GetComponent<board>();
        }

        // Update is called once per frame
        void Update()
        {
            actualTimeRotation += Time.deltaTime;
            if (actualTimeRotation > speedRotation && CrossPlatformInputManager.GetButton("Rotate"))
            {
                if (!checkFieldRotation())
                    return;

                actualTimeRotation = 0;

                actualState += 1;
                if (actualState >= rotateposition.Length)
                {
                    actualState = 0;
                }

                rotation();
            }
        }

        public void rotationSpeed(float rotation)
        {
            speedRotation = rotation;
        }

        public void startRotation()
        {
            actualState = Random.Range(0, rotateposition.Length);
            actualTimeRotation = 0;
            rotation();
        }

        private void rotation()
        {
            for(int i = 0; i < rotateposition[actualState].block.Length; i++)
            {
                Vector3 oldPosition = rotateposition[actualState].block[i].transform.localPosition;
                oldPosition.x = rotateposition[actualState].x[i];
                oldPosition.y = rotateposition[actualState].y[i];

                rotateposition[actualState].block[i].transform.localPosition = oldPosition;
            }
        }

        private bool checkFieldRotation()
        {
            int newPosition = actualState + 1;
            if (newPosition >= rotateposition.Length)
            {
                newPosition = 0;
            }

            for (int i = 0; i < rotateposition[newPosition].block.Length; i++)
            {
                int yy = 0;
                if (rotateposition[newPosition].y[i] > 0)
                {
                    yy = -rotateposition[newPosition].y[i];
                }
                else if (rotateposition[newPosition].y[i] < 0)
                {
                    yy = Mathf.Abs(rotateposition[newPosition].y[i]);
                }

                int x = (int)(Mathf.Abs(transform.localPosition.x) + rotateposition[newPosition].x[i]);
                int y = (int)(Mathf.Abs(transform.localPosition.y) + yy);

                if (!boardScript.getStateOfField(y, x))
                {
                    return false;
                }
            }

            return true;
        }
    }
}