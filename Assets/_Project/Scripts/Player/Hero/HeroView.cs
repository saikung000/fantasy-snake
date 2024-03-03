using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroView : MonoBehaviour
{
    public GameObject faceDirObject;
    public bool isControlHero = false;
    public GameObject collectEffect;
    public Animator animator;
    private Dictionary<DirectionType, Vector3> moveDirectionDict = new Dictionary<DirectionType, Vector3>()
    {
        {DirectionType.Up, Vector3.forward},
        {DirectionType.Down, Vector3.back},
        {DirectionType.Left, Vector3.left},
        {DirectionType.Right, Vector3.right},
    };

    private Dictionary<DirectionType, Quaternion> rotateDict = new Dictionary<DirectionType, Quaternion>()
    {
        {DirectionType.Up,Quaternion.Euler(0, 0, 0)},
        {DirectionType.Down, Quaternion.Euler(0, 180, 0)},
        {DirectionType.Left, Quaternion.Euler(0, 270, 0)},
        {DirectionType.Right, Quaternion.Euler(0, 90, 0)},
    };

    void Awake()
    {
        RandomColor();
    }

    public void Collected()
    {
        collectEffect.SetActive(false);
        this.tag = "Hero";
        animator.SetBool("Walk", true);
        foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            Color modelColor = meshRenderer.material.color;
            modelColor.a = 1;
            meshRenderer.material.color = modelColor;
        }

    }

    public void ControlHero()
    {
        isControlHero = true;
        faceDirObject.gameObject.SetActive(true);
    }
    public void NotControlHero()
    {
        isControlHero = false;
        faceDirObject.gameObject.SetActive(false);
    }

    private void RandomColor()
    {
        Color randomColor = UnityEngine.Random.ColorHSV();
        randomColor.a = 0.5f;
        foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material.color = randomColor;
        }
    }


    void Update()
    {
        //if (isControlHero)
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward * 1, Color.yellow);
    }

    public GameObject CheckMove(DirectionType type)
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), moveDirectionDict[type], out RaycastHit hit, 1))
        {
            Debug.Log("Did Hit : " + hit.collider.gameObject.name + ":" + hit.collider.tag);
            return hit.collider.gameObject;
        }
        else
        {
            //Debug.Log("Didn't Hit");
            return null;
        }
    }

    public void Move(DirectionType type)
    {
        Vector3 dir = moveDirectionDict[type];
        Vector3 newPosition = transform.position + dir;
        transform.position = newPosition;
    }

    public void Rotate(DirectionType type)
    {
        Quaternion rotateTo = rotateDict[type];
        transform.rotation = rotateTo;
    }

    public void MoveToFollowTarget(HeroPresenter anotherHero)
    {
        transform.position = anotherHero.transform.position - moveDirectionDict[anotherHero.currentDirection];
    }
}
