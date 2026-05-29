using EventFlow.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> ObterAsync();
    }
}
