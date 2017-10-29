using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace Flybiletter.Models
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
        public bool Cancelled { get; set; }
    }
}