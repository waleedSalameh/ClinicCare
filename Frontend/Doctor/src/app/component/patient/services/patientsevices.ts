import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { patientModels } from '../models/patientmodels';
@Injectable({
  providedIn: 'root'
})
export class PatientService {

  private apiUrl = 'https://localhost:7130/api/Patient';

  constructor(private http: HttpClient) {}

  getAll(
    fullName?: string,
    phoneNumber?: string,
    nationalId?: string
  ): Observable<patientModels[]> {

    return this.http.get<patientModels[]>(
      this.apiUrl,
      {
        params: {
          fullName: fullName || '',
          phoneNumber: phoneNumber || '',
          nationalId: nationalId || ''
        }
      }
    );
  }
  create(
    patient: patientModels
  ): Observable<patientModels> {

    return this.http.post<patientModels>(
      this.apiUrl,
      patient
    );
  }
   update(
    id: number,
    patient: any
  ): Observable<boolean> {

    return this.http.put<boolean>(
      `${this.apiUrl}/${id}`,
      patient
    );
  }
   getById(
      id: number
    ): Observable<patientModels> {

      return this.http.get<patientModels>(
        `${this.apiUrl}/${id}`
      );
  }
   delete(
    id: number): Observable<boolean> {

    return this.http.delete<boolean>(
      `${this.apiUrl}/${id}`
    );
  }
}
