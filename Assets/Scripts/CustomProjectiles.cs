using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomProjectiles : MonoBehaviour
{
    public bool activated;

    [Header("Please attatch RigidBody")]
    public Rigidbody rb;
    

    [Header("Basic ball stats")]
    public float bounciness;
    public bool useGravity;


    [Header("Lifetime:")]
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;



    //Vanish
    [Header("Vanishing Settings")]
    [Space(7)]
    public float timeBeforeVanish;
    public float timeBetweenVanishAndAppear;
    public bool dontAppearAgain;
    

    
    private int collisions;

    private PhysicMaterial physic_mat;
    public bool alreadyExploded;

    
    void Start()
    {
        Setup();


        //Set timers
        if (timeBeforeVanish > 0)
            currentTimeBeforeVanish = timeBeforeVanish;
    }

    
    void Update()
    {
        if (!activated) return;

        maxLifetime -= Time.deltaTime;
        if (timeBeforeVanish != 0) Vanish();

        
    }

    private void Setup()
    {
        //Setup physics material
        physic_mat = new PhysicMaterial();
        physic_mat.bounciness = bounciness;
        physic_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physic_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Apply the physics material to the collider
        GetComponent<SphereCollider>().material = physic_mat;

        rb.useGravity = useGravity;
    }

    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!activated) return;
        if (isVanished) return;
        collisions++;
    }

    #region Attribute functions

    
    

    bool isVanished;
    float currentTimeBeforeVanish;
    private void Vanish()
    {
        //Count down timer and start vanishing when timer at 0
        
        if (!isVanished)
            currentTimeBeforeVanish -= Time.deltaTime;

        if (currentTimeBeforeVanish <= 0)
        {
            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<TrailRenderer>());
        }

    }
    #endregion

    
    #region Setters

    public void SetBounciness(float v)
    {
        bounciness = v;
    }
    public void SetGravity(float v)
    {
        if (v == 1) useGravity = true;
        else useGravity = false;
    }
    public void SetMaxCollisions(float v)
    {
        int _v = Mathf.RoundToInt(v);
        maxCollisions = _v;
    }
    public void SetMaxLifetime(float v)
    {
        int _v = Mathf.RoundToInt(v);
        maxLifetime = _v;
    }
    #endregion
}







