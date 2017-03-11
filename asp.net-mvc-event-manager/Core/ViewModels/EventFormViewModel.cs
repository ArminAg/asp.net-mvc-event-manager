using asp.net_mvc_event_manager.Controllers;
using asp.net_mvc_event_manager.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace asp.net_mvc_event_manager.Core.ViewModels
{
    public class EventFormViewModel
    {
        public int Id { get; set; }
        public string Action
        {
            get
            {
                Expression<Func<EventsController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<EventsController, ActionResult>> create = (c => c.Create(this));
                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        public string Heading { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}