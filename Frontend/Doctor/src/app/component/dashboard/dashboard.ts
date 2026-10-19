import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardService, DashboardSummary,TodayAppointment,ClinicAppointmentStats  } from './services/dasboard.service';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ChartModule } from 'primeng/chart';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    TableModule,
    TagModule,
    ChartModule,
    RouterLink
  ],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css']
})
export class DashboardComponent implements OnInit {


  private dashboardService = inject(DashboardService);
   constructor(
    private cdr: ChangeDetectorRef
  ) {}
todayAppointments: TodayAppointment[] = [];
clinicStats: ClinicAppointmentStats[] = [];
appointmentsLoading = true;
appointmentsError = '';
summary: DashboardSummary = {
    todayAppointmentsCount: 0,
    bookedCount: 0,
    completedCount: 0,
    cancelledCount: 0
  };
clinicChartData: any = {
  labels: [],
  datasets: [
    {
      label: 'Appointments',
      data: [],
      backgroundColor: '#6366f1',
      borderRadius: 6
    }
  ]
};
clinicChartOptions: any = {
  responsive: true,
  maintainAspectRatio: false,
  plugins: {
    legend: {
      display: false
    },
    tooltip: {
      enabled: true
    }
  },
  scales: {
    y: {
      beginAtZero: true,
      ticks: {
        precision: 0
      }
    }
  }
};
  loading = true;
  errorMessage = '';

  ngOnInit(): void {
    this.loadSummary();
    this.loadTodayAppointments();
    this.loadClinicChart();

  }

  loadSummary(): void {
    this.loading = true;
    this.errorMessage = '';

    this.dashboardService.getSummary().subscribe({
      next: (data) => {
        this.summary = data;
        this.loading = false;
        this.cdr.detectChanges();

      },
      error: (error) => {
        console.error('Failed to load dashboard summary', error);
        this.errorMessage = 'Failed to load dashboard statistics.';
        this.loading = false;
      }
    });
  }
  loadTodayAppointments(): void {
  this.appointmentsLoading = true;
  this.appointmentsError = '';

  this.dashboardService.getTodayAppointments().subscribe({
    next: (data) => {
      this.todayAppointments = data;
      this.appointmentsLoading = false;
      this.cdr.detectChanges();
    },
    error: (error) => {
      console.error('Failed to load today appointments', error);
      this.appointmentsError = 'Failed to load today appointments.';
      this.appointmentsLoading = false;
    }
  });
}
getStatusLabel(status: number): string {
  switch (status) {
    case 0: return 'Booked';
    case 1: return 'Confirmed';
    case 2: return 'Completed';
    case 3: return 'Cancelled';
    case 4: return 'No Show';
    default: return 'Unknown';
  }
}
loadClinicChart(): void {
  this.dashboardService.getAppointmentsByClinic().subscribe({
    next: (data) => {
      this.clinicStats = data;
      this.cdr.detectChanges();
      this.clinicChartData = {
        labels: data.map(x => x.clincName),
        datasets: [
          {
            label: 'Appointments',
            data: data.map(x => x.appointmentCount),
            backgroundColor: '#6366f1',
            hoverBackgroundColor: '#4f46e5',
            borderRadius: 6,
            maxBarThickness: 45
          }
        ]
      };
    },
    error: (error) => {
      console.error('Failed to load clinic chart', error);
    }
  });
}
}
