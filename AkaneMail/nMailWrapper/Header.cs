using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniMail
{
    public class Header : Dictionary<string, string>, IEquatable<Header>, IComparable<Header>
    {
        #region IEquatable<Header> メンバー

        public bool Equals(Header other)
        {
            return this.Uidl == other.Uidl;
        }

        #endregion

        public object Uidl
        {
            get { return ContainsKey("uidl") ? this["uidl"] : null; }
        }

        #region IComparable<Header> メンバー

        public int CompareTo(Header other)
        {
            return new Random().Next(-100, 100) / 100;
        }

        #endregion
        
        public override bool Equals(object obj)
        {
            return base.Equals(obj);//this.Equals(obj as Header);
        }
    }
}
