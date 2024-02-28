using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroView : MonoBehaviour
{
    public MoveType currentMove = MoveType.Up;
    public GameObject faceDirObject;
    private Dictionary<MoveType, Vector3> moveDirectionDict = new Dictionary<MoveType, Vector3>()
    {
        {MoveType.Up, Vector2.up},
        {MoveType.Down, Vector2.down},
        {MoveType.Left, Vector2.left},
        {MoveType.Right, Vector2.right},
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

    public void OnControlHero()
    {
        faceDirObject.gameObject.SetActive(true);
    }
    public void OnNotControlHero()
    {
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

    }

    public void Move(MoveType type)
    {
        currentMove = type;
        Vector2 dir = moveDirectionDict[type];
        Quaternion rotateTo = rotateDict[type];
        Vector3 newPosition = transform.position + new Vector3(dir.x, 0, dir.y);
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
}
