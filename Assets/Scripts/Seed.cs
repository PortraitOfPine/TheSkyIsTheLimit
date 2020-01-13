using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Seed : Plant
{
    #region Variables

    // Editor variables

    [SerializeField, Header("seed attributes"), Tooltip("Age at which the seed will become a tree")]
    private float ageTransformTree;
    [SerializeField, Tooltip("GameObject that represents the tree that the seed will become")]
    private GameObject treeObject;

    #endregion

    #region Methods

    protected override void Grow(float deltaTime)
    {
        base.Grow(deltaTime);


        // Check if object should die
        if (Age > ageTransformTree)
        {
            // Instantiate a new tree object at the current position of the seed
           Instantiate(treeObject, transform.position, transform.rotation);

            // Destroy the seed
            Destroy(this.gameObject);
        }

    }

    #endregion

    #region Functions

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Grow(Time.deltaTime);
    }

    #endregion
}
