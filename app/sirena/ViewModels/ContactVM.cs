using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.ViewModels
{
    public class ContactVM
    {
        [ScaffoldColumn(false)]
        public Guid Id { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string Phone { set; get; }

        public string Phone2 { set; get; }

        public string Phone3 { set; get; }

        public string Phone4 { set; get; }

        public string Phon5 { set; get; }

        public string Email { set; get; }

        public string Facebook { set; get; }

        public string Telegram { set; get; }

        public string Viber { set; get; }

        public string Instagram { set; get; }

        public string Address { set; get; }
    }
}
