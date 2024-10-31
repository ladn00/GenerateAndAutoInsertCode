using System.Runtime.InteropServices;
using WindowsInput;

class Program
{
    static string WindowName = "Tower of Fantasy  ";


    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;

    static string fixedPart = "7B2Z";
    static char[] characters = { '4', 'T', '1', '7', '6', 'I', 'N', 'Q', 'F', 'Z', 'G', '8', 'J', 'R', 'E', 'U', 'P', 'M', 'L', 'V', 'X', '2', 'Y', 'K', 'S', 'B', '0', 'W', 'H', '3', 'C', '9', 'D' };
    static (int, int)[] clickCoordinates = { (1485, 348), (1490, 489), (665, 461), (1732, 338) };

    static void Main(string[] args)
    {
        try
        {
            PerformActions();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void PerformActions()
    {
        Thread.Sleep(5000);

        while (true)
        {
            if (!IsTargetWindowActive())
            {
                Console.WriteLine("Выбранное окон неактивно");
                Thread.Sleep(1000);
                continue;
            }

            foreach (var (x, y) in clickCoordinates)
            {
                MouseClick(x, y);
                Console.WriteLine($"Координаты клика: x={x}, y={y}");

                if (x == 1485 && y == 348)
                {
                    string code = GenerateCode();
                    Console.WriteLine($"Ввод кода: {code}");
                    TypeCodeSlowly(code);
                }
                else if (x == 1490 && y == 489)
                {
                    Thread.Sleep(5000);
                }
                else if (x == 665 && y == 461)
                {
                    Thread.Sleep(1000);
                }
                else if (x == 1732 && y == 338)
                {
                    Thread.Sleep(1000);
                }
                if (!IsTargetWindowActive())
                {
                    break;
                }
            }
        }
    }

    static bool IsTargetWindowActive()
    {
        IntPtr hWnd = FindWindow(null, WindowName);
        return hWnd != IntPtr.Zero && SetForegroundWindow(hWnd);
    }

    static string GenerateCode()
    {
        Random random = new Random();
        string randomPart = new string(Enumerable.Range(0, 9).Select(_ => characters[random.Next(characters.Length)]).ToArray());
        return fixedPart + randomPart;
    }

    static void TypeCodeSlowly(string code)
    {
        InputSimulator simulator = new InputSimulator();
        foreach (char c in code)
        {
            simulator.Keyboard.TextEntry(c);
            Thread.Sleep(10);
        }
    }

    static void MouseClick(int x, int y)
    {
        SetCursorPos(x, y);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }
}