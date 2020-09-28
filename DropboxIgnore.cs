using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dropboxignore
{
    class DropboxIgnore
    {
        enum Error { None, InvalidArguments, DropboxIgnoreFileMissing, CannotRemoveFromDropbox}

        static bool OPTION_DEBUG_MESSAGES = false;
        static string version = "0.01";

        static void writeLineDebug(String str)
        {
            if (OPTION_DEBUG_MESSAGES)
            {
                setDebugColor();
                Console.WriteLine(str);
                Console.ResetColor();
            }
        }

        static void writeLineError(String str)
        {
            setErrorColor();
            Console.WriteLine(str);
            Console.ResetColor();
        }

        static void writeLineWarning(String str)
        {
            setWarningColor();
            Console.WriteLine(str);
            Console.ResetColor();
        }

        static void writeLineSuccess(String str)
        {
            setSuccessColor();
            Console.WriteLine(str);
            Console.ResetColor();
        }

        static void setErrorColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        static void setWarningColor()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        static void setSuccessColor()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        static void setDebugColor()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\ndropboxignore v" + version + " ©2020 Pablo Saavedra López. Make Dropbox ignore files/folders specified in .dropboxignore file\n");

            //TODO Add -r option in order to restore sync of files/folders passed as parameter

            String currentDirStr = Directory.GetCurrentDirectory();

            if (!File.Exists(currentDirStr + "\\.dropboxignore"))
            {
                writeLineError("ERROR: .dropboxignore file not found");
                Environment.Exit((int)Error.DropboxIgnoreFileMissing);
            }
            else
            {
                writeLineSuccess("File '.dropboxignore' found");

                string[] lines = File.ReadAllLines(currentDirStr + "\\.dropboxignore");

                foreach (string line in lines)
                {
                    string currentLine = line.Trim();

                    if (currentLine != "")
                    {
                        if (!(currentLine[0] == '#'))
                        {
                            Console.WriteLine("Processing... [" + currentLine + "]");

                            //TODO Check if file/folder exists. If it doesn't exist, create it

                            System.Diagnostics.Process process = new System.Diagnostics.Process();
                            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                            startInfo.FileName = "powershell.exe";
                            startInfo.Arguments = "/C Set-Content -Path '" + currentLine + "' -Stream com.dropbox.ignored -Value 1";
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();

                            if (process.ExitCode == 0)
                            {
                                writeLineSuccess("[" + currentLine + "] removed from Dropbox sync");
                            }
                            else
                            {
                                writeLineError("ERROR: [" + currentLine + "] couldn't be removed from Dropbox sync");
                                Environment.Exit((int)Error.CannotRemoveFromDropbox);
                            }
                        }
                    }
                }
            }

            Environment.Exit((int)Error.None);
        }
    }
}