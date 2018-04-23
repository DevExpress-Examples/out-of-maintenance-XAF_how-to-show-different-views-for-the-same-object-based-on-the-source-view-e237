using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;

namespace WinWebSolution.Module {
    [DefaultClassOptions]
    public class Producer : BaseObject {
        public Producer(Session s) : base(s) { }

        public string Name;

        [Aggregated]
        [Association("Producer-Transactions")]
        public XPCollection<Transaction> Transactions {
            get { return GetCollection<Transaction>("Transactions"); }
        }
    }

    [DefaultClassOptions]
    public class Consumer : BaseObject {
        public Consumer(Session s) : base(s) { }

        public string Name;

        [Aggregated]
        [Association("Consumer-Transactions")]
        public XPCollection<Transaction> Transactions {
            get { return GetCollection<Transaction>("Transactions"); }
        }
    }

    public class Transaction : BaseObject {
        public Transaction(Session s) : base(s) { }

        public string Product;
        public decimal Amount;

        private Producer producer;
        [Association("Producer-Transactions")]
        public Producer Producer {
            get { return producer; }
            set { SetPropertyValue("Producer", ref producer, value); }
        }

        private Consumer consumer;
        [Association("Consumer-Transactions")]
        public Consumer Consumer {
            get { return consumer; }
            set { SetPropertyValue("Consumer", ref consumer, value); }
        }
    }
}
