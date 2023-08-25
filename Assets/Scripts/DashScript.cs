using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashScript : MonoBehaviour
{

    [SerializeField] Button _dashButton;
    [SerializeField] float coolDownTime;
    [SerializeField] float dashDistance;
    private Animator _animator;

    Color color;

    private bool isDashing = false;

    private bool onCoolDown = false;

    private Vector3 dashPosition;
    // Start is called before the first frame update

    private void Awake()
    {
        _dashButton.onClick.AddListener(dash);
        _animator = GetComponent<Animator>();
        color =_dashButton.image.color;

    }


    // Update is called once per frame
    void Update()
    {
        
        if (onCoolDown)
        {
            color.a = 0.2f;
            _dashButton.image.color = color;
        }
        else
        {
            color.a = 0.8f;
            _dashButton.image.color = color;
        }
        if (isDashing)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashPosition, 5f);
        }
        if(transform.position == dashPosition)
        {
            isDashing = false;
        }
    }

    void dash()
    {
        if (!onCoolDown)
        {
            _animator.SetTrigger("Dash");
            dashPosition  = transform.position + transform.forward * dashDistance;
            isDashing = true;
            StartCoroutine(coolDown());
        }
    }

    IEnumerator coolDown()
    {
        onCoolDown = true;
        yield return new WaitForSeconds(coolDownTime);
        onCoolDown = false;
    }
}
