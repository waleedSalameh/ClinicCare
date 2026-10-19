import { ChangeDetectorRef, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AppointmentService } from './services/appointment.service';
import { AppointmentModel } from './models/appointment.model';
import { AppointmentStatus } from './models/appointment-status';
import { doctorModels } from '../../models/doctorModels';
import { MedicalServiceModel } from '../medical-services/models/medical-service.model';
import { CommonModule } from '@angular/common';
import { ClincModele } from '../clinc/models/clinc.models';
import { patientModels } from '../patient/models/patientmodels';
import { disabled } from '@angular/forms/signals';
import { MatDividerModule } from '@angular/material/divider';
@Component({
  selector: 'app-appointment',
  standalone: true,
  imports: [FormsModule, MatDividerModule, ReactiveFormsModule, MatTableModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatInputModule, MatDialogModule, MatSelectModule, MatCheckboxModule, CommonModule],
  templateUrl: './appointment.html',
  styleUrl: './appointment.css'
})
export class AppointmentComponent implements OnInit {
  AppointmentServices: AppointmentModel[] = [];
  ClincList: ClincModele[] = [];
  PatientList: patientModels[] = [];
  MSList: MedicalServiceModel[] = [];
  AllDoctorList: doctorModels[] = [];
  displayedColumns: string[] = ['id', 'appointmentCode', 'patientName', 'doctorName', 'clincName', 'serviceName', 'appointmentDate', 'time', 'notes', 'createdAt', 'status', 'actions'];
  AppointmentForm: FormGroup;
  isEditMode: boolean = false;
  selectedAppointmentId: number = 0;
  fromDate: string = '';
  toDate: string = '';
  doctorId: number | null = null;
  patientId: number | null = null;
  clincId: number | null = null;
  status: AppointmentStatus | null = null;
  DoctorList: doctorModels[] = [];
  availableAppointments: any[] = [];
  MedicalServiceList: MedicalServiceModel[] = [];
  statuses = Object.values(AppointmentStatus)
    .filter(value => typeof value === 'number');
  AppointmentStatus = AppointmentStatus;
  errorMessage: string = '';
  selectedStatusAppointmentId: number = 0;
  @ViewChild('addAppointmentDialog')
  addAppointmentDialog!: TemplateRef<any>;
  @ViewChild('successDialog')
  successDialog!: TemplateRef<any>;
  @ViewChild('errorDialog')
  errorDialog!: TemplateRef<any>;
  @ViewChild('deleteDialog')
  deleteDialog!: TemplateRef<any>;
  @ViewChild('deleteSuccessDialog')
  deleteSuccessDialog!: TemplateRef<any>;
  @ViewChild('deleteErrorDialog')
  deleteErrorDialog!: TemplateRef<any>;
  @ViewChild('changeStatusDialog')
  changeStatusDialog!: TemplateRef<any>;
  @ViewChild('statusSuccessDialog')
  statusSuccessDialog!: TemplateRef<any>;
  @ViewChild('statusErrorDialog')
  statusErrorDialog!: TemplateRef<any>;
  @ViewChild('appointmentDetailsDialog')
  appointmentDetailsDialog!: TemplateRef<any>;
  @ViewChild('AppointmentDialog')
  AppointmentDialog!: TemplateRef<any>;
  constructor(
    private AppointmentService: AppointmentService,
    private cdr: ChangeDetectorRef,
    private dialog: MatDialog,
    private fb: FormBuilder
  ) {

    this.AppointmentForm = this.fb.group({
      appointmentCode: [''],
      patientId: [null, Validators.required],
      doctorId: [{ value: null, disabled: true }, Validators.required],
      clincId: [null, Validators.required],
      medicalServiceId: [{ value: null, disabled: true }, Validators.required],
      appointmentDate: [{ value: null, disabled: true }, Validators.required],
      startTime: ['', Validators.required],
      endTime: ['', Validators.required],
      status: [AppointmentStatus.Booked, Validators.required],
      notes: [''],

      doctorName: ['']
    });

  }

  ngOnInit(): void {

    this.loadAppointmentService();
    this.getClinclist();
    this.getPatientList();
    this.getDoctorList();
    this.getBookingAppointments();


  }
  loadAppointmentService(): void {

    this.AppointmentService.getAll(

      this.fromDate,

      this.toDate,

      this.doctorId,

      this.patientId,

      this.clincId,

      this.status

    ).subscribe({

      next: (response) => {

        this.AppointmentServices = response;

        this.cdr.detectChanges();

      },

      error: (error) => {

        console.error(
          'API ERROR:',
          error
        );

      }

    });

  }
  search(): void {

    this.loadAppointmentService();

  }
  clearFilters(): void {

    this.fromDate = '';

    this.toDate = '';

    this.doctorId = null;

    this.patientId = null;

    this.clincId = null;

    this.status = null;

    this.loadAppointmentService();

  }
  openAddAppointment(): void {

    this.isEditMode = false;
    this.selectedAppointmentId = 0;

    let nextCode = 1;

    if (this.AppointmentServices.length > 0) {

      const lastCode = Math.max(
        ...this.AppointmentServices.map(x => Number(x.appointmentCode))
      );

      nextCode = lastCode + 1;
    }

    this.DoctorList = [];
    this.MedicalServiceList = [];

    this.AppointmentForm.reset({

      appointmentCode: nextCode.toString(),

      patientId: null,
      doctorId: null,
      clincId: null,
      medicalServiceId: null,

      appointmentDate: '',
      startTime: '',
      endTime: '',

      status: AppointmentStatus.Booked,

      notes: ''

    });

    this.dialog.open(
      this.addAppointmentDialog,
      {
        width: '600px'
      }
    );
    this.cdr.detectChanges();
    this.getClinclist();
    this.getPatientList();
    this.getBookingAppointments();
  }
  clinicChanged(
    clinicId: number,
    isEdit: boolean = false
  ): void {

    this.AppointmentForm.patchValue({
      clincId: clinicId
    });

    if (!isEdit) {
      this.AppointmentForm.patchValue({
        doctorId: null,
        medicalServiceId: null
      });
    }

    this.AppointmentForm.get('doctorId')?.disable();
    this.AppointmentForm.get('medicalServiceId')?.disable();

    if (!clinicId) {
      this.DoctorList = [];
      this.MSList = [];
      return;
    }

    this.cdr.detectChanges();

    this.getDoctorListByClinic(clinicId);
    this.getMSListByClinic(clinicId);
  }
  addAppointment(): void {

    if (this.AppointmentForm.invalid) {

      this.AppointmentForm.markAllAsTouched();

      return;

    }
   const value = this.AppointmentForm.getRawValue();

const { doctorName, ...formValue } = value;

const appointment = {
  ...formValue,
  doctorId: Number(formValue.doctorId),
  startTime: formValue.startTime + ':00',
  endTime: formValue.endTime + ':00'
};

console.log('Add Appointment:', appointment);
    this.cdr.detectChanges();
    this.AppointmentService
      .create(appointment)
      .subscribe({

        next: (response) => {

          if (response) {

            this.loadAppointmentService();

            this.cdr.detectChanges();

            this.dialog.closeAll();

            this.dialog.open(

              this.successDialog,

              {
                width: '400px'
              }

            );

          }

        },

        error: (error) => {

          console.error(
            'API ERROR:',
            error
          );

          this.showError(error);

        }

      });

  }
  openEditAppointment(id: number): void {
    this.isEditMode = true;

    this.AppointmentService.getById(id).subscribe({
      next: (appointment) => {
        this.selectedAppointmentId = appointment.id;

        this.AppointmentForm.patchValue({
          appointmentCode: appointment.appointmentCode.toString(),
          patientId: appointment.patientId,
          doctorId: appointment.doctorId,
          clincId: appointment.clincId,
          medicalServiceId: appointment.medicalServiceId,
          appointmentDate: appointment.appointmentDate?.toString().substring(0, 10),
          startTime: appointment.startTime?.toString().substring(0, 5),
          endTime: appointment.endTime?.toString().substring(0, 5),
          status: appointment.status,
          notes: appointment.notes
        });

        if (appointment.status === AppointmentStatus.Completed) {
          this.AppointmentForm.disable();
        } else {
          this.AppointmentForm.enable();
          this.AppointmentForm.get('patientId')?.disable();
          this.AppointmentForm.get('clincId')?.disable();
          this.AppointmentForm.get('appointmentDate')?.enable();
        }

        this.clinicChanged(appointment.clincId, true);

        this.dialog.open(
          appointment.status === AppointmentStatus.Completed
            ? this.AppointmentDialog
            : this.addAppointmentDialog,
          { width: '600px' }
        );

        console.log('Appointment doctorId:', appointment.doctorId);
      },
      error: (error) => console.error('API ERROR:', error)
    });
  }
  updateAppointment(): void {
    if (this.AppointmentForm.invalid) {
      this.AppointmentForm.markAllAsTouched();
      return;
    }

    const value = this.AppointmentForm.getRawValue();

    const appointment = {
      ...value,
      doctorId: Number(value.doctorId),
      startTime: value.startTime + ':00',
      endTime: value.endTime + ':00'
    };


    console.log('Appointment:', appointment);

    this.AppointmentService
      .update(this.selectedAppointmentId, appointment)
      .subscribe({
        next: (response) => {
          if (response) {
            this.loadAppointmentService();
            this.dialog.closeAll();

            this.dialog.open(this.successDialog, {
              width: '400px'
            });
          }
        },
        error: (error) => {
          console.error('API ERROR:', error);
          this.showError(error);
        }
      });
  }
  save(): void {

    if (this.isEditMode) {

      this.updateAppointment();

    }
    else {

      this.addAppointment();

    }

  }
  deleteAppointment(
    id: number
  ): void {

    this.selectedAppointmentId = id;


    const dialogRef =
      this.dialog.open(
        this.deleteDialog
      );


    dialogRef.afterClosed()
      .subscribe(result => {

        if (result === true) {

          this.confirmDelete();

        }

      });

  }
  confirmDelete(): void {

    this.AppointmentService
      .delete(
        this.selectedAppointmentId
      )
      .subscribe({

        next: (response) => {

          if (response) {

            this.loadAppointmentService();

            this.dialog.open(
              this.deleteSuccessDialog
            );

          }
          else {

            this.dialog.open(
              this.deleteErrorDialog
            );

          }

        },

        error: (error) => {

          console.error(
            'API ERROR:',
            error
          );

          this.showError(error);

        }

      });

  }
  openChangeStatus(
    id: number
  ): void {

    this.selectedStatusAppointmentId =
      id;


    this.dialog.open(

      this.changeStatusDialog,

      {
        width: '400px'
      }

    );

  }
  changeStatus(
    status: AppointmentStatus
  ): void {

    this.AppointmentService
      .changeStatus(

        this.selectedStatusAppointmentId,

        status

      )
      .subscribe({

        next: (response) => {

          if (response) {

            this.dialog.closeAll();

            this.loadAppointmentService();
            this.cdr.detectChanges();
            this.dialog.open(

              this.statusSuccessDialog,

              {
                width: '400px'
              }

            );

          }

        },

        error: (error) => {

          console.error(
            'API ERROR:',
            error
          );

          this.showError(error);

        }

      });

  }
  showError(error: any): void {

    const errors =
      error.error?.errors;


    if (errors) {

      const messages: string[] = [];


      Object.keys(errors)
        .forEach(key => {

          messages.push(
            ...errors[key]
          );

        });


      this.errorMessage =
        messages.join('\n');

    }
    else {

      this.errorMessage =
        error.error?.message ||
        'Something went wrong.';

    }





    this.dialog.open(

      this.errorDialog,

      {
        width: '400px'
      }

    );

  }
  getClinclist() {
    this.AppointmentService.getClinclist().subscribe({
      next: (data) => {
        this.ClincList = data;
        if (this.isEditMode) {
          this.AppointmentForm.get('clincId')?.disable();
        } else {
          this.AppointmentForm.get('clincId')?.enable();
        }
      }
    });
  }
  getPatientList() {
    this.AppointmentService.getPatientList().subscribe({
      next: (data) => {
        this.PatientList = data;
        if (this.isEditMode) {
          this.AppointmentForm.get('patientId')?.disable();
        } else {
          this.AppointmentForm.get('patientId')?.enable();
        }
      }
    });
  }
  getMSListByClinic(clinicId: number) {

    this.AppointmentService.getMSListByClinic(clinicId).subscribe({

      next: (data) => {

        this.MSList = data;
        this.AppointmentForm.get('medicalServiceId')?.enable();
        if (this.isEditMode) {
          this.AppointmentForm.get('medicalServiceId')?.disable();
        } else {
          this.AppointmentForm.get('medicalServiceId')?.enable();
        }
      },
      error: (error) => {

        console.error(
          'API ERROR:',
          error
        );

        this.showError(error);

      }


    });

  }
  getDoctorListByClinic(clinicId: number) {

    this.AppointmentService.getDoctorListByClinic(clinicId).subscribe({

      next: (data) => {
        this.DoctorList = data;
        this.AppointmentForm.patchValue({
          doctorId: data[0].id,
          doctorName: data[0].fullName
        })

        this.AppointmentForm.get('doctorName')?.enable();
        if (this.isEditMode) {
          this.AppointmentForm.get('doctorName')?.disable();
        } else {
          this.AppointmentForm.get('doctorName')?.enable();
        }

      },
      error: (error) => {

        console.error(
          'API ERROR:',
          error
        );

        this.showError(error);

      }

    });

  }
  getDoctorList() {
    this.AppointmentService.getDoctorList().subscribe({
      next: (data) => {
        this.DoctorList = data;


      },
      error: (error) => console.error(error)
    });
  }
  getBookingAppointments() {
    const clincId = this.AppointmentForm.get('clincId')?.value;
    const MSId = this.AppointmentForm.get('medicalServiceId')?.value;
    const doctorId = this.AppointmentForm.get('doctorId')?.value;
    const appointmentDate = this.AppointmentForm.get('appointmentDate')?.value;

    if (!clincId || !MSId || !doctorId || !appointmentDate) {
      this.availableAppointments = [];
      return;
    }

    this.AppointmentService.getBookingAppointments(
      clincId,
      MSId,
      doctorId,
      appointmentDate
    ).subscribe({
      next: (data) => {
        this.availableAppointments = data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.showError(err);
      }
    });
  }
  selectAppointment(appointment: any) {

    this.AppointmentForm.patchValue({
      startTime: appointment.startTime?.substring(0, 5),
      endTime: appointment.endTime?.substring(0, 5)
    });
  }
  serviceChanged(): void {
    this.AppointmentForm.patchValue({
      appointmentDate: null,
      endTime: null,
      startTime: null
    });
  }
  openAppointmentDetails(id: number) {
    this.AppointmentService.getById(id).subscribe({
      next: (appointment) => {
        this.dialog.open(this.appointmentDetailsDialog, {
          width: '600px',
          data: {
            ...appointment,

            appointmentDate: appointment.appointmentDate
              ?.toString()
              .substring(0, 10),

            startTime: appointment.startTime
              ?.toString()
              .substring(0, 5),

            endTime: appointment.endTime
              ?.toString()
              .substring(0, 5)
          }
        });
      },

      error: (error) => {
        console.error('Error loading appointment details', error);
      }
    });
  }


}
