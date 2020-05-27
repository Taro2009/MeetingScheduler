using MeetingScheduler.Model;
using System;
using System.Collections.Generic;

namespace MeetingScheduler.UI.Wrapper
{
    // ModelWrapper-ből szedi az adatokat
    public class PersonWrapper : ModelWrapper<Person>
    {
        public PersonWrapper(Person model): base(model)
        {

        }

        public int Id { get { return Model.Id; } }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

    }
}
