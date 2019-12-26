using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    #region Variables

        // Editor variables

        [SerializeField, Header("tree attributes"), Tooltip("Starting age for this particular tree")]
        private float startingAge;
        [SerializeField, Tooltip("Maximum age for this particular tree")]
        private float maxAge;
        [SerializeField, Tooltip("Height gained per age unit")]
        private float growthFactor;

        // Private variables

        #endregion

    #region Properties

        public float Age { get; set; }

        #endregion

    #region Methods

        void SpawnSeed()
            {

            }

        #endregion

        void Grow(float deltaTime)
        {
            Age += Time.deltaTime;
            this.transform.localScale += new Vector3(0.0f, growthFactor *Time.deltaTime, 0.0f);
        }

    #region Functions

        // Start is called before the first frame update
        void Start()
        {
            Age = startingAge;

        }

        // Update is called once per frame
        void Update()
        {
            if (Age < maxAge)
            {
                Grow(Time.deltaTime);
            }
        }

    #endregion
}
