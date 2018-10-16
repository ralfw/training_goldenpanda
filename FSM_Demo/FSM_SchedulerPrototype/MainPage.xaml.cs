#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfSchedule.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FSM_SchedulerPrototype
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private double _currentIntervalHeight;

        public ScheduleAppointmentCollection Appointments { get; }

        public double CurrentIntervalHeight
        {
            get => _currentIntervalHeight;
            set
            {
                _currentIntervalHeight = value;
                OnPropertyChanged();
            }
        }

        public bool ShowDetails => schedule.ScheduleView == ScheduleView.DayView;

        public Command ToggleCommand { get; }

        public MainPage()
        {
            InitializeComponent();
            Appointments = new ScheduleAppointmentCollection();
            ToggleCommand = new Command(async () =>
            {
                if (schedule.ScheduleView == ScheduleView.WeekView)
                {
                    schedule.ScheduleView = ScheduleView.DayView;
                    toggleViewItem.Text = "Week";

                    var targetDateTime = schedule.SelectedDate ?? DateTime.Now.Date.AddHours(DateTime.Now.Hour);
                    await NavigateToDate(schedule.Height / 4f, targetDateTime);
                }
                else
                {
                    schedule.ScheduleView = ScheduleView.WeekView;
                    toggleViewItem.Text = "Day";

                    await NavigateToDate(schedule.Height / 9f, DateTime.Now.Date.AddHours(9));
                }
            });

            BindingContext = this;

            var appointment = new ScheduleAppointment();
            appointment.Color = Color.Blue;
            appointment.StartTime = DateTime.Now;
            appointment.EndTime = DateTime.Now.AddHours(1);
            appointment.Subject = "12";
            appointment.Location = "123456";
            appointment.Notes = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam";

            Appointments.Add(appointment);

            appointment = new ScheduleAppointment();
            appointment.Color = Color.Red;
            appointment.StartTime = DateTime.Now.AddMinutes(15);
            appointment.EndTime = DateTime.Now.AddHours(2);
            appointment.Subject = "123";
            appointment.Location = "123456";

            Appointments.Add(appointment);

            appointment = new ScheduleAppointment();
            appointment.Color = Color.Blue;
            appointment.StartTime = DateTime.Now;
            appointment.EndTime = DateTime.Now.AddHours(1);
            appointment.Subject = "1234";
            appointment.Location = "123456";

            Appointments.Add(appointment);

            appointment = new ScheduleAppointment();
            appointment.Color = Color.Red;
            appointment.StartTime = DateTime.Now.AddMinutes(15);
            appointment.EndTime = DateTime.Now.AddHours(2);
            appointment.Subject = "12345";
            appointment.Location = "123456";

            Appointments.Add(appointment);
        }

        private async Task NavigateToDate(double intervalHeight, DateTime focusDateTime)
        {
            CurrentIntervalHeight = intervalHeight;
            await Task.Delay(200);
            schedule.NavigateTo(focusDateTime);
        }

        private void MainPage_OnAppearing(object sender, EventArgs e)
        {
            NavigateToDate(schedule.Height / 9f, DateTime.Now.Date.AddHours(9));
        }

        private void MainPage_OnSizeChanged(object sender, EventArgs e)
        {
            NavigateToDate(schedule.Height / 9f, DateTime.Now.Date.AddHours(9));
        }
    }
}
