using ManagerSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    public class MainDefaultModel
    {
        public MainDefaultModel() {

            this.Data = new MainDefaultModelList();
        }
        public MainDefaultModelList Data { get; set; }

    }
}