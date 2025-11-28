namespace Sokoban
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //좌표 알아내기
            //GetCursorPosition();: 커서의 위치를 가져온다
            //SetCursorPosition();: 커서의 위치를 설정한다
            //SetWindowPosition();: 창의 위치를 설정한다
            //Console.SetCursorPosition(5, 10);
            //Console.WriteLine(Console.GetCursorPosition());

            //-------------------초기화-------------------------
            // 콘솔창 초기화
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            //Console.CursorVisible = false;         
            Console.Title = "이남원의 소코반 만들자";
            Console.Clear();

            // 게임 데이터 초기화
            int playerX = 5;            
            int playerY = 10;
            int wallX = 0;
            int wallY = 0;
            int mapSizeMinX = 0;
            int mapSizeMinY = 0;
            int mapSizeMaxX = 10;
            int mapSizeMaxY = 10;


            //------------------게임루프-----------------------
            while (true)
            {

                //-----------------Render---------------------
                // 이전화면 지움
                Console.Clear();
                // 플레이어 출력
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("★");
                // 벽출력
                Console.SetCursorPosition(wallX, wallY);
                Console.Write("■");


                // -----------------ProcessInput--------------------
                // 키보드 입력
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                // -------------------Update--------------------
                // 플레이어 이동 처리
                // 나중에 switch문으로 변경해보자
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    playerY += 1; //math.Max 함수로 바꿔서 해보자
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (playerY > 0) //math.Max 함수로 바꿔서 해보자
                    {
                        playerY -= 1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (playerX > 0) //math.Max 함수로 바꿔서 해보자
                    {
                        playerX -= 1;
                    }

                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    playerX += 1; //math.Max 함수로 바꿔서 해보자

                }

                //if (playerX == wallX && playerY == wallY)
                //{
                //    Console.SetCursorPosition(20, 20);
                //    Console.Write("충돌함");
                //}

                //플레이어랑 벽과의 충돌 처리
                //1. 플레이어랑 벽이 충돌했는가? => 플레이어 좌표 == 벽 좌표
                bool isSamePlayerXWallX = playerX == wallX;
                bool isSamePlayerYWallY = playerY == wallY;
                bool isCollidedPlayerWithWall = isSamePlayerXWallX && isSamePlayerYWallY;
                //      ㄴ 어떤 데이터? (플레이어좌표) (벽 좌표)
                //      ㄴ 어떤 연산자? ==
                //1-1. 이전 좌표로 되돌린다.
                //      ㄴ(20,20) "충돌함"
                if (isCollidedPlayerWithWall)
                {
                    Console.SetCursorPosition(20, 20);
                    Console.Write("충돌함");
                }

                //콘솔 키 입력에 따라 기존 좌표로 이동시킨다.
                //충돌방향에 따라 충돌시 어떻게 할것인가
                // #p => 오른쪽(playerX + 1)

                // p# => 왼쪽(playerX - 1)

                // #
                // p => 위쪽(playerY + 1)

                // p => 아래쪽(playerY - 1)
                // #
                //구분하려면 콘솔 키 입력을 이용
                Console.GetCursorPosition();



                //플레이어 좌표값를 가져온다.
                //벽의 좌표값를 가져온다.

                //경우의 수 4가지
                //벽의 좌표값에서 Y좌표값+1
                //벽의 좌표값에서 Y좌표값-1
                //벽의 좌표값에서 x좌표값-1
                //벽의 좌표값에서 x좌표값+1

                //현재 플레이어 좌표값이 위의 4가지 경우에 해당되는지 판별한다.
                //해당 된다면 플레이어는 그 자리에서 움직이지 않는다.
            }

        }
    }
}
