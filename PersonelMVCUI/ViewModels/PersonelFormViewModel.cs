using PersonelMVCUI.Models.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonelMVCUI.ViewModels
{
    public class PersonelFormViewModel
    {
        public IEnumerable<DepartmanTb> Departmanlar{ get; set; }

        public PersonelTb Personel { get; set; }
    }
}