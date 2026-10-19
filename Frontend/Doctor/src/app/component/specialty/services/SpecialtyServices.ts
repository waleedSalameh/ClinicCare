import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { specialtyModels } from '../../../models/specialtyModels';
import { doctorModels } from '../../../models/doctorModels';
@Injectable({
  providedIn:'root'
})
export class SpecialtyService{
private apiUrl='https://localhost:7130/api/Specialty'
constructor(private http:HttpClient){}
getAll(name?:string,code?:string,isActive?:boolean | null):Observable<specialtyModels[]>{
  return this.http.get<specialtyModels[]>(this.apiUrl,{
    params:{name:name||'',code:code||'',isActive:  isActive == null || isActive == undefined ? '' : isActive.toString()}});
}
getActive():Observable<specialtyModels[]>{
  return this.http.get<specialtyModels[]>(
    this.apiUrl+'/active'
  );
}
create(Specialty: specialtyModels): Observable<specialtyModels> {
    return this.http.post<specialtyModels>(
      this.apiUrl,
      Specialty
    );
}
update(id: number,Specialty: any): Observable<boolean> {
    return this.http.put<boolean>(
      `${this.apiUrl}/${id}`,
      Specialty
    );
}
getById(id: number): Observable<specialtyModels> {
      return this.http.get<specialtyModels>(
        `${this.apiUrl}/${id}`
      );
}
delete(id: number): Observable<boolean> {
    return this.http.delete<boolean>(
      `${this.apiUrl}/${id}`
    );
}
}
