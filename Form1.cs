using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventEmailAttemptTwo
{
    public partial class Form1 : Form
    {
        string calendID = "YOUR CALENDAR ID";
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Create Event Test";
        UserCredential credential;
        CalendarService service;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                service = createCredentialsAndService();

                
            }
            catch (Exception ez)
            {
                Console.WriteLine(ez.Message);
            }
            //EventsResource.ListRequest request = service.Events.List("greensaver.org_1lokosgre3bqpnue80ks6dvkdk@group.calendar.google.com");

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string calendarId = calendID;
            IList<string> attendees = new List<string>();
            attendees.Add("EMAIL ADDRESS");
            attendees.Add("EMAIL ADDRESS");

            createEvent("TestSummary", "TestDescription", attendees, startDate.Value, endDate.Value, service, calendarId);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            string calId = calendID;
            deleteEvent("TestSummary", calId, service);

        }

        public static void deleteEvent(string eventName, string calendarID, CalendarService service)
        {
            //Grab all events with respect to calendar ID greensaver.org_1lokosgre3bqpnue80ks6dvkdk@group.calendar.google.com TEST Calendar
            Events events = service.Events.List("greensaver.org_1lokosgre3bqpnue80ks6dvkdk@group.calendar.google.com").Execute();
            //Iterate through list
            IList<Event> eventList = (IList<Event>)events.Items;
            foreach (Event eventItem in eventList)
            {
                
                //Delete Event based on Event Summary (the title of event NOT description)
                if (eventName == eventItem.Summary)
                {
                    var result = service.Events.Delete("greensaver.org_1lokosgre3bqpnue80ks6dvkdk@group.calendar.google.com", eventItem.Id.ToString()).Execute();
                }

            }
        }
        public static void createEvent(string eventName, string eventDescription, IList<string> attendees, DateTime startDate, DateTime endDate, CalendarService service, string calId)
        {
            IList<EventAttendee> attending = new List<EventAttendee>();
            Event newEvent = new Event();
            newEvent.Summary = eventName;
            newEvent.Description = eventDescription;
            newEvent.Start = new EventDateTime();
            newEvent.Start.DateTime = startDate;
            newEvent.End = new EventDateTime();
            newEvent.End.DateTime = endDate;
            
            //Iterate through attendees List and add new EventAttendee to our attending list
            foreach (var eventAttendee in attendees)
            {
                attending.Add(new EventAttendee() { Email = eventAttendee });
            }
            //set attending to new event attendees
            newEvent.Attendees = attending;
            //Insert new event into calendar using calendar ID
            var eventResult = service.Events.Insert(newEvent, calId);
            //set notification to be sent.
            eventResult.SendNotifications = true;
            Event createdEvent = eventResult.Execute();

        }

        public static CalendarService createCredentialsAndService()
        {
            CalendarService service;
            UserCredential cred = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        //Adjust ClientID and ClientSecret according to Google developer API credential INFO
                        ClientId = "YOUR CLIENT ID FROM GOOGLE DEV CONSOLE",
                        ClientSecret = "YOUR CLIENT ID FROM GOOGLE DEV CONSOLE",
                    },
                    new[] { CalendarService.Scope.Calendar },
                    "user",
                    CancellationToken.None).Result;
            service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = cred,
                ApplicationName = ApplicationName,

            });

            return service;
        }
    }
}
