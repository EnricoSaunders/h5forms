﻿using System;
using System.Collections.Generic;
using H5Forms.Dtos.Form.Controls;

namespace H5Forms.Dtos.Form
{
    public class Form
    {        
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Enabled { get; set; }
        public string Title { get; set; }
        public IList<Control> Controls { get; set; }
        public User User { get; set; }       
    }
}
