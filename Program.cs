using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SecretSantaDraw
{
    class Program
    {
        private static List<Colleague> _devtechColleagues;
        private static Dictionary<Colleague, Colleague> _inpairedColleagues = new Dictionary<Colleague, Colleague>();
        static void Main(string[] args)
        {
            _devtechColleagues = new List<Colleague>()
            {
                new Colleague("Vladimir", "Djordjevic", Position.Dev, Gender.Male),
                new Colleague("Aleksandra", "Josipovic", Position.Finance, Gender.Female),
                new Colleague("Jovana", "Nikolic", Position.Finance, Gender.Female),
                new Colleague("Jovanka", "Spasojevic", Position.Finance, Gender.Female),
                new Colleague("Lazar", "Stajcic", Position.Dev, Gender.Male),
                new Colleague("Maja", "Ucek", Position.Dev, Gender.Female),
                new Colleague("Nevena", "Prokic", Position.BtoB, Gender.Female),
                new Colleague("Nikola", "Curcic", Position.Finance, Gender.Male),
                new Colleague("Nikola", "Papic", Position.Dev, Gender.Male),
                new Colleague("Nikola", "Zivkovic", Position.Dev, Gender.Male),
                new Colleague("Vladimir", "Bojovic", Position.Law, Gender.Male),
                new Colleague("Iva", "Milic", Position.Dev, Gender.Female),
                new Colleague("Ivan", "Milivojevic", Position.Dev, Gender.Male),
                new Colleague("Sarah", "Gak", Position.Law, Gender.Female)
                //new Colleague("Iva", "Posta", Position.Dev, Gender.Female, "milaiva@ptt.rs"),
                //new Colleague("Iva", "Lujka", Position.BtoB, Gender.Other, "milaiva.milic.1993@gmail.com")
            };

            List<Colleague> drawList = new List<Colleague>(_devtechColleagues);
            PopulatePairList(drawList);

            int i = 1;
            foreach (var pair in _inpairedColleagues)
            {
                Console.WriteLine($"{i++}: {pair.Key.GetFullName()} buys present to {pair.Value.GetFullName()}!");
                //SendSantaNotice(pair);
            }
            Console.ReadKey();
        }

        static void PopulatePairList(List<Colleague> drawList)
        {
            if (drawList.Count == 1)
            {
                Colleague randomSantaChild = drawList.First();
                Colleague SantaColleague = _devtechColleagues.ElementAt(_devtechColleagues.Count() - drawList.Count());
                _inpairedColleagues.Add(SantaColleague, randomSantaChild);
            }
            else
            {
                Colleague randomSantaChild = drawList.ElementAt(new Random().Next(drawList.Count()));
                Colleague SantaColleague = _devtechColleagues.ElementAt(_devtechColleagues.Count() - drawList.Count());
                if (!String.Equals(SantaColleague.GetFullName(), randomSantaChild.GetFullName()))
                {
                    _inpairedColleagues.Add(SantaColleague, randomSantaChild);
                    drawList.Remove(randomSantaChild);
                    PopulatePairList(drawList);
                }
            }
        }

        private static void SendSantaNotice(KeyValuePair<Colleague, Colleague> pair)
        {
            String FromMail, ToMail, Subject, EmailHead, EmailSignature, Body;
            try
            {
                Subject = "Secret Santa draw is finished";
                EmailHead = $"<b>Dear colleague,</b><br/>you have been drawn to be the Secret Santa to {pair.Value.GetFullName()}.<br/>Please choose your present with much care, but also wisely to stay inside the budget.<br/><br/>";
                EmailSignature = "<br/>Thanks and Regards,<br/><b>Secret Santa Organisation</b><br/>";
                Body = EmailHead + EmailSignature;

                using (MailMessage mailMessage = new MailMessage())
                {
                    FromMail = "milaiva.milic.1993@gmail.com";
                    ToMail = pair.Key.Email;

                    mailMessage.From = new MailAddress(FromMail);
                    mailMessage.Subject = Subject;
                    mailMessage.Body = Body;
                    mailMessage.IsBodyHtml = true;

                    mailMessage.To.Add(ToMail);
                    var smtp = new SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new NetworkCredential("milaiva.milic.1993@gmail.com", "ivAmil1c93"),
                        EnableSsl = true
                    };

                    smtp.Send(mailMessage); //sending Email  
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}
