﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using Umbraco.Core.Models;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }
            {
                //// sender mail
                //MailMessage message = new MailMessage();
                //message.To.Add("nikolajstorgaardpedersen@gmail.com");
                //message.Subject = model.Subject;
                //message.From = new MailAddress(model.Email, model.Name);
                //message.Body = model.Message + "\n my email is: " + model.Email;

                //// mailopsætning
                //using (SmtpClient smtp = new SmtpClient())
                //{
                //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network; smtp.UseDefaultCredentials = false;
                //    smtp.EnableSsl = true;
                //    smtp.Host = "smtp.gmail.com"; smtp.Port = 587;
                //    smtp.Credentials = new System.Net.NetworkCredential("nikolajstorgaardpedersen@gmail.com", "kode");
                //    smtp.EnableSsl = true;

                //    smtp.Send(message);
                //}

                TempData["success"] = true;

                IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "Comment");

                comment.SetValue("name", model.Name);
                comment.SetValue("email", model.Email);
                comment.SetValue("subject", model.Subject);
                comment.SetValue("message", model.Message);

                // Save
                Services.ContentService.Save(comment);

                //Save and publish
                //Services.ContentService.SaveAndPublishWithStatus(comment);

                return RedirectToCurrentUmbracoPage();
            }

            

        }
    }
}
