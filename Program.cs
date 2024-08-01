using System.Text;
using Spectre.Console;
using Spectre.Console.Rendering;

Console.OutputEncoding = Encoding.UTF8;

string[][] matrix =
    [
        [ "", "", "" ],
        [ "", "", "" ],
        [ "", "", "" ],
    ];

bool computersTurn = false;
bool isUserX = new Random().Next() % 2 == 0;
string winner = string.Empty;
bool hasWinner() => string.IsNullOrWhiteSpace(winner) is false;
bool isFinished() => matrix.All(row => row.All(x => x != string.Empty)) || hasWinner();


int boxWidth = 9;
int boxHeight = 5;

string inputBox = "";
string userInput = isUserX ? "❌" : "⭕";

var computerChar = isUserX ? "⭕" : "❌";
var userChar = isUserX is false ? "⭕" : "❌";

AnsiConsole.MarkupLine("Biron bir katakchani tanlash uchun, o'sha katakchani tepasidagi [green]raqamni[/] kiriting [gray](1-9)[/]\n");
AnsiConsole.MarkupLine("O'yinni to'xtatish uchun esa [green]`q`[/] harfini kiritish yoki [green]`CTRL+C`[/] klavish kombinatsi bosish kerak\n");
AnsiConsole.MarkupLine("O'yinni boshlash uchun istalgan klaviatura tugmasini bosing");
Console.ReadKey();

do
{
    AnsiConsole.Clear();
    AnsiConsole.MarkupLine($"[bold green]Computer[/] {computerChar} ni o'ynaydi, [bold green]Siz[/] {userChar} ni o'ynaysiz.");
    Console.WriteLine();
    AnsiConsole.WriteLine(computersTurn ? "🤖 Computerni naxbati" : "🫵 ning navbatiz");
    Console.WriteLine();

    ShowGrid();

    if (!computersTurn)
    {
        inputBox = AnsiConsole.Ask<string>($"Qaysi katakchaga {userChar} ni joylamoqchisiz?[gray](1-9)[/]:");
        UpdateGrid(inputBox, userInput);
        computersTurn = true;
    }

    CheckWinner();

    if (computersTurn && winner == string.Empty)
        await ComputerMoves();

    if (isFinished())
    {
        if (string.IsNullOrEmpty(winner))
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[bold green]Computer[/] {computerChar} ni o'ynaydi, [bold green]Siz[/] {userChar} ni o'ynaysiz.");
            ShowGrid();
            AnsiConsole.MarkupLine("[bold green]Do'stlik g'alaba qozondi![/]");
        }
        else
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[bold green]Computer[/] {computerChar} ni o'ynaydi, [bold green]Siz[/] {userChar} ni o'ynaysiz.");
            ShowGrid();
            if (winner == "❌" && userChar == "❌" || winner == "⭕" && userChar == "⭕")
                AnsiConsole.MarkupLine($"[bold green]Siz yutdiz!!![/]");
            else if (winner == "❌" && computerChar == "❌" || winner == "⭕" && computerChar == "⭕")
                AnsiConsole.MarkupLine($"[bold green]Computer yutdi!!![/]");

            Console.WriteLine($"");
        }

        break;
    }

} while (inputBox.Trim().ToLower() != "q");


void ShowGrid()
{
    int boxNumber = 1;
    var grid = new Grid();
    grid.AddColumns(3);

    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        var rowContent2 = new IRenderable[3];
        for (int j = 0; j < matrix[i].Length; j++)
        {
            var text = new Text(matrix[i][j], new Style(Color.White)).Centered();

            var panel = new Panel(text)
            {
                Padding = new Padding(),
                Width = boxWidth,
                Height = boxHeight,
                Header = new PanelHeader($"{boxNumber}", Justify.Center),
                Border = BoxBorder.Rounded
            };
            rowContent2[j] = panel;
            boxNumber++;
        }
        grid.AddRow(rowContent2);
    }
    AnsiConsole.Write(grid);
    Console.WriteLine();
}

void UpdateGrid(string position, string content)
{
    switch (position)
    {
        case "1":
            if (matrix[0][0] is "")
                matrix[0][0] = content;
            else
            {
                AnsiConsole.MarkupLine("[red]Bu katakcha bo'sh emas, boshqasini tanlang![/]");
                position = AnsiConsole.Ask<string>($"Qaysi katakchaga {userChar} ni joylamoqchisiz?[gray](1-9)[/]:");
                UpdateGrid(position, content);
            }
            break;

        case "2":
            if (matrix[0][1] is "")
                matrix[0][1] = content;
            else
            {
                AnsiConsole.MarkupLine("[red]Bu katakcha bo'sh emas, boshqasini tanlang![/]");
                position = AnsiConsole.Ask<string>($"Qaysi katakchaga {userChar} ni joylamoqchisiz?[gray](1-9)[/]:");
                UpdateGrid(position, content);
            }
            break;
        case "3":

            if (matrix[0][2] is "")
                matrix[0][2] = content;
            else
            {
                AnsiConsole.MarkupLine("[red]Bu katakcha bo'sh emas, boshqasini tanlang![/]");
                position = AnsiConsole.Ask<string>($"Qaysi katakchaga {userChar} ni joylamoqchisiz?[gray](1-9)[/]:");
                UpdateGrid(position, content);
            }
            break;

        case "4":
            if (matrix[1][0] is "")
                matrix[1][0] = content;
            else
            {
                AnsiConsole.MarkupLine("[red]Bu katakcha bo'sh emas, boshqasini tanlang![/]");
                position = AnsiConsole.Ask<string>($"Qaysi katakchaga {userChar} ni joylamoqchisiz?[gray](1-9)[/]:");
                UpdateGrid(position, content);
            }
            break;

        case "5":
            if (matrix[1][1] is "")
                matrix[1][1] = content;
            else
            {
                AnsiConsole.MarkupLine("[red]Bu katakcha bo'sh emas, boshqasini tanlang![/]");
                position = AnsiConsole.Ask<string>($"Qaysi katakchaga {userChar} ni joylamoqchisiz?[gray](1-9)[/]:");
                UpdateGrid(position, content);
            }
            break;

        case "6":
            if (matrix[1][2] is "")
                matrix[1][2] = content;
            else
            {
                AnsiConsole.MarkupLine("[red]Bu katakcha bo'sh emas, boshqasini tanlang![/]");
                position = AnsiConsole.Ask<string>($"Qaysi katakchaga {userChar} ni joylamoqchisiz?[gray](1-9)[/]:");
                UpdateGrid(position, content);
            }
            break;

        case "7":
            if (matrix[2][0] is "")
                matrix[2][0] = content;
            else
            {
                AnsiConsole.MarkupLine("[red]Bu katakcha bo'sh emas, boshqasini tanlang![/]");
                position = AnsiConsole.Ask<string>($"Qaysi katakchaga {userChar} ni joylamoqchisiz?[gray](1-9)[/]:");
                UpdateGrid(position, content);
            }
            break;

        case "8":
            if (matrix[2][1] is "")
                matrix[2][1] = content;
            else
            {
                AnsiConsole.MarkupLine("[red]Bu katakcha bo'sh emas, boshqasini tanlang![/]");
                position = AnsiConsole.Ask<string>($"Qaysi katakchaga {userChar} ni joylamoqchisiz?[gray](1-9)[/]:");
                UpdateGrid(position, content);
            }
            break;

        case "9":
            if (matrix[2][2] is "")
                matrix[2][2] = content;
            else
            {
                AnsiConsole.MarkupLine("[red]Bu katakcha bo'sh emas, boshqasini tanlang![/]");
                position = AnsiConsole.Ask<string>($"Qaysi katakchaga {userChar} ni joylamoqchisiz?[gray](1-9)[/]:");
                UpdateGrid(position, content);
            }
            break;

        case "q":
            break;

        default:
            AnsiConsole.MarkupLine("\n[red]Mavjud bo'lmagan katakchani tanladingiz, tekshirib qaytadan kiriting!!![/]\n");
            position = AnsiConsole.Ask<string>($"Qaysi katakchaga {userChar} ni joylamoqchisiz?[gray](1-9)[/]:");
            UpdateGrid(position, content);
            break;
    }
}

void CheckWinner()
{
    for (int row = 0; row < 3; row++)
    {
        if (matrix[row].All(x => x == "❌"))
        {
            winner = "❌";
            return;
        }
        else if (matrix[row].All(x => x == "⭕"))
        {
            winner = "⭕";
            return;
        }
    }

    for (int column = 0; column < 3; column++)
    {
        var columnValues = matrix.Select(row => row[column]).ToArray();
        if (columnValues.All(x => x == "❌"))
        {
            winner = "❌";
            return;
        }
        else if (columnValues.All(x => x == "⭕"))
        {
            winner = "⭕";
            return;
        }
    }

    var leftDiagonal = matrix.Select((_, i) => matrix[i][i]).ToArray();
    if (leftDiagonal.All(x => x == "❌"))
    {
        winner = "❌";
        return;
    }
    else if (leftDiagonal.All(x => x == "⭕"))
    {
        winner = "⭕";
        return;
    }

    var rightDiagonal = matrix.Select((_, i) => matrix[i][2 - i]).ToArray();
    if (rightDiagonal.All(x => x == "❌"))
    {
        winner = "❌";
        return;
    }
    else if (rightDiagonal.All(x => x == "⭕"))
    {
        winner = "⭕";
        return;
    }
}

async Task ComputerMoves()
{
    await Task.Delay(1000);

    var emptyCards = new List<(int Row, int Column)>();

    for (int row = 0; row < 3; row++)
        for (int column = 0; column < 3; column++)
            if (matrix[row][column] == string.Empty)
                emptyCards.Add((row, column));


    if (emptyCards.Any())
    {
        var randomCard = emptyCards.OrderBy(_ => Guid.NewGuid()).First();
        matrix[randomCard.Row][randomCard.Column] = computerChar;
    }

    computersTurn = false;
    CheckWinner();
}

