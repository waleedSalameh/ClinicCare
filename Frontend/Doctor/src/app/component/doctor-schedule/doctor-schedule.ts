import {ChangeDetectorRef,Component,OnInit,TemplateRef,ViewChild} from '@angular/core';
import {FormBuilder,FormGroup,FormsModule,ReactiveFormsModule,Validators} from '@angular/forms';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { DoctorScheduleModel } from './models/doctor-scheduleModels';
import { DoctorScheduleService } from './services/doctor-scheduleServices';
@Component({
  selector: 'app-doctor-schedule',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDialogModule
  ],
  templateUrl: './doctor-schedule.html',
  styleUrl: './doctor-schedule.css'
})
export class DoctorScheduleComponent implements OnInit {
  schedules: DoctorScheduleModel[] = [];
  doctors: any[] = [];
  scheduleForm: FormGroup;
  selectedDoctorId: number | null = null;
  selectedDay: number | null = null;
  isAddMode: boolean = false;
  errorMessage: string = '';
  displayedColumns: string[] = [
    'doctorName',
    'dayOfWeek',
    'starTime',
    'endTime',
    'slotDurationMinutes',
    'actions'
  ];
  days = [
    {
      value: 0,
      name: 'Sunday'
    },
    {
      value: 1,
      name: 'Monday'
    },
    {
      value: 2,
      name: 'Tuesday'
    },
    {
      value: 3,
      name: 'Wednesday'
    },
    {
      value: 4,
      name: 'Thursday'
    },
    {
      value: 5,
      name: 'Friday'
    },
    {
      value: 6,
      name: 'Saturday'
    }
  ];
  @ViewChild('addScheduleDialog')
  addScheduleDialog!: TemplateRef<any>;
  @ViewChild('successDialog')
  successDialog!: TemplateRef<any>;
  @ViewChild('errorDialog')
  errorDialog!: TemplateRef<any>;
  @ViewChild('saveErrorDialog')
  saveErrorDialog!: TemplateRef<any>;
  @ViewChild('deleteDialog')
  deleteDialog!: TemplateRef<any>;
  @ViewChild('deleteSuccessDialog')
  deleteSuccessDialog!: TemplateRef<any>;
  @ViewChild('deleteErrorDialog')
  deleteErrorDialog!: TemplateRef<any>;
  constructor(
    private doctorScheduleService: DoctorScheduleService,
    private fb: FormBuilder,
    private dialog: MatDialog,
    private cdr: ChangeDetectorRef
  ) {

    this.scheduleForm = this.fb.group({

      doctorId: [
        null,
        Validators.required
      ],

      dayOfWeek: [
        null,
        Validators.required
      ],

      starTime: [
        '',
        Validators.required
      ],

      endTime: [
        '',
        Validators.required
      ],

      slotDurationMinutes: [
        0,
        Validators.required
      ]

    });

  }


  ngOnInit(): void {

    this.getDoctorList();

    this.search();

  }
  getDoctorList(): void {

    this.doctorScheduleService
      .getDoctorList()
      .subscribe({

        next: (result) => {

          this.doctors = result;

          this.cdr.detectChanges();

        },

        error: (error: any) => {

          console.error(
            'API ERROR:',
            error
          );

        }

      });

  }
 search(): void {

  this.doctorScheduleService
    .getFilter(
      this.selectedDoctorId,
      this.selectedDay
    )
    .subscribe({

      next: (result) => {

        this.schedules = result;

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
  clearFilters(): void {

    this.selectedDoctorId = null;

    this.selectedDay = null;

      this.search();

  }
  openAddSchedule(): void {

    this.scheduleForm.reset({

      doctorId: null,

      dayOfWeek: null,

      starTime: '',

      endTime: '',

      slotDurationMinutes: 0

    });


    this.dialog.open(
      this.addScheduleDialog,
      {
        width: '600px'
      }
    );

  }
  saveSchedule(): void {

    const data = {

      doctorId:
        this.scheduleForm.get('doctorId')?.value,

      dayOfWeek:
        this.scheduleForm.get('dayOfWeek')?.value,

      starTime:
        this.scheduleForm.get('starTime')?.value + ':00',

      endTime:
        this.scheduleForm.get('endTime')?.value + ':00',

      slotDurationMinutes:
        this.scheduleForm.get('slotDurationMinutes')?.value

    };


    this.doctorScheduleService
      .create(data)
      .subscribe({

        next: (response) => {

          if (response) {

            this.search();

            this.cdr.detectChanges();

            this.dialog.closeAll();


            this.dialog.open(
              this.successDialog,
              {
                width: '400px'
              }
            );

          }

          else {

            this.dialog.open(
              this.saveErrorDialog,
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


          const errors =
            error.error?.errors;


          if (errors) {

            const messages: string[] = [];


            Object.keys(errors).forEach(key => {

              messages.push(
                ...errors[key]
              );

            });


            this.errorMessage =
              messages.join('\n');

          }

          else {

            this.errorMessage =
              error.error?.message ??
              error.error?.title ??
              'Something went wrong.';

          }





          this.dialog.open(
            this.errorDialog,
            {
              width: '400px'
            }
          );

        }

      });

  }
  deleteSchedule(id: number): void {

    const dialogRef =
      this.dialog.open(
        this.deleteDialog
      );


    dialogRef
      .afterClosed()
      .subscribe(result => {

        if (result === true) {

          this.confirmDelete(id);

        }

      });

  }
  confirmDelete(id: number): void {

    this.doctorScheduleService
      .delete(id)
      .subscribe({

        next: (response) => {

          if (response) {

            this.search();


            this.dialog.open(
              this.deleteSuccessDialog,
              {
                width: '400px'
              }
            );

          }

          else {

            this.dialog.open(
              this.deleteErrorDialog,
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


          this.dialog.open(
            this.deleteErrorDialog,
            {
              width: '400px'
            }
          );

        }

      });

  }
  getDayName(day: number): string {

    return this.days
      .find(x => x.value === day)
      ?.name ?? '';

  }
}
