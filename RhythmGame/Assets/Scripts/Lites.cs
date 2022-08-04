using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lites : MonoBehaviour
{
    [SerializeField] private float lightSpeed = 3;
    [SerializeField] private float lightNum = 0;
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
                if(Input.GetKeyDown(KeyCode.A))
                {
                    ColorChange();
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.S))
                {
                    ColorChange();
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.D))
                {
                    ColorChange();
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.F))
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
