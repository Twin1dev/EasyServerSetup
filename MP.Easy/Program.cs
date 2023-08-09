using System.Diagnostics;
using System.IO.Compression;
using System.Net;

static void SetForeground(ConsoleColor color)
{
    Console.ForegroundColor = color;
}

static void ResetConsole()
{
    Console.ForegroundColor = ConsoleColor.White;
}

SetForeground(ConsoleColor.Blue);
Console.WriteLine("[-] Started EasyServerSetup!");

ResetConsole();

Console.WriteLine("Choose your path!\n\n");

Console.WriteLine("[1] Gameserver + Backend");

/*Console.WriteLine("[2] VPS Setup (Node, Reboot Launcher, Dll)");

Console.WriteLine("[3] Gameserver");

Console.WriteLine("[4] Backend");*/

start:
Console.Write(">> ");

var output = Console.ReadLine();
int value;

if (Int32.TryParse(output, out value))
{
    switch (value)
    {
        case 1:
            SetForeground(ConsoleColor.Green);
            Console.WriteLine("Downloading RebootV3");
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += ProgressChanged;

                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Backend");
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Gameserver");

                if (File.Exists(Directory.GetCurrentDirectory() + "\\rebootv3.zip"))
                {
                    File.Delete(Directory.GetCurrentDirectory() + "\\rebootv3.zip");
                }

                Thread.Sleep(500);

                wc.DownloadFileAsync(new Uri("https://github.com/Milxnor/Project-Reboot-3.0/archive/refs/heads/master.zip"), Directory.GetCurrentDirectory() + "\\rebootv3.zip");

                while (wc.IsBusy)
                {
                    Thread.Sleep(100);
                }

            
                Console.WriteLine("\nDownloading Backend!");

                if (File.Exists(Directory.GetCurrentDirectory() + "\\backend.zip"))
                {
                    File.Delete(Directory.GetCurrentDirectory() + "\\backend.zip");
                }

                wc.DownloadFileAsync(new Uri("https://github.com/Lawin0129/LawinServerV2/archive/refs/heads/main.zip"), Directory.GetCurrentDirectory() + "\\backend.zip");

                while (wc.IsBusy)
                {
                    Thread.Sleep(100);
                }

                Console.WriteLine("Downloaded!");

                SetForeground(ConsoleColor.Blue);

                Console.WriteLine("Extracting.");

                ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + "\\backend.zip", Directory.GetCurrentDirectory() + "\\Backend");
                ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + "\\rebootv3.zip", Directory.GetCurrentDirectory() + "\\Gameserver");

                SetForeground(ConsoleColor.Green);

                Console.WriteLine("Extracted!");

                ResetConsole();

                Process.Start("explorer.exe", $"/open, {Directory.GetCurrentDirectory() + "\\Backend"}");
                Process.Start("explorer.exe", $"/open, {Directory.GetCurrentDirectory() + "\\Gameserver"}");
            }
            break;
        case 2:
            break;
        case 3:
            break;
        case 4:
            break;
        default:
            Console.WriteLine("Incorrect!");
            goto start;
            break;
        
    }
} else
{
    Console.WriteLine("Incorrect!");
    goto start;
}

static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
{
    if (e.TotalBytesToReceive <= 0)
    { return; }
    int percentage = (int)((double)e.BytesReceived / e.TotalBytesToReceive * 100);

    Console.Write("\rProgress: [{0}{1}] {2}%   ",
        new string('#', percentage / 5), new string(' ', 20 - percentage / 5), percentage);

    if (percentage == 100)
    {
        Console.WriteLine();
    }
}

