import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppointmentModel } from '../models/appointment.model';
import { doctorModels } from '../../../models/doctorModels';
import { MedicalServiceModel } from '../../medical-services/models/medical-service.model';
import { AppointmentStatus } from '../models/appointment-status';
import { ClincModele } from '../../clinc/models/clinc.models';
import { patientModels } from '../../patient/models/patientmodels';
@Injectable({
  providedIn: 'root'
})
export class AppointmentService {

  private apiUrl = 'https://localhost:7130/api/Appointment'
  constructor(private http: HttpClient) { }


  getAll(fromDate?: string, toDate?: string, doctorId?: number | null, patientId?: number | null, clincId?: number | null, status?: AppointmentStatus | null)
    : Observable<AppointmentModel[]> {
    return this.http.get<AppointmentModel[]>(
      this.apiUrl,
      {
        params: {
          fromDate: fromDate || '',
          toDate: toDate || '',
          doctorId: doctorId == null ? '' : doctorId.toString(),
          patientId: patientId == null ? '' : patientId.toString(),
          clincId: clincId == null ? '' : clincId.toString(),
          status: status == null ? '' : status.toString()
        }
      }
    );
  }
  getById(id: number): Observable<AppointmentModel> {
    return this.http.get<AppointmentModel>(
      `https://localhost:7130/api/Appointment/getbyid/${id}`
    );
  }
  create(appointmentService: AppointmentModel)
    : Observable<AppointmentModel> { return this.http.post<AppointmentModel>(this.apiUrl, appointmentService); }
  update(id: number, appointmentService: any)
    : Observable<boolean> { return this.http.put<boolean>(`${this.apiUrl}/${id}`, appointmentService); }
  delete(id: number)
    : Observable<boolean> { return this.http.delete<boolean>(`${this.apiUrl}/${id}`); }
  getDoctorListByClinic(clinicId: number)
    : Observable<doctorModels[]> {

    return this.http.get<doctorModels[]>(
      'https://localhost:7130/api/Share/GetDoctorsByClinic',
      {
        params: {
          clinicId: clinicId.toString()
        }
      }
    );
  }
  getMSListByClinic(clinicId: number)
    : Observable<MedicalServiceModel[]> {

    return this.http.get<MedicalServiceModel[]>(
      'https://localhost:7130/api/Share/GetMSByClinic',
      {
        params: {
          clinicId: clinicId.toString()
        }
      }
    );
  }
  changeStatus(id: number, status: AppointmentStatus)
    : Observable<boolean> {
      return this.http.patch<boolean>(
        `${this.apiUrl}/${id}`, { status: status });
  }
  getClinclist(): Observable<ClincModele[]> {
    return this.http.get<ClincModele[]>(
      'https://localhost:7130/api/Share/Clinclist'
    );
  }
  getPatientList(): Observable<patientModels[]> {
    return this.http.get<patientModels[]>(
      'https://localhost:7130/api/Share/Patientslist'
    );
  }
  getDoctorList(): Observable<doctorModels[]> {
    return this.http.get<doctorModels[]>(
      'https://localhost:7130/api/Share/Docterlist'
    );
  }
  getBookingAppointments(
    clincId: number,
    MSId: number,
    DoctorId: number,
    AppointmentDate: string
  ) {
    return this.http.get<any>(
      `${this.apiUrl}/BookingAppointments`,
      {
        params: {
          clincId: clincId,
          MSId: MSId,
          DoctorId: DoctorId,
          AppointmentDate: AppointmentDate
        }
      }
    );
  }

}
