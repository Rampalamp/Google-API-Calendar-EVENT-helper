# Google-API-Calendar-EVENT-helper
createServiceAndCredentials, createEvent, deleteEVent

In Form1.cs you will find simple buttons to use to test the execution of the three main methods

createCredentialsAndService() -- No parameters, returns a CalendarService -- For user Credentials obtain your personal ones from google dev console

--Service must be initalized before being used in createEvent();
createEvent(string eventName, string eventDescription, IList<string> attendees, DateTime, startDate, DateTime, endDate, CalendarService service)

--Again service must be initalized before being used in deleteEvent();
deleteEvent(string eventName, string calendarID, CalendarService service);
