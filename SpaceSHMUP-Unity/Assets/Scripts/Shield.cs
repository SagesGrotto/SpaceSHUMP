/**** 
 * Created by: Akram Taghavi-Burris
 * Date Created: March 16, 2022
 * 
 * Last Edited by: NA
 * Last Edited: March 16, 2022
 * 
 * Description: Shield level controler, adjusts the fresnel effect power based on level of shields.
****/


/** Using Namespaces **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Color shieldColor; //color of shields
    public Color shieldHitColor; //color of shields hit
    public Color shieldPowerColor; //color of shields powerup
    public float alphaLevel = 0.5f; //the shiled level shown by shader
    public float minAlpha = 0.5f; //semi-transparent fresnel effect
    public float maxAlpha = 1f; //transparent fresnel effect

    private float currLevel; //the ships current shield level
    private float lastLevel; //the last recorded amount of shield level
    private Material mat; //material of shield

    // Start is called before the first frame update
    void Start()
    {
        currLevel = Hero.SHIP.shieldLevel; //get the shield level
        lastLevel = currLevel; //record the shield level
        alphaLevel = 0.5f; //set the default alpha
        mat = GetComponent<Renderer>().material; //get the material for the shield
        mat.SetColor("_color", shieldColor); //set the defualt color
        mat.SetFloat("_alphaLevel", alphaLevel); //finds the materials shader variable, and sets the value
    }

    // Update is called once per frame
    void Update()
    {
        currLevel = Hero.SHIP.shieldLevel;

        //if we are at maximum shields
        if (currLevel == Hero.SHIP.maxShield)
        {
            alphaLevel = 0.5f; //set the level shown to our default 
            mat.SetFloat("_alphaLevel", alphaLevel); //set the level shown for the Fresnel effect
        }

        //if shield levels have decreased
        if (lastLevel > currLevel)
        {
            lastLevel = currLevel; //set the level Level to the current level shield
            alphaLevel += 0.1f; // decrease the levelShown value slowly (by time) therby decresing the transprancy
            mat.SetColor("_color", shieldHitColor); //Change the color on hit
            mat.SetFloat("_alphaLevel", Mathf.Clamp(alphaLevel, minAlpha, maxAlpha)); //finds the materials shader variable, and sets the value
            
            Invoke("ColorChange", 0.25f); //delay color rest
        }

        //if shield levels have increased
        if (lastLevel < currLevel)
        {
            lastLevel = currLevel;//set the level Level to the current level shield
            alphaLevel -= 0.1f; // increase the levelShown value slowly (by time) therby incresing the transprancy
            mat.SetFloat("_alphaLevel", Mathf.Clamp(alphaLevel, minAlpha, maxAlpha)); //finds the materials shader variable, and sets the value
            mat.SetColor("_color", shieldPowerColor); //Change the color 

            Invoke("ColorChange", 0.25f); //delay color rest
        }

    }//end Update()

    //Reset the shield color
    private void ColorChange()
    {
        mat.SetColor("_color", shieldColor);
    }//end ColorChange()

}
