using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroView : MonoBehaviour
{
    public MoveType currentMove = MoveType.Up;
    public GameObject faceDirObject;
    public bool isControlHero = false;
    private Dictionary<MoveType, Vector3> moveDirectionDict = new Dictionary<MoveType, Vector3>()
    {
        {MoveType.Up, Vector3.forward},
        {MoveType.Down, Vector3.back},
        {MoveType.Left, Vector3.left},
        {MoveType.Right, Vector3.right},
    };

    private Dictionary<MoveType, Quaternion> rotateDict = new Dictionary<MoveType, Quaternion>()
    {
        {MoveType.Up,Quaternion.Euler(0, 0, 0)},
        {MoveType.Down, Quaternion.Euler(0, 180, 0)},
        {MoveType.Left, Quaternion.Euler(0, 270, 0)},
        {MoveType.Right, Quaternion.Euler(0, 90, 0)},
    };

    void Awake()
    {
        OnNotControlHero();
        RandomColor();
    }


    void Start()
    {

    }

    public void Collected()
    {
        this.tag = "Hero";
    }

    public void OnControlHero()
    {
        isControlHero = true;
        faceDirObject.gameObject.SetActive(true);
    }
    public void OnNotControlHero()
    {
        isControlHero = false;
        faceDirObject.gameObject.SetActive(false);
    }

    private void RandomColor()
    {
        Color randomColor = UnityEngine.Random.ColorHSV();
        foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material.color = randomColor;
        }
    }


    void Update()
    {
        if (isControlHero)
            Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), moveDirectionDict[currentMove] * 1, Color.yellow);
    }

    public GameObject CheckMove(MoveType type)
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), moveDirectionDict[type], out RaycastHit hit, 1))
        {
            Debug.Log("Did Hit : " + hit.collider.gameObject.name + ":" + hit.collider.tag);
            return hit.collider.gameObject;
        }
        else
            return null;
    }

    public void Move(MoveType type)
    {
        currentMove = type;
        Vector3 dir = moveDirectionDict[type];
        Quaternion rotateTo = rotateDict[type];
        Vector3 newPosition = transform.position + dir;
        transform.position = newPosition;
        transform.rotation = rotateTo;

    }

    public void SwapPosition(HeroView heroView)
    {
        currentMove = heroView.currentMove;
        transform.position = heroView.transform.position;
        Quaternion rotateTo = rotateDict[currentMove];
        transform.rotation = rotateTo;
    }

    public void ChangePosition(Vector3 tempLastPosition, MoveType tempLastCurrentMove)
    {
        currentMove = tempLastCurrentMove;
        transform.position = tempLastPosition;
        Quaternion rotateTo = rotateDict[currentMove];
        transform.rotation = rotateTo;
    }

    public void MoveFollow(HeroView heroView)
    {
        transform.position = heroView.transform.position - moveDirectionDict[heroView.currentMove];
        currentMove = heroView.currentMove;
        Quaternion rotateTo = rotateDict[currentMove];
        transform.rotation = rotateTo;

    }
}
