using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
   #region Variables

        // Private variables

        private int _birthTime;

        #endregion
        
    #region Properties

        public float Age { get; set; }

    #endregion

    #region Methods

        // Grow serves as a generic method to increment the age of the plant
        protected virtual void Grow(float deltaTime)
        {
            Age += deltaTime;
        }
        

    #endregion


    #region Functions

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // Initialize the age of the plant
            Age = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            Grow(Time.deltaTime);
        }

    #endregion
}
