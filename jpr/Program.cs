using System;
using System.IO;
using System.Linq;

namespace jpr
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length > 0)
            {
                var p = new Program();
                p.Main_(args[0]);

            }
            else
            {
                Console.WriteLine("usage jpr Jpg-Filename");
             
            }

        }
        public void Main_(string filename)
        {
            //read jpg
            var buf = File.ReadAllBytes(filename);
            var maxlen = 20000;
            if (buf.Length < maxlen) maxlen = buf.Length;
            int pos = 0;
            //search for timestamp in first 20000 bytes
            for (int i = 1; (i < maxlen) && (pos == 0); i++)
            {
                if (
                    (buf[i] >= 48) && (buf[i] <= 57)
                    && (buf[i + 1] >= 48) && (buf[i + 1] <= 57)
                    && (buf[i + 2] >= 48) && (buf[i + 2] <= 57)
                    && (buf[i + 3] >= 48) && (buf[i + 3] <= 57)
                    && (buf[i + 5] >= 48) && (buf[i + 5] <= 57)
                    && (buf[i + 6] >= 48) && (buf[i + 6] <= 57)
                    && (buf[i + 8] >= 48) && (buf[i + 8] <= 57)
                    && (buf[i + 9] >= 48) && (buf[i + 9] <= 57)
                    && (buf[i + 11] >= 48) && (buf[i + 11] <= 57)
                    && (buf[i + 12] >= 48) && (buf[i + 12] <= 57)
                    && (buf[i + 14] >= 48) && (buf[i + 14] <= 57)
                    && (buf[i + 15] >= 48) && (buf[i + 15] <= 57)
                    && (buf[i + 17] >= 48) && (buf[i + 17] <= 57)
                    && (buf[i + 18] >= 48) && (buf[i + 18] <= 57)
                    )
                {
                    pos = i;
                }
            }
            if (pos == 0) return;
            //build new Filename
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            var sdat = String.Format("{0:D}", buf[pos] - 48); pos++;
            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos++;
            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos++;
            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos += 2;

            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos++;
            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos += 2;

            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos++;
            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos += 2;
            sdat = sdat + "-";
            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos++;
            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos += 2;

            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos++;
            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos += 2;

            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos++;
            sdat = sdat + String.Format("{0:D}", buf[pos] - 48); pos += 2;

            Console.WriteLine(sdat);

            var ext = ".jpg";

            //check if filename exist, otherwise appen N
            if (!File.Exists(sdat + ext))
            {
                File.Move(filename, sdat + ext);
                Console.WriteLine(filename + " moved to " + sdat + ext);
            }
            else
            {
                //extends filename with number
                int i = 1;
                string fn = "";
                do
                {
                    fn = sdat + String.Format("-{0:D}", i++) + ext;
                } while (File.Exists(fn));
                File.Move(filename, fn);
                Console.WriteLine(filename + " moved to " + fn);
            }

            //rename

        }


    }
}
