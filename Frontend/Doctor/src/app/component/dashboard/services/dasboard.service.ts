import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface DashboardSummary {
  todayAppointmentsCount: number;
  bookedCount: number;
  completedCount: number;
  cancelledCount: number;
}

export interface TodayAppointment {
  appointmentId: number;
  appointmentTime: string;
  clincName: string;
  patientName: string;
  doctorName: string;
  status: number;
}
export interface ClinicAppointmentStats {
  clincId: number;
  clincName: string;
  appointmentCount: number;
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7130/api/Dashboard';

  getSummary(): Observable<DashboardSummary> {
    return this.http.get<DashboardSummary>(
      `${this.apiUrl}/summary`
    );
  }

  getTodayAppointments(): Observable<TodayAppointment[]> {
    return this.http.get<TodayAppointment[]>(
      `${this.apiUrl}/today-appointments`
    );
  }
  getAppointmentsByClinic(): Observable<ClinicAppointmentStats[]> {
  return this.http.get<ClinicAppointmentStats[]>(
    `${this.apiUrl}/appointments-by-clinic`
  );
}
}
