import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ClincModele } from '../models/clinc.models';
import { doctorModels } from '../../../models/doctorModels';

@Injectable({
  providedIn: 'root'
})
export class ClincService {

  private apiUrl = 'https://localhost:7130/api/Clinc';

  constructor(private http: HttpClient) {}


getAll( clincName?: string,clincCode?: number | null,isActive?:boolean | null): Observable<ClincModele[]> {
  return this.http.get<ClincModele[]>(this.apiUrl ,
    {
      params: {
        clincName: clincName || '',
         clincCode: clincCode == null ? '' : clincCode.toString(),
        isActive:  isActive == null || isActive == undefined ? '' : isActive.toString()
      }
    });
}
getById(
    id: number): Observable<ClincModele> {
    return this.http.get<ClincModele>(
      `${this.apiUrl}/${id}`
    );
}
create(clinc: Omit<ClincModele, 'id' | 'isDeleted'>): Observable<ClincModele> {

  return this.http.post<ClincModele>(
    this.apiUrl,
    clinc
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
getDocterlist(): Observable<doctorModels[]> {
  return this.http.get<doctorModels[]>(
    'https://localhost:7130/api/Share/Docterlist'
  );
}
}
