import { Routes } from '@angular/router';
import { DoctorListComponent } from './components/doctor-list/doctor-list';
import { DoctorFormComponent } from './components/doctor-form/doctor-form';
import { Homepage } from './component/homepage/homepage';
import { PatientComponent } from './component/patient/patient';
import { MedicalServicesComponent } from './component/medical-services/medical-services';
import { ClincComponent } from './component/clinc/clinc';
import { SpecialtyComponent } from './component/specialty/specialty';
import { DoctorScheduleComponent } from './component/doctor-schedule/doctor-schedule';
import { AppointmentComponent } from './component/appointment/appointment';
import { DashboardComponent } from './component/dashboard/dashboard';
export const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'doctors',
    component: DoctorListComponent
  },
  {
    path: 'doctors/add',
    component: DoctorFormComponent
  },
  {
    path: 'doctors/edit/:id',
    component: DoctorFormComponent
  },
  {
    path: 'medical-services',
    component: MedicalServicesComponent
  },
  {
    path: 'clinc',
    component: ClincComponent
  },
  {
    path: 'specialty',
    component: SpecialtyComponent
  },
  {
    path: 'doctor-schedule',
    component: DoctorScheduleComponent
  },
  {
    path: 'appointment',
    component: AppointmentComponent
  },
  {
    path: 'patients',
    component: PatientComponent
  },
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'dashboard'
  }
];
