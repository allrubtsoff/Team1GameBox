using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cullable : MonoBehaviour
{
    //speed of the transition
    public float m_alphaChangeSpeed = 3f;

    // name of the variable in the shader that will be adjusted
    public string m_shaderVariableName = "Alpha";

    // end point of culling
    public float m_fadeTo = 0;
    // start point of culling
    public float m_fadeFrom = 1;

    // the float for the transition
    private float m_currentAlpha = 1.0f;

    // The material that will be affected by the transition 
    private Material m_mat;
    // Set to true if we want to fade the object out because it's in the way
    private bool m_occluding;

    // Set to true when the fade control co-routine is active
    private bool m_inCoroutine = false;


    public bool Occluding
    {
        get { return m_occluding; }
        set
        {
            if (m_occluding != value)
            {
                m_occluding = value;
                OnOccludingChanged();
            }
        }

    }

    void Start()
    {
        // grab the renderer's material and set the current alpha 
        m_mat = GetComponent<Renderer>().material;
        
        for(int i=0;i < m_mat.shader.GetPropertyCount();i++)
        {
            Debug.Log(m_mat.shader.GetPropertyName(i));
        }   
        
        m_currentAlpha = m_fadeFrom;
    }

    // Called when the Occluding value is changed
    private void OnOccludingChanged()
    {
        if (!m_inCoroutine)
        {
            StartCoroutine(FadeAlphaRoutine());
            m_inCoroutine = true;
            Debug.Log("Started OnOccludingChanged");
        }
    }

    private IEnumerator FadeAlphaRoutine()
    {
        while (m_currentAlpha != GetTargetAlpha())
        {
            float alphaShift = m_alphaChangeSpeed * Time.deltaTime;

            float targetAlpha = GetTargetAlpha();
            if (m_currentAlpha < targetAlpha)
            {
                m_currentAlpha += alphaShift;
                if (m_currentAlpha > targetAlpha)
                {
                    m_currentAlpha = targetAlpha;
                }
            }
            else
            {
                m_currentAlpha -= alphaShift;
                if (m_currentAlpha < targetAlpha)
                {
                    m_currentAlpha = targetAlpha;
                }
            }

            /*m_mat.color = new Color(m_mat.color.r, m_mat.color.g, m_mat.color.b, m_currentAlpha); */
            m_mat.SetFloat(m_shaderVariableName, m_currentAlpha);

            yield return null;
        }
        m_inCoroutine = false;
    }

    // Return the alpha value we want on all of our models
    private float GetTargetAlpha()
    {
        if (m_occluding)
        {
            return m_fadeTo;
        }
        else
        {
            return m_fadeFrom;
        }
    }
}



