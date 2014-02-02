using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nMail;

namespace MiniMail
{
    public class Pop3
    {
        private nMail.Pop3 Mailer = null;

        public bool Connect(string hostName, int port, MailOption option = null)
        {
            if (hostName == null) throw new ArgumentNullException("ホスト名をnullにすることはできません。");
            Mailer = new nMail.Pop3
            {
                HostName = hostName,
                Port = port
            };
            if (option != null) {
                Mailer.APop = option.UseApop;
                Mailer.SSL = option.UseSSL ? nMail.Pop3.SSL3 : Mailer.SSL;
            }
            try {
                Mailer.Connect();
            }
            catch (nMailException ex) {
                throw new MiniMailException("接続時に例外が発生しました。内部例外を確認してください。", ex);
            }
                return true;
            }
    }
}
