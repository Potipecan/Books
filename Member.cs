using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    public class Member
    {
        private string NameSurname, Phone, Address, Mail;

        public Member()
        {

        }

        public Member(string namesurname, string phone, string address, string mail)
        {
            NameSurname = namesurname;
            Phone = phone;
            Address = address;
            Mail = mail;
        }
    }
}
