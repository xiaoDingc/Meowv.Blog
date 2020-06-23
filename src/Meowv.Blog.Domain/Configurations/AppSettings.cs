﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Meowv.Blog.Domain.Configurations
{
    public class AppSettings
    {
        /// <summary>
        /// 配置文件的根节点
        /// </summary>
        private static readonly IConfigurationRoot _config;

        static AppSettings()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            _config = builder.Build();
        }

        /// <summary>
        /// EnableDb
        /// </summary>
        public static string EnableDb => _config["ConnectionStrings:Enable"];

        /// <summary>
        /// ConnectionStrings
        /// </summary>
        public static string ConnectionStrings => _config.GetConnectionString(EnableDb);

        public static string ApiVersion => _config["ApiVersion"];

        /// <summary>
        /// GitHub
        /// </summary>
        public static class GitHub
        {
            public static int UserId => Convert.ToInt32(_config["Github:UserId"]);
            public static string Client_ID => _config["Github:ClientID"];
            public static string Client_Secret => _config["Github:ClientSecret"];
            public static string Redirect_Uri => _config["Github:RedirectUri"];
            public static string ApplicationName => _config["Github:ApplicationName"];

        }

        public static class JWT
        {
            public static string Domain => _config["JWT:Domain"];

            public static string SecurityKey => _config["JWT:SecurityKey"];

            public static int Expires => Convert.ToInt32(_config["JWT:Expires"]);

        }

        public static class HangFire
        {
            public static string Login = _config["HangFire:Login"];
            public static string Password = _config["HangFire:Password"];

        }
        public static class Caching
        {
            public static string RedisConnectionString => _config["Caching:RedisConnectionString"];
        }



        public static class Email
        {
            /// <summary>
            /// Host
            /// </summary>
            public static string Host => _config["Email:host"];

            /// <summary>
            /// Port
            /// </summary>
            public static int Port => Convert.ToInt32(_config["Email:Port"]);

            /// <summary>
            /// UseSsl
            /// </summary>
            public static bool UseSsl => Convert.ToBoolean(_config["Email:UseSsl"]);

            /// <summary>
            /// From
            /// </summary>
            public static class From
            {
                /// <summary>
                /// Username
                /// </summary>
                public static string Username => _config["Email:From:Username"];

                /// <summary>
                /// Password
                /// </summary>
                public static string Password => _config["Email:From:Password"];

                /// <summary>
                /// Name
                /// </summary>
                public static string Name => _config["Email:From:Name"];

                /// <summary>
                /// Address
                /// </summary>
                public static string Address => _config["Email:From:Address"];
            }

            public static IDictionary<string, string> To
            {
                get
                {
                    var dic = new Dictionary<string, string>();
                    var emails = _config.GetSection("Email:To");
                    foreach (IConfigurationSection section in emails.GetChildren())
                    {
                        var name = section["Name"];
                        var address = section["Address"];

                        dic.Add(name, address);
                    }
                    return dic;
                }
            }
        }
    }
}
