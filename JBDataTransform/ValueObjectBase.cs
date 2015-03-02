using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace JBDataTransform
{
    class ValueObjectBase : DynamicObject
    {
        private Dictionary<string, string> Record { get; set; }

        public ValueObjectBase()
        {
        }

        public void InitData(DataRow row, List<string> columns)
        {
            Record = new Dictionary<string, string>();

            foreach (string name in columns)
            {
                Record.Add(name, (row[name] + "").Trim());
            }
        }

        /// <summary>
        /// 資料欄位，不包含額外的自定屬性或方法。
        /// </summary>
        public virtual string[] DataFields
        {
            get { return Record.Keys.ToArray(); }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            if (Record.ContainsKey(binder.Name))
            {
                result = Record[binder.Name];
                return true;
            }
            else
                return base.TryGetMember(binder, out result);
        }

        public string GetData(string name)
        {
            if (Record.ContainsKey(name))
                return Record[name];
            else
                return string.Empty;
        }

        public override string ToString()
        {
            List<string> kvs = new List<string>();
            foreach (KeyValuePair<string, string> each in Record)
            {
                kvs.Add(string.Format("{0} = {1}", each.Key, each.Value));
            }

            return string.Join(",", kvs.ToArray());
        }

        public dynamic Self
        {
            get
            {
                return (dynamic)this;
            }
        }
    }
}
