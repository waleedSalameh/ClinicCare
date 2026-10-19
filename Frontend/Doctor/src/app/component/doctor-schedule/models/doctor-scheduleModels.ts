export interface DoctorScheduleModel {
  id: number;
  doctorId: number;
  dayOfWeek: number;
  starTime: string;
  endTime: string;
  slotDurationMinutes: number;
  isDeleted: boolean;
}
