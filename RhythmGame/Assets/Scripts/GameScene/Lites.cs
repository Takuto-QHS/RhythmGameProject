using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lites : MonoBehaviour
{
    [SerializeField] private float lightSpeed = 3;
    public int lightNum = 0;
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

        alfa -= lightSpeed*Time.deltaTime;
    }

    public void ColorChange(float _alfa = 0.3f)
    {
        alfa = _alfa;
        rend.material.color = new Color(
            rend.material.color.r,
            rend.material.color.g,
            rend.material.color.b,
            alfa);
    }
}
