using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XF_Nodejs.Models
{
    public class Messenger_Model: ModelPropertyChanged
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Messenger { get; set; }
        public bool isInputChat { get; set; }
        public DateTime createDate { get; set; }
    }
}
