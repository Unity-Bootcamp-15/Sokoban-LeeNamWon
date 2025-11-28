using System;
using System.Text;

public class Sokoban
{
    // === 맵 정의 ===
    // 'P': 플레이어 (Player)
    // '#': 벽 (Wall)
    // '$': 상자 (Box)
    // '.': 목표 지점 (Goal)
    // '@': 상자가 목표 지점에 놓인 상태 (Box on Goal)
    // '+': 플레이어가 목표 지점에 서 있는 상태 (Player on Goal)
    // ' ': 빈 공간 (Empty)
    private static char[,] map = new char[,]
    {
        { '#', '#', '#', '#', '#', '#', '#', '#' },
        { '#', 'P', ' ', ' ', ' ', '.', ' ', '#' },
        { '#', ' ', '#', '$', ' ', '.', ' ', '#' },
        { '#', ' ', ' ', ' ', '#', '#', '#', '#' },
        { '#', ' ', '$', ' ', ' ', '.', ' ', '#' },
        { '#', '#', '#', '#', '#', '#', '#', '#' }
    };

    private static int playerX, playerY;
    private static int totalGoals;
    private static int boxesOnGoals;
    private static bool isRunning = true;

    public static void Main()
    {
        // 콘솔 인코딩 설정 (한글 깨짐 방지)
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false; // 커서 숨기기

        InitializeGame();

        while (isRunning)
        {
            DrawMap();
            DisplayStatus();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true); // 키 입력 받기 (화면에 표시 안 함)
            HandleInput(keyInfo);

            CheckWinCondition();
        }

        Console.SetCursorPosition(0, map.GetLength(0) + 4);
        Console.WriteLine("🏆 게임 종료! 축하합니다! 🏆");
        Console.WriteLine("아무 키나 누르면 프로그램을 닫습니다.");
        Console.ReadKey();
    }

    /// <summary>
    /// 게임 초기 설정: 플레이어 위치 찾기 및 목표 지점 개수 세기
    /// </summary>
    private static void InitializeGame()
    {
        totalGoals = 0;
        boxesOnGoals = 0;

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 'P')
                {
                    playerX = x;
                    playerY = y;
                }
                if (map[y, x] == '.' || map[y, x] == '+')
                {
                    totalGoals++;
                }
            }
        }
    }

    /// <summary>
    /// 현재 맵 상태를 콘솔에 그립니다.
    /// </summary>
    private static void DrawMap()
    {
        // 맵을 항상 0, 0 위치에서 다시 그리기 위해 커서 이동
        Console.SetCursorPosition(0, 0);

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                char tile = map[y, x];
                ConsoleColor originalColor = Console.ForegroundColor;

                switch (tile)
                {
                    case '#':
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write('■'); // 벽
                        break;
                    case 'P':
                    case '+':
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('P'); // 플레이어
                        break;
                    case '$':
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('B'); // 상자
                        break;
                    case '.':
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('ⓞ'); // 목표 지점
                        break;
                    case '@':
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('✅'); // 상자가 목표 지점에 있음
                        boxesOnGoals++; // (Draw 시점마다 세는 것이 비효율적이지만, 간단한 예시로 사용)
                        break;
                    default:
                        Console.Write(' '); // 빈 공간
                        break;
                }
                Console.ForegroundColor = originalColor;
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 게임 상태 정보를 표시합니다.
    /// </summary>
    private static void DisplayStatus()
    {
        // DrawMap 아래에 상태 메시지 출력
        Console.SetCursorPosition(0, map.GetLength(0) + 1);
        Console.WriteLine("------------------------------------------");
        Console.WriteLine($"목표: {boxesOnGoals} / {totalGoals} 달성");
        Console.WriteLine("이동: WASD (또는 화살표), Q: 종료");

        // boxesOnGoals 카운트 초기화 (DrawMap에서 증가시켰으므로 다음 프레임을 위해 초기화)
        boxesOnGoals = 0;
    }

    /// <summary>
    /// 키 입력을 처리하고 플레이어 이동 로직을 수행합니다.
    /// </summary>
    private static void HandleInput(ConsoleKeyInfo keyInfo)
    {
        int dx = 0; // x 변화량
        int dy = 0; // y 변화량

        switch (keyInfo.Key)
        {
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                dy = -1; // 위로 이동
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                dy = 1; // 아래로 이동
                break;
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                dx = -1; // 왼쪽으로 이동
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                dx = 1; // 오른쪽으로 이동
                break;
            case ConsoleKey.Q:
                isRunning = false; // 게임 종료
                return;
        }

        if (dx != 0 || dy != 0)
        {
            MovePlayer(dx, dy);
        }
    }

    /// <summary>
    /// 플레이어 이동 및 상자 밀기 로직을 처리합니다.
    /// </summary>
    private static void MovePlayer(int dx, int dy)
    {
        int nextX = playerX + dx;
        int nextY = playerY + dy;
        int nextNextX = nextX + dx;
        int nextNextY = nextY + dy;

        char nextTile = map[nextY, nextX];

        // 1. 벽(#)이면 이동 불가
        if (nextTile == '#')
        {
            return;
        }

        // 2. 상자($) 또는 목표 지점 위의 상자(@)를 만났을 때
        if (nextTile == '$' || nextTile == '@')
        {
            char nextNextTile = map[nextNextY, nextNextX];

            // 상자 다음 칸이 벽(#)이거나 다른 상자($ 또는 @)이면 상자를 밀 수 없음
            if (nextNextTile == '#' || nextNextTile == '$' || nextNextTile == '@')
            {
                return;
            }

            // --- 상자 밀기 로직 시작 ---

            // 상자 다음 칸을 새 상태로 업데이트
            if (nextNextTile == '.')
            {
                map[nextNextY, nextNextX] = '@'; // 목표 지점에 상자가 놓임
            }
            else // ' ' (빈 공간)
            {
                map[nextNextY, nextNextX] = '$'; // 빈 공간으로 상자 이동
            }

            // 상자가 있던 현재 칸을 플레이어가 이동할 자리로 변경
            if (nextTile == '@')
            {
                map[nextY, nextX] = '.'; // 상자가 사라진 후, 목표 지점(.)만 남음
            }
            else // '$'
            {
                map[nextY, nextX] = ' '; // 상자가 사라진 후, 빈 공간( )만 남음
            }
        }

        // 3. 플레이어 이동 처리 (이동이 확정된 후)

        // 플레이어가 서 있던 기존 위치를 원래 상태로 되돌림
        if (map[playerY, playerX] == 'P')
        {
            map[playerY, playerX] = ' '; // 플레이어가 빈 공간에 서 있었다면 빈 공간으로
        }
        else if (map[playerY, playerX] == '+')
        {
            map[playerY, playerX] = '.'; // 플레이어가 목표 지점에 서 있었다면 목표 지점으로
        }

        // 플레이어의 새 위치를 설정
        if (map[nextY, nextX] == '.')
        {
            map[nextY, nextX] = '+'; // 플레이어가 목표 지점으로 이동
        }
        else // ' ' (빈 공간)
        {
            map[nextY, nextX] = 'P'; // 플레이어가 빈 공간으로 이동
        }

        // 플레이어 좌표 업데이트
        playerX = nextX;
        playerY = nextY;
    }

    /// <summary>
    /// 승리 조건을 확인합니다.
    /// </summary>
    private static void CheckWinCondition()
    {
        int currentBoxesOnGoals = 0;
        // 맵을 순회하며 '@' (상자가 목표에 있는 상태)의 개수를 셉니다.
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == '@')
                {
                    currentBoxesOnGoals++;
                }
            }
        }

        boxesOnGoals = currentBoxesOnGoals; // DisplayStatus를 위해 업데이트

        if (boxesOnGoals == totalGoals)
        {
            isRunning = false; // 모든 상자가 목표 지점에 도착
        }
    }
}