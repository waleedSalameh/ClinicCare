using BLL.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public interface IDashboardService
    {
        DashboardSummaryDto GetSummary();
        List<TodayAppointmentDto> GetTodayAppointments();
        object GetClincByAppointmets();
    }
}
