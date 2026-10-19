import { AppointmentStatus } from "./appointment-status";
export interface  AppointmentModel{
id:number;
appointmentCode:string;
patientId:number;
doctorId:number;
clincId:number;
medicalServiceId:number;
appointmentDate:string;
startTime:string;
endTime:string;
notes:string;
isDeleted:boolean;
status: AppointmentStatus;
createdAt: string;
}
