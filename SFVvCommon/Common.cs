﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFVvCommon
{
    public class Common
    {
        public const string guidString = "7A1BB9C4-DF39-4E01-A8DC-20DC1A0C03C6";

        /// <summary>
        /// TTSエンジンのcomで使用されるCLSID
        /// </summary>
        public static Guid CLSID { get; } = new Guid(guidString);


        private const string tokensRegKey = @"SOFTWARE\Microsoft\Speech\Voices\Tokens\";
        private const string regClsid = "CLSID";

        /// <summary>
        /// Windowsのレジストリから、SAPIForVOICEVOXのスピーカー情報を削除します。
        /// </summary>
        public static void ClearStyleFromWindowsRegistry()
        {
            //SAPIForVOICEVOXのトークンを表すキーの列挙
            using (RegistryKey regTokensKey = Registry.LocalMachine.OpenSubKey(tokensRegKey, true))
            {
                string[] tokenNames = regTokensKey.GetSubKeyNames();
                foreach (string tokenName in tokenNames)
                {
                    using (RegistryKey tokenKey = regTokensKey.OpenSubKey(tokenName))
                    {
                        string clsid = (string)tokenKey.GetValue(regClsid);
                        if (clsid == CLSID.ToString("B"))
                        {
                            regTokensKey.DeleteSubKeyTree(tokenName);
                        }
                    }
                }
            }
        }

    }
}
