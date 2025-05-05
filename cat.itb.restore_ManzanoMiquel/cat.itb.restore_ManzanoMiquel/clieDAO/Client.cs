﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.restore_ManzanoMiquel.clieDAO
{
    public class Client
    {
        public virtual int _id {  get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string St { get; set; }
        public virtual string Zipcode { get; set; }
        public virtual int Area { get; set; }
        public virtual string Phone { get; set; }
        public virtual int Empid { get; set; } // FK empoyee
        public virtual decimal Credit { get; set; }
        public virtual string Comments { get; set; }
    }
}
