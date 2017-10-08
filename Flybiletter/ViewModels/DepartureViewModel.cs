using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flybiletter.ViewModels
{
    public class DepartureViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Price { get; set; }

    }
}