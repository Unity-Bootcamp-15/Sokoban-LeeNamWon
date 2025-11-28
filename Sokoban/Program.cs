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
            Console.CursorVisible = false;         
            Console.Title = "소코반 만들자";
            Console.Clear();

            // 게임 데이터 초기화
            int x = 5;
            int y = 10;
            
            //------------------게임루프-----------------------
            while (true)
            {

                //-----------------Render---------------------
                // 이전화면 지움
                Console.Clear();
                Console.SetCursorPosition(x, y);                
                Console.Write("★");

                // -----------------ProcessInput--------------------
                // 키보드 입력
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                // -------------------Update--------------------
                // 플레이어 이동 처리
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    y += 1;                  

                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    y -= 1;
                    
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    x -= 1;
                    
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    x += 1;
                    
                }                              

            }        
        }
    }
}
