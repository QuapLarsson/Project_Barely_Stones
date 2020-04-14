using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Stat Block Start
    public int myExperience = 0;
    public int myLevel = 0;
    public int myPower = 0;
    public int myDefence = 0;
    public int myMovement = 0;
    public int myLuck = 0;
    public int myMaxHP = 0;
    public int myCurrentHP = 0;
    public int myGuts = 0;
    //Stat Block End

    //Equipment Block Start
    public Weapon myWeapon;
    public Armour myArmour;
    //Equipment Block End

    float myAnimationTime = 2f;
    public Renderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myWeapon.MakeSword();
        myArmour.MakeLeather();
    }

    public bool TakeDamage(int aAmount)
    {
        myCurrentHP -= aAmount;
        Debug.Log("Oof, " + aAmount + " damage");
        if (myCurrentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Die()
    {
        StartCoroutine(DeathFade());
    }

    IEnumerator DeathFade()
    {
        float counter = 0f;
        //Get current color

        while (counter < myAnimationTime)
        { 
            Color matColor = myRenderer.material.GetColor("_BaseColor");
            counter += Time.deltaTime;
            //Fade from 255 to 0
            int alpha = (int)Mathf.Lerp(3, 0, counter / myAnimationTime);
            Debug.Log(alpha);
            //Change alpha only
            matColor.a = alpha;
            myRenderer.material.SetColor("_BaseColor", matColor);// = matColor;
            //Debug.Log(myRenderer.material.color.a);
            //myRenderer.material.color = new Color(matColor.r, matColor.g, matColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
        //Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
