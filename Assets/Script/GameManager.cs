using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int[,] _mapData = new int[5, 5]
    {
        { 3, 7, 7, 7, 3 },
        { 4, 0, 0, 0, 5 },
        { 4, 0, 1, 0, 5 },
        { 4, 0, 0, 0, 5 },
        { 3, 6, 6, 6, 3 }
    };

    public Transform Container;
    public Transform Head;
    public int CurrentLevel;
    public int MoveSpeed;
    private LevelHandler levelHandler;

    
    public Action OnWin { get; private set; }
    public Action OnLose { get; private set; }


    private GameState _gameState;
    private Direction _lastMoveDirection;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        levelHandler = new(_mapData);
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
            var body = Instantiate(AssetsManager.Instance.GetBlockByType(BlockType.Body), targetPoint, Quaternion.identity, Container);
            if (_lastMoveDirection == Direction.Up)
                Instantiate(AssetsManager.Instance.GetBlockByKey("UpLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);

            if (_lastMoveDirection == Direction.Down )
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
            var body = Instantiate(AssetsManager.Instance.GetBlockByType(BlockType.Body), targetPoint, Quaternion.identity, Container);
            if(direction == Direction.Left||direction==Direction.Right)
            {
                Instantiate(AssetsManager.Instance.GetBlockByKey($"UpLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);
                Instantiate(AssetsManager.Instance.GetBlockByKey($"DownLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);
            }
            if(direction == Direction.Up || direction == Direction.Down)
            {
                Instantiate(AssetsManager.Instance.GetBlockByKey($"RightLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);
                Instantiate(AssetsManager.Instance.GetBlockByKey($"LeftLine"), body.transform).transform.localPosition = new Vector3(0, 0.501f, 0);
            }
        }
    }    

    public GameState GetState() =>  _gameState; 

    public void SetState(GameState state)
    {
        _gameState = state;
    }

    public void GenerateMap(Cell[,] map)
    {
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
        if (cell.BlockType != BlockType.Empty)
        {
            var block = Instantiate(AssetsManager.Instance.GetBlockByType(cell.BlockType), Container);
            block.transform.position = cell.GetPosition();
            if (cell.BlockType == BlockType.Head)
                Head = block.transform;
        }
    }

}
