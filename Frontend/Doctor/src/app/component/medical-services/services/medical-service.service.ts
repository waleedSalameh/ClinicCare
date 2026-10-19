import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MedicalServiceModel } from '../models/medical-service.model';
import { ClincModele } from '../../clinc/models/clinc.models';
@Injectable({
  providedIn: 'root'
})
export class MedicalServiceService {

  private apiUrl = 'https://localhost:7130/api/MedicalServices';

  constructor(private http: HttpClient) {}

 getAll(
  serviceName?: string,
  serviceCode?: string,
  isActive?: number
): Observable<MedicalServiceModel[]> {

  return this.http.get<MedicalServiceModel[]>(
    this.apiUrl,
    {
      params: {
        serviceName: serviceName || '',
        serviceCode: serviceCode || '',
        isActive: isActive?.toString() || ''
      }
    }
  );
}

  getById(
    id: number): Observable<MedicalServiceModel> {
    return this.http.get<MedicalServiceModel>(
      `${this.apiUrl}/${id}`
    );
}
  create(
  medicalService: MedicalServiceModel
): Observable<MedicalServiceModel> {

  return this.http.post<MedicalServiceModel>(
    this.apiUrl,
    medicalService
  );
}

  update(
  id: number,
  medicalService: any
): Observable<boolean> {

  return this.http.put<boolean>(
    `${this.apiUrl}/${id}`,
    medicalService
  );
}
  delete(
    id: number): Observable<boolean> {

    return this.http.delete<boolean>(
      `${this.apiUrl}/${id}`
    );
  }
  getClinclist(): Observable<ClincModele[]> {
    return this.http.get<ClincModele[]>(
      'https://localhost:7130/api/Share/Clinclist'
    );
  }
}
