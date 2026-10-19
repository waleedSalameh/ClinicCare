
import {ChangeDetectorRef,Component,OnInit,TemplateRef,ViewChild} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatDialog,MatDialogModule} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { doctorModels } from '../../models/doctorModels';
import { DoctorService } from '../../services/doctor';
import { specialtyModels } from '../../models/specialtyModels';
@Component({
  selector: 'app-doctor-list',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule,MatTableModule,MatButtonModule,MatIconModule,MatDialogModule,MatFormFieldModule,MatInputModule,MatSelectModule,MatCheckboxModule],
  templateUrl: './doctor-list.html',
  styleUrl: './doctor-list.css'
})
export class DoctorListComponent implements OnInit {
  specialties: specialtyModels[] = [];
  doctors: doctorModels[] = [];
  doctorForm: FormGroup;
  doctorName: string = '';
  phoneNumber: string = '';
  specialtyId: number | null = null;
  isPhoneValid: boolean = true;
  isEditMode: boolean = false;
  selectedDoctorId: number = 0;
  displayedColumns: string[] = [
    'id',
    'fullName',
    'specialty',
    'gender',
    'phoneNumber',
    'createdDateTime',
    'isActive',
    'actions'
  ];

  errorMessage: string = '';

  @ViewChild('addDoctorDialog')
  addDoctorDialog!: TemplateRef<any>;

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
  constructor(
    private doctorService: DoctorService,
    private cdr: ChangeDetectorRef,
    private dialog: MatDialog,
    private fb: FormBuilder
  ) {

    this.doctorForm = this.fb.group({
      fullName: [''],
      specialtyId: [null],
      phoneNumber: [''],
      yearsOfExperience: [0],
      email: [''],
      gender: [''],
      isActive: [true],
      createdDateTime: ['']
    });

  }
  ngOnInit(): void {

    this.getAllData();

    this.getSpecialties();

  }
  getSpecialties(): void {

    this.doctorService
      .getSpecialties()
      .subscribe({

        next: (data) => {

          this.specialties = data;

          this.cdr.detectChanges();

        },

        error: (error) => {

          console.error(
            'Specialties API ERROR:',
            error
          );

        }

      });

  }
  getAllData(): void {

    this.doctorService
      .getAll()
      .subscribe({

        next: (data) => {

          this.doctors = data;

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
  onlyNumbers(
    event: KeyboardEvent
  ): void {

    const key = event.key;

    if (
      !/^[0-9]$/.test(key) &&
      key !== 'Backspace' &&
      key !== 'Delete' &&
      key !== 'ArrowLeft' &&
      key !== 'ArrowRight' &&
      key !== 'Tab'
    ) {

      event.preventDefault();

    }

  }
  search(): void {

    console.log(
      'Doctor Name:',
      this.doctorName
    );

    console.log(
      'Phone Number:',
      this.phoneNumber
    );

    console.log(
      'Specialty Id:',
      this.specialtyId
    );


    this.doctorService
      .getAll(
        this.doctorName,
        this.phoneNumber,
        this.specialtyId || undefined
      )
      .subscribe({

        next: (data) => {

          console.log(
            'Search Result:',
            data
          );

          this.doctors = data;

          this.cdr.detectChanges();

        },

        error: (error) => {

          console.error(
            'Search Error:',
            error
          );

        }

      });

  }
  reset(): void {

    this.doctorName = '';

    this.phoneNumber = '';

    this.isPhoneValid = true;

    this.specialtyId = null;

    this.getAllData();

  }
  openAddDoctor(): void {

    this.isEditMode = false;

    this.selectedDoctorId = 0;


    this.doctorForm.reset({

      fullName: '',

      specialtyId: null,

      phoneNumber: '',

      yearsOfExperience: 0,

      email: '',

      gender: '',

      isActive: true,

      createdDateTime: ''

    });


    this.dialog.open(
      this.addDoctorDialog,
      {
        width: '600px'
      }
    );

  }
  openEditDoctor(id: number): void {

    this.isEditMode = true;

    this.doctorService
      .getById(id)
      .subscribe({

        next: (doctor) => {

          this.selectedDoctorId = doctor.id;


          this.doctorForm.patchValue({

            fullName:
              doctor.fullName,

            specialtyId:
              doctor.specialtyId,

            phoneNumber:
              doctor.phoneNumber,

            yearsOfExperience:
              doctor.yearsOfExperience,

            email:
              doctor.email,

            gender:
              doctor.gender,

            isActive:
              doctor.isActive,

            createdDateTime:
              doctor.createdDateTime

          });


          this.only(
            doctor.isActive
          );


          this.dialog.open(
            this.addDoctorDialog,
            {
              width: '600px'
            }
          );

        },

        error: (error) => {

          console.error(
            'API ERROR:',
            error
          );

        }

      });

  }
  saveDoctor(): void {

    if (this.isEditMode) {

      this.updateDoctor();

    } else {

      this.addDoctor();

    }

  }
  addDoctor(): void {

    this.doctorService
      .create(
        this.DoctorData()
      )
      .subscribe({

        next: (response) => {

          if (response) {

            this.getAllData();

            this.dialog.closeAll();

            this.dialog.open(
              this.successDialog,
              {
                width: '400px'
              }
            );

          } else {

            this.dialog.open(
              this.errorDialog,
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


            Object.keys(errors)
              .forEach(key => {

                messages.push(
                  ...errors[key]
                );

              });


            this.errorMessage =
              messages.join('\n');

          } else {

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

      });

  }
  updateDoctor(): void {

    this.doctorService
      .update(
        this.selectedDoctorId,
        this.DoctorData()
      )
      .subscribe({

        next: (response) => {

          if (response) {

            this.getAllData();

            this.dialog.closeAll();

            this.dialog.open(
              this.successDialog,
              {
                width: '400px'
              }
            );

          } else {

            this.dialog.open(
              this.errorDialog,
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


            Object.keys(errors)
              .forEach(key => {

                messages.push(
                  ...errors[key]
                );

              });


            this.errorMessage =
              messages.join('\n');

          } else {

            this.errorMessage =
              error.error?.message ||
              'Something went wrong.';

          }


          this.dialog.closeAll();


          this.dialog.open(
            this.errorDialog,
            {
              width: '400px'
            }
          );

        }

      });

  }
  DoctorData() {

    return {

      fullName:
        this.doctorForm
          .get('fullName')
          ?.value,

      specialtyId:
        this.doctorForm
          .get('specialtyId')
          ?.value,

      phoneNumber:
        this.doctorForm
          .get('phoneNumber')
          ?.value,

      yearsOfExperience:
        this.doctorForm
          .get('yearsOfExperience')
          ?.value,

      email:
        this.doctorForm
          .get('email')
          ?.value,

      gender:
        this.doctorForm
          .get('gender')
          ?.value,

      isActive:
        this.doctorForm
          .get('isActive')
          ?.value,

      createdDateTime:
        this.doctorForm
          .get('createdDateTime')
          ?.value

    };

  }
  only(
    isChecked: boolean
  ): void {

    const nameControl =
      this.doctorForm
        .get('fullName');

    const specialtyControl =
      this.doctorForm
        .get('specialtyId');

    const phoneControl =
      this.doctorForm
        .get('phoneNumber');

    const yearsControl =
      this.doctorForm
        .get('yearsOfExperience');

    const emailControl =
      this.doctorForm
        .get('email');

    const createdControl =
      this.doctorForm
        .get('createdDateTime');
const genderdControl =
      this.doctorForm
        .get('gender');

    if (
      !isChecked &&
      this.isEditMode == true
    ) {

      nameControl?.disable();
      specialtyControl?.disable();
      phoneControl?.disable();
      yearsControl?.disable();
      emailControl?.disable();
      createdControl?.disable();
      genderdControl?.disable();

    } else {

      nameControl?.enable();
      specialtyControl?.enable();
      phoneControl?.enable();
      yearsControl?.enable();
      emailControl?.enable();
      createdControl?.enable();
       genderdControl?.enable();

    }

  }
deleteDoctor(id: number): void {

    this.selectedDoctorId = id;


    const dialogRef =
      this.dialog.open(
        this.deleteDialog,
        {
          width: '400px',
          disableClose: true
        }
      );


    dialogRef
      .afterClosed()
      .subscribe(result => {

        if (result === true) {

          this.confirmDelete();

        }

      });

  }
confirmDelete(): void {

    this.doctorService
      .delete(
        this.selectedDoctorId
      )
      .subscribe({

        next: (response) => {

          if (response) {

            this.getAllData();

            this.dialog.open(
              this.deleteSuccessDialog,
              {
                width: '400px'
              }
            );

          } else {

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
            'Delete error:',
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

}

