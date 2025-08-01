﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    List<int[,]> mapList = new List<int[,]>
    {
        new int[,]
        {
            { 3, 3, 3, 3, 3 },
            { 3, 0, 0, 0, 3 },
            { 3, 0, 1, 0, 3 },
            { 3, 0, 0, 0, 3 },
            { 3, 3, 3, 3, 3 }
        },
        new int[,]
        {
            { 3, 3, 3, 3, 3, 3 },
            { 3, 0, 0, 0, 3, 3 },
            { 3, 0, 0, 0, 0, 3 },
            { 3, 3, 0, 1, 0, 3 },
            { 3, 3, 0, 0, 0, 3 },
            { 3, 3, 3, 3, 3, 3 }
        },
        new int[,]
        {
            { 3, 3, 3, 3, 3, 3 },
            { 3, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 1, 0, 3 },
            { 3, 0, 3, 3, 0, 3 },
            { 3, 0, 0, 0, 0, 3 },
            { 3, 3, 3, 3, 3, 3 }
        }
    };


    public Transform Container;
    public CameraController camera;
    public ParticleSystem particalEffect;
    private Transform Head;
    public int CurrentLevel { get; set; }
    [SerializeField] private int MoveSpeed;
    private LevelHandler levelHandler;

    private GameState _gameState;
    private Direction _lastMoveDirection;

    public Action OnWin { get; set; }
    public Action OnLose { get; set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadLevel(0);
    }

    public void ReloadLevel()
    {
        LoadLevel(CurrentLevel);
    }

    public void LoadNextLevel()
    {
        LoadLevel(CurrentLevel + 1);
    }

    public void LoadLevel(int level)
    {
        CurrentLevel = level;
        levelHandler = new(mapList[level]);
        _lastMoveDirection = Direction.None;
        GenerateMap(levelHandler.Map);
        _gameState = GameState.WaitingForInput;
    }


    private void Update()
    {
        switch (_gameState)
        {
            case GameState.WaitingForInput:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    TryMove(Direction.Up);
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    TryMove(Direction.Down);
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    TryMove(Direction.Left);
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                    TryMove(Direction.Right);

                break;

            case GameState.PlayingAnimation:
                break;

            case GameState.Win:
                break;

            case GameState.Lose:
                break;

            case GameState.Pause:
                break;

            default:
                break;
        }

    }
    void TryMove(Direction direction)
    {
        Debug.Log($"Try move on direction: {direction}");
        if (levelHandler.CanMove(direction))
        {

            _gameState = GameState.PlayingAnimation;
            StartCoroutine(PlayCatMoveAnimation(direction, levelHandler.MoveInDirection(direction).GetPosition()));
        }
    }
    public IEnumerator PlayCatMoveAnimation(Direction direction, Vector3 endPoint)
    {
        Debug.Log($"Move on direction: {direction}, target: {endPoint}");
        Vector3 targetPoint = Head.transform.position;

        if (_lastMoveDirection != Direction.None)
        {
            Debug.Log("log");
            var body = Instantiate(AssetsManager.Instance.GetBlockByType(CellType.Body), targetPoint, Quaternion.identity, Container);
            if (_lastMoveDirection == Direction.Up)
                Instantiate(AssetsManager.Instance.GetBlockByKey("UpLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);

            if (_lastMoveDirection == Direction.Down)
                Instantiate(AssetsManager.Instance.GetBlockByKey("DownLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);

            if (_lastMoveDirection == Direction.Left)
                Instantiate(AssetsManager.Instance.GetBlockByKey("LeftLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);

            if (_lastMoveDirection == Direction.Right)
                Instantiate(AssetsManager.Instance.GetBlockByKey("RightLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);

            if (direction == Direction.Up)
                Instantiate(AssetsManager.Instance.GetBlockByKey("DownLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);

            if (direction == Direction.Down)
                Instantiate(AssetsManager.Instance.GetBlockByKey("UpLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);

            if (direction == Direction.Left)
                Instantiate(AssetsManager.Instance.GetBlockByKey("RightLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);

            if (direction == Direction.Right)
                Instantiate(AssetsManager.Instance.GetBlockByKey("LeftLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);
        }
        else
        {
            CreateCatBody();
        }
        targetPoint = Vector3.MoveTowards(targetPoint, endPoint, 1f);
        while (true)
        {
            Head.transform.position = Vector3.MoveTowards(Head.transform.position, targetPoint, MoveSpeed * Time.deltaTime);
            if (direction == Direction.Left && Head.transform.position.x <= targetPoint.x
               || direction == Direction.Right && Head.transform.position.x >= targetPoint.x
               || direction == Direction.Down && Head.transform.position.z <= targetPoint.z
               || direction == Direction.Up && Head.transform.position.z >= targetPoint.z)
            {

                if (Mathf.Abs(Vector3.Distance(endPoint, targetPoint)) < 0.01f)
                {
                    camera.Shake(direction);
                    particalEffect.transform.parent.position = Head.transform.position;
                    switch (direction)
                    {
                        case Direction.Left:
                            particalEffect.transform.parent.forward = Vector3.left;
                            break;
                        case Direction.Right:
                            particalEffect.transform.parent.forward = Vector3.right;
                            break;
                        case Direction.Up:
                            particalEffect.transform.parent.forward = Vector3.forward;
                            break;
                        case Direction.Down:
                            particalEffect.transform.parent.forward = Vector3.back;
                            break;
                        case Direction.None:
                        default:
                            break;
                    }

                    particalEffect.Play();

                    if (levelHandler.CheckWinCondition())
                    {
                        SetState(GameState.Win);
                        OnWin?.Invoke();
                        break;
                    }
                    if (levelHandler.CheckLoseCondition())
                    {
                        SetState(GameState.Lose);
                        OnLose?.Invoke();
                        break;
                    }
                    else
                    {
                        _lastMoveDirection = direction;
                        SetState(GameState.WaitingForInput);
                        break;
                    }
                }
                else
                {
                    CreateCatBody();
                    targetPoint = Vector3.MoveTowards(targetPoint, endPoint, 1f);
                    Head.transform.position = Vector3.MoveTowards(Head.transform.position, targetPoint, MoveSpeed * Time.deltaTime);
                }

            }
            else
            {
                Head.transform.position = Vector3.MoveTowards(Head.transform.position, targetPoint, MoveSpeed * Time.deltaTime);
            }

            yield return null;
        }
        void CreateCatBody()
        {
            var body = Instantiate(AssetsManager.Instance.GetBlockByType(CellType.Body), targetPoint, Quaternion.identity, Container);
            if (direction == Direction.Left || direction == Direction.Right)
            {
                Instantiate(AssetsManager.Instance.GetBlockByKey($"UpLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);
                Instantiate(AssetsManager.Instance.GetBlockByKey($"DownLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);
            }
            if (direction == Direction.Up || direction == Direction.Down)
            {
                Instantiate(AssetsManager.Instance.GetBlockByKey($"RightLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);
                Instantiate(AssetsManager.Instance.GetBlockByKey($"LeftLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);
            }
        }
    }

    public GameState GetState() => _gameState;

    public void SetState(GameState state)
    {
        _gameState = state;
    }

    public void GenerateMap(Cell[,] map)
    {
        for (int i = 0; i < Container.childCount; i++)
        {
            Destroy(Container.GetChild(i).gameObject);
        }
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int row = 0; row < width; row++)
        {
            for (int column = 0; column < height; column++)
            {
                CreateBlock(map[row, column]);
            }
        }
    }

    public void CreateBlock(Cell cell)
    {
        if (cell.BlockType != CellType.Empty)
        {
            var block = Instantiate(AssetsManager.Instance.GetBlockByType(cell.BlockType), Container);
            block.transform.position = cell.GetPosition();
            if (cell.BlockType == CellType.Head)
                Head = block.transform;
        }
    }

}
