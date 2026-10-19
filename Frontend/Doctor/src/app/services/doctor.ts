import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { doctorModels } from '../models/doctorModels';
import { specialtyModels } from '../models/specialtyModels';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  private apiUrl = 'https://localhost:7130/api/Doctor';

  constructor(private http: HttpClient) {}

  // Get All Doctors
  getAll(
    fullName?: string,
    phoneNumber?: string,
    specialtyId?: number
  ): Observable<doctorModels[]> {

    return this.http.get<doctorModels[]>(
      this.apiUrl,
      {
        params: {
          fullName: fullName || '',
          phoneNumber: phoneNumber || '',
          specialtyId: specialtyId || ''
        }
      }
    );
  }

  // Get Specialties
  getSpecialties(): Observable<specialtyModels[]> {

    return this.http.get<specialtyModels[]>(
      `${this.apiUrl}/specialties`
    );
  }

  // Delete Doctor
  delete(id: number): Observable<any> {

    return this.http.delete<any>(
      `${this.apiUrl}/${id}`
    );
  }

  // Create Doctor
  create(
    doctor: any
  ): Observable<doctorModels> {

    return this.http.post<doctorModels>(
      this.apiUrl,
      doctor
    );
  }

  // Get Doctor By Id
  getById(
    id: number
  ): Observable<doctorModels> {

    return this.http.get<doctorModels>(
      `${this.apiUrl}/${id}`
    );
  }

  // Update Doctor
  update(
    id: number,
    doctor: any
  ): Observable<any> {

    return this.http.put<any>(
      `${this.apiUrl}/${id}`,
      doctor
    );
  }
}
