using System;
using CORE;
using Connecter2.Sending;

namespace Connecter2
{
	class MainClass
	{
        public static void test()
        {
            DP_SCH_LIB.crypt_V4.Crypt tmp0 = new DP_SCH_LIB.crypt_V4.Crypt("Test");
            string t0 = tmp0.Enc("Test");
            sys.writeln(t0);
            string t01 = tmp0.Dec(t0);
            sys.writeln(t01);
            return;


            DP_SCH_LIB.CRYPT cr = new DP_SCH_LIB.CRYPT("Test");
            DP_SCH_LIB.CRYPT crU = new DP_SCH_LIB.CRYPT("Test", "SCH5U");
            DP_SCH_LIB.CRYPT crM = new DP_SCH_LIB.CRYPT("Test", "SCH5-1");
            DP_SCH_LIB.CRYPT cr1 = new DP_SCH_LIB.CRYPT("Test", "SCH1");
            DP_SCH_LIB.CRYPT cr2 = new DP_SCH_LIB.CRYPT("Test", "SCH2");
            string text = "MyTestEncrypt";
            sys.writeln(cr.Enc(text));
            string t = crU.Enc(text);
            sys.writeln(t);
            string t2 = crM.Enc(text);
            sys.writeln(t2);
            string t3 = cr1.Enc(text);
            sys.writeln(t3);
            string t4 = cr2.Enc(text);
            sys.writeln(t4);

            string tmp = t3.Substring(0, 8);
            for (int i = 0; i < 8; i++)
                tmp += "F";
            tmp += t3.Substring(16);
            sys.writeln(tmp);
            tmp=tmp.Insert(4,"F");
            cr = new DP_SCH_LIB.CRYPT("Test");
            sys.writeln(cr.Dec(tmp));

                sys.writeln(crU.Dec(t));
            sys.writeln(crM.Dec(t2));
            sys.writeln(cr1.Dec(t3));
            sys.writeln(cr2.Dec(t4));
            cr = new DP_SCH_LIB.CRYPT("test", "SCH2");
            sys.writeln(cr.Dec(t4));

        }
        public static void LoadSystem(string[] args)
        {
            string password = "std::password";
            sys.fileSetting = "conf.sys";
            string lang = "std";
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("--test"))
                {
                    test();
                    continue;
                }
                if (args[i].Equals("--pass"))
                {
                    password = args[i + 1];
                    i += 1;
                    continue;
                }
                if (args[i].Equals("--dsch"))
                {
                    string DecFile = args[i + 2];
                    string SFile = args[i + 3];
                    string DecPass = args[i + 1];
                    i += 3;
                    DP_SCH_LIB.CRYPT cr = new DP_SCH_LIB.CRYPT(DecPass);
                    System.IO.File.WriteAllText(SFile, cr.Dec(System.IO.File.ReadAllText(DecFile)));
                    continue;
                }
                if (args[i].Equals("--dec"))
                {
                    string DecFile = args[i + 2];
                    string DecPass = args[i + 1];
                    i += 2;
                    DP_SCH_LIB.CRYPT cr = new DP_SCH_LIB.CRYPT(DecPass);
                    sys.writeln(cr.Dec(System.IO.File.ReadAllText(DecFile)));
                    continue;
                }
                if (args[i].Equals("--enc"))
                {
                    string DecFile = args[i + 2];
                    string DecPass = args[i + 1];
                    i += 2;
                    DP_SCH_LIB.CRYPT cr = new DP_SCH_LIB.CRYPT(DecPass);
                    sys.writeln(cr.Enc(System.IO.File.ReadAllText(DecFile)));
                    continue;
                }
                if (args[i].Equals("--esch"))
                {
                    string DecFile = args[i + 2];
                    string SFile = args[i + 3];
                    string DecPass = args[i + 1];
                    i += 3;
                    DP_SCH_LIB.CRYPT cr = new DP_SCH_LIB.CRYPT(DecPass);
                    System.IO.File.WriteAllText(SFile, cr.Enc(System.IO.File.ReadAllText(DecFile)));
                    continue;
                }
                if (args[i].Equals("-lang"))
                {
                    lang = args[i + 1];
                    i++;
                    continue;
                }
                if (args[i].Equals("--conf"))
                {
                    sys.fileSetting = args[i + 1];
                    i++;
                    continue;
                }
            }
            sys.cr = new DP_SCH_LIB.CRYPT(password);
            password = "pass";
            if (!System.IO.File.Exists(sys.fileSetting))
            {
                sys.setting = new INI("lang=ru");
            }
            else
            {
                string text = System.IO.File.ReadAllText(sys.fileSetting);
                text = sys.cr.Dec(text);
                sys.setting = new INI(text);
            }
            if (lang.Equals("std"))
            {
                if (sys.setting.Exists("lang"))
                    lang = sys.setting.Get("lang");
                else
                    lang = "ru";
            }
            sys.lang = new INI(System.IO.File.ReadAllText(lang + ".txt"));
        }
		public static void Main (string[] args)
		{
            LoadSystem(args);
            Master boot = new Master(args);
            boot.Send();

            sys.read();
			//CONNECTER.Test2 (args);
			//CONNECTER.SendData();
		}
	}
}
