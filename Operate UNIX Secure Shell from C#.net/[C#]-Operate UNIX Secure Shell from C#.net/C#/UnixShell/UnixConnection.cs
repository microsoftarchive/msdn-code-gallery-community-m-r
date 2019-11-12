using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnixShell
{
    class UnixConnection
    {
        public ConnectionInfo CreateConnection()
        {
            const string PrivateKeyFilePath = @"D:\SSH-RSA";

            ConnectionInfo connection;

            ConnectionInfo connInfo = new ConnectionInfo("HOSTNAME", 22, "UserName",
                new AuthenticationMethod[]{
                new PasswordAuthenticationMethod("Username","password"),
                new PrivateKeyAuthenticationMethod("username",new PrivateKeyFile[]
                {
                    new PrivateKeyFile(PrivateKeyFilePath,"Passphrase")
                }),
            });

            return connInfo;
        }
    }
}
