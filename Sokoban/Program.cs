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
                    playerY += 1;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (playerY > 0)
                    {
                        playerY -= 1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (playerX > 0)
                    {
                        playerX -= 1;
                    }
                        
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    playerX += 1;
                    
                }
                
            }        
        }
    }
}
