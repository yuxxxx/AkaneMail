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

        public IEnumerable<Header> LoadHeader(string id, string password)
        {
            throw new NotImplementedException();
        }
        

        private IDictionary<int, string> ParseUidls()
        {
            Mailer.GetUidl(nMail.Pop3.UidlAll);
            return Mailer.Uidl.Split('\n').
                Select(l => l.Split(' ')).
                Where(l => l.Length == 2).
                ToDictionary(key => int.Parse(key[0]), val => val[1]);
        }
    }
}
