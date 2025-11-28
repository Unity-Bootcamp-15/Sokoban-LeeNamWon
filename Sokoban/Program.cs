namespace Sokoban
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 초기화
            Console.ResetColor();        
            // 색깔 조정
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            // 커서 숨김
            Console.CursorVisible = false;
            // 제목 수정
            Console.Title = "소코반 만들자";
            //콘솔창 내용 정리
            Console.Clear();
            
        }
    }
}
