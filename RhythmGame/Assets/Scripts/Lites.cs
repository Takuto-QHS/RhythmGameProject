using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lites : MonoBehaviour
{
    [SerializeField] private float lightSpeed = 3;
    public float lightNum = 0;
    private Renderer rend;
    private float alfa = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rend = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rend.material.color.a > 0)
        {
            rend.material.color = new Color(
            rend.material.color.r,
            rend.material.color.g,
            rend.material.color.b,
            alfa);
        }
        else if (rend.material.color.a < 0)
        {
            ColorChange(0.0f);
        }

        switch(lightNum)
        {
            case 0:
                if(Keyboard.current.aKey.isPressed)
                {
                    ColorChange();
                }
                break;
            case 1:
                if (Keyboard.current.sKey.isPressed)
                {
                    ColorChange();
                }
                break;
            case 2:
                if (Keyboard.current.dKey.isPressed)
                {
                    ColorChange();
                }
                break;
            case 3:
                if (Keyboard.current.fKey.isPressed)
                {
                    ColorChange();
                }
                break;
        }

        alfa -= lightSpeed*Time.deltaTime;
    }

    void ColorChange(float _alfa = 0.3f)
    {
        alfa = _alfa;
        rend.material.color = new Color(
            rend.material.color.r,
            rend.material.color.g,
            rend.material.color.b,
            alfa);
    }
}
