import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DoctorScheduleModel } from '../models/doctor-scheduleModels';
import { doctorModels } from '../../../models/doctorModels';
@Injectable({
  providedIn: 'root'
})
export class DoctorScheduleService{

private apiUrl =  'https://localhost:7130/api/DoctorSchedule';
 constructor(private http: HttpClient) {}
getById(
    id: number): Observable<DoctorScheduleModel> {
    return this.http.get<DoctorScheduleModel>(
      `${this.apiUrl}/${id}`
    );
}
delete(
    id: number): Observable<boolean> {

    return this.http.delete<boolean>(
      `${this.apiUrl}/${id}`
    );
}
create(
  DoctorScheduleModel: any
): Observable<DoctorScheduleModel> {

  return this.http.post<DoctorScheduleModel>(
    this.apiUrl,
    DoctorScheduleModel
  );
}
getFilter( doctorId?: number | null, dayOfWeek?: number | null):Observable<DoctorScheduleModel[]>{
return this.http.get<DoctorScheduleModel[]>(this.apiUrl , {
  params: {
     doctorId: doctorId || '',
          dayOfWeek: dayOfWeek || '',
}
}
);
}
getDoctorList(): Observable<doctorModels[]> {
  return this.http.get<doctorModels[]>(
    'https://localhost:7130/api/Share/Docterlist'
  );
}
}

