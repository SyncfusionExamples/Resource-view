using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ResourceView
{
    public class SchedulerPageBehavior : Behavior<ContentPage>
    {
        SfSchedule schedule;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.schedule = bindable.Content.FindByName<SfSchedule>("schedule");
            this.WireEvents();


            var col = schedule.ViewHeaderStyle.DateTextColor;
        }

        private void WireEvents()
        {
            this.schedule.VisibleDatesChangedEvent += OnVisibleDatesChangedEvent;
        }

        private void OnVisibleDatesChangedEvent(object sender, VisibleDatesChangedEventArgs e)
        {
            var currentMonth = e.visibleDates[e.visibleDates.Count / 2].Month;
            var intern = schedule.ScheduleResources.FirstOrDefault(emp => (emp as Employee).Id.ToString() == "8600");
            if (currentMonth == 2 || currentMonth == 7)
            {
                if (intern != null)
                    return;

                Employee employee = new Employee();
                employee.Name = "Sophiya";
                employee.Id = "8600";
                employee.Color = Color.FromHex("#FFE671B8");
                employee.DisplayPicture = "schedule-resource-4.png";

                schedule.ScheduleResources.Add(employee);
            }
            else
            {
                if (intern == null)
                    return;

                schedule.ScheduleResources.Remove(intern);
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            this.UnWireEvents();
        }

        private void UnWireEvents()
        {
            this.schedule.VisibleDatesChangedEvent -= OnVisibleDatesChangedEvent;
        }
    }
}
